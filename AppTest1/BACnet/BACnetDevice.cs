using System.Collections.Generic;
using System.Net;

namespace ConnectTools.BACnet
{
    public class BaCnetDevice
    {
        public string ApplicationSoftwareVersion;

        public List<string> DeviceObjects;
        public string FirmwareRevision;
        public readonly uint InstanceNumber;
        public readonly IPEndPoint IpAddress;
        public bool IsBbmdActive;
        public string MobileUri;
        public string ModelName;
        public int Network;
        public string ObjectName;
        public int ProtocolRevision;
        public int ProtocolVersion;
        public byte SourceLength;
        public string SystemStatus;
        public string Uri;
        public int VendorIdentifier;
        public string VendorName;

        public BaCnetDevice()
        {
            InstanceNumber = 0;
            ObjectName = "unknown";
            SystemStatus = "unknown";
            VendorName = "unknown";
            VendorIdentifier = 0;
            ModelName = "unknown";
            FirmwareRevision = "unknown";
            ApplicationSoftwareVersion = "unknown";
            ProtocolVersion = 0;
            ProtocolRevision = 0;
            SourceLength = 0;
            Network = 65535;
            DeviceObjects = null;
            IsBbmdActive = false;
            Uri = "unknown";
            MobileUri = "unknown";
        }

        public BaCnetDevice(IPEndPoint ipAddress, uint instanceNumber)
        {
            this.IpAddress = ipAddress;
            Network = 0;
            this.InstanceNumber = instanceNumber;
            VendorIdentifier = 0;
            SourceLength = 0;
        }

        public BaCnetDevice(IPEndPoint ipAddress, int network, uint instanceNumber, int vendorIdentifier,
            byte sourceLength)
        {
            this.IpAddress = ipAddress;
            this.Network = network;
            this.InstanceNumber = instanceNumber;
            this.VendorIdentifier = vendorIdentifier;
            this.SourceLength = sourceLength;
        }
    }

    public class BaCnetDeviceWithBbmd : BaCnetDevice
    {
        public bool SupportsFdRegistration;
    }
}