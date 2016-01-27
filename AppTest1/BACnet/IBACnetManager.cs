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

using System.Collections.Generic;
using System.Net;

namespace ConnectTools.BACnet
{
    public interface IBaCnetManager
    {
        /// <summary>
        ///     Find all BACnet devices.
        /// </summary>
        /// <remarks>
        ///     BACnet devices should return an I-Am when requested by a Who-Is (with no device instance range).
        ///     Use unicast Who-Is service as we're not registered at a BBMD in the network.
        ///     Reading the required Device properties can be accomplished with a single ReadPropertyMultiple request.
        /// </remarks>
        /// <returns>A list of all found BACnet IP devices.</returns>
        List<BaCnetDevice> FindBaCnetDevices();

        /// <summary>
        ///     Finds the device properties.
        /// </summary>
        /// <returns></returns>
        bool FindDeviceProperties(ref BaCnetDevice device);

        /// <summary>
        ///     Finds the device objects.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        bool FindDeviceObjects(ref BaCnetDevice device);

        /// <summary>
        ///     Determines whether there is a BACnet device at the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>The BACnet Device Instance Number or 4194303 (BACnet "null") if not a BACnet device.</returns>
        int GetBaCnetDeviceInstanceNumber(IPEndPoint ipAddress);

        /// <summary>
        ///     Determines whether the BBMD is enabled.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet device.</param>
        /// <returns>True if BBMD is enabled, false otherwise.</returns>
        bool IsBbmdEnabled(IPEndPoint ipAddress);

        /// <summary>
        ///     Determines whether the BBMD option "Foreign Device Registration" is enabled on a given BACnet BBMD.
        /// </summary>
        /// <param name="ipAddress">The IP address of a BACnet BBMD.</param>
        /// <returns>True if FD registration is enabled, false otherwise.</returns>
        bool IsFdRegistrationSupported(IPEndPoint ipAddress);
    }
}