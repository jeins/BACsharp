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

using System;

namespace ConnectTools.BACnet.Properties
{
    public static class Npdu
    {
        private static byte pduControl;
        private static byte dlen;
        private static byte[] dadr;
        public static ushort Snet;
        public static byte Slen;
        private static byte[] sadr;
        private static byte messageType;
        private static ushort vendorId;
        public static uint SAddress;
        private static uint dAddress;
        private static int HopCount;
        private static int Dnet;

        private static void Clear()
        {
            pduControl = 0;
            Dnet = 0;
            dlen = 0;
            dadr = null;
            Snet = 0;
            Slen = 0;
            sadr = null;
            HopCount = 0;
            messageType = 0;
            vendorId = 0;
            dAddress = 0;
            SAddress = 0;
        }

        /// <summary>
        ///     Parses the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static int Parse(byte[] bytes, int offset)
        {
            var len = offset; // 4
            byte[] temp;
            Clear();
            if (bytes[len++] != 0x01) return 0;
            pduControl = bytes[len++]; // 5
            if ((pduControl & 0x20) > 0)
            {
                temp = new byte[2];
                temp[1] = bytes[len++];
                temp[0] = bytes[len++];
                Dnet = BitConverter.ToUInt16(temp, 0);
                dlen = bytes[len++];
                if (dlen == 1)
                {
                    dadr = new byte[1];
                    dadr[0] = bytes[len++];
                    dAddress = dadr[0];
                }
                if (dlen == 2)
                {
                    dadr[1] = bytes[len++];
                    dadr[0] = bytes[len++];
                    dAddress = BitConverter.ToUInt16(dadr, 0);
                }
                if (dlen == 4)
                {
                    dadr[3] = bytes[len++];
                    dadr[2] = bytes[len++];
                    dadr[1] = bytes[len++];
                    dadr[0] = bytes[len++];
                    dAddress = BitConverter.ToUInt32(dadr, 0);
                }
            }
            else
                dlen = 0;

            if ((pduControl & 0x08) > 0)
            {
                temp = new byte[2];
                temp[1] = bytes[len++];
                temp[0] = bytes[len++];
                Snet = BitConverter.ToUInt16(temp, 0);
                Slen = bytes[len++];
                if (Slen == 1)
                {
                    sadr = new byte[1];
                    sadr[0] = bytes[len++];
                    SAddress = sadr[0];
                }
                if (Slen == 2)
                {
                    sadr = new byte[2];
                    sadr[1] = bytes[len++];
                    sadr[0] = bytes[len++];
                    SAddress = BitConverter.ToUInt16(sadr, 0);
                }
                if (Slen == 4)
                {
                    sadr = new byte[4];
                    sadr[3] = bytes[len++];
                    sadr[2] = bytes[len++];
                    sadr[1] = bytes[len++];
                    sadr[0] = bytes[len++];
                    SAddress = BitConverter.ToUInt32(sadr, 0);
                }
            }
            else
                Slen = 0;

            if ((pduControl & 0x20) > 0)
            {
                HopCount = bytes[len++];
            }

            return len;
        }
    }
}