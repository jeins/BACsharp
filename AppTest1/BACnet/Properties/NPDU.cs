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
    using System;

    public static class Npdu
    {
        private static byte _pduControl;
        private static byte _dlen;
        private static byte[] _dadr;
        public static ushort Snet;
        public static byte Slen;
        private static byte[] _sadr;
        private static byte _messageType;
        private static ushort _vendorId;
        public static uint SAddress;
        private static uint _dAddress;
        private static int HopCount;
        private static int Dnet;

        private static void Clear()
        {
            _pduControl = 0;
            Dnet = 0;
            _dlen = 0;
            _dadr = null;
            Snet = 0;
            Slen = 0;
            _sadr = null;
            HopCount = 0;
            _messageType = 0;
            _vendorId = 0;
            _dAddress = 0;
            SAddress = 0;
        }

        /// <summary>
        /// Parses the specified bytes.
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
            _pduControl = bytes[len++]; // 5
            if ((_pduControl & 0x20) > 0)
            {
                temp = new byte[2];
                temp[1] = bytes[len++];
                temp[0] = bytes[len++];
                Dnet = BitConverter.ToUInt16(temp, 0);
                _dlen = bytes[len++];
                if (_dlen == 1)
                {
                    _dadr = new byte[1];
                    _dadr[0] = bytes[len++];
                    _dAddress = _dadr[0];
                }
                if (_dlen == 2)
                {
                    _dadr[1] = bytes[len++];
                    _dadr[0] = bytes[len++];
                    _dAddress = BitConverter.ToUInt16(_dadr, 0);
                }
                if (_dlen == 4)
                {
                    _dadr[3] = bytes[len++];
                    _dadr[2] = bytes[len++];
                    _dadr[1] = bytes[len++];
                    _dadr[0] = bytes[len++];
                    _dAddress = BitConverter.ToUInt32(_dadr, 0);
                }
            }
            else
                _dlen = 0;

            if ((_pduControl & 0x08) > 0)
            {
                temp = new byte[2];
                temp[1] = bytes[len++];
                temp[0] = bytes[len++];
                Snet = BitConverter.ToUInt16(temp, 0);
                Slen = bytes[len++];
                if (Slen == 1)
                {
                    _sadr = new byte[1];
                    _sadr[0] = bytes[len++];
                    SAddress = _sadr[0];
                }
                if (Slen == 2)
                {
                    _sadr = new byte[2];
                    _sadr[1] = bytes[len++];
                    _sadr[0] = bytes[len++];
                    SAddress = BitConverter.ToUInt16(_sadr, 0);
                }
                if (Slen == 4)
                {
                    _sadr = new byte[4];
                    _sadr[3] = bytes[len++];
                    _sadr[2] = bytes[len++];
                    _sadr[1] = bytes[len++];
                    _sadr[0] = bytes[len++];
                    SAddress = BitConverter.ToUInt32(_sadr, 0);
                }
            }
            else
                Slen = 0;

            if ((_pduControl & 0x20) > 0)
            {
                HopCount = bytes[len++];
            }

            return len;
        }
    }
}