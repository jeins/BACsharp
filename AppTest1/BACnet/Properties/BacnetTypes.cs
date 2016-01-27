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

using System.Net;

namespace ConnectTools.BACnet.Properties
{
    /// <summary>
    ///     the Bdt Entry
    /// </summary>
    public class BdtEntry
    {
        public BdtEntry()
        {
            var temp = new IPAddress(0);
            MacAddress = new IPEndPoint(temp, 0);
            Mask = new IPAddress(0);
        }

        public IPEndPoint MacAddress { get; set; }
        public IPAddress Mask { get; set; }
    }


    /// <summary>
    ///     the Fdt Entry
    /// </summary>
    public class FdtEntry
    {
        public FdtEntry()
        {
            var temp = new IPAddress(0);
            MacAddress = new IPEndPoint(temp, 0);
            TimeToLive = 0;
            TimeRemaining = 0;
        }

        public IPEndPoint MacAddress { get; set; }
        public ushort TimeToLive { get; set; }
        public ushort TimeRemaining { get; set; }
    }
}