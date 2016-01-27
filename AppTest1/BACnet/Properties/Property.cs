using ConnectTools.BACnet.Properties;

namespace ConnectTools.BACnet
{
    //-----------------------------------------------------------------------------------------------
    // Property Class
    public class Property
    {
        public string ToStringValue;
        //PEP Others ...

        // Constructors
        public Property()
        {
            Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagNull;
            ValueBool = false;
            ValueUInt = 0;
            ValueInt = 0;
            ValueSingle = 0;
            ValueDouble = 0;
            ValueOctet = null;
            ValueString = "";
            ValueEnum = 0;
            ValueObjectType = BacnetEnums.BacnetObjectType.ObjectDevice;
            ValueObjectInstance = 0;
            ToStringValue = "";
        }

        public BacnetEnums.BacnetApplicationTag Tag { get; set; }
        public bool ValueBool { get; set; }
        public uint ValueUInt { get; set; }
        public int ValueInt { get; set; }
        public float ValueSingle { get; set; }
        public double ValueDouble { get; set; }
        public byte[] ValueOctet { get; set; }
        public string ValueString { get; set; }
        public uint ValueEnum { get; set; }
        public BacnetEnums.BacnetObjectType ValueObjectType { get; set; }
        public uint ValueObjectInstance { get; set; }

        public override string ToString()
        {
            return ToStringValue;
        }
    }
}