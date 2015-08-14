using System;
using System.Collections.Generic;
using System.Net;

namespace BACnet
{
    public interface IBACnetStack
    {
        /// <summary>
        /// Who-Is, and collect information about who answers
        /// </summary>
        /// <param name="milliseconds"></param>
        List<Device> GetDevices(int milliseconds);

        /// <summary>
        /// I-Am.
        /// </summary>
        /// <param name="network">The network.</param>
        /// <param name="objectid">The objectid.</param>
        /// <returns></returns>
        bool GetIAm(int network, UInt32 objectid);

        /// <summary>
        /// Read Property.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="arrayidx">The arrayidx.</param>
        /// <param name="objtype">The objtype.</param>
        /// <param name="objprop">The objprop.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        bool SendReadProperty(
            Device recipient,
            int arrayidx,
            BACnetEnums.BACNET_OBJECT_TYPE objtype,
            BACnetEnums.BACNET_PROPERTY_ID objprop,
            Property property);

        /// <summary>
        /// Write Property.
        /// </summary>
        /// <param name="recipient">The receipient.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="arrayidx">The arrayidx.</param>
        /// <param name="objtype">The objtype.</param>
        /// <param name="objprop">The objprop.</param>
        /// <param name="property">The property.</param>
        /// <param name="priority">The priority.</param>
        /// <returns></returns>
        bool SendWriteProperty(
            Device recipient,
            int arrayidx,
            BACnetEnums.BACNET_OBJECT_TYPE objtype,
            BACnetEnums.BACNET_PROPERTY_ID objprop,
            Property property,
            int priority);

        /// <summary>
        /// Sends the read BDT.
        /// </summary>
        /// <param name="deviceidx">The deviceidx.</param>
        /// <returns></returns>
        bool SendReadBdt(IPEndPoint bIpAddress);

        /// <summary>
        /// Sends the read FDT.
        /// </summary>
        /// <param name="deviceidx">The deviceidx.</param>
        /// <returns></returns>
        bool SendReadFdt(IPEndPoint bIpAddress);
    }
}