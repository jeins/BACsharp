using System.Net;

namespace ConnectTools.BACnet
{
    /// <summary>
    ///     Represents BACnet Devices
    /// </summary>
    public class Device
    {
        //public NPDU NetPDU;

        // Constructors
        public Device()
        {
            //NetPDU = new NPDU();
            Name = "(no name)";
            VendorId = 0;
            ServerEp = null;
            Network = 0;
            SourceLength = 0;
            Instance = BaCnetEnums.MaxBacnetPropertyId;
            MacAddress = 0;
        }

        public Device(string name, int vendorid, byte slen, IPEndPoint server, int network, uint instance)
        {
            Name = name;
            VendorId = vendorid;
            SourceLength = slen;
            ServerEp = server;
            Network = network;
            Instance = instance;
            //NetPDU = new NPDU();
        }

        public string Name { get; set; }
        public int VendorId { get; set; }
        public IPEndPoint ServerEp { get; set; }
        public int Network { get; set; }
        public byte SourceLength { get; set; }
        public uint Instance { get; set; }
        public uint MacAddress { get; set; }

        // We need a ToString() for the ListBox
        public override string ToString()
        {
            return Name;
        }

        /* public override int GetHashCode()
         {
             return (this.Network ^ this.Instance);
         }*/

        public override bool Equals(object obj)
        {
            var obj2 = obj as Device;
            return (Instance == obj2.Instance);
        }

        public static bool operator ==(Device obj1, Device obj2)
        {
            if ((null == (object) obj1) || (null == (object) obj2))
            {
                return false;
            }
            return (obj1.Instance == obj2.Instance) && (obj1.Network == obj2.Network);
        }

        public static bool operator !=(Device obj1, Device obj2)
        {
            return (obj1.Instance != obj2.Instance) || (obj1.Network != obj2.Network);
        }
    }
}