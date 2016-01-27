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
    using System.Net;

    public class Device
    {
        public string Name { get; set; }
        public int VendorId { get; set; }
        public IPEndPoint ServerEp { get; set; }
        public int Network { get; set; }
        public byte SourceLength { get; set; }
        public uint Instance { get; set; }
        public uint MacAddress { get; set; }

        public Device()
        {
            Name = "(no name)";
            VendorId = 0;
            ServerEp = null;
            Network = 0;
            SourceLength = 0;
            Instance = BacnetEnums.MaxBacnetPropertyId;
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
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var obj2 = obj as Device;
            return (Instance == obj2.Instance);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Device obj1, Device obj2)
        {
            if ((null == (object) obj1) || (null == (object) obj2))
            {
                return false;
            }
            return (obj1.Instance == obj2.Instance) && (obj1.Network == obj2.Network);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Device obj1, Device obj2)
        {
            return (obj1.Instance != obj2.Instance) || (obj1.Network != obj2.Network);
        }
    }
}