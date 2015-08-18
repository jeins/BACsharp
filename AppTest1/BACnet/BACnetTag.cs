using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BACnet
{
    public static class BACnetTag
    {
        public static byte TagNumber(byte tag)
        {
            int x = ((int)tag >> 4) & 0x0F;
            return (byte)x;
        }

        public static byte Class(byte tag)
        {
            int x = ((int)tag >> 3) & 0x01;
            return (byte)x;
        }
        public static byte LenValType(byte tag)
        {
            int x = (int)tag & 0x07;
            return (byte)x;
        }
    }
}
