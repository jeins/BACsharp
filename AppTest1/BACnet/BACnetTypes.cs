using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BACnet;

namespace BACnet
{

    public class BDTEntry
    {
        public IPEndPoint MACAddress { get; set; }
        public IPAddress Mask { get; set; }

        public BDTEntry()
        {
            IPAddress temp = new IPAddress(0);
            MACAddress = new IPEndPoint(temp, 0);
            Mask = new IPAddress(0);
        }
    }

    public class FDTEntry
    {
        public IPEndPoint MACAddress { get; set; }
        public UInt16 TimeToLive { get; set; }
        public UInt16 TimeRemaining { get; set; }

        public FDTEntry()
        {
            IPAddress temp = new IPAddress(0);
            MACAddress = new IPEndPoint(temp, 0);
            TimeToLive = 0;
            TimeRemaining = 0;
        }
    }
}
