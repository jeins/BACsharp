using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BACnet;

namespace AppTest1.BACnet
{
    
    /// <summary>
    /// Scouts networks for BACnet information.
    /// </summary>
    /// <remarks>See <a hre="https://confluence.kieback-peter.de/pages/viewpage.action?pageId=17694768" /> for the technical concept.</remarks>
    public interface IBACnetManager
    {
        /// <summary>
        /// Find all BACnet devices.
        /// </summary>
        /// <remarks>
        /// BACnet devices should return an I-Am when requested by a Who-Is (with no device instance range).
        /// Use unicast Who-Is service as we're not registered at a BBMD in the network.
        /// Reading the required Device properties can be accomplished with a single ReadPropertyMultiple request.
        /// </remarks>
        /// <returns>A list of all found BACnet IP devices.</returns>
        List<BACnetDevice> FindBACnetDevices();

        /// <summary>
        /// Finds the device properties.
        /// </summary>
        /// <returns></returns>
        bool FindDeviceProperties(ref BACnetDevice device);

        /// <summary>
        /// Finds the device objects.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        bool FindDeviceObjects(ref BACnetDevice device);

        /// <summary>
        /// Find all BACnet devices with enabled BBMD functionality.
        /// </summary>
        /// <param name="ipNetwork">The IP network address in CIDR format.</param>
        /// <remarks>
        /// Devices with enabled BBMD should ACK ReadBroadcastDistributionTable requests. 
        /// BBMDs with enabled FD registration should ACK ReadForeignDeviceTable requests.
        /// </remarks>
        /// <returns></returns>
        List<BACnetDeviceWithBBMD> FindBACnetBBMDs(IPAddress ipNetwork);

        /// <summary>
        /// Determines whether there is a BACnet device at the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>The BACnet Device Instance Number or 4194303 (BACnet "null") if not a BACnet device.</returns>
        int GetBACnetDeviceInstanceNumber(IPEndPoint ipAddress);

        /// <summary>
        /// Determines whether the BBMD is enabled.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet device.</param>
        /// <returns>True if BBMD is enabled, false otherwise.</returns>
        bool IsBbmdEnabled(IPEndPoint ipAddress);

        /// <summary>
        /// Determines whether the BBMD option "Foreign Device Registration" is enabled on a given BACnet BBMD.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet BBMD.</param>
        /// <returns>True if FD registration is enabled, false otherwise.</returns>
        bool IsFdRegistrationSupported(IPEndPoint ipAddress);

    }
}
