using System.Collections.Generic;
using System.Net;

namespace BACnet
{
    /// <summary>
    /// BACnet Device object properties.
    /// </summary>
    /// <remarks>See <a href="http://www.bacnet.org/Bibliography/ES-7-96/ES-7-96.htm" /></remarks>
    public class BACnetDevice
    {
        public IPEndPoint IpAddress;
        public uint InstanceNumber;
        public string ObjectName;
        public string SystemStatus;
        public string VendorName;
        public int VendorIdentifier;
        public string ModelName;
        public string FirmwareRevision;
        public string ApplicationSoftwareVersion;
        public int ProtocolVersion;
        public int ProtocolRevision;
        public byte SourceLength;
        public int Network;
        public List<string> DeviceObjects;

        public BACnetDevice()
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
        }

        public BACnetDevice(IPEndPoint IpAddress, uint InstanceNumber)
        {
            this.IpAddress = IpAddress;
            this.Network = 0;
            this.InstanceNumber = InstanceNumber;
            this.VendorIdentifier = 0;
            this.SourceLength = 0;
        }

        public BACnetDevice(IPEndPoint IpAddress, int Network, uint InstanceNumber, int VendorIdentifier, byte SourceLength)
        {
            this.IpAddress = IpAddress;
            this.Network = Network;
            this.InstanceNumber = InstanceNumber;
            this.VendorIdentifier = VendorIdentifier;
            this.SourceLength = SourceLength;
        }
    }

    public class BACnetDeviceWithBBMD : BACnetDevice
    {
        public bool SupportsFdRegistration;
    }
}
