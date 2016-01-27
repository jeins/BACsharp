// -----------------------------------------------------------------------------------
// Copyright (C) 2015 Kieback&Peter GmbH & Co KG All Rights Reserved
// 
// Kieback&Peter Confidential Proprietary Information
// 
// This Software is confidential and proprietary to Kieback&Peter. 
// The reproduction or disclosure in whole or part to anyone outside of Kieback&Peter
// without the written approval of an officer of Kieback&Peter GmbH & Co.KG,
// under a Non-Disclosure Agreement, or to any employee who has not previously
// obtained a written authorization for access from the individual responsible
// for the software will have a significant detrimental effect on
// Kieback&Peter and is expressly PROHIBITED.
// -----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using ConnectTools.BACnet.Properties;
using log4net;

namespace ConnectTools.BACnet
{
    public class BacnetStack : IBacnetStack
    {
        private const int BacnetUnicastRequestRepeatCount = 3; // repeat request x times

        private const int UdpPort = 47808;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IPEndPoint _broadcastEp;

        private readonly List<Device> _devices;
        private readonly bool _isMatch;

        private readonly UdpClient _receiveUdp;

        private readonly UdpClient _sendUdp;
        private int _invokeCounter;


        // The Constructor needs an IpEndpoint with the IpAddress to use. 
        // The IpAddress will be matched with a "probably found Networkinterface" if there is a match we break the Loop.
        public BacnetStack(IPAddress bindIpAddress)
        {
            var maskbytes = new byte[4];
            var addrbytes = new byte[4];
            var checkMask = "";
            // Find the local IP address and Subnet Mask
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var Interface in interfaces)
            {
                if (Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
                //MessageBox.Show(Interface.Description);
                var unicastIpInfoCol = Interface.GetIPProperties().UnicastAddresses;
                foreach (var unicatIpInfo in unicastIpInfoCol)
                {
                    //MessageBox.Show("\tIP Address is {0}" + UnicatIPInfo.Address);
                    //MessageBox.Show("\tSubnet Mask is {0}" + UnicatIPInfo.IPv4Mask);
                    if (unicatIpInfo.IPv4Mask != null)
                    {
                        if (unicatIpInfo.IPv4Mask != null)
                        {
                            var tempbytes = unicatIpInfo.IPv4Mask.GetAddressBytes();
                            if (tempbytes[0] == 255)
                            {
                                // We found the correct subnet mask, and probably the correct IP address
                                addrbytes = unicatIpInfo.Address.GetAddressBytes();
                                maskbytes = unicatIpInfo.IPv4Mask.GetAddressBytes();
                                break;
                            }
                        }
                    }
                }
            }

            // Set up broadcast address
            if (maskbytes[3] == 0) maskbytes[3] = 255;
            else maskbytes[3] = addrbytes[3];
            if (maskbytes[2] == 0) maskbytes[2] = 255;
            else maskbytes[2] = addrbytes[2];
            if (maskbytes[1] == 0) maskbytes[1] = 255;
            else maskbytes[1] = addrbytes[1];
            if (maskbytes[0] == 0) maskbytes[0] = 255;
            else maskbytes[0] = addrbytes[0];
            var myip = new IPAddress(addrbytes);
            var broadcast = new IPAddress(maskbytes);

            var localEp = new IPEndPoint(myip, UdpPort);
            _broadcastEp = new IPEndPoint(broadcast, UdpPort);

            _sendUdp = new UdpClient();
            _sendUdp.ExclusiveAddressUse = false;
            _sendUdp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _sendUdp.Client.Bind(localEp);

            _receiveUdp = new UdpClient(UdpPort, AddressFamily.InterNetwork);

            // Init the Devices list
            _devices = new List<Device>();
//            // Machine dependent (little endian vs big endian) 
//            // In this case we have to reverse the bytes for the Server IP
//            IPAddress currentUsedIpAddress;
//            var maskbytes = new byte[4];
//            var addrbytes = new byte[4];
//            var checkMask = "";
//            // Find the local IP address and Subnet Mask
//            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
//            foreach (var Interface in interfaces)
//            {
//                if (Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
//
//                var unicastIpInfoCol = Interface.GetIPProperties().UnicastAddresses;
//                foreach (var unicatIpInfo in unicastIpInfoCol)
//                {
//                    if (unicatIpInfo.IPv4Mask != null)
//                    {
//                        if (new IPAddress(unicatIpInfo.Address.GetAddressBytes()).ToString() == bindIpAddress.ToString())
//                        {
//                            addrbytes = unicatIpInfo.Address.GetAddressBytes();
//                            maskbytes = unicatIpInfo.IPv4Mask.GetAddressBytes();
//                            checkMask = unicatIpInfo.IPv4Mask.ToString();
//                            _isMatch = true;
//                            break;
//                        }
//                    }
//                }
//                if (_isMatch)//check if IpAddress was matched 
//                {
//                    if (checkMask != "0.0.0.0") // check  if the subnetmask is valid
//                    {
//                        break;
//                    }
//                    _isMatch = false;
//                    break;
//                }
//            }
//
//   
//            if (_isMatch)
//            {
//                currentUsedIpAddress = new IPAddress(addrbytes);
//            }
//            else
//            {
//                throw new Exception("NetworkInterface not found. Either the IpAddress : " +
//                    new IPAddress(addrbytes) +
//                    " ,the Subnetmask is wrong or not configured." +
//                    "Please check your networkconfiguration! And restart the Service.");
//            }
//
//            var broadcast = new IPAddress(maskbytes);
//            var localEp = new IPEndPoint(currentUsedIpAddress, UdpPort);
//            _broadcastEp = new IPEndPoint(broadcast, UdpPort);
//
//            _sendUdp = new UdpClient { ExclusiveAddressUse = false };
//            _sendUdp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
//            _sendUdp.Client.Bind(localEp);
//
//            _receiveUdp = new UdpClient(UdpPort, AddressFamily.InterNetwork);
//
//            // Init the Devices list
//            _devices = new List<Device>();
        }

        /// <summary>
        ///     Who-Is, and collect the device about who answers
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public List<Device> GetDevices(int milliseconds)
        {
            // Get the host data, send a Who-Is, accept responses and save in the DeviceList
            var sendBytes = new byte[12];

            _devices.Clear();

            // Send the request
            try
            {
                //PEP Use NPDU.Create and APDU.Create (when written)
                sendBytes[0] = Bvlc.BacnetBvlcTypeBip;
                sendBytes[1] = Bvlc.BacnetBvlcFuncUnicastNpdu;
                sendBytes[2] = 0;
                sendBytes[3] = 12;
                sendBytes[4] = BacnetEnums.BacnetProtocolVersion;
                sendBytes[5] = 0x20; // Control flags
                sendBytes[6] = 0xFF; // Destination network address (65535)
                sendBytes[7] = 0xFF;
                sendBytes[8] = 0; // Destination MAC layer address length, 0 = Broadcast
                sendBytes[9] = 0xFF; // Hop count = 255

                sendBytes[10] = (byte) BacnetEnums.BacnetPduType.PduTypeUnconfirmedServiceRequest;
                sendBytes[11] = (byte) BacnetEnums.BacnetUnconfirmedService.ServiceUnconfirmedWhoIs;

                _sendUdp.EnableBroadcast = false;
                _sendUdp.Send(sendBytes, 12, _broadcastEp);

                var watch = new Stopwatch();
                watch.Start();

                while (true)
                {
                    if (watch.Elapsed.TotalMilliseconds >= milliseconds)
                        break;
                    var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    if (_sendUdp.Client.Available > 0)
                    {
                        var recvBytes = _sendUdp.Receive(ref remoteIpEndPoint);
                        // Parse and save the BACnet data
                        var npduOffset = Bvlc.Parse(recvBytes, 0);
                        var apduOffset = Npdu.Parse(recvBytes, npduOffset);

                        if (Apdu.ParseIAm(recvBytes, apduOffset) <= 0)
                        {
                            continue;
                        }
                        var device = new Device
                        {
                            Name = "Device",
                            SourceLength = Npdu.Slen,
                            ServerEp = remoteIpEndPoint,
                            Network = Npdu.Snet,
                            MacAddress = Npdu.SAddress,
                            Instance = Apdu.ObjectId
                        };
                        if (!_devices.Contains(device))
                        {
                            _devices.Add(device);
                        }
                    }
                }

                watch.Stop();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error GetDevices {0}", ex.Message);
            }
            return _devices;
        }

        /// <summary>
        ///     Collect Who-Is information from a single IP address.
        ///     @todo: Use exceptions for timeout.
        /// </summary>
        /// <param name="bIpAddress">IP host address of suspected BACnet device.</param>
        /// <param name="milliseconds">I-Am receive timeout in milliseconds.</param>
        /// <returns>A BACnet Device object representation MAYBE with filled properties.</returns>
        public Device UnicastWhoIsOnSingleIp(IPEndPoint bIpAddress, int milliseconds)
        {
            var sendBytes = new byte[12];
            var device = new Device();

            try
            {
                //PEP Use NPDU.Create and APDU.Create (when written)
                sendBytes[0] = Bvlc.BacnetBvlcTypeBip;
                sendBytes[1] = Bvlc.BacnetBvlcFuncUnicastNpdu;
                sendBytes[2] = 0;
                sendBytes[3] = 12;
                sendBytes[4] = BacnetEnums.BacnetProtocolVersion;
                sendBytes[5] = 0x20; // Control flags
                sendBytes[6] = 0xFF; // Destination network address (65535)
                sendBytes[7] = 0xFF;
                sendBytes[8] = 0; // Destination MAC layer address length, 0 = Broadcast
                sendBytes[9] = 0xFF; // Hop count = 255

                sendBytes[10] = (byte) BacnetEnums.BacnetPduType.PduTypeUnconfirmedServiceRequest;
                sendBytes[11] = (byte) BacnetEnums.BacnetUnconfirmedService.ServiceUnconfirmedWhoIs;

                _sendUdp.EnableBroadcast = false;
                _sendUdp.Send(sendBytes, 12, bIpAddress);

                var watch = new Stopwatch();
                watch.Start();

                while (true)
                {
                    if (watch.Elapsed.TotalMilliseconds >= milliseconds)
                        break;
                    // Process the response packets
                    //if (WinSockRecvReady() > 0)
                    //{
                    //  if (WinSockRecvFrom(recvBytes, ref count, ref ipaddr) > 0)
                    // Process the response packets
                    if (_sendUdp.Client.Available > 0)
                    {
                        var recvBytes = _sendUdp.Receive(ref bIpAddress);
                        {
                            // Parse and save the BACnet data
                            var npduOffset = Bvlc.Parse(recvBytes, 0);
                            var apduOffset = Npdu.Parse(recvBytes, npduOffset);
                            if (Apdu.ParseIAm(recvBytes, apduOffset) <= 0)
                            {
                                continue;
                            }
                            device.Name = "Device";
                            device.SourceLength = Npdu.Slen;
                            device.ServerEp = bIpAddress;
                            device.Network = Npdu.Snet;
                            device.MacAddress = Npdu.SAddress;
                            device.Instance = Apdu.ObjectId;

                            // We should now have enough info to read/write properties for this device
                        }
                    }
                }

                watch.Stop();
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Error on UnicastWhoIsOnSingleIp {0}", e.Message);
            }

            return device;
        }

        /// <summary>
        ///     I-Am.
        /// </summary>
        /// <param name="network">The network.</param>
        /// <param name="objectid">The objectid.</param>
        /// <returns></returns>
        public bool GetIAm(int network, uint objectid)
        {
            // Wait for I-Am packet
            var found = false;

            try
            {
                var sock = _receiveUdp.Client;
                var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Process receive packets
                if (sock.Available > 0)
                {
                    var recvBytes = _receiveUdp.Receive(ref remoteIpEndPoint);
                    {
                        // Parse the packet - is it IAm?
                        var npduOffset = Bvlc.Parse(recvBytes, 0);
                        var apduOffset = Npdu.Parse(recvBytes, npduOffset);
                        if (Apdu.ParseIAm(recvBytes, apduOffset) > 0)
                        {
                            if ((network == Npdu.Snet) && (objectid == Apdu.ObjectId))
                            {
                                // Found it!
                                found = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            return found;
        }

        /// <summary>
        ///     Read Property.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="arrayidx">The arrayidx.</param>
        /// <param name="objtype">The objtype.</param>
        /// <param name="objprop">The objprop.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public bool SendReadProperty(
            Device recipient,
            int arrayidx,
            BacnetEnums.BacnetObjectType objtype,
            BacnetEnums.BacnetPropertyId objprop,
            Property property)
        {
            // Create and send an Confirmed Request

            //value = "(none)";
            if (recipient == null) return false;

            if (property == null) return false;

            //uint instance = BACnetData.Devices[deviceidx].Instance;

            var sendBytes = new byte[50];

            // BVLL
            sendBytes[0] = Bvlc.BacnetBvlcTypeBip;
            sendBytes[1] = Bvlc.BacnetBvlcFuncUnicastNpdu;
            sendBytes[2] = 0x00;
            sendBytes[3] = 0x00; // BVLL Length, fix later (24?)

            // NPDU
            sendBytes[4] = BacnetEnums.BacnetProtocolVersion;
            if (recipient.SourceLength == 0)
                sendBytes[5] = 0x04; // Control flags, no destination address
            else
                sendBytes[5] = 0x24; // Control flags, with broadcast or destination address

            uint len = 6;
            if (recipient.SourceLength > 0)
            {
                // Get the (MSTP) Network number (2001)
                var temp2 = BitConverter.GetBytes(recipient.Network);
                sendBytes[len++] = temp2[1];
                sendBytes[len++] = temp2[0];

                // Get the MAC address (0x0D)
                var temp4 = BitConverter.GetBytes(recipient.MacAddress);

                sendBytes[len++] = 0x01; // MAC address length - adjust for other lengths ...
                sendBytes[len++] = temp4[0];
                sendBytes[len++] = 0xFF; // Hop count = 255
            }

            // APDU
            sendBytes[len++] = 0x00; // Control flags
            sendBytes[len++] = 0x05; // Max APDU length (1476)

            // Create invoke counter
            sendBytes[len++] = (byte) (_invokeCounter);
            _invokeCounter = ((_invokeCounter + 1) & 0xFF);

            sendBytes[len++] = 0x0C; // Service Choice: Read Property request

            // Service Request (var part of APDU):
            // Set up Object ID (Context Tag)
            len = Apdu.SetObjectId(ref sendBytes, len, objtype, recipient.Instance);

            // Set up Property ID (Context Tag)
            len = Apdu.SetPropertyId(ref sendBytes, len, objprop);

            // Optional array index goes here
            if (arrayidx >= 0)
                len = Apdu.SetArrayIdx(ref sendBytes, len, arrayidx);

            // Fix the BVLL length
            sendBytes[3] = (byte) len;

            var getResponse = false;
            var count = 0;
            while (count < BacnetUnicastRequestRepeatCount && !getResponse)
            {
                _sendUdp.EnableBroadcast = false;
                _sendUdp.Send(sendBytes, (int) len, recipient.ServerEp);

                while (!getResponse)
                {
                    if (_sendUdp.Client.Available <= 0)
                    {
                        continue;
                    }
                    //recvBytes = SendUDP.Receive(ref RemoteEP);
                    var sendTo = recipient.ServerEp;
                    var recvBytes = _sendUdp.Receive(ref sendTo);

                    var apduOffset = Npdu.Parse(recvBytes, Bvlc.BacnetBvlcHeaderLen); // BVLL is always 4 bytes

                    // Check for APDU response 
                    // 0x - Confirmed Request 
                    // 1x - Un-Confirmed Request
                    // 2x - Simple ACK
                    // 3x - Complex ACK
                    // 4x - Segment ACK
                    // 5x - Error
                    // 6x - Reject
                    // 7x - Abort
                    if (recvBytes[apduOffset] != 0x30)
                    {
                        continue;
                    }
                    // Verify the Invoke ID is the same
                    var ic = (byte) (_invokeCounter == 0 ? 255 : _invokeCounter - 1);
                    if (ic != recvBytes[apduOffset + 1])
                    {
                        continue;
                    }
                    Apdu.ParseProperty(ref recvBytes, apduOffset, property);
                    getResponse = true; // This will still execute the finally
                }

                count++;
            }
            return getResponse;
        }

        /// <summary>
        ///     Write Property.
        /// </summary>
        /// <param name="recipient">The receipient.</param>
        /// <param name="arrayidx">The arrayidx.</param>
        /// <param name="objtype">The objtype.</param>
        /// <param name="objprop">The objprop.</param>
        /// <param name="property">The property.</param>
        /// <param name="priority">The priority.</param>
        /// <returns></returns>
        public bool SendWriteProperty(
            Device recipient,
            int arrayidx,
            BacnetEnums.BacnetObjectType objtype,
            BacnetEnums.BacnetPropertyId objprop,
            Property property,
            int priority)
        {
            // Create and send an Confirmed Request
            if (recipient == null) return false;

            if (property == null) return false;

            var sendBytes = new byte[50];

            // BVLL
            var len = Bvlc.Fill(ref sendBytes, Bvlc.BacnetBvlcFuncUnicastNpdu, 0);

            // NPDU
            sendBytes[len++] = BacnetEnums.BacnetProtocolVersion;
            if (recipient.SourceLength == 0)
                sendBytes[len++] = 0x04; // Control flags, no destination address
            else
                sendBytes[len++] = 0x24; // Control flags, with broadcast or destination

            if (recipient.SourceLength > 0)
            {
                // Get the (MSTP) Network number (2001)
                //sendBytes[6] = 0x07;  // Destination network address (2001)
                //sendBytes[7] = 0xD1;
                var temp2 = BitConverter.GetBytes(recipient.Network);
                sendBytes[len++] = temp2[1];
                sendBytes[len++] = temp2[0];

                // Get the MAC address (0x0D)
                //sendBytes[8] = 0x01;  // MAC address length
                //sendBytes[9] = 0x0D;  // Destination MAC layer address
                var temp4 = BitConverter.GetBytes(recipient.MacAddress);
                sendBytes[len++] = 0x01; // MAC address length - adjust for other lengths ...
                sendBytes[len++] = temp4[0];

                sendBytes[len++] = 0xFF; // Hop count = 255
            }

            // APDU
            sendBytes[len++] = 0x00; // Control flags
            sendBytes[len++] = 0x05; // Max APDU length (1476)

            // Create invoke counter
            //sendBytes[len++] = InvokeCounter++;  // Invoke ID
            sendBytes[len++] = (byte) (_invokeCounter);
            _invokeCounter = ((_invokeCounter + 1) & 0xFF);

            sendBytes[len++] = 0x0F; // Service Choice: Write Property request

            // Service Request (var part of APDU):
            // Set up Object ID (Context Tag)
            len = Apdu.SetObjectId(ref sendBytes, len, objtype, recipient.Instance);

            // Set up Property ID (Context Tag)
            len = Apdu.SetPropertyId(ref sendBytes, len, objprop);

            // Optional array index goes here
            if (arrayidx >= 0)
                len = Apdu.SetArrayIdx(ref sendBytes, len, arrayidx);

            // Set the value to send
            len = Apdu.SetProperty(ref sendBytes, len, property);

            //PEP Optional array index goes here

            // Set priority
            if (priority > 0)
                len = Apdu.SetPriority(ref sendBytes, len, priority);

            // Fix the BVLL length
            sendBytes[3] = (byte) len;

            var count = 0;
            var getResponse = false;
            while (count < BacnetUnicastRequestRepeatCount && !getResponse)
            {
                _sendUdp.EnableBroadcast = false;
                _sendUdp.Send(sendBytes, (int) len, recipient.ServerEp);

                while (!getResponse)
                {
                    if (_sendUdp.Client.Available <= 0)
                    {
                        continue;
                    }
                    //recvBytes = SendUDP.Receive(ref RemoteEP);
                    var sendTo = recipient.ServerEp;
                    var recvBytes = _sendUdp.Receive(ref sendTo);

                    var apduOffset = Npdu.Parse(recvBytes, 4); // BVLL is always 4 bytes
                    // Check for APDU response, and decide what to do
                    // 0x - Confirmed Request 
                    // 1x - Un-Confirmed Request
                    // 2x - Simple ACK
                    // 3x - Complex ACK
                    // 4x - Segment ACK
                    // 5x - Error
                    // 6x - Reject
                    // 7x - Abort
                    if (recvBytes[apduOffset] != 0x20)
                    {
                        continue;
                    }
                    // Verify the Invoke ID is the same
                    var ic = (byte) (_invokeCounter == 0 ? 255 : _invokeCounter - 1);
                    if (ic == recvBytes[apduOffset + 1])
                    {
                        getResponse = true; // This will still execute the finally
                    }
                }
                count++;
            }
            return getResponse; // This will still execute the finally
        }

        /// <summary>
        ///     Sends the read BDT.
        /// </summary>
        /// <param name="bIpAddress">The bacnet ip address.</param>
        /// <returns></returns>
        public bool SendReadBdt(IPEndPoint bIpAddress)
        {
            // Create and send an Confirmed Request
            if (bIpAddress == null) return false;

            //uint instance = BACnetData.Devices[deviceidx].Instance;

            var sendBytes = new byte[50];

            // BVLL
            var len = Bvlc.Fill(ref sendBytes, Bvlc.BacnetBvlcFuncReadBdt, 0);
            var count = 0;
            var getResponse = false;

            while (count < BacnetUnicastRequestRepeatCount && !getResponse)
            {
                _sendUdp.EnableBroadcast = false;
                _sendUdp.Send(sendBytes, (int) len, bIpAddress);

                while (!getResponse)
                {
                    if (_sendUdp.Client.Available > 0)
                    {
                        var recvBytes = _sendUdp.Receive(ref bIpAddress);

                        Bvlc.Parse(recvBytes, 0);
                        if (Bvlc.BacnetBvlcFuncReadBdtAck == Bvlc.BvlcFunction &&
                            null != Bvlc.BvlcListOfBdtEntries)
                        {
                            getResponse = true;
                        }
                        else if (Bvlc.BacnetBvlcFuncResult == Bvlc.BvlcFunction)
                        {
                            if (0x0020 == Bvlc.BvlcFunctionResultCode)
                            {
                                getResponse = true;
                            }
                        }
                    }
                }
                count++;
            }
            return getResponse; // This will still execute the finally
        }

        /// <summary>
        ///     Sends the read FDT.
        /// </summary>
        /// <param name="bIpAddress">The bacnet ip address.</param>
        /// <returns></returns>
        public bool SendReadFdt(IPEndPoint bIpAddress)
        {
            // Create and send an Confirmed Request
            if (bIpAddress == null) return false;


            var sendBytes = new byte[50];

            // BVLL
            var len = Bvlc.Fill(ref sendBytes, Bvlc.BacnetBvlcFuncReadFdt, 0);

            var count = 0;
            var getResponse = false;

            while (count < BacnetUnicastRequestRepeatCount && !getResponse)
            {
                _sendUdp.EnableBroadcast = false;
                _sendUdp.Send(sendBytes, (int) len, bIpAddress);

                while (!getResponse)
                {
                    if (_sendUdp.Client.Available > 0)
                    {
                        var recvBytes = _sendUdp.Receive(ref bIpAddress);
                        Bvlc.Parse(recvBytes, 0);
                        if (Bvlc.BacnetBvlcFuncReadFdtAck == Bvlc.BvlcFunction &&
                            null != Bvlc.BvlcListOfFdtEntries)
                        {
                            getResponse = true;
                        }
                        else if (Bvlc.BacnetBvlcFuncResult == Bvlc.BvlcFunction)
                        {
                            if (0x0040 == Bvlc.BvlcFunctionResultCode)
                            {
                                getResponse = true;
                            }
                        }
                    }
                }
                count++;
            }
            return getResponse; // This will still execute the finally
        }
    }
}