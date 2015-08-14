using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AppTest1.BACnet
{
    /// <summary>
    /// BACnet Device object properties.
    /// </summary>
    /// <remarks>See <a href="http://www.bacnet.org/Bibliography/ES-7-96/ES-7-96.htm" /></remarks>
    public class BACnetDevice
    {
        public uint InstanceNumber;
        public string ObjectName;
        public int SystemStatus;
        public string VendorName;
        public int VendorIdentifier;
        public string ModelName;
        public string FirmwareRevision;
        public string ApplicationSoftwareVersion;
        public int ProtocolVersion;
        public int ProtocolRevision;
        public byte SourceLength;
        public int Network;

        public BACnetDevice()
        {
            InstanceNumber = 0;
            ObjectName = "unknown";
            SystemStatus = -1;
            VendorName = "unknown";
            VendorIdentifier = 0;
            ModelName = "unknown";
            FirmwareRevision = "unknown";
            ApplicationSoftwareVersion = "unknown";
            ProtocolVersion = 0;
            ProtocolRevision = 0;
            SourceLength = 0;
            Network = 65535;
        }
    }

    public class BACnetIpDevice : BACnetDevice
    {
        public IPEndPoint IpAddress;

        public BACnetIpDevice()
        {
        }

        public BACnetIpDevice(IPEndPoint IpAddress, int Network, uint InstanceNumber, int VendorIdentifier, byte SourceLength )
        {
            this.IpAddress = IpAddress;
            this.Network = Network;
            this.InstanceNumber = InstanceNumber;
            this.VendorIdentifier = VendorIdentifier;
            this.SourceLength = SourceLength;
        }

    }


    public class BACnetDeviceWithBBMD : BACnetIpDevice
    {
        public bool SupportsFdRegistration;
    }

    /// <summary>
    /// Scouts networks for BACnet information.
    /// </summary>
    /// <remarks>See <a hre="https://confluence.kieback-peter.de/pages/viewpage.action?pageId=17694768" /> for the technical concept.</remarks>
    public interface IBACnetScout
    {
        /// <summary>
        /// Find all BACnet devices in a given IP network.
        /// </summary>
        /// <param name="ipNetwork">The IP network address in CIDR format.</param>
        /// <remarks>
        /// BACnet devices should return an I-Am when requested by a Who-Is (with no device instance range).
        /// Use unicast Who-Is service as we're not registered at a BBMD in the network.
        /// Reading the required Device properties can be accomplished with a single ReadPropertyMultiple request.
        /// </remarks>
        /// <returns>A list of all found BACnet IP devices.</returns>
        List<BACnetIpDevice> FindBACnetDevices(IPEndPoint IpAddress);

        /// <summary>
        /// Finds the device properties.
        /// </summary>
        /// <returns></returns>
        bool FindDeviceProperties(ref BACnetIpDevice device);

            /// <summary>
        /// Find all BACnet devices with enabled BBMD functionality.
        /// </summary>
        /// <param name="ipNetwork">The IP network address in CIDR format.</param>
        /// <remarks>
        /// Devices with enabled BBMD should ACK ReadBroadcastDistributionTable requests. 
        /// BBMDs with enabled FD registration should ACK ReadForeignDeviceTable requests.
        /// </remarks>
        /// <returns></returns>
        List<BACnetDeviceWithBBMD> FindBACnetBBMDs(IPAddress ip);

        /// <summary>
        /// Determines whether there is a BACnet device at the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>The BACnet Device Instance Number or 4194303 (BACnet "null") if not a BACnet device.</returns>
        int IsBACnetIpDevice(IPEndPoint IpAddress);

        /// <summary>
        /// Determines whether the BBMD is enabled.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet device.</param>
        /// <returns>True if BBMD is enabled, false otherwise.</returns>
        bool IsBbmdEnabled(IPEndPoint IpAddress);

        /// <summary>
        /// Determines whether the BBMD option "Foreign Device Registration" is enabled on a given BACnet BBMD.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet BBMD.</param>
        /// <returns>True if FD registration is enabled, false otherwise.</returns>
        bool IsFdRegistrationSupported(IPEndPoint IpAddress);

    }
}
