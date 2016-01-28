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

using System.Collections.Generic;
using System.Net;

namespace ConnectTools.BACnet
{
    public class BacnetDevice
    {
        public uint InstanceNumber;
        public IPEndPoint IpAddress;
        public string ApplicationSoftwareVersion;

        public List<string> DeviceObjects;
        public string FirmwareRevision;
        public bool IsBbmdActive;
        public string MobileUri;
        public string ModelName;
        public readonly int Network;
        public string ObjectName;
        public int ProtocolRevision;
        public int ProtocolVersion;
        public readonly byte SourceLength;
        public string SystemStatus;
        public string Uri;
        public int VendorIdentifier;
        public string VendorName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BacnetDevice" /> class.
        /// </summary>
        public BacnetDevice()
        {
            InstanceNumber = 0;
            ObjectName = "unknown";
            SystemStatus = "unknown";
            VendorName = "unknown";
            VendorIdentifier = 0;
            ModelName = "unknown";
            FirmwareRevision = "unknown";
            ApplicationSoftwareVersion = "unknown";
            ProtocolVersion = 0;
            ProtocolRevision = 0;
            SourceLength = 0;
            Network = 65535;
            DeviceObjects = null;
            IsBbmdActive = false;
            Uri = "unknown";
            MobileUri = "unknown";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BacnetDevice"/> class.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="instanceNumber">The instance number.</param>
        public BacnetDevice(IPEndPoint ipAddress, uint instanceNumber)
        {
            IpAddress = ipAddress;
            Network = 0;
            InstanceNumber = instanceNumber;
            VendorIdentifier = 0;
            SourceLength = 0;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BacnetDevice" /> class.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="network">The network.</param>
        /// <param name="instanceNumber">The instance number.</param>
        /// <param name="vendorIdentifier">The vendor identifier.</param>
        /// <param name="sourceLength">Length of the source.</param>
        public BacnetDevice(IPEndPoint ipAddress, int network, uint instanceNumber, int vendorIdentifier,
            byte sourceLength)
        {
            IpAddress = ipAddress;
            Network = network;
            InstanceNumber = instanceNumber;
            VendorIdentifier = vendorIdentifier;
            SourceLength = sourceLength;
        }
    }
}