using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BACnet;

namespace BACnet
{

    // APDU Routines
    public class BDTEntry
    {
        public IPEndPoint MACAddress { get; set; }
        public IPAddress Mask { get; set; }
    }
}
