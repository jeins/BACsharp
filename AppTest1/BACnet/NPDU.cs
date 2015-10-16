using System;

namespace BACnet
{
    //-----------------------------------------------------------------------------------------------
    // NPDU Routines
    public static class NPDU
    {
        public static byte PDUControl;
        public static UInt16 DNET;
        public static byte DLEN;
        public static byte[] DADR;
        public static UInt16 SNET;
        public static byte SLEN;
        public static byte[] SADR;
        public static byte HopCount;
        public static byte MessageType;
        public static UInt16 VendorID;
        public static UInt32 DAddress;
        public static UInt32 SAddress;

        public static void /*NPDU*/ Clear()
        {
            // Clear the packet members
            PDUControl = 0;
            DNET = 0;
            DLEN = 0;
            DADR = null;
            SNET = 0;
            SLEN = 0;
            SADR = null;
            HopCount = 0;
            MessageType = 0;
            VendorID = 0;
            DAddress = 0;
            SAddress = 0;
        }

        public static int /*NPDU*/ Assemble(byte[] bytes, int offset)
        {
            // Create a NPUD packet in the bytes array, starting at offset given
            // Return the length
            int len = 0;
            return len;
        }

        public static int /*NPDU*/ Parse(byte[] bytes, int offset)
        {
            // Returns the Length of the NPDU portion of the packet (offset of APDU)
            // We assume the BVLL is always present, so the NPDU always starts at offset 4
            int len = offset; // 4
            byte[] temp;
            Clear();
            if (bytes[len++] != 0x01) return 0;
            PDUControl = bytes[len++];  // 5
            if ((PDUControl & 0x20) > 0)
            {
                // We have a Destination 
                temp = new byte[2];
                temp[1] = bytes[len++];
                temp[0] = bytes[len++];
                DNET = BitConverter.ToUInt16(temp, 0);
                DLEN = bytes[len++];
                if (DLEN == 1)
                {
                    DADR = new byte[1];
                    DADR[0] = bytes[len++];
                    DAddress = (UInt32)DADR[0];
                }
                if (DLEN == 2)
                {
                    temp = new byte[2];
                    DADR[1] = bytes[len++];
                    DADR[0] = bytes[len++];
                    DAddress = (UInt32)BitConverter.ToUInt16(DADR, 0);
                }
                if (DLEN == 4)
                {
                    temp = new byte[4];
                    DADR[3] = bytes[len++];
                    DADR[2] = bytes[len++];
                    DADR[1] = bytes[len++];
                    DADR[0] = bytes[len++];
                    DAddress = BitConverter.ToUInt32(DADR, 0);
                }
                //PEP Other DLEN values ...

            }
            else
                DLEN = 0;

            if ((PDUControl & 0x08) > 0)
            {
                // We have a Source
                temp = new byte[2];
                temp[1] = bytes[len++];
                temp[0] = bytes[len++];
                SNET = BitConverter.ToUInt16(temp, 0);
                SLEN = bytes[len++];
                if (SLEN == 1)
                {
                    SADR = new byte[1];
                    SADR[0] = bytes[len++];
                    SAddress = (UInt32)SADR[0];
                }
                if (SLEN == 2)
                {
                    SADR = new byte[2];
                    SADR[1] = bytes[len++];
                    SADR[0] = bytes[len++];
                    SAddress = (UInt32)BitConverter.ToUInt16(SADR, 0);
                }
                if (SLEN == 4)
                {
                    SADR = new byte[4];
                    SADR[3] = bytes[len++];
                    SADR[2] = bytes[len++];
                    SADR[1] = bytes[len++];
                    SADR[0] = bytes[len++];
                    SAddress = BitConverter.ToUInt32(SADR, 0);
                }
                //PEP Other SLEN values ...
            }
            else
                SLEN = 0;

            if ((PDUControl & 0x20) > 0)
            {
                HopCount = bytes[len++];  // Get the Hop Count 
            }

            /*
                  if ((PDUControl & 0x80) > 0)
                  {
                    MessageType = bytes[len + offset];
                    len++;                  // Message Type field
                      if (MessageType >= 0x80)
                      {
                        temp = new byte[2];
                        temp[0] = bytes[len + offset + 2];
                        temp[1] = bytes[len + offset + 1];
                        VendorID = BitConverter.ToUInt16(temp, 0);
                        len += 2;             // VendorID field
                      }
                    }
                    len += offset;
                  }
            */
            return len;
        }

    }
}
