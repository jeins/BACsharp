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
            if (!(GetObjectName (ref device))) {return false;}
            if (!(GetVendorName (ref device))) { /*not required*/ }
            if (!(GetApplicationSoftwareVersion(ref device))) { /*not required*/ }
            if (!(GetModelName(ref device))) { return false; }
            if (!(GetFirmwareRevision(ref device))) { return false; }

            return false;
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
            //TODO: implement
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
    
    }

}





