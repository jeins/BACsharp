using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppTest1.BACnet
{
    /// <summary>
    /// BACnet Device object properties.
    /// </summary>
    /// <remarks>See <a href="http://www.bacnet.org/Bibliography/ES-7-96/ES-7-96.htm" /></remarks>
    public class BACnetDevice
    {
        public struct ObjectIdentifier
        {
            public string ObjectType;

            public int InstanceNumber;
        }

        public string ObjectName;

        public string SystemStatus;

        public string VendorName;

        public int VendorIdentifier;

        public string ModelName;

        public string FirmwareRevision;

        public string ApplicationSoftwareVersion;

        public int ProtocolVersion;

        public int ProtocolRevision;
    }

    public class BACnetIpDevice : BACnetDevice
    {
        public string IpAddress;
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
        /// <param name="port">The UDP port used for BACnet communication.</param>
        /// <remarks>
        /// BACnet devices should return an I-Am when requested by a Who-Is (with no device instance range).
        /// Use unicast Who-Is service as we're not registered at a BBMD in the network.
        /// Reading the required Device properties can be accomplished with a single ReadPropertyMultiple request.
        /// </remarks>
        /// <returns>A list of all found BACnet IP devices.</returns>
        List<BACnetIpDevice> FindBACnetDevices(string ipNetwork, int port = 47808);


        /// <summary>
        /// Find all BACnet devices with enabled BBMD functionality.
        /// </summary>
        /// <param name="ipNetwork">The IP network address in CIDR format.</param>
        /// <param name="port">The UDP port used for BACnet communication.</param>
        /// <remarks>
        /// Devices with enabled BBMD should ACK ReadBroadcastDistributionTable requests. 
        /// BBMDs with enabled FD registration should ACK ReadForeignDeviceTable requests.
        /// </remarks>
        /// <returns></returns>
        List<BACnetDeviceWithBBMD> FindBACnetBBMDs(string ipNetwork, int port = 47808);

        /// <summary>
        /// Determines whether there is a BACnet device at the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="port">The BACnet port.</param>
        /// <returns>The BACnet Device Instance Number or 4194303 (BACnet "null") if not a BACnet device.</returns>
        int IsBACnetIpDevice(string ipAddress, int port = 47808);

        /// <summary>
        /// Determines whether the BBMD is enabled.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet device.</param>
        /// <param name="port">The BACnet port.</param>
        /// <returns>True if BBMD is enabled, false otherwise.</returns>
        bool IsBbmdEnabled(string ipAddress, int port = 47808);

        /// <summary>
        /// Determines whether the BBMD option "Foreign Device Registration" is enabled on a given BACnet BBMD.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet BBMD.</param>
        /// <param name="port">The BACnet port.</param>
        /// <returns>True if FD registration is enabled, false otherwise.</returns>
        bool IsFdRegistrationSupported(string ipAddress, int port = 47808);

    }
}
