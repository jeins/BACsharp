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

namespace ConnectTools.BACnet
{
    using System.Collections.Generic;
    using System.Net;

    using Properties;

    public interface IBacnetStack
    {
        /// <summary>
        ///     Who-Is, and collect information about who answers
        /// </summary>
        /// <param name="milliseconds"></param>
        List<Device> GetDevices(int milliseconds);

        /// <summary>
        ///     Checks the single device.
        /// </summary>
        /// <param name="bIpAddress">The bacnet ip address.</param>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns></returns>
        Device UnicastWhoIsOnSingleIp(IPEndPoint bIpAddress, int milliseconds);

        /// <summary>
        ///     I-Am.
        /// </summary>
        /// <param name="network">The network.</param>
        /// <param name="objectid">The objectid.</param>
        /// <returns></returns>
        bool GetIAm(int network, uint objectid);

        /// <summary>
        ///     Read Property.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="arrayidx">The arrayidx.</param>
        /// <param name="objtype">The objtype.</param>
        /// <param name="objprop">The objprop.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        bool SendReadProperty(
            Device recipient,
            int arrayidx,
            BacnetEnums.BacnetObjectType objtype,
            BacnetEnums.BacnetPropertyId objprop,
            Property property);

        /// <summary>
        ///     Write Property.
        /// </summary>
        /// <param name="recipient">The receipient.</param>
        /// <param name="arrayidx">The arrayidx.</param>
        /// <param name="objtype">The objtype.</param>
        /// <param name="objprop">The objprop.</param>
        /// <param name="property">The property.</param>
        /// <param name="priority">The priority.</param>
        /// <returns></returns>
        bool SendWriteProperty(
            Device recipient,
            int arrayidx,
            BacnetEnums.BacnetObjectType objtype,
            BacnetEnums.BacnetPropertyId objprop,
            Property property,
            int priority);

        /// <summary>
        ///     Sends the read BDT.
        /// </summary>
        /// <param name="bIpAddress">The bacnet ip address.</param>
        /// <returns></returns>
        bool SendReadBdt(IPEndPoint bIpAddress);

        /// <summary>
        ///     Sends the read FDT.
        /// </summary>
        /// <param name="bIpAddress">The bacnet ip address.</param>
        /// <returns></returns>
        bool SendReadFdt(IPEndPoint bIpAddress);
    }
}