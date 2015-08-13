using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using BACnet;

namespace BACnet
{

    //-----------------------------------------------------------------------------------------------
    // BVLC Routines
    public static class BVLC
    {

        public const int BACNET_BVLC_HEADER_LEN = 0x04;

        public const byte BACNET_BVLC_TYPE_BIP = 0x81;

        public const int BACNET_BVLC_FUNC_RESULT = 0x00;
        public const int BACNET_BVLC_FUNC_WRITE_BDT = 0x01;
        public const int BACNET_BVLC_FUNC_READ_BDT = 0x02;
        public const int BACNET_BVLC_FUNC_READ_BDT_ACK = 0x03;
        public const int BACNET_BVLC_FUNC_FORW_NPDU = 0x04;
        public const int BACNET_BVLC_FUNC_REGISTER_FD = 0x05;
        public const int BACNET_BVLC_FUNC_READ_FDT = 0x06;
        public const int BACNET_BVLC_FUNC_READ_FDT_ACK = 0x07;
        public const int BACNET_BVLC_FUNC_DELETE_FD = 0x08;
        public const int BACNET_BVLC_FUNC_DISTRIBUTE_BROADCAST_TO_NETWORK = 0x09;
        public const int BACNET_BVLC_FUNC_UNICAST_NPDU = 0x0A;
        public const int BACNET_BVLC_FUNC_BROADCAST_NPDU = 0x0B;
        


        


        public static byte BVLC_Type;
        public static byte BVLC_Function;
        public static UInt16 BVLC_Length;
        public static UInt16 BVLC_Function_ResultCode;
        public static UInt16 BVLC_TimeToLive;
        public static BDTEntry[] BVLC_ListOfBdtEntries;
        public static FDTEntry[] BVLC_ListOfFdtEntries;
        public static IPEndPoint BVLC_ForwardAddress;
        
        public static void /*BVLC*/ Clear()
        {
            // Clear the packet members
            BVLC_Type     = 0;
            BVLC_Function = 0;
            BVLC_Length   = 0;

            BVLC_Function_ResultCode = 0;
            BVLC_ListOfBdtEntries = null;
            BVLC_ForwardAddress = null;
        }

        public static int /*BVLC*/ Parse(byte[] bytes, int offset)
        {
            // Returns the Length of the BVLC portion of the packet (offset of NPDU)
            int len = offset;
            byte[] temp;
            Clear();
            BVLC_Type     = bytes[len++];
            BVLC_Function = bytes[len++];

            temp = new byte[2];
            temp[0] = bytes[len++];
            temp[1] = bytes[len++];

            BVLC_Length = (UInt16) (temp[0] * 255 + temp[1]);

            if (BACNET_BVLC_TYPE_BIP != BVLC_Type)
            {
                // currently only BACnet/IP is supported
                return 0;
            }

            switch ( BVLC_Function )
            {
                case BACNET_BVLC_FUNC_RESULT:
                    return ParseResultCode( bytes, len );
                case BACNET_BVLC_FUNC_WRITE_BDT:
                case BACNET_BVLC_FUNC_READ_BDT_ACK:
                    return ParseListOfBdtEntries(bytes, len);
                case BACNET_BVLC_FUNC_FORW_NPDU:
                    return ParseForwardAddress(bytes, len);
                case BACNET_BVLC_FUNC_REGISTER_FD:
                    return ParseTimeToLive(bytes, len);
                case BACNET_BVLC_FUNC_READ_FDT_ACK:
                case BACNET_BVLC_FUNC_DELETE_FD:
                    return ParseListOfFdtEntries(bytes, len);
                case BACNET_BVLC_FUNC_UNICAST_NPDU:                  
                case BACNET_BVLC_FUNC_BROADCAST_NPDU:
                case BACNET_BVLC_FUNC_DISTRIBUTE_BROADCAST_TO_NETWORK:
                case BACNET_BVLC_FUNC_READ_BDT:
                case BACNET_BVLC_FUNC_READ_FDT:
                    return len;
                default:
                    return len;
            }
            
        }

        public static int /*BVLC*/ ParseTimeToLive(byte[] bytes, int offset)
        {
            int len = offset;

            byte[] timeToLive;

            timeToLive = new byte[2];
            timeToLive[1] = bytes[len++];
            timeToLive[0] = bytes[len++];

            BVLC_TimeToLive = (UInt16) (timeToLive[1] * 256 + timeToLive[0]);

            return len;
        }

        public static int /*BVLC*/ ParseForwardAddress(byte[] bytes, int offset)
        {
            int len = offset;

            byte[] ipAddr;
            byte[] udpPort;

            ipAddr = new byte[4];
            ipAddr[0] = bytes[len++];
            ipAddr[1] = bytes[len++];
            ipAddr[2] = bytes[len++];
            ipAddr[3] = bytes[len++];
            IPAddress ip = new IPAddress(ipAddr);

            udpPort = new byte[2];
            udpPort[1] = bytes[len++];
            udpPort[0] = bytes[len++];

            BVLC_ForwardAddress = new IPEndPoint(ip, udpPort[1] * 256 + udpPort[0]);

            return len;
        }

        public static int /*BVLC*/ ParseResultCode(byte[] bytes, int offset)
        {
            int len = offset;
            byte[] temp;
            temp = new byte[2];
            temp[1] = bytes[len++];
            temp[0] = bytes[len++];
            BVLC_Function_ResultCode = (UInt16)BitConverter.ToUInt16(temp, 0);
            return len;
        }

        public static int /*BVLC*/ ParseListOfBdtEntries(byte[] bytes, int offset)
        {
            int len = offset;
            int bytesBDTEntries = BVLC_Length - offset;
            int numberOfBDTEntries = bytesBDTEntries/10; // 10 Bytes per Entry
            BVLC_ListOfBdtEntries = new BDTEntry[numberOfBDTEntries];

            for (int i = 0; i < numberOfBDTEntries; i++)
            {
                byte[] ipAddr;
                byte[] udpPort;
                byte[] ipMask;

                ipAddr = new byte[4];
                ipAddr[0] = bytes[len++];
                ipAddr[1] = bytes[len++];
                ipAddr[2] = bytes[len++];
                ipAddr[3] = bytes[len++];
                IPAddress ip = new IPAddress(ipAddr);

                udpPort = new byte[2];
                udpPort[1] = bytes[len++];
                udpPort[0] = bytes[len++];

                ipMask = new byte[4];
                ipMask[3] = bytes[len++];
                ipMask[2] = bytes[len++];
                ipMask[1] = bytes[len++];
                ipMask[0] = bytes[len++];
                IPAddress mask = new IPAddress(ipMask);

                IPEndPoint point = new IPEndPoint(ip, udpPort[1] * 256 + udpPort[0]);
                BDTEntry bdtEntry = new BDTEntry();
                
                bdtEntry.MACAddress = point;
                bdtEntry.Mask = mask;

                BVLC_ListOfBdtEntries[i] = bdtEntry;
            }
            return len;
        }

        public static int /*BVLC*/ ParseListOfFdtEntries(byte[] bytes, int offset)
        {
            int len = offset;
            int bytesFDTEntries = BVLC_Length - offset;
            int numberOfFDTEntries = bytesFDTEntries / 10; // 10 Bytes per Entry
            BVLC_ListOfBdtEntries = new BDTEntry[numberOfFDTEntries];

            for (int i = 0; i < numberOfFDTEntries; i++)
            {
                byte[] ipAddr;
                byte[] udpPort;
                byte[] timeToLive;
                byte[] timeRemaining;

                ipAddr = new byte[4];
                ipAddr[0] = bytes[len++];
                ipAddr[1] = bytes[len++];
                ipAddr[2] = bytes[len++];
                ipAddr[3] = bytes[len++];
                IPAddress ip = new IPAddress(ipAddr);

                udpPort = new byte[2];
                udpPort[1] = bytes[len++];
                udpPort[0] = bytes[len++];

                timeToLive = new byte[2];
                timeToLive[1] = bytes[len++];
                timeToLive[0] = bytes[len++];

                timeRemaining = new byte[2];
                timeRemaining[1] = bytes[len++];
                timeRemaining[0] = bytes[len++];

                IPEndPoint point = new IPEndPoint(ip, udpPort[1] * 256 + udpPort[0]);
                FDTEntry fdtEntry = new FDTEntry();

                fdtEntry.MACAddress     = point;
                fdtEntry.TimeToLive     = (UInt16) (timeToLive[1] * 256 + timeToLive[0]);
                fdtEntry.RemainingTime  = (UInt16) (timeRemaining[1] * 256 + timeRemaining[0]);

                BVLC_ListOfFdtEntries[i] = fdtEntry;
            }
            return len;
        }

    }
}
