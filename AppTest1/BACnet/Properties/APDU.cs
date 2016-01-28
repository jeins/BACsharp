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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;

namespace ConnectTools.BACnet.Properties
{
    public static class Apdu
    {
        private static byte apduType;
        public static uint ObjectId;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     Parses the i am.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static int ParseIAm(byte[] bytes, int offset)
        {
            // Look for and parse I-Am Packet
            ObjectId = 0;
            apduType = bytes[offset];
            if ((apduType != 0x10) || (bytes[offset + 1] != 0x00)) return 0;

            // Get the ObjectID
            if (BacnetTag.TagNumber(bytes[offset + 2]) != 12)
                return 0;
            var temp = new byte[4];
            temp[0] = bytes[offset + 6];
            temp[1] = bytes[offset + 5];
            temp[2] = (byte)(bytes[offset + 4] & 0x3F);
            temp[3] = 0;
            ObjectId = BitConverter.ToUInt32(temp, 0);
            var len = 5;
            return len;
        }

        /// <summary>
        ///     Sets the object identifier.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="pos">The position.</param>
        /// <param name="type">The type.</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static uint SetObjectId(ref byte[] bytes, uint pos,
            BacnetEnums.BacnetObjectType type, uint instance)
        {
            bytes[pos++] = 0x0C; // Tag number (BACnet Object ID)
            //bytes[pos++] = 0x01;
            //bytes[pos++] = 0x00;
            //bytes[pos++] = 0x00;
            //bytes[pos++] = 0x00;

            var value = (uint)type;
            value = value & BacnetEnums.BacnetMaxObject;
            value = value << BacnetEnums.BacnetInstanceBits;
            value = value | (instance & BacnetEnums.BacnetMaxInstance);
            //len = encode_unsigned32(apdu, value);
            var temp4 = BitConverter.GetBytes(value);
            bytes[pos++] = temp4[3];
            bytes[pos++] = temp4[2];
            bytes[pos++] = temp4[1];
            bytes[pos++] = temp4[0];

            return pos;
        }

        /// <summary>
        ///     Sets the property identifier.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="pos">The position.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static uint SetPropertyId(ref byte[] bytes, uint pos,
            BacnetEnums.BacnetPropertyId type)
        {
            var value = (uint)type;
            if (value <= 255)
            {
                bytes[pos++] = 0x19;
                bytes[pos++] = (byte)type;
            }
            else if (value < 65535)
            {
                bytes[pos++] = 0x1A;
                var temp2 = BitConverter.GetBytes(value);
                bytes[pos++] = temp2[1];
                bytes[pos++] = temp2[0];
            }
            return pos;
        }

        /// <summary>
        ///     Sets the index of the array.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="pos">The position.</param>
        /// <param name="aidx">The aidx.</param>
        /// <returns></returns>
        public static uint SetArrayIdx(ref byte[] bytes, uint pos, int aidx)
        {
            var value = (uint)aidx;
            if (value <= 255)
            {
                bytes[pos++] = 0x29;
                bytes[pos++] = (byte)aidx;
            }
            else if (value < 65535)
            {
                bytes[pos++] = 0x2A;
                var temp2 = BitConverter.GetBytes(value);
                bytes[pos++] = temp2[1];
                bytes[pos++] = temp2[0];
            }
            return pos;
        }

        /// <summary>
        ///     Sets the property.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="pos">The position.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static uint SetProperty(ref byte[] bytes, uint pos, Property property)
        {
            if (property != null)
            {
                bytes[pos++] = 0x3E;
                int len;
                switch (property.Tag)
                {
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagNull:
                        bytes[pos++] = 0x00;
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagBoolean:
                        if (property.ValueBool)
                            bytes[pos++] = 0x11;
                        else
                            bytes[pos++] = 0x10;
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagUnsignedInt:
                        var value = property.ValueUInt;
                        if (value <= 255) // 1 byte
                        {
                            bytes[pos++] = 0x21;
                            bytes[pos++] = (byte)value;
                        }
                        else if (value <= 65535) // 2 bytes
                        {
                            bytes[pos++] = 0x22;
                            var temp2 = BitConverter.GetBytes(value);
                            bytes[pos++] = temp2[1];
                            bytes[pos++] = temp2[0];
                        }
                        else if (value <= 16777215) // 3 bytes
                        {
                            bytes[pos++] = 0x23;
                            var temp3 = BitConverter.GetBytes(value);
                            bytes[pos++] = temp3[2];
                            bytes[pos++] = temp3[1];
                            bytes[pos++] = temp3[0];
                        }
                        else // 4 bytes
                        {
                            bytes[pos++] = 0x24;
                            var temp4 = BitConverter.GetBytes(value);
                            bytes[pos++] = temp4[3];
                            bytes[pos++] = temp4[2];
                            bytes[pos++] = temp4[1];
                            bytes[pos++] = temp4[0];
                        }
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagSignedInt:
                        // Tag could be 0x31, 0x32, 0x33, 0x34
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagReal:
                        // Tag is 0x44
                        bytes[pos++] = 0x44;
                        var temp5 = BitConverter.GetBytes(property.ValueSingle);
                        bytes[pos++] = temp5[3];
                        bytes[pos++] = temp5[2];
                        bytes[pos++] = temp5[1];
                        bytes[pos++] = temp5[0];
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagDouble:
                        // Tag is 0x55
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagOctetString:
                        // Tag is 0x65, maximum 16 bytes!
                        bytes[pos++] = 0x65;
                        len = property.ValueOctet.Length;
                        bytes[pos++] = (byte)len;
                        for (var i = 0; i < len; i++)
                            bytes[pos++] = property.ValueOctet[i];
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagCharacterString:
                        // Tag is 0x75, maximum 15 chars!
                        bytes[pos++] = 0x75;
                        len = property.ValueString.Length;
                        bytes[pos++] = (byte)(len + 1); // Include character set byte
                        bytes[pos++] = 0; // ANSI
                        for (var i = 0; i < len; i++)
                            bytes[pos++] = (byte)property.ValueString[i];
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagEnumerated:
                        // Tag could be 0x91, 0x92, 0x93, 0x94
                        bytes[pos++] = 0x91;
                        bytes[pos++] = (byte)property.ValueEnum;
                        break;
                    case BacnetEnums.BacnetApplicationTag.BacnetApplicationTagObjectId:
                        // Tag is 0xC4
                        bytes[pos++] = 0xC4;
                        var id = ((uint)property.ValueObjectType) << 22;
                        id += (property.ValueObjectInstance & 0x3FFFFF);
                        var temp6 = BitConverter.GetBytes(id);
                        bytes[pos++] = temp6[3];
                        bytes[pos++] = temp6[2];
                        bytes[pos++] = temp6[1];
                        bytes[pos++] = temp6[0];
                        break;
                }
                bytes[pos++] = 0x3F; // Tag Close
            }
            return pos;
        }

        /// <summary>
        ///     Parses the property.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="pos">The position.</param>
        /// <param name="property">The property.</param>
        public static void ParseProperty(ref byte[] bytes, int pos, Property property)
        {
            if (property == null) return;
            property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagNull;
            int tag;
            var offset = ParseRead(bytes, pos, out tag);
            if (tag == 0x21)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagUnsignedInt;
                property.ValueUInt = AppUInt(bytes, offset);
                property.ToStringValue = property.ValueUInt.ToString();
            }
            else if (tag == 0x22)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagUnsignedInt;
                property.ValueUInt = AppUInt16(bytes, offset);
                property.ToStringValue = property.ValueUInt.ToString();
            }
            else if (tag == 0x23)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagUnsignedInt;
                property.ValueUInt = AppUInt24(bytes, offset);
                property.ToStringValue = property.ValueUInt.ToString();
            }
            else if (tag == 0x24)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagUnsignedInt;
                property.ValueUInt = AppUInt32(bytes, offset);
                property.ToStringValue = property.ValueUInt.ToString();
            }
            else if (tag == 0x44)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagReal;
                property.ValueSingle = AppSingle(bytes, offset);
                property.ToStringValue = property.ValueSingle.ToString(CultureInfo.InvariantCulture);
            }
            else if (tag == 0x65)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagOctetString;
                property.ValueOctet = AppOctet(bytes, offset);

                var s = property.ValueOctet.Aggregate("", (current, t) => current + t.ToString("X2"));
                property.ToStringValue = s;
            }
            else if (tag == 0x74)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagCharacterString;
                property.ValueString = AppSpecialString(bytes, offset);
                property.ToStringValue = property.ValueString;
            }
            else if (tag == 0x75)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagCharacterString;
                property.ValueString = AppString(bytes, offset);
                property.ToStringValue = property.ValueString;
            }
            else if (tag == 0x91)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagEnumerated;
                property.ValueEnum = bytes[offset];
                property.ToStringValue = property.ValueEnum.ToString();
            }
            else if (tag == 0xC4)
            {
                property.Tag = BacnetEnums.BacnetApplicationTag.BacnetApplicationTagObjectId;
                var value = AppUInt32(bytes, offset);
                property.ValueObjectType = (BacnetEnums.BacnetObjectType)(value >> 22);
                property.ValueObjectInstance = value & 0x3FFFFF;
                property.ToStringValue = property.ValueObjectInstance.ToString();
            }
            else
            {
                Log.Warn("WARNING: Unsupported property tag " + tag.ToString("X"));
            }
        }

        /// <summary>
        ///     Sets the priority.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="pos">The position.</param>
        /// <param name="priority">The priority.</param>
        /// <returns></returns>
        public static uint SetPriority(ref byte[] bytes, uint pos, int priority)
        {
            bytes[pos++] = 0x49;
            bytes[pos++] = (byte)priority;
            return pos;
        }

        private static int ParseRead(byte[] bytes, int offset, out int apptag)
        {
            // Look for and parse Read Property Complex ACK 
            apptag = 0xFF;
            var len = offset;
            if (bytes[len] != 0x30) return 0; // APDU Complex ACK
            len += 2;
            if (bytes[len++] != 0x0C) return 0; // Read Property ACK
            len += 5;

            // Parse the Property ID
            if (bytes[len] == 0x19)
                len += 2; // 1 byte Property ID
            else if (bytes[len] == 0x1A)
                len += 3; // 2 byte Property ID

            // Look for Array Index
            if (bytes[len] == 0x29)
                len += 2; // 1 byte Array Index
            else if (bytes[len] == 0x2A)
                len += 3; // 2 byte Array Index

            // Lok for Property Value
            len++; // 1 byte opening tag 0x3E
            apptag = bytes[len++]; // Look at Application Tag
            return len;
        }

        private static uint AppUInt(IReadOnlyList<byte> bytes, int offset)
        {
            // AppTag = 0x21
            return bytes[offset];
        }

        private static ushort AppUInt16(IReadOnlyList<byte> bytes, int offset)
        {
            // AppTag = 0x22
            var temp = new byte[2];
            temp[1] = bytes[offset++];
            temp[0] = bytes[offset++];
            return BitConverter.ToUInt16(temp, 0);
        }

        private static uint AppUInt24(IReadOnlyList<byte> bytes, int offset)
        {
            // AppTag = 0x23
            var temp = new byte[4];
            temp[3] = 0;
            temp[2] = bytes[offset++];
            temp[1] = bytes[offset++];
            temp[0] = bytes[offset++];
            return BitConverter.ToUInt32(temp, 0);
        }

        private static uint AppUInt32(IReadOnlyList<byte> bytes, int offset)
        {
            // AppTag = 0x24
            var temp = new byte[4];
            temp[3] = bytes[offset++];
            temp[2] = bytes[offset++];
            temp[1] = bytes[offset++];
            temp[0] = bytes[offset++];
            return BitConverter.ToUInt32(temp, 0);
        }

        private static float AppSingle(IReadOnlyList<byte> bytes, int offset)
        {
            // Apptag = 0x44
            var temp = new byte[4];
            temp[3] = bytes[offset++];
            temp[2] = bytes[offset++];
            temp[1] = bytes[offset++];
            temp[0] = bytes[offset++];
            return BitConverter.ToSingle(temp, 0);
        }

        private static byte[] AppOctet(IReadOnlyList<byte> bytes, int offset)
        {
            // AppTag = 0x65
            int length = bytes[offset++]; // length/value/type
            if ((offset > 0) && (length > 0))
            {
                var octet = new byte[length];
                for (var i = 0; i < length; i++)
                    octet[i] = bytes[offset++];
                return octet;
            }
            return null;
        }

        private static string AppString(byte[] bytes, int offset)
        {
            // AppTag = 0x75
            var length = bytes[offset] - 1; // length/value/type
            if ((offset > 0) && (length > 0))
                return Encoding.ASCII.GetString(bytes, offset + 2, length);
            return "???";
        }

        private static string AppSpecialString(byte[] bytes, int offset)
        {
            // AppTag = 0x74
            if (offset < 0) return "Error!";
            var length = 0;
            for (var i = offset + 1; i < offset + 255; i++)
            {
                if (bytes[i] == 0x3f) break;
                ++length;
            }
            if ((offset > 0) && (length > 0))
                return Encoding.UTF8.GetString(bytes, offset + 1, length);
            return "???";
        }
    }
}