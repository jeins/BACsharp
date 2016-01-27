namespace ConnectTools.BACnet
{
    /// <summary>
    /// Provides BACnetTag Routines.
    /// </summary>
    public static class BaCnetTag
    {
        public static byte TagNumber(byte tag)
        {
            var x = (tag >> 4) & 0x0F;
            return (byte)x;
        }

        public static byte Class(byte tag)
        {
            var x = (tag >> 3) & 0x01;
            return (byte)x;
        }
        public static byte LenValType(byte tag)
        {
            var x = tag & 0x07;
            return (byte)x;
        }
    }
}