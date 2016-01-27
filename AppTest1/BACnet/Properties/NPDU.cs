using System;

namespace ConnectTools.BACnet
{
    //-----------------------------------------------------------------------------------------------
    // NPDU Routines
    public class Npdu
    {
        public static byte PduControl;
        public static ushort Dnet;
        public static byte Dlen;
        public static byte[] Dadr;
        public static ushort Snet;
        public static byte Slen;
        public static byte[] Sadr;
        public static byte HopCount;
        public static byte MessageType;
        public static ushort VendorId;
        public static uint DAddress;
        public static uint SAddress;

        public static void /*NPDU*/ Clear()
        {
            // Clear the packet members
            PduControl = 0;
            Dnet = 0;
            Dlen = 0;
            Dadr = null;
            Snet = 0;
            Slen = 0;
            Sadr = null;
            HopCount = 0;
            MessageType = 0;
            VendorId = 0;
            DAddress = 0;
            SAddress = 0;
        }

        public static int /*NPDU*/ Assemble(byte[] bytes, int offset)
        {
            // Create a NPUD packet in the bytes array, starting at offset given
            // Return the length
            var len = 0;
            return len;
        }

        public static int /*NPDU*/ Parse(byte[] bytes, int offset)
        {
            // Returns the Length of the NPDU portion of the packet (offset of APDU)
            // We assume the BVLL is always present, so the NPDU always starts at offset 4
            var len = offset; // 4
            byte[] temp;
            Clear();
            if (bytes[len++] != 0x01) return 0;
            PduControl = bytes[len++]; // 5
            if ((PduControl & 0x20) > 0)
            {
                // We have a Destination 
                temp = new byte[2];
                temp[1] = bytes[len++];
                temp[0] = bytes[len++];
                Dnet = BitConverter.ToUInt16(temp, 0);
                Dlen = bytes[len++];
                if (Dlen == 1)
                {
                    Dadr = new byte[1];
                    Dadr[0] = bytes[len++];
                    DAddress = Dadr[0];
                }
                if (Dlen == 2)
                {
                    temp = new byte[2];
                    Dadr[1] = bytes[len++];
                    Dadr[0] = bytes[len++];
                    DAddress = BitConverter.ToUInt16(Dadr, 0);
                }
                if (Dlen == 4)
                {
                    temp = new byte[4];
                    Dadr[3] = bytes[len++];
                    Dadr[2] = bytes[len++];
                    Dadr[1] = bytes[len++];
                    Dadr[0] = bytes[len++];
                    DAddress = BitConverter.ToUInt32(Dadr, 0);
                }
                //PEP Other DLEN values ...
            }
            else
                Dlen = 0;

            if ((PduControl & 0x08) > 0)
            {
                // We have a Source
                temp = new byte[2];
                temp[1] = bytes[len++];
                temp[0] = bytes[len++];
                Snet = BitConverter.ToUInt16(temp, 0);
                Slen = bytes[len++];
                if (Slen == 1)
                {
                    Sadr = new byte[1];
                    Sadr[0] = bytes[len++];
                    SAddress = Sadr[0];
                }
                if (Slen == 2)
                {
                    Sadr = new byte[2];
                    Sadr[1] = bytes[len++];
                    Sadr[0] = bytes[len++];
                    SAddress = BitConverter.ToUInt16(Sadr, 0);
                }
                if (Slen == 4)
                {
                    Sadr = new byte[4];
                    Sadr[3] = bytes[len++];
                    Sadr[2] = bytes[len++];
                    Sadr[1] = bytes[len++];
                    Sadr[0] = bytes[len++];
                    SAddress = BitConverter.ToUInt32(Sadr, 0);
                }
                //PEP Other SLEN values ...
            }
            else
                Slen = 0;

            if ((PduControl & 0x20) > 0)
            {
                HopCount = bytes[len++]; // Get the Hop Count 
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