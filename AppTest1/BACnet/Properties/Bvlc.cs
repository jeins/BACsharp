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

namespace ConnectTools.BACnet.Properties
{
    public static class Bvlc
    {
        public const int BacnetBvlcHeaderLen = 0x04;

        public const byte BacnetBvlcTypeBip = 0x81;

        public const int BacnetBvlcFuncResult = 0x00;
        private const int BacnetBvlcFuncWriteBdt = 0x01;
        public const int BacnetBvlcFuncReadBdt = 0x02;
        public const int BacnetBvlcFuncReadBdtAck = 0x03;
        private const int BacnetBvlcFuncForwNpdu = 0x04;
        private const int BacnetBvlcFuncRegisterFd = 0x05;
        public const int BacnetBvlcFuncReadFdt = 0x06;
        public const int BacnetBvlcFuncReadFdtAck = 0x07;
        private const int BacnetBvlcFuncDeleteFd = 0x08;
        private const int BacnetBvlcFuncDistributeBroadcastToNetwork = 0x09;
        public const int BacnetBvlcFuncUnicastNpdu = 0x0A;
        private const int BacnetBvlcFuncBroadcastNpdu = 0x0B;

        private static byte _bvlcType;
        public static byte BvlcFunction;
        private static ushort _bvlcLength;
        public static ushort BvlcFunctionResultCode;
        public static BdtEntry[] BvlcListOfBdtEntries;
        public static FdtEntry[] BvlcListOfFdtEntries;

        private static void Clear()
        {
            _bvlcType = 0;
            BvlcFunction = 0;
            _bvlcLength = 0;

            BvlcFunctionResultCode = 0;
            BvlcListOfBdtEntries = null;
            BvlcListOfFdtEntries = null;
        }

        /// <summary>
        ///     Fills the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="bvlcFunctionType">Type of the BVLC function.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static uint Fill(ref byte[] bytes, int bvlcFunctionType, uint offset)
        {
            bytes[offset + 0] = BacnetBvlcTypeBip;
            bytes[offset + 1] = (byte) bvlcFunctionType;
            bytes[offset + 2] = 0x00;
            bytes[offset + 3] = BacnetBvlcHeaderLen;

            return offset + BacnetBvlcHeaderLen;
        }

        /// <summary>
        ///     Parses the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static int Parse(byte[] bytes, int offset)
        {
            var len = offset;
            Clear();
            _bvlcType = bytes[len++];
            BvlcFunction = bytes[len++];

            var temp = new byte[2];
            temp[0] = bytes[len++];
            temp[1] = bytes[len++];

            _bvlcLength = (ushort) (temp[0]*255 + temp[1]);

            if (BacnetBvlcTypeBip != _bvlcType)
            {
                return 0;
            }

            switch (BvlcFunction)
            {
                case BacnetBvlcFuncResult:
                    return ParseResultCode(bytes, len);
                case BacnetBvlcFuncWriteBdt:
                case BacnetBvlcFuncReadBdtAck:
                    return ParseListOfBdtEntries(bytes, len);
                case BacnetBvlcFuncForwNpdu:
                    return ParseBacnetIpAddress(bytes, len);
                case BacnetBvlcFuncRegisterFd:
                    return ParseTimeToLive(bytes, len);
                case BacnetBvlcFuncReadFdtAck:
                    return ParseListOfFdtEntries(bytes, len);
                case BacnetBvlcFuncDeleteFd:
                    return ParseBacnetIpAddress(bytes, len);
                case BacnetBvlcFuncUnicastNpdu:
                case BacnetBvlcFuncBroadcastNpdu:
                case BacnetBvlcFuncDistributeBroadcastToNetwork:
                case BacnetBvlcFuncReadBdt:
                case BacnetBvlcFuncReadFdt:
                    return len;
                default:
                    return len;
            }
        }

        private static int ParseTimeToLive(IReadOnlyList<byte> bytes, int offset)
        {
            var len = offset;

            var timeToLive = new byte[2];
            timeToLive[1] = bytes[len++];
            timeToLive[0] = bytes[len++];

            return len;
        }

        private static int ParseBacnetIpAddress(IReadOnlyList<byte> bytes, int offset)
        {
            var len = offset;

            var ipAddr = new byte[4];
            ipAddr[0] = bytes[len++];
            ipAddr[1] = bytes[len++];
            ipAddr[2] = bytes[len++];
            ipAddr[3] = bytes[len++];

            var udpPort = new byte[2];
            udpPort[1] = bytes[len++];
            udpPort[0] = bytes[len++];

            return len;
        }

        private static int ParseResultCode(IReadOnlyList<byte> bytes, int offset)
        {
            var len = offset;
            var temp = new byte[2];
            temp[1] = bytes[len++];
            temp[0] = bytes[len++];
            BvlcFunctionResultCode = (ushort) (temp[1]*256 + temp[0]);

            return len;
        }

        private static int ParseListOfBdtEntries(IReadOnlyList<byte> bytes, int offset)
        {
            var len = offset;
            var bytesBdtEntries = _bvlcLength - offset;
            var numberOfBdtEntries = bytesBdtEntries/10;
            BvlcListOfBdtEntries = new BdtEntry[numberOfBdtEntries];

            for (var i = 0; i < numberOfBdtEntries; i++)
            {
                var ipAddr = new byte[4];
                ipAddr[0] = bytes[len++];
                ipAddr[1] = bytes[len++];
                ipAddr[2] = bytes[len++];
                ipAddr[3] = bytes[len++];
                var ip = new IPAddress(ipAddr);

                var udpPort = new byte[2];
                udpPort[1] = bytes[len++];
                udpPort[0] = bytes[len++];

                var ipMask = new byte[4];
                ipMask[3] = bytes[len++];
                ipMask[2] = bytes[len++];
                ipMask[1] = bytes[len++];
                ipMask[0] = bytes[len++];
                var mask = new IPAddress(ipMask);

                var point = new IPEndPoint(ip, udpPort[1]*256 + udpPort[0]);
                var bdtEntry = new BdtEntry
                {
                    MacAddress = point,
                    Mask = mask
                };


                BvlcListOfBdtEntries[i] = bdtEntry;
            }
            return len;
        }

        private static int ParseListOfFdtEntries(IReadOnlyList<byte> bytes, int offset)
        {
            var len = offset;
            var bytesFdtEntries = _bvlcLength - offset;
            var numberOfFdtEntries = bytesFdtEntries/10; // 10 Bytes per Entry
            BvlcListOfFdtEntries = new FdtEntry[numberOfFdtEntries];

            for (var i = 0; i < numberOfFdtEntries; i++)
            {
                var ipAddr = new byte[4];
                ipAddr[0] = bytes[len++];
                ipAddr[1] = bytes[len++];
                ipAddr[2] = bytes[len++];
                ipAddr[3] = bytes[len++];
                var ip = new IPAddress(ipAddr);

                var udpPort = new byte[2];
                udpPort[1] = bytes[len++];
                udpPort[0] = bytes[len++];

                var timeToLive = new byte[2];
                timeToLive[1] = bytes[len++];
                timeToLive[0] = bytes[len++];

                var timeRemaining = new byte[2];
                timeRemaining[1] = bytes[len++];
                timeRemaining[0] = bytes[len++];

                var point = new IPEndPoint(ip, udpPort[1]*256 + udpPort[0]);
                var fdtEntry = new FdtEntry
                {
                    MacAddress = point,
                    TimeToLive = (ushort) (timeToLive[1]*256 + timeToLive[0]),
                    TimeRemaining = (ushort) (timeRemaining[1]*256 + timeRemaining[0])
                };


                BvlcListOfFdtEntries[i] = fdtEntry;
            }
            return len;
        }
    }
}