using System;
using System.Collections.Generic;
using System.Net;
using AppTest1.BACnet;
using BACnet;

namespace BACnet
{
    //-----------------------------------------------------------------------------------------------
    // The Stack
    public class Service : IBACnetScout
    {
        public UInt16 UdpPort { get; set; }
        private IBACnetStack BACStack = null;

        public Service(UInt16 Port)
        {
            UdpPort = Port;
            BACStack = new BACnetStack();
        }

        public List<BACnetIpDevice> FindBACnetDevices()
        {
            List<Device> FoundDevices = BACStack.GetDevices(2000);
            List<BACnetIpDevice> FoundIpBacnetDevices = new List<BACnetIpDevice>();
            for (UInt16 i = 0; i < FoundDevices.Count; i++)
            {
                FoundIpBacnetDevices.Add(new BACnetIpDevice(FoundDevices[i].ServerEP, FoundDevices[i].Network,
                    (uint) FoundDevices[i].Instance, FoundDevices[i].VendorID, FoundDevices[i].SourceLength));
            }
            return FoundIpBacnetDevices;
        }

        public bool FindDeviceProperties(ref BACnetIpDevice device)
        {
            bool successful = true;

            if (!(GetObjectName (ref device))) { successful = false;}
            if (!(GetVendorName(ref device))) { successful = false; }
            if (!(GetApplicationSoftwareVersion(ref device))) { successful = false; }
            if (!(GetModelName(ref device))) { successful = false; }
            if (!(GetFirmwareRevision(ref device))) { successful = false; }

            if (!(GetVendorIdentifier(ref device))) { successful = false; }
            if (!(GetProtocolRevision(ref device))) { successful = false; }
            if (!(GetProtocolVersion(ref device))) { successful = false; }

            if (!(GetSystemStatus(ref device))) { successful = false; }

            return successful;
        }

        public bool FindDeviceObjects(ref BACnetIpDevice device)
        {
            bool successful = true;

            List<string> deviceObjects = new List<string>();

            Device recipient = new Device();
            recipient.ServerEP = device.IpAddress;
            recipient.Instance = device.InstanceNumber;
            recipient.Network = device.Network;
            recipient.SourceLength = device.SourceLength;

            Property property = new Property();
            property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_ENUMERATED;
            if (!BACStack.SendReadProperty(
              recipient,
              0, // Array[0] is Object Count
              BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_DEVICE,
              BACnetEnums.BACNET_PROPERTY_ID.PROP_OBJECT_LIST,
              property))
            {
                successful = false;
            }

            if (property.Tag != BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_UNSIGNED_INT)
            {
                successful = false;
            }

            int i, tries;
            uint total = property.ValueUInt;
            if (total > 0) for (i = 1; i <= total; i++)
                {
                    // Read through Array[x] up to Object Count
                    // Need to try the read again if it times out
                    tries = 0;
                    while (tries < 5)
                    {
                        tries++;
                        if (BACStack.SendReadProperty(
                          recipient,
                          i, // each array index
                          BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_DEVICE,
                          BACnetEnums.BACNET_PROPERTY_ID.PROP_OBJECT_LIST,
                          property))
                        {
                            tries = 5; // Next object
                            string s;
                            if (property.Tag != BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_OBJECT_ID)
                                tries = 5; // continue;
                            switch (property.ValueObjectType)
                            {
                                case BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_DEVICE: s = "Device"; break;
                                case BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_ANALOG_INPUT: s = "Analog Input"; break;
                                case BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_ANALOG_OUTPUT: s = "Analog Output"; break;
                                case BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_ANALOG_VALUE: s = "Analog value"; break;
                                case BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_BINARY_INPUT: s = "Binary Input"; break;
                                case BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_BINARY_OUTPUT: s = "Binary Output"; break;
                                case BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_BINARY_VALUE: s = "Binary value"; break;
                                case BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_FILE: s = "File"; break;
                                default: s = "Other"; break;
                            }
                            s = s + "  " + property.ValueObjectInstance.ToString();
                            deviceObjects.Add(s);
                        }
                    }
                }
            device.DeviceObjects = deviceObjects;
            return successful;
        }

        public List<BACnetDeviceWithBBMD> FindBACnetBBMDs(IPAddress ip)
        {
            List<BACnetDeviceWithBBMD> FoundIpBacnetDevices = new List<BACnetDeviceWithBBMD>();
            //TODO: implement
            return FoundIpBacnetDevices;
        }

        public int IsBACnetIpDevice(IPEndPoint IpAddress)
        {
            //TODO: implement
            return -1;

        }

        public bool IsBbmdEnabled(IPEndPoint IpAddress)
        {
            //TODO: implement
            return false;

        }

        public bool IsFdRegistrationSupported(IPEndPoint IpAddress)
        {
            if (BACStack.SendReadFdt(IpAddress))
            {
                if (BVLC.BVLC_Function_ResultCode == 0)
                    return true;
                else
                    return false;
            }            
            return false;

        }

        private bool GetObjectName(ref BACnetIpDevice device)
        {
            return GetStringPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_OBJECT_NAME,
                ref device.ObjectName);
        }
        private bool GetVendorName(ref BACnetIpDevice device)
        {
            return GetStringPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_VENDOR_NAME,
                ref device.VendorName);
        }
        private bool GetModelName(ref BACnetIpDevice device)
        {
            return GetStringPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_MODEL_NAME,
                ref device.ModelName);
        }

        private bool GetApplicationSoftwareVersion(ref BACnetIpDevice device)
        {
            return GetStringPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_APPLICATION_SOFTWARE_VERSION,
                ref device.ApplicationSoftwareVersion);
        }

        private bool GetFirmwareRevision(ref BACnetIpDevice device)
        {
            return GetStringPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_FIRMWARE_REVISION,
                ref device.FirmwareRevision);
        }

        private bool GetVendorIdentifier(ref BACnetIpDevice device)
        {
            uint value = 0; 
            if ( GetUnsignedPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_VENDOR_IDENTIFIER,
                ref value) )
            {
                device.VendorIdentifier = (int)value;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private bool GetProtocolVersion(ref BACnetIpDevice device)
        {
            uint value = 0;
            if (GetUnsignedPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_PROTOCOL_VERSION,
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

        private bool GetProtocolRevision(ref BACnetIpDevice device)
        {
            uint value = 0;
            if (GetUnsignedPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_PROTOCOL_REVISION,
                ref value))
            {
                device.ProtocolRevision = (int)value;
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool GetSystemStatus(ref BACnetIpDevice device)
        {
            string[] BacnetDeviceSytemStatusString =
            {
                "operational", "operational-read-only", "download-required",
                "download-in-progress", "non-operational", "backup-in-progress"
            };
            uint value = 0;
            if (GetEnumeratedPropertyValue(ref device, BACnetEnums.BACNET_PROPERTY_ID.PROP_PROTOCOL_REVISION,
                ref value))
            {
                if ( value < BacnetDeviceSytemStatusString.Length )
                    device.SystemStatus = BacnetDeviceSytemStatusString[value];
                else
                    device.SystemStatus = "SystemStatus " + value.ToString();
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool GetStringPropertyValue(ref BACnetIpDevice device, BACnetEnums.BACNET_PROPERTY_ID propId, ref string value)
        {
            Property property = new Property();
            property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_ENUMERATED;

            Device recipient = new Device();
            recipient.ServerEP = device.IpAddress;
            recipient.Instance = device.InstanceNumber;
            recipient.Network = device.Network;
            recipient.SourceLength = device.SourceLength;

            if (!BACStack.SendReadProperty(
                recipient,
                -1, // Array[0] is Object Count
                BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_DEVICE,
                propId,
                property))
            {
                return false;
            }

            if (property.Tag != BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_CHARACTER_STRING)
            {
                return false;
            }
            value = property.ValueString;
            return true;
        }

        private bool GetUnsignedPropertyValue(ref BACnetIpDevice device, BACnetEnums.BACNET_PROPERTY_ID propId, ref uint value)
        {
            Property property = new Property();
            property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_ENUMERATED;

            Device recipient = new Device();
            recipient.ServerEP = device.IpAddress;
            recipient.Instance = device.InstanceNumber;
            recipient.Network = device.Network;
            recipient.SourceLength = device.SourceLength;

            if (!BACStack.SendReadProperty(
                recipient,
                -1, // Array[0] is Object Count
                BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_DEVICE,
                propId,
                property))
            {
                return false;
            }

            if (property.Tag != BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_UNSIGNED_INT)
            {
                return false;
            }
            value = property.ValueUInt;
            return true;
        }
        
        private bool GetEnumeratedPropertyValue(ref BACnetIpDevice device, BACnetEnums.BACNET_PROPERTY_ID propId, ref uint value)
        {
            Property property = new Property();
            property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_NULL;

            Device recipient = new Device();
            recipient.ServerEP = device.IpAddress;
            recipient.Instance = device.InstanceNumber;
            recipient.Network = device.Network;
            recipient.SourceLength = device.SourceLength;

            if (!BACStack.SendReadProperty(
                recipient,
                -1, // Array[0] is Object Count
                BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_DEVICE,
                propId,
                property))
            {
                return false;
            }

            if (property.Tag != BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_ENUMERATED)
            {
                return false;
            }
            value = property.ValueEnum;
            return true;
        }
    
    }

}





