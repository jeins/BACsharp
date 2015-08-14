using System;
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace BACnet
{
    /// <summary>
    /// Represents BACnet Devices
    /// </summary>
    public class Device
    {
        public string Name { get; set; }
        public int VendorID { get; set; }
        public IPEndPoint ServerEP { get; set; }
        public int Network { get; set; }
        public byte SourceLength { get; set; }
        public UInt32 Instance { get; set; }
        public UInt32 MACAddress { get; set; }
        //public NPDU NetPDU;

        // Constructors
        public Device()
        {
            //NetPDU = new NPDU();
            Name = "(no name)";
            VendorID = 0;
            ServerEP = null;
            Network = 0;
            SourceLength = 0;
            Instance = 0;
            MACAddress = 0;
        }

        public Device(string name, int vendorid, byte slen, IPEndPoint server, int network, UInt32 instance)
        {
            this.Name = name;
            this.VendorID = vendorid;
            this.SourceLength = slen;
            this.ServerEP = server;
            this.Network = network;
            this.Instance = instance;
            //NetPDU = new NPDU();
        }

        // We need a ToString() for the ListBox
        public override string ToString()
        {
            return this.Name;
        }

        /* public override int GetHashCode()
         {
             return (this.Network ^ this.Instance);
         }*/

        public override bool Equals(object obj)
        {
            Device obj2 = obj as Device;
            return (this.Instance == obj2.Instance);
        }

        public static bool operator ==(Device obj1, Device obj2)
        {
            if ((null == (Object)obj1) || (null == (Object)obj2))
            {
                return false;
            }
            else
            {
                return (obj1.Instance == obj2.Instance) && (obj1.Network == obj2.Network);    
            }
        }

        public static bool operator !=(Device obj1, Device obj2)
        {
            return (obj1.Instance != obj2.Instance) || (obj1.Network != obj2.Network);
        }
    }
}