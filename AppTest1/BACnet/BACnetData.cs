using ConnectTools.BACnet.Properties;

namespace ConnectTools.BACnet
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides BACnetData functionality.
    /// </summary>
    public static class BaCnetData
    {
        public static List<Device> Devices;   // A list of BACnet devices after the WhoIs
        public static int DeviceIndex;        // The current BACnet device selected
        public static UInt32 PacketRetryCount;
    }
}