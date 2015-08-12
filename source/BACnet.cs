/**************************************************************************
*
* THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
* OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
* CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
* SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*
*********************************************************************/

/* COPYRIGHT
 -------------------------------------------
 Copyright (C) 2013-2015 Plus 1 Micro, Inc.

 This program is free software; you can redistribute it and/or
 modify it under the terms of the GNU General Public License
 as published by the Free Software Foundation; either version 2
 of the License, or (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program; if not, write to:
 The Free Software Foundation, Inc.
 59 Temple Place - Suite 330
 Boston, MA  02111-1307, USA.

 As a special exception, if other files instantiate templates or
 use macros or inline functions from this file, or you compile
 this file and link it with other works to produce a work based
 on this file, this file does not by itself cause the resulting
 work to be covered by the GNU General Public License. However
 the source code for this file must still be made available in
 accordance with section (3) of the GNU General Public License.

 This exception does not invalidate any other reasons why a work
 based on this file might be covered by the GNU General Public
 License.
 -------------------------------------------
*/

using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace BACnet
{

  //===============================================================================================
  // A .NET Implementaion of the BACnet Stack
  // Class Abstract:
  //    A BACnetStack is a Client-Server interface protocol implementation of the BACnet 
  //    Specification, allowing a connection between the Appication Layer and BACnet Devices
  //    Application Entity => Application Layer <--> BACnet Devices
  //    Specifically, it is a "BACnet User Element"
  //    Members include:
  //      Packet Creation and Processing
  //      Services (Who-Is, I-Am, ReadProperty, WriteProperty, Reject, etc)
  //      Objects (Device, etc)
  //      Network Layer Protocol
  //      Application Layer Protocol
  //      Transactions

  //-----------------------------------------------------------------------------------------------
  // BACnet Services
  class BACnetService
  {
  }

  class BACnetServiceRequest : BACnetService
  {
  }

  class BACnetServiceResponse : BACnetService
  {
  }

  class BACnetServiceIndication : BACnetService
  {
  }

  class BACnetServiceConfirm : BACnetService
  {
  }

  //-----------------------------------------------------------------------------------------------
  // BACnetTag Routines
  public static class BACnetTag
  {
    public static byte TagNumber(byte tag)
    {
      int x = ((int)tag >> 4) & 0x0F;
      return (byte)x;
    }

    public static byte Class(byte tag) 
    {
      int x = ((int)tag >> 3) & 0x01;
      return (byte)x;
    }
    public static byte LenValType(byte tag)
    {
      int x = (int)tag & 0x07;
      return (byte)x;
    }
  }

  //-----------------------------------------------------------------------------------------------
  // NPDU Routines
  public static class NPDU
  {
    public static byte PDUControl;
    public static UInt16 DNET;
    public static byte DLEN;
    public static byte[] DADR;
    public static UInt16 SNET;
    public static byte SLEN;
    public static byte[] SADR;
    public static byte HopCount;
    public static byte MessageType;
    public static UInt16 VendorID;
    public static UInt32 DAddress;
    public static UInt32 SAddress;

    public static void /*NPDU*/ Clear()
    {
      // Clear the packet members
      PDUControl = 0;
      DNET = 0;
      DLEN = 0;
      DADR = null;
      SNET = 0;
      SLEN = 0;
      SADR = null;
      HopCount = 0;
      MessageType = 0;
      VendorID = 0;
      DAddress = 0;
      SAddress = 0;
    }

    public static int /*NPDU*/ Assemble(byte[] bytes, int offset)
    {
      // Create a NPUD packet in the bytes array, starting at offset given
      // Return the length
      int len = 0;
      return len;
    }

    public static int /*NPDU*/ Parse(byte[] bytes, int offset)
    {
      // Returns the Length of the NPDU portion of the packet (offset of APDU)
      // We assume the BVLL is always present, so the NPDU always starts at offset 4
      int len = offset; // 4
      byte[] temp;
      Clear();
      if (bytes[len++] != 0x01) return 0;
      PDUControl = bytes[len++];  // 5
      if ((PDUControl & 0x20) > 0)
      {
        // We have a Destination 
        temp = new byte[2];
        temp[1] = bytes[len++];
        temp[0] = bytes[len++];
        DNET = BitConverter.ToUInt16(temp, 0);
        DLEN = bytes[len++];
        if (DLEN == 1)
        {
          DADR = new byte[1];
          DADR[0] = bytes[len++];
          DAddress = (UInt32)DADR[0];
        }
        if (DLEN == 2)
        {
          temp = new byte[2];
          DADR[1] = bytes[len++];
          DADR[0] = bytes[len++];
          DAddress = (UInt32)BitConverter.ToUInt16(DADR, 0);
        }
        if (DLEN == 4)
        {
          temp = new byte[4];
          DADR[3] = bytes[len++];
          DADR[2] = bytes[len++];
          DADR[1] = bytes[len++];
          DADR[0] = bytes[len++];
          DAddress = BitConverter.ToUInt32(DADR, 0);
        }
        //PEP Other DLEN values ...

      }
      else
        DLEN = 0;

      if ((PDUControl & 0x08) > 0)
      {
        // We have a Source
        temp = new byte[2];
        temp[1] = bytes[len++];
        temp[0] = bytes[len++];
        SNET = BitConverter.ToUInt16(temp, 0);
        SLEN = bytes[len++];
        if (SLEN == 1)
        {
          SADR = new byte[1];
          SADR[0] = bytes[len++];
          SAddress = (UInt32)SADR[0];
        }
        if (SLEN == 2)
        {
          SADR = new byte[2];
          SADR[1] = bytes[len++];
          SADR[0] = bytes[len++];
          SAddress = (UInt32)BitConverter.ToUInt16(SADR, 0);
        }
        if (SLEN == 4)
        {
          SADR = new byte[4];
          SADR[3] = bytes[len++];
          SADR[2] = bytes[len++];
          SADR[1] = bytes[len++];
          SADR[0] = bytes[len++];
          SAddress = BitConverter.ToUInt32(SADR, 0);
        }
        //PEP Other SLEN values ...
      }
      else
        SLEN = 0;

      if ((PDUControl & 0x20) > 0)
      {
        HopCount = bytes[len++];  // Get the Hop Count 
      }

/*
      if ((PDUControl & 0x80) > 0)
      {
        MessageType = bytes[len + offset];
        len++;                  // Message Type field
          if (MessageType >= 0x80)
          {
            temp = new byte[2];
            temp[0] = bytes[len + offset + 2];
            temp[1] = bytes[len + offset + 1];
            VendorID = BitConverter.ToUInt16(temp, 0);
            len += 2;             // VendorID field
          }
        }
        len += offset;
      }
*/
      return len;
    }

  }


  //-----------------------------------------------------------------------------------------------
  // Octet Class

  //-----------------------------------------------------------------------------------------------
  // Property Class
  public class Property
  {
    public BACnetEnums.BACNET_APPLICATION_TAG Tag { get; set; }
    public bool ValueBool { get; set; }
    public uint ValueUInt { get; set; }
    public int ValueInt { get; set; }
    public float ValueSingle { get; set; }
    public double ValueDouble { get; set; }
    public byte[] ValueOctet { get; set; }
    public string ValueString { get; set; }
    public uint ValueEnum { get; set; }
    public BACnetEnums.BACNET_OBJECT_TYPE ValueObjectType { get; set; }
    public uint ValueObjectInstance { get; set; }
    public string ToStringValue;
    //PEP Others ...

    // Constructors
    public Property()
    {
      this.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_NULL;
      this.ValueBool = false;
      this.ValueUInt = 0;
      this.ValueInt = 0;
      this.ValueSingle = 0;
      this.ValueDouble = 0;
      this.ValueOctet = null;
      this.ValueString = "";
      this.ValueEnum = 0;
      this.ValueObjectType = BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_DEVICE;
      this.ValueObjectInstance = 0;
      this.ToStringValue = "";
    }

    public override string ToString()
    {
      return this.ToStringValue;
    }
  }

  //-----------------------------------------------------------------------------------------------
    
  //-----------------------------------------------------------------------------------------------
  // Transaction State Machine
  class TransactionStateMachine
  {
    public enum TSMState { IDLE, AWAIT_CONFIRMATION, AWAIT_RESPONSE };
    TSMState State = TSMState.IDLE;
    int RetryCount;

    // Constructor
    public TransactionStateMachine()
    {
      // Create the timer
      //Timer RequestTimer = new Timer();
      //RequestTimer.Tick += new EventHandler(RequestTimer_Tick);
    }

    // Welcome To The Machine - what to do here ?

  }

  public static class BACnetData
  {
    public static List<Device> Devices;   // A list of BACnet devices after the WhoIs
    public static int DeviceIndex;        // The current BACnet device selected
    public static UInt32 PacketRetryCount;
  }

  //-----------------------------------------------------------------------------------------------
  // The Stack
  public class BACnetStack
  {

    //// Start up WSA, open a Socket, and Bind it
    //[DllImport("WinSockWrap.dll")]
    //static extern int WinSockStartUp();

    //// Send a packet to the ip specified (or broadcast)
    //[DllImport("WinSockWrap.dll")]
    //static extern int WinSockSendTo(byte[] bytes, int count, ulong ipaddr);

    //// See if a receive packet is ready
    //[DllImport("WinSockWrap.dll")]
    //static extern int WinSockRecvReady();

    //// Get the receive packet
    //[DllImport("WinSockWrap.dll")]
    //static extern int WinSockRecvFrom(byte[] bytes, ref int count, ref ulong ipaddr);

    //// Shut down the socket
    //[DllImport("WinSockWrap.dll")]
    //static extern int WinSockShutDown();

    //// If there was an error from any of the above, call this method
    //[DllImport("WinSockWrap.dll")]
    //static extern int WinSockLastError();

    // Requesting User Confirmed service primitives:
    BACnetServiceRequest Request; 
    BACnetServiceConfirm Confirm;
  
    // Responding User Confirmed service primitives:
    BACnetServiceIndication Indication;
    BACnetServiceResponse Response;

    UdpClient SendUDP = null; // = new UdpClient(UDPPort);
    UdpClient ReceiveUDP = null; // = new UdpClient(UDPPort, AddressFamily.InterNetwork);

    IPEndPoint LocalEP = null;
    IPEndPoint BroadcastEP = null;
    //IPEndPoint RemoteEP = null;

    private const int UDPPort = 47808;
    private bool TimerDone = false;
    private int InvokeCounter = 0;
    //private byte InvokeCounter = 0;

    // We won't be doing Segments for now
    bool Segmented = false;

    // Create a TSM when a Request is initiated
    TransactionStateMachine TSM;

    // Constructor --------------------------------------------------------------------------------
    //public BACnetStack(string server)
    public BACnetStack()
    {
      // Machine dependent (little endian vs big endian) 
      // In this case we have to reverse the bytes for the Server IP
      byte[] maskbytes = new byte[4];
      byte[] addrbytes = new byte[4];

      //byte[] addr = IPAddress.Parse(server).GetAddressBytes();
      //if (BitConverter.IsLittleEndian) 
      //  Array.Reverse(addr);
      //Server = BitConverter.ToUInt32(addr, 0);

      //if (WinSockStartUp() < 1)
      //  MessageBox.Show("Socket StartUp Error " + WinSockLastError().ToString());

      // Find the local IP address and Subnet Mask
      NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();
      foreach (NetworkInterface Interface in Interfaces)
      {
        if (Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
        //MessageBox.Show(Interface.Description);
        UnicastIPAddressInformationCollection UnicastIPInfoCol = Interface.GetIPProperties().UnicastAddresses;
        foreach (UnicastIPAddressInformation UnicatIPInfo in UnicastIPInfoCol)
        {
          //MessageBox.Show("\tIP Address is {0}" + UnicatIPInfo.Address);
          //MessageBox.Show("\tSubnet Mask is {0}" + UnicatIPInfo.IPv4Mask);
          if (UnicatIPInfo.IPv4Mask != null)
          {
            byte[] tempbytes = UnicatIPInfo.IPv4Mask.GetAddressBytes();
            if (tempbytes[0] == 255)
            {
              // We found the correct subnet mask, and probably the correct IP address
              addrbytes = UnicatIPInfo.Address.GetAddressBytes();
              maskbytes = UnicatIPInfo.IPv4Mask.GetAddressBytes();
              break;
            }
          }
        }
      }
      // Set up broadcast address
      if (maskbytes[3] == 0) maskbytes[3] = 255; else maskbytes[3] = addrbytes[3];
      if (maskbytes[2] == 0) maskbytes[2] = 255; else maskbytes[2] = addrbytes[2];
      if (maskbytes[1] == 0) maskbytes[1] = 255; else maskbytes[1] = addrbytes[1];
      if (maskbytes[0] == 0) maskbytes[0] = 255; else maskbytes[0] = addrbytes[0];
      IPAddress myip = new IPAddress(addrbytes);
      IPAddress broadcast = new IPAddress(maskbytes);

      LocalEP = new IPEndPoint(myip, UDPPort);
      BroadcastEP = new IPEndPoint(broadcast, UDPPort);

      SendUDP = new UdpClient();
      SendUDP.ExclusiveAddressUse = false;
      SendUDP.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
      SendUDP.Client.Bind(LocalEP);

      ReceiveUDP = new UdpClient(UDPPort, AddressFamily.InterNetwork);

      //// Create a TSM
      //TSM = new TransactionStateMachine();

      // Init the Devices list
      BACnetData.Devices = new List<Device>();
    }

    // Bind Device Instance to the BACnet Address (we need SNET, SLEN, SADR, etc)
    public bool /*BACnetStack*/ BindBACnetDevice(UInt32 instance, ref int devidx)
    {
      // Linear (brute force) search for now
      for (int i = 0; i < BACnetData.Devices.Count; i++)
      {
        Device dev = BACnetData.Devices[i];
        if (instance == dev.Instance)
        {
          devidx = i;
          return true;
        }
      }
      return false;
    }

    // Timer Event for the Socket I/O
    private void /*BACnetStack*/ Timer_Tick(object sender, EventArgs e)
    {
      TimerDone = true;
    }

    public bool /*BACnetStack*/ GetIAm(int network, UInt32 objectid)
    {
      // Wait for I-Am packet
      Byte[] recvBytes = new Byte[512];
      bool found = false;

      // Create the timer
      Timer IAmTimer = new Timer();
      using (IAmTimer)
      {
        IAmTimer.Tick += new EventHandler(Timer_Tick);

        try
        {
          Socket sock = ReceiveUDP.Client;
          IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

          // Start the timer
          TimerDone = false;
          IAmTimer.Interval = 1000;
          IAmTimer.Start();
          while (!TimerDone && !found)
          {
            Application.DoEvents();

            // Process receive packets
            if (sock.Available > 0)
            {
              recvBytes = ReceiveUDP.Receive(ref RemoteIpEndPoint);
              {
                // Parse the packet - is it IAm?
                int APDUOffset = NPDU.Parse(recvBytes, 4); // BVLL is always 4 bytes
                if (APDU.ParseIAm(recvBytes, APDUOffset) > 0)
                {
                  if ((network == NPDU.SNET) && (objectid == APDU.ObjectID))
                  {
                    // Found it!
                    found = true;
                  }
                }
              }
            }
          }
        }
        catch (Exception e)
        {
          MessageBox.Show(e.Message);
        }
        finally
        {
          IAmTimer.Stop();
        }
      }
      return found;
    }

    // Do a Who-Is, and collect information about who answers -------------------------------------
    public void  /*BACnetStack*/ GetDevices(int milliseconds) 
    { 
      // Get the host data, send a Who-Is, accept responses and save in the DeviceList
      //ulong ipaddr = 0;
      //int count = 0;
      Byte[] sendBytes = new Byte[12];
      Byte[] recvBytes = new Byte[512];

      // Dns stuff obsoleted ...
      //string hostname = Dns.GetHostName();
      //IPHostEntry host = Dns.GetHostByName(hostname);
      //IPHostEntry host = Dns.GetHostEntry(hostname);

      BACnetData.Devices.Clear();

      // Send the request
      //MessageBox.Show("Send Who-Is (" + broadcast + ")");
      //MessageBox.Show("Send Who-Is");

      // Create the timer
      Timer IAmTimer = new Timer();
      using (IAmTimer)
      {
        IAmTimer.Tick += new EventHandler(Timer_Tick);

        try
        {
          //PEP Use NPDU.Create and APDU.Create (when written)
          sendBytes[0] = BACnetEnums.BACNET_BVLC_TYPE_BIP;
          sendBytes[1] = BACnetEnums.BACNET_UNICAST_NPDU;
          sendBytes[2] = 0;
          sendBytes[3] = 12;
          sendBytes[4] = BACnetEnums.BACNET_PROTOCOL_VERSION;
          sendBytes[5] = 0x20;  // Control flags
          sendBytes[6] = 0xFF;  // Destination network address (65535)
          sendBytes[7] = 0xFF;
          sendBytes[8] = 0;     // Destination MAC layer address length, 0 = Broadcast
          sendBytes[9] = 0xFF;  // Hop count = 255

          sendBytes[10] = (Byte)BACnetEnums.BACNET_PDU_TYPE.PDU_TYPE_UNCONFIRMED_SERVICE_REQUEST;
          sendBytes[11] = (Byte)BACnetEnums.BACNET_UNCONFIRMED_SERVICE.SERVICE_UNCONFIRMED_WHO_IS;

          //ipaddr = 0xC0A85CFF; // 192.168.92.FF
          //if (WinSockSendTo(sendBytes, 12, ipaddr) < 1)
          //{
          //  MessageBox.Show("Socket Send Error " + WinSockLastError().ToString());
          //  return;
          //}
          // Send the broadcast "who-is"
          //SendUDP.EnableBroadcast = true;
          //SendUDP.Connect(broadcast, UDPPort);
          SendUDP.EnableBroadcast = true;
          SendUDP.Send(sendBytes, 12, BroadcastEP);

          Socket sock = ReceiveUDP.Client;
          IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

          // Start the timer so we can receive multiple responses
          TimerDone = false;
          IAmTimer.Interval = milliseconds;
          IAmTimer.Start();
          while (!TimerDone)
          {
            Application.DoEvents();

            // Process the response packets
            //if (WinSockRecvReady() > 0)
            //{
            //  if (WinSockRecvFrom(recvBytes, ref count, ref ipaddr) > 0)
            // Process the response packets
            if (sock.Available > 0)
            {
              recvBytes = ReceiveUDP.Receive(ref RemoteIpEndPoint);
              {
                // Parse and save the BACnet data
                int APDUOffset = NPDU.Parse(recvBytes, 4); // BVLL is always 4 bytes
                if (APDU.ParseIAm(recvBytes, APDUOffset) > 0)
                {
                    Device device = new Device();
                    device.Name = "Device";
                    device.SourceLength = NPDU.SLEN;
                    device.ServerEP = RemoteIpEndPoint;
                    device.Network = NPDU.SNET;
                    device.MACAddress = NPDU.SAddress;
                    device.Instance = APDU.ObjectID;
                    if (! BACnetData.Devices.Contains(device) )
                    {
                        BACnetData.Devices.Add(device);
                    }

                  // We should now have enough info to read/write properties for this device
                }
              }
              // Restart the timer - as long as I-AM packets come, we'll wait
              IAmTimer.Stop();
              IAmTimer.Start();
            }
          }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.StackTrace);
        }
        finally
        {
            IAmTimer.Stop();
        }
      }
    }

    // Read Property -------------------------------------------------------------------------
    public bool /*BACnetStack*/ SendReadProperty(
      int deviceidx,
      uint instance,
      int arrayidx,
      BACnetEnums.BACNET_OBJECT_TYPE objtype,
      BACnetEnums.BACNET_PROPERTY_ID objprop,
      Property property)

      //out string value)
      // Parameters:
      //   Device index (for network and MAC address),
      //   Object Type, 
      //   Property ID,
      //   Value returned
    {
      // Create and send an Confirmed Request

      //value = "(none)";
      if ((deviceidx < 0) || (deviceidx >= BACnetData.Devices.Count)) return false;

      IPEndPoint remoteEP = BACnetData.Devices[deviceidx].ServerEP;
      if (remoteEP == null) return false;

      if (property == null) return false;

      //uint instance = BACnetData.Devices[deviceidx].Instance;

      Byte[] sendBytes = new Byte[50];
      Byte[] recvBytes = new Byte[512];
      uint len;

      // BVLL
      sendBytes[0] = BACnetEnums.BACNET_BVLC_TYPE_BIP;
      sendBytes[1] = BACnetEnums.BACNET_UNICAST_NPDU;
      sendBytes[2] = 0x00;
      sendBytes[3] = 0x00;  // BVLL Length, fix later (24?)

      // NPDU
      sendBytes[4] = BACnetEnums.BACNET_PROTOCOL_VERSION;
      if (BACnetData.Devices[deviceidx].SourceLength == 0)
        sendBytes[5] = 0x04;  // Control flags, no destination address
      else
        sendBytes[5] = 0x24;  // Control flags, with broadcast or destination address

      len = 6;
      if (BACnetData.Devices[deviceidx].SourceLength > 0)
      {
        // Get the (MSTP) Network number (2001)
        //sendBytes[6] = 0x07;  // Destination network address (2001)
        //sendBytes[7] = 0xD1;
        byte[] temp2 = new byte[2];
        temp2 = BitConverter.GetBytes(BACnetData.Devices[deviceidx].Network);
        sendBytes[len++] = temp2[1];
        sendBytes[len++] = temp2[0];

        // Get the MAC address (0x0D)
        //sendBytes[8] = 0x01;  // MAC address length
        //sendBytes[9] = 0x0D;  // Destination MAC layer address
        byte[] temp4 = new byte[4];
        temp4 = BitConverter.GetBytes(BACnetData.Devices[deviceidx].MACAddress);

        sendBytes[len++] = 0x01;  // MAC address length - adjust for other lengths ...
        sendBytes[len++] = temp4[0];
        sendBytes[len++] = 0xFF;  // Hop count = 255
      }

      // APDU
      sendBytes[len++] = 0x00;  // Control flags
      sendBytes[len++] = 0x05;  // Max APDU length (1476)

      // Create invoke counter
      sendBytes[len++] = (byte)(InvokeCounter);
      InvokeCounter = ((InvokeCounter + 1) & 0xFF);

      sendBytes[len++] = 0x0C;  // Service Choice: Read Property request

      // Service Request (var part of APDU):
      // Set up Object ID (Context Tag)
      len = APDU.SetObjectID(ref sendBytes, len, objtype, instance);

      // Set up Property ID (Context Tag)
      len = APDU.SetPropertyID(ref sendBytes, len, objprop);

      // Optional array index goes here
      if (arrayidx >= 0)
        len = APDU.SetArrayIdx(ref sendBytes, len, arrayidx);

      // Fix the BVLL length
      sendBytes[3] = (byte)len;

      // Create the timer (we could use a blocking recvFrom instead ...)
      Timer ReadPropTimer = new Timer();
      try
      {
        int Count = 0;
        using (ReadPropTimer)
        {
          ReadPropTimer.Tick += new EventHandler(Timer_Tick);

          while (Count < 3)
          {
            SendUDP.EnableBroadcast = false;
            SendUDP.Send(sendBytes, (int)len, remoteEP);

            // Start the timer
            TimerDone = false;
            ReadPropTimer.Interval = 400;  // 100 ms
            ReadPropTimer.Start();
            while (!TimerDone)
            {
              // Wait for Confirmed Response
              Application.DoEvents();

              if (SendUDP.Client.Available > 0)
              {
                //recvBytes = SendUDP.Receive(ref RemoteEP);
                recvBytes = SendUDP.Receive(ref remoteEP);

                int APDUOffset = NPDU.Parse(recvBytes, 4); // BVLL is always 4 bytes

                // Check for APDU response 
                // 0x - Confirmed Request 
                // 1x - Un-Confirmed Request
                // 2x - Simple ACK
                // 3x - Complex ACK
                // 4x - Segment ACK
                // 5x - Error
                // 6x - Reject
                // 7x - Abort
                if (recvBytes[APDUOffset] == 0x30)
                {
                  // Verify the Invoke ID is the same
                  byte ic = (byte)(InvokeCounter == 0 ? 255 : InvokeCounter - 1);
                  if (ic == recvBytes[APDUOffset + 1])
                  {
                    APDU.ParseProperty(ref recvBytes, APDUOffset, property);
                    return true;  // This will still execute the finally
                  }
                  //else
                  //{
                  //  MessageBox.Show("Invoke Counter Error");
                  //  return false;
                  //}
                }
              }
            }
            Count++;
            BACnetData.PacketRetryCount++;
            ReadPropTimer.Stop(); // We'll start it over at the top of the loop
          }
          return false;  // This will still execute the finally
        }
      }
      finally
      {
        ReadPropTimer.Stop();
      }
    }

    // Write Property -------------------------------------------------------------------------
    public bool /*BACnetStack*/ SendWriteProperty(
      int deviceidx,
      uint instance,
      int arrayidx,
      BACnetEnums.BACNET_OBJECT_TYPE objtype,
      BACnetEnums.BACNET_PROPERTY_ID objprop,
      Property property,
      int priority)
    // Parameters:
    //   Device index (for network and MAC address),
    //   Object Type, 
    //   Property ID,
    //   Property Value
    //   Priority
    {
      // Create and send an Confirmed Request
      if ((deviceidx < 0) || (deviceidx >= BACnetData.Devices.Count)) return false;

      IPEndPoint remoteEP = BACnetData.Devices[deviceidx].ServerEP;
      if (remoteEP == null) return false;

      if (property == null) return false;

      Byte[] sendBytes = new Byte[50];
      Byte[] recvBytes = new Byte[512];
      uint len;
      //int count = 0;

      // BVLL
      sendBytes[0] = BACnetEnums.BACNET_BVLC_TYPE_BIP;
      sendBytes[1] = BACnetEnums.BACNET_UNICAST_NPDU;
      sendBytes[2] = 0x00;
      sendBytes[3] = 0x00;  // BVLL Length = 24?

      // NPDU
      sendBytes[4] = BACnetEnums.BACNET_PROTOCOL_VERSION;
      if (BACnetData.Devices[deviceidx].SourceLength == 0)
        sendBytes[5] = 0x04;  // Control flags, no destination address
      else
        sendBytes[5] = 0x24;  // Control flags, with broadcast or destination

      len = 6;
      if (BACnetData.Devices[deviceidx].SourceLength > 0)
      {
        // Get the (MSTP) Network number (2001)
        //sendBytes[6] = 0x07;  // Destination network address (2001)
        //sendBytes[7] = 0xD1;
        byte[] temp2 = new byte[2];
        temp2 = BitConverter.GetBytes(BACnetData.Devices[deviceidx].Network);
        sendBytes[len++] = temp2[1];
        sendBytes[len++] = temp2[0];

        // Get the MAC address (0x0D)
        //sendBytes[8] = 0x01;  // MAC address length
        //sendBytes[9] = 0x0D;  // Destination MAC layer address
        byte[] temp4 = new byte[4];
        temp4 = BitConverter.GetBytes(BACnetData.Devices[deviceidx].MACAddress);
        sendBytes[len++] = 0x01;  // MAC address length - adjust for other lengths ...
        sendBytes[len++] = temp4[0];

        sendBytes[len++] = 0xFF;  // Hop count = 255
      }

      // APDU
      sendBytes[len++] = 0x00;  // Control flags
      sendBytes[len++] = 0x05;  // Max APDU length (1476)

      // Create invoke counter
      //sendBytes[len++] = InvokeCounter++;  // Invoke ID
      sendBytes[len++] = (byte)(InvokeCounter);
      InvokeCounter = ((InvokeCounter + 1) & 0xFF);

      sendBytes[len++] = 0x0F;  // Service Choice: Write Property request

      // Service Request (var part of APDU):
      // Set up Object ID (Context Tag)
      len = APDU.SetObjectID(ref sendBytes, len, objtype, instance);

      // Set up Property ID (Context Tag)
      len = APDU.SetPropertyID(ref sendBytes, len, objprop);

      // Optional array index goes here
      if (arrayidx >= 0)
        len = APDU.SetArrayIdx(ref sendBytes, len, arrayidx);

      // Set the value to send
      len = APDU.SetProperty(ref sendBytes, len, property);

      //PEP Optional array index goes here

      // Set priority
      if (priority > 0)
        len = APDU.SetPriority(ref sendBytes, len, priority);

      // Fix the BVLL length
      sendBytes[3] = (byte)len;

      // Create the timer (we could use a blocking recvFrom instead ...)
      Timer ReadPropTimer = new Timer();

      try
      {
        using (ReadPropTimer)
        {
          int Count = 0;
          ReadPropTimer.Tick += new EventHandler(Timer_Tick);

          while (Count < 3)
          {
            SendUDP.EnableBroadcast = false;
            SendUDP.Send(sendBytes, (int)len, remoteEP);

            // Start the timer
            TimerDone = false;
            ReadPropTimer.Interval = 400; // 300;  // 100 ms
            ReadPropTimer.Start();
            while (!TimerDone)
            {
              // Wait for Confirmed Response
              Application.DoEvents();

              if (SendUDP.Client.Available > 0)
              {
                //recvBytes = SendUDP.Receive(ref RemoteEP);
                recvBytes = SendUDP.Receive(ref remoteEP);

                int APDUOffset = NPDU.Parse(recvBytes, 4); // BVLL is always 4 bytes
                // Check for APDU response, and decide what to do
                // 0x - Confirmed Request 
                // 1x - Un-Confirmed Request
                // 2x - Simple ACK
                // 3x - Complex ACK
                // 4x - Segment ACK
                // 5x - Error
                // 6x - Reject
                // 7x - Abort
                if (recvBytes[APDUOffset] == 0x20)
                {
                  // Verify the Invoke ID is the same
                  byte ic = (byte)(InvokeCounter == 0 ? 255 : InvokeCounter - 1);
                  if (ic == recvBytes[APDUOffset + 1])
                  {
                    return true; // This will still execute the finally
                  }
                  //else
                  //{
                  //  MessageBox.Show("Invoke Counter Error");
                  //  return false;
                  //}
                }
              }
            }
            Count++;
            BACnetData.PacketRetryCount++;
            ReadPropTimer.Stop(); // We'll start it over at the top of the loop
          }
          return false; // This will still execute the finally
        }
      }
      finally
      {
        ReadPropTimer.Stop();
      }
    }

    // Read Broadcast Distribution Table -------------------------------------------------------------------------
    public bool /*BACnetStack*/ SendReadBDT( int deviceidx, Property property)
      //out string value)
      // Parameters:
      //   Device index (for network and MAC address),
      //   Object Type, 
      //   Property ID,
      //   Value returned
    {
      // Create and send an Confirmed Request

      //value = "(none)";
      if ((deviceidx < 0) || (deviceidx >= BACnetData.Devices.Count)) return false;

      IPEndPoint remoteEP = BACnetData.Devices[deviceidx].ServerEP;
      if (remoteEP == null) return false;

      if (property == null) return false;

      //uint instance = BACnetData.Devices[deviceidx].Instance;

      Byte[] sendBytes = new Byte[50];
      Byte[] recvBytes = new Byte[512];
      uint len;

      // BVLL
      sendBytes[0] = BACnetEnums.BACNET_BVLC_TYPE_BIP;
      sendBytes[1] = BACnetEnums.BACNET_UNICAST_NPDU;
      sendBytes[2] = 0x00;
      sendBytes[3] = 0x00;  // BVLL Length, fix later (24?)

      // NPDU
      sendBytes[4] = BACnetEnums.BACNET_PROTOCOL_VERSION;
      if (BACnetData.Devices[deviceidx].SourceLength == 0)
        sendBytes[5] = 0x04;  // Control flags, no destination address
      else
        sendBytes[5] = 0x24;  // Control flags, with broadcast or destination address

      len = 6;
      if (BACnetData.Devices[deviceidx].SourceLength > 0)
      {
        // Get the (MSTP) Network number (2001)
        //sendBytes[6] = 0x07;  // Destination network address (2001)
        //sendBytes[7] = 0xD1;
        byte[] temp2 = new byte[2];
        temp2 = BitConverter.GetBytes(BACnetData.Devices[deviceidx].Network);
        sendBytes[len++] = temp2[1];
        sendBytes[len++] = temp2[0];

        // Get the MAC address (0x0D)
        //sendBytes[8] = 0x01;  // MAC address length
        //sendBytes[9] = 0x0D;  // Destination MAC layer address
        byte[] temp4 = new byte[4];
        temp4 = BitConverter.GetBytes(BACnetData.Devices[deviceidx].MACAddress);

        sendBytes[len++] = 0x01;  // MAC address length - adjust for other lengths ...
        sendBytes[len++] = temp4[0];
        sendBytes[len++] = 0xFF;  // Hop count = 255
      }

      // APDU
      sendBytes[len++] = 0x00;  // Control flags
      sendBytes[len++] = 0x05;  // Max APDU length (1476)

      // Create invoke counter
      sendBytes[len++] = (byte)(InvokeCounter);
      InvokeCounter = ((InvokeCounter + 1) & 0xFF);

      sendBytes[len++] = 0x0C;  // Service Choice: Read Property request

      // Fix the BVLL length
      sendBytes[3] = (byte)0;

      // Create the timer (we could use a blocking recvFrom instead ...)
      Timer ReadPropTimer = new Timer();
      try
      {
        int Count = 0;
        using (ReadPropTimer)
        {
          ReadPropTimer.Tick += new EventHandler(Timer_Tick);

          while (Count < 3)
          {
            SendUDP.EnableBroadcast = false;
            SendUDP.Send(sendBytes, (int)len, remoteEP);

            // Start the timer
            TimerDone = false;
            ReadPropTimer.Interval = 400;  // 100 ms
            ReadPropTimer.Start();
            while (!TimerDone)
            {
              // Wait for Confirmed Response
              Application.DoEvents();

              if (SendUDP.Client.Available > 0)
              {
                //recvBytes = SendUDP.Receive(ref RemoteEP);
                recvBytes = SendUDP.Receive(ref remoteEP);

                int APDUOffset = NPDU.Parse(recvBytes, 4); // BVLL is always 4 bytes

                // Check for APDU response 
                // 0x - Confirmed Request 
                // 1x - Un-Confirmed Request
                // 2x - Simple ACK
                // 3x - Complex ACK
                // 4x - Segment ACK
                // 5x - Error
                // 6x - Reject
                // 7x - Abort
                if (recvBytes[APDUOffset] == 0x30)
                {
                  // Verify the Invoke ID is the same
                  byte ic = (byte)(InvokeCounter == 0 ? 255 : InvokeCounter - 1);
                  if (ic == recvBytes[APDUOffset + 1])
                  {
                    APDU.ParseProperty(ref recvBytes, APDUOffset, property);
                    return true;  // This will still execute the finally
                  }
                  //else
                  //{
                  //  MessageBox.Show("Invoke Counter Error");
                  //  return false;
                  //}
                }
              }
            }
            Count++;
            BACnetData.PacketRetryCount++;
            ReadPropTimer.Stop(); // We'll start it over at the top of the loop
          }
          return false;  // This will still execute the finally
        }
      }
      finally
      {
        ReadPropTimer.Stop();
      }
    }

  }

}