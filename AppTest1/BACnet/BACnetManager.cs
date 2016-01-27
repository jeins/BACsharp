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

using ConnectTools.BACnet.Properties;

namespace ConnectTools.BACnet
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Reflection;

    using log4net;

    public class BaCnetManager : IBaCnetManager
    {
        private readonly IBaCnetStack _bacStack;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Class provides functions to handle BACnet actions.
        /// </summary>
        /// <param name="localIpAddress">The host's IP address where BACnet communication should take place.</param>
        public BaCnetManager(IPAddress localIpAddress)
        {
            try
            {
                //IPAddress localIpAddress = System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[0];
                _bacStack = new BaCnetStack(localIpAddress);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error on initializing BACStack: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Find all BACnet devices.
        /// </summary>
        /// <returns>
        /// A list of all found BACnet IP devices.
        /// </returns>
        /// <remarks>
        /// BACnet devices should return an I-Am when requested by a Who-Is (with no device instance range).
        /// Use unicast Who-Is service as we're not registered at a BBMD in the network.
        /// Reading the required Device properties can be accomplished with a single ReadPropertyMultiple request.
        /// </remarks>
        public List<BaCnetDevice> FindBaCnetDevices()
        {
            var foundIpBacnetDevices = new List<BaCnetDevice>();

            try
            {
                var foundDevices = _bacStack.GetDevices(2000);

                for (ushort i = 0; i < foundDevices.Count; i++)
                {
                    foundIpBacnetDevices.Add(new BaCnetDevice(foundDevices[i].ServerEp, foundDevices[i].Network,
                        foundDevices[i].Instance, foundDevices[i].VendorId, foundDevices[i].SourceLength));
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error finding BACnet devices: {0}", ex.Message);
            }

            return foundIpBacnetDevices;
        }

        /// <summary>
        /// Finds the device properties.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public bool FindDeviceProperties(ref BaCnetDevice device)
        {
            var successful = GetObjectName(ref device);
            if (!GetApplicationSoftwareVersion(ref device)) { successful = false; }
            if (!GetModelName(ref device)) { successful = false; }
            if (!GetFirmwareRevision(ref device)) { successful = false; }
            if (!GetVendorName(ref device)) { successful = false; }
            if (!GetVendorIdentifier(ref device)) { successful = false; }
            if (!GetProtocolRevision(ref device)) { successful = false; }
            if (!GetProtocolVersion(ref device)) { successful = false; }

            if (!(GetSystemStatus(ref device))) { successful = false; }

            return successful;
        }

        /// <summary>
        /// Finds the device objects.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        public bool FindDeviceObjects(ref BaCnetDevice device)
        {
            var successful = true;

            var deviceObjects = new List<string>();

            var recipient = new Device
            {
                ServerEp = device.IpAddress,
                Instance = device.InstanceNumber,
                Network = device.Network,
                SourceLength = device.SourceLength
            };

            var property = new Property();
            property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagEnumerated;
            if (!_bacStack.SendReadProperty(
              recipient,
              0, // Array[0] is Object Count
              BacnetEnums.BacnetObjectType.ObjectDevice,
              BacnetEnums.BacnetPropertyId.PropObjectList,
              property))
            {
                successful = false;
            }

            if (property.Tag != BacnetEnums.BacnetApplicationTag.BacnetApplicationTagUnsignedInt)
            {
                successful = false;
            }

            int i;
            var total = property.ValueUInt;
            if (total > 0) for (i = 1; i <= total; i++)
                {
                    // Read through Array[x] up to Object Count
                    // Need to try the read again if it times out
                    var tries = 0;
                    while (tries < 5)
                    {
                        tries++;
                        if (!_bacStack.SendReadProperty(
                            recipient,
                            i,
                            // each array index
                            BacnetEnums.BacnetObjectType.ObjectDevice,
                            BacnetEnums.BacnetPropertyId.PropObjectList,
                            property))
                        {
                            continue;
                        }
                        tries = 5; // Next object
                        string s;
                        if (property.Tag != BacnetEnums.BacnetApplicationTag.BacnetApplicationTagObjectId)
                            tries = 5; // continue;
                        switch (property.ValueObjectType)
                        {
                            case BacnetEnums.BacnetObjectType.ObjectDevice: s = "Device"; break;
                            case BacnetEnums.BacnetObjectType.ObjectAnalogInput: s = "Analog Input"; break;
                            case BacnetEnums.BacnetObjectType.ObjectAnalogOutput: s = "Analog Output"; break;
                            case BacnetEnums.BacnetObjectType.ObjectAnalogValue: s = "Analog value"; break;
                            case BacnetEnums.BacnetObjectType.ObjectBinaryInput: s = "Binary Input"; break;
                            case BacnetEnums.BacnetObjectType.ObjectBinaryOutput: s = "Binary Output"; break;
                            case BacnetEnums.BacnetObjectType.ObjectBinaryValue: s = "Binary value"; break;
                            case BacnetEnums.BacnetObjectType.ObjectFile: s = "File"; break;
                            default: s = "Other"; break;
                        }
                        s = s + "  " + property.ValueObjectInstance.ToString();
                        deviceObjects.Add(s);
                    }
                }
            device.DeviceObjects = deviceObjects;
            return successful;
        }

        /// <summary>
        /// Find all BACnet devices with enabled BBMD functionality.
        /// </summary>
        /// <param name="ipNetwork">The IP network address in CIDR format.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>
        /// Devices with enabled BBMD should ACK ReadBroadcastDistributionTable requests.
        /// BBMDs with enabled FD registration should ACK ReadForeignDeviceTable requests.
        /// </remarks>
        public List<BaCnetDeviceWithBbmd> FindBacnetBbmds(IPAddress ipNetwork)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether there is a BACnet device at the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>
        /// The BACnet Device Instance Number or 4194303 (BACnet "null") if not a BACnet device.
        /// </returns>
        public int GetBaCnetDeviceInstanceNumber(IPEndPoint ipAddress)
        {
            var newDev = (_bacStack.UnicastWhoIsOnSingleIp(ipAddress, 1000));
            if (newDev == null)
            {
                return -1;
            }

            return (int)newDev.Instance;
        }

        /// <summary>
        /// Determines whether the BBMD is enabled.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet device.</param>
        /// <returns>
        /// True if BBMD is enabled, false otherwise.
        /// </returns>
        public bool IsBbmdEnabled(IPEndPoint ipAddress)
        {
            if (!_bacStack.SendReadBdt(ipAddress)) return false;

            return Bvlc.BvlcFunctionResultCode == 0;
        }

        /// <summary>
        /// Determines whether the BBMD option "Foreign Device Registration" is enabled on a given BACnet BBMD.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet BBMD.</param>
        /// <returns>
        /// True if FD registration is enabled, false otherwise.
        /// </returns>
        public bool IsFdRegistrationSupported(IPEndPoint ipAddress)
        {
            if (!_bacStack.SendReadFdt(ipAddress)) return false;

            return Bvlc.BvlcFunctionResultCode == 0;
        }

        private bool GetObjectName(ref BaCnetDevice device)
        {
            return GetStringPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropObjectName,
                ref device.ObjectName);
        }
        private bool GetVendorName(ref BaCnetDevice device)
        {
            return GetStringPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropVendorName,
                ref device.VendorName);
        }
        private bool GetModelName(ref BaCnetDevice device)
        {
            return GetStringPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropModelName,
                ref device.ModelName);
        }

        private bool GetApplicationSoftwareVersion(ref BaCnetDevice device)
        {
            return GetStringPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropApplicationSoftwareVersion,
                ref device.ApplicationSoftwareVersion);
        }

        private bool GetFirmwareRevision(ref BaCnetDevice device)
        {
            return GetStringPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropFirmwareRevision,
                ref device.FirmwareRevision);
        }

        private bool GetVendorIdentifier(ref BaCnetDevice device)
        {
            uint value = 0;
            if (GetUnsignedPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropVendorIdentifier,
                ref value))
            {
                device.VendorIdentifier = (int)value;
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool GetProtocolVersion(ref BaCnetDevice device)
        {
            uint value = 0;
            if (GetUnsignedPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropProtocolVersion,
                ref value))
            {
                device.ProtocolVersion = (int)value;
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool GetProtocolRevision(ref BaCnetDevice device)
        {
            uint value = 0;
            if (!GetUnsignedPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropProtocolRevision, ref value))
                return false;

            device.ProtocolRevision = (int)value;
            return true;
        }

        private bool GetSystemStatus(ref BaCnetDevice device)
        {
            string[] bacnetDeviceSytemStatusString =
            {
                "operational", "operational-read-only", "download-required",
                "download-in-progress", "non-operational", "backup-in-progress"
            };
            uint value = 0;
            if (GetEnumeratedPropertyValue(ref device, BacnetEnums.BacnetPropertyId.PropSystemStatus,
                ref value))
            {
                if (value < bacnetDeviceSytemStatusString.Length)
                    device.SystemStatus = bacnetDeviceSytemStatusString[value];
                else
                    device.SystemStatus = "SystemStatus " + value;
                return true;
            }

            device.SystemStatus = "Failed to get System Status " + value;
            return false;
        }

        private bool GetStringPropertyValue(ref BaCnetDevice device, BacnetEnums.BacnetPropertyId propId, ref string value)
        {
            var property = new Property { Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagEnumerated };

            var recipient = new Device
            {
                ServerEp = device.IpAddress,
                Instance = device.InstanceNumber,
                Network = device.Network,
                SourceLength = device.SourceLength
            };

            if (!_bacStack.SendReadProperty(
                recipient,
                -1, // Array[0] is Object Count
                BacnetEnums.BacnetObjectType.ObjectDevice,
                propId,
                property))
            {
                return false;
            }

            if (property.Tag != BacnetEnums.BacnetApplicationTag.BacnetApplicationTagCharacterString)
            {
                return false;
            }
            value = property.ValueString;
            return true;
        }

        private bool GetUnsignedPropertyValue(ref BaCnetDevice device, BacnetEnums.BacnetPropertyId propId, ref uint value)
        {
            var property = new Property { Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagEnumerated };

            var recipient = new Device
            {
                ServerEp = device.IpAddress,
                Instance = device.InstanceNumber,
                Network = device.Network,
                SourceLength = device.SourceLength
            };

            if (!_bacStack.SendReadProperty(
                recipient,
                -1, // Array[0] is Object Count
                BacnetEnums.BacnetObjectType.ObjectDevice,
                propId,
                property))
            {
                return false;
            }

            if (property.Tag != BacnetEnums.BacnetApplicationTag.BacnetApplicationTagUnsignedInt)
            {
                return false;
            }

            value = property.ValueUInt;
            return true;
        }

        private bool GetEnumeratedPropertyValue(ref BaCnetDevice device, BacnetEnums.BacnetPropertyId propId, ref uint value)
        {
            var property = new Property { Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagNull };

            var recipient = new Device
            {
                ServerEp = device.IpAddress,
                Instance = device.InstanceNumber,
                Network = device.Network,
                SourceLength = device.SourceLength
            };

            if (!_bacStack.SendReadProperty(
                recipient,
                -1, // Array[0] is Object Count
                BacnetEnums.BacnetObjectType.ObjectDevice,
                propId,
                property))
            {
                return false;
            }

            if (property.Tag != BacnetEnums.BacnetApplicationTag.BacnetApplicationTagEnumerated)
            {
                return false;
            }

            value = property.ValueEnum;
            return true;
        }
    }
}