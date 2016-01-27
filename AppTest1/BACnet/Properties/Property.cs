// -----------------------------------------------------------------------------------
// Copyright (C) 2015 Kieback&Peter GmbH & Co KG All Rights Reserved
// 
// Kieback&Peter Confidential Proprietary Information
// 
// This Software is confidential and proprietary to Kieback&Peter. 
// The reproduction or disclosure in whole or part to anyone outside of Kieback&Peter
// without the written approval of an officer of Kieback&Peter GmbH & Co.KG,
// under a Non-Disclosure Agreement, or to any employee who has not previously
// obtained a written authorization for access from the individual responsible
// for the software will have a significant detrimental effect on
// Kieback&Peter and is expressly PROHIBITED.
// -----------------------------------------------------------------------------------

namespace ConnectTools.BACnet.Properties
{
    public class Property
    {
        public string ToStringValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Property" /> class.
        /// </summary>
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
        private int ValueInt { get; set; }
        public float ValueSingle { get; set; }
        private double ValueDouble { get; set; }
        public byte[] ValueOctet { get; set; }
        public string ValueString { get; set; }
        public uint ValueEnum { get; set; }
        public BacnetEnums.BacnetObjectType ValueObjectType { get; set; }
        public uint ValueObjectInstance { get; set; }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToStringValue;
        }
    }
}