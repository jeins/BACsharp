using System;
using System.Collections.Generic;
using System.Linq;
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

        


        public static byte BLVC_Type;
        public static byte BLVC_Function;
        public static UInt16 BLVC_Length;
        public static UInt16 BLVC_Function_ResultCode;
        public static BDTEntry[] BLVC_ListOfBdtEntries;
        
        public static void /*BVLC*/ Clear()
        {
            // Clear the packet members
            BLVC_Type     = 0;
            BLVC_Function = 0;
            BLVC_Length   = 0;

            BLVC_Function_ResultCode = 0;
            BLVC_ListOfBdtEntries = null;
        }

        public static int /*BVLC*/ Parse(byte[] bytes, int offset)
        {
            // Returns the Length of the BVLC portion of the packet (offset of NPDU)
            int len = offset;
            byte[] temp;
            Clear();
            BLVC_Type     = bytes[len++];
            BLVC_Function = bytes[len++];

            temp = new byte[2];
            temp[0] = bytes[len++];
            temp[1] = bytes[len++];

            BLVC_Length = (UInt16) (temp[0] * 255 + temp[1]);

            if (BACNET_BVLC_TYPE_BIP != BLVC_Type)
            {
                // currently only BACnet/IP is supported
                return 0;
            }

            switch ( BLVC_Function )
            {
                case BACNET_BVLC_FUNC_RESULT:
                    return ParseResultCode( bytes, len );
                case BACNET_BVLC_FUNC_WRITE_BDT:
                case BACNET_BVLC_FUNC_READ_BDT_ACK:
                    return ParseListOfBdtEntries(bytes, len);
                default:
                    return len;
            }
            
        }

        public static int /*BVLC*/ ParseResultCode(byte[] bytes, int offset)
        {
            int len = offset;
            byte[] temp;
            temp = new byte[2];
            temp[1] = bytes[len++];
            temp[0] = bytes[len++];
            BLVC_Function_ResultCode = (UInt16)BitConverter.ToUInt16(temp, 0);
            return len;
        }

        public static int /*BVLC*/ ParseListOfBdtEntries(byte[] bytes, int offset)
        {
            int len = offset;
            byte[] temp;
            temp = new byte[2];
            temp[1] = bytes[len++];
            temp[0] = bytes[len++];
            BLVC_Function_ResultCode = (UInt16)BitConverter.ToUInt16(temp, 0);
            return len;
        }

    }
}
