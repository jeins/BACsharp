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

namespace ConnectTools.BACnet.Properties
{
    /// <summary>
    ///     Provides BACnetTag Routines.
    /// </summary>
    public static class BacnetTag
    {
        /// <summary>
        ///     Tags the number.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public static byte TagNumber(byte tag)
        {
            var x = (tag >> 4) & 0x0F;
            return (byte) x;
        }

        public static byte Class(byte tag)
        {
            var x = (tag >> 3) & 0x01;
            return (byte) x;
        }

        public static byte LenValType(byte tag)
        {
            var x = tag & 0x07;
            return (byte) x;
        }
    }
}