using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BACnet;

namespace BACnet
{

    //-----------------------------------------------------------------------------------------------
    // Property Class
    public class Property
    {
        public BACnetEnums.BACNET_APPLICATION_TAG Tag { get; set; }
        public bool ValueBool { get; set; }
        public uint ValueUInt { get; set; }
        public int ValueInt { get; set; }
        public float ValueSingle { get; set; }
        public double ValueDouble { get; set; }
        public byte[] ValueOctet { get; set; }
        public string ValueString { get; set; }
        public uint ValueEnum { get; set; }
        public BACnetEnums.BACNET_OBJECT_TYPE ValueObjectType { get; set; }
        public uint ValueObjectInstance { get; set; }
        public string ToStringValue;
        //PEP Others ...

        // Constructors
        public Property()
        {
            this.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_NULL;
            this.ValueBool = false;
            this.ValueUInt = 0;
            this.ValueInt = 0;
            this.ValueSingle = 0;
            this.ValueDouble = 0;
            this.ValueOctet = null;
            this.ValueString = "";
            this.ValueEnum = 0;
            this.ValueObjectType = BACnetEnums.BACNET_OBJECT_TYPE.OBJECT_DEVICE;
            this.ValueObjectInstance = 0;
            this.ToStringValue = "";
        }

        public override string ToString()
        {
            return this.ToStringValue;
        }
    }
}
