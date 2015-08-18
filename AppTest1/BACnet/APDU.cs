using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BACnet;

namespace BACnet
{

    // APDU Routines
    public static class APDU
    {
        public static byte APDUType;
        public static UInt16 ObjectType;
        public static UInt32 ObjectID;

        public static int /*APDU*/ ParseIAm(byte[] bytes, int offset)
        {
            // Look for and parse I-Am Packet
            int len = 0;
            ObjectID = 0;
            APDUType = bytes[offset];
            if ((APDUType == 0x10) && (bytes[offset + 1] == 0x00))
            {
                // Get the ObjectID
                if (BACnetTag.TagNumber(bytes[offset + 2]) != 12)
                    return 0;
                byte[] temp = new byte[4];
                temp[0] = bytes[offset + 6];
                temp[1] = bytes[offset + 5];
                temp[2] = (byte)((int)bytes[offset + 4] & 0x3F);
                temp[3] = 0;
                ObjectID = BitConverter.ToUInt32(temp, 0);
                len = 5; //PEP Make the APDU length ...
                return len;
            }
            else
                return 0;
        }

        public static int /*APDU*/ ParseRead(byte[] bytes, int offset, out int apptag)
        {
            // Look for and parse Read Property Complex ACK 
            apptag = 0xFF;
            int len = offset;
            if (bytes[len] != 0x30) return 0;   // APDU Complex ACK
            len += 2;
            if (bytes[len++] != 0x0C) return 0; // Read Property ACK

            //PEP Parse the Object ID
            //PEP 5 Bytes for Binary Object: 0x0C 0x00 0x0C 0x00 0x01
            //byte[] temp = new byte[4];
            //temp[0] = bytes[offset + 6];
            //temp[1] = bytes[offset + 5];
            //temp[2] = (byte)((int)bytes[offset + 4] & 0x3F);
            //temp[3] = 0;
            //ObjectID = BitConverter.ToUInt32(temp, 0);
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

        public static uint /*APDU*/ AppUInt(byte[] bytes, int offset)
        {
            // AppTag = 0x21
            return bytes[offset];
        }

        public static UInt16 /*APDU*/ AppUInt16(byte[] bytes, int offset)
        {
            // AppTag = 0x22
            byte[] temp = new byte[2];
            temp[1] = bytes[offset++];
            temp[0] = bytes[offset++];
            return BitConverter.ToUInt16(temp, 0);
        }

        public static UInt32 /*APDU*/ AppUInt24(byte[] bytes, int offset)
        {
            // AppTag = 0x23
            byte[] temp = new byte[4];
            temp[3] = 0;
            temp[2] = bytes[offset++];
            temp[1] = bytes[offset++];
            temp[0] = bytes[offset++];
            return BitConverter.ToUInt32(temp, 0);
        }

        public static UInt32 /*APDU*/ AppUInt32(byte[] bytes, int offset)
        {
            // AppTag = 0x24
            byte[] temp = new byte[4];
            temp[3] = bytes[offset++];
            temp[2] = bytes[offset++];
            temp[1] = bytes[offset++];
            temp[0] = bytes[offset++];
            return BitConverter.ToUInt32(temp, 0);
        }

        public static float /*APDU*/ AppSingle(byte[] bytes, int offset)
        {
            // Apptag = 0x44
            byte[] temp = new byte[4];
            temp[3] = bytes[offset++];
            temp[2] = bytes[offset++];
            temp[1] = bytes[offset++];
            temp[0] = bytes[offset++];
            return BitConverter.ToSingle(temp, 0);
        }

        public static byte[] /*APDU*/ AppOctet(byte[] bytes, int offset)
        {
            // AppTag = 0x65
            int length = bytes[offset++]; // length/value/type
            if ((offset > 0) && (length > 0))
            {
                byte[] octet = new byte[length];
                for (int i = 0; i < length; i++)
                    octet[i] = bytes[offset++];
                return octet;
            }
            else
                return null;
        }

        public static string /*APDU*/ AppString(byte[] bytes, int offset)
        {
            // AppTag = 0x75
            int length = bytes[offset] - 1; // length/value/type
            if ((offset > 0) && (length > 0))
                return Encoding.ASCII.GetString(bytes, offset + 2, length);
            else
                return "???";
        }

        public static string /*APDU*/ AppSpecialString(byte[] bytes, int offset)
        {
            // AppTag = 0x74
            if (offset < 0) return "Error!";
            int length = 0;
            for (int i = offset + 1; i < offset + 255; i++)
            {
                if (bytes[i] == 0x3f) break;
                else ++length;
            }
            if ((offset > 0) && (length > 0))
                return Encoding.UTF8.GetString(bytes, offset + 1, length);
            else
                return "???";
        }
        public static uint /*APDU*/ SetObjectID(ref byte[] bytes, uint pos,
          BACnetEnums.BACNET_OBJECT_TYPE type, uint instance)
        {
            // Assemble Object ID portion of APDU
            UInt32 value = 0;

            //PEP Context Specific Tag number could differ
            bytes[pos++] = 0x0C;  // Tag number (BACnet Object ID)
            //bytes[pos++] = 0x01;
            //bytes[pos++] = 0x00;
            //bytes[pos++] = 0x00;
            //bytes[pos++] = 0x00;

            value = (UInt32)type;
            value = value & BACnetEnums.BACNET_MAX_OBJECT;
            value = value << BACnetEnums.BACNET_INSTANCE_BITS;
            value = value | (instance & BACnetEnums.BACNET_MAX_INSTANCE);
            //len = encode_unsigned32(apdu, value);
            byte[] temp4 = new byte[4];
            temp4 = BitConverter.GetBytes(value);
            bytes[pos++] = temp4[3];
            bytes[pos++] = temp4[2];
            bytes[pos++] = temp4[1];
            bytes[pos++] = temp4[0];

            return pos;
        }

        public static uint /*APDU*/ SetPropertyID(ref byte[] bytes, uint pos,
          BACnetEnums.BACNET_PROPERTY_ID type)
        {
            // Assemble Property ID portion of APDU
            UInt32 value = (UInt32)type;
            if (value <= 255)
            {
                bytes[pos++] = 0x19;  //PEP Context Specific Tag number, could differ
                bytes[pos++] = (byte)type;
            }
            else if (value < 65535)
            {
                bytes[pos++] = 0x1A;  //PEP Context Specific Tag number, could differ
                byte[] temp2 = new byte[2];
                temp2 = BitConverter.GetBytes(value);
                bytes[pos++] = temp2[1];
                bytes[pos++] = temp2[0];
            }
            return pos;
        }

        public static uint /*APDU*/ SetArrayIdx(ref byte[] bytes, uint pos, int aidx)
        {
            // Assemble Property ID portion of APDU
            UInt32 value = (UInt32)aidx;
            if (value <= 255)
            {
                bytes[pos++] = 0x29;  //PEP Context Specific Tag number, could differ
                bytes[pos++] = (byte)aidx;
            }
            else if (value < 65535)
            {
                bytes[pos++] = 0x2A;  //PEP Context Specific Tag number, could differ
                byte[] temp2 = new byte[2];
                temp2 = BitConverter.GetBytes(value);
                bytes[pos++] = temp2[1];
                bytes[pos++] = temp2[0];
            }
            return pos;
        }

        public static uint /*APDU*/ SetProperty(ref byte[] bytes, uint pos, Property property)
        {
            // Convert property class into bytes
            int len;
            if (property != null)
            {
                bytes[pos++] = 0x3E;  // Tag Open
                switch (property.Tag)
                {
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_NULL:
                        bytes[pos++] = 0x00;
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_BOOLEAN:
                        if (property.ValueBool)
                            bytes[pos++] = 0x11;
                        else
                            bytes[pos++] = 0x10;
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_UNSIGNED_INT:
                        // Tag could be 0x21, 0x22, 0x23, or 0x24
                        // We can't do Uint64?
                        UInt32 value = (UInt32)property.ValueUInt;
                        if (value <= 255) // 1 byte
                        {
                            bytes[pos++] = 0x21;
                            bytes[pos++] = (byte)value;
                        }
                        else if (value <= 65535)  // 2 bytes
                        {
                            bytes[pos++] = 0x22;
                            byte[] temp2 = new byte[2];
                            temp2 = BitConverter.GetBytes(value);
                            bytes[pos++] = temp2[1];
                            bytes[pos++] = temp2[0];
                        }
                        else if (value <= 16777215) // 3 bytes
                        {
                            bytes[pos++] = 0x23;
                            byte[] temp3 = new byte[3];
                            temp3 = BitConverter.GetBytes(value);
                            bytes[pos++] = temp3[2];
                            bytes[pos++] = temp3[1];
                            bytes[pos++] = temp3[0];
                        }
                        else // 4 bytes
                        {
                            bytes[pos++] = 0x24;
                            byte[] temp4 = new byte[4];
                            temp4 = BitConverter.GetBytes(value);
                            bytes[pos++] = temp4[3];
                            bytes[pos++] = temp4[2];
                            bytes[pos++] = temp4[1];
                            bytes[pos++] = temp4[0];
                        }
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_SIGNED_INT:
                        // Tag could be 0x31, 0x32, 0x33, 0x34
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_REAL:
                        // Tag is 0x44
                        bytes[pos++] = 0x44;
                        byte[] temp5 = new byte[4];
                        temp5 = BitConverter.GetBytes(property.ValueSingle);
                        bytes[pos++] = temp5[3];
                        bytes[pos++] = temp5[2];
                        bytes[pos++] = temp5[1];
                        bytes[pos++] = temp5[0];
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_DOUBLE:
                        // Tag is 0x55
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_OCTET_STRING:
                        // Tag is 0x65, maximum 16 bytes!
                        bytes[pos++] = 0x65;
                        len = property.ValueOctet.Length;
                        bytes[pos++] = (byte)len;
                        for (int i = 0; i < len; i++)
                            bytes[pos++] = property.ValueOctet[i];
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_CHARACTER_STRING:
                        // Tag is 0x75, maximum 15 chars!
                        bytes[pos++] = 0x75;
                        len = property.ValueString.Length;
                        bytes[pos++] = (byte)(len + 1);  // Include character set byte
                        bytes[pos++] = 0; // ANSI
                        for (int i = 0; i < len; i++)
                            bytes[pos++] = (byte)property.ValueString[i];
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_ENUMERATED:
                        // Tag could be 0x91, 0x92, 0x93, 0x94
                        bytes[pos++] = 0x91;
                        bytes[pos++] = (byte)property.ValueEnum;
                        break;
                    case BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_OBJECT_ID:
                        // Tag is 0xC4
                        bytes[pos++] = 0xC4;
                        UInt32 id = ((UInt32)property.ValueObjectType) << 22;
                        id += (property.ValueObjectInstance & 0x3FFFFF);
                        byte[] temp6 = new byte[4];
                        temp6 = BitConverter.GetBytes(id);
                        bytes[pos++] = temp6[3];
                        bytes[pos++] = temp6[2];
                        bytes[pos++] = temp6[1];
                        bytes[pos++] = temp6[0];
                        break;
                }
                bytes[pos++] = 0x3F;  // Tag Close
            }
            return pos;
        }

        public static bool /*APDU*/ ParseProperty(ref byte[] bytes, int pos, Property property)
        {
            // Convert bytes into Property
            if (property == null) return false;
            property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_NULL;
            int tag;
            int offset = APDU.ParseRead(bytes, pos, out tag);
            if (tag == 0x21)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_UNSIGNED_INT;
                property.ValueUInt = APDU.AppUInt(bytes, offset);
                property.ToStringValue = property.ValueUInt.ToString();
            }
            else if (tag == 0x22)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_UNSIGNED_INT;
                property.ValueUInt = APDU.AppUInt16(bytes, offset);
                property.ToStringValue = property.ValueUInt.ToString();
            }
            else if (tag == 0x23)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_UNSIGNED_INT;
                property.ValueUInt = APDU.AppUInt24(bytes, offset);
                property.ToStringValue = property.ValueUInt.ToString();
            }
            else if (tag == 0x24)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_UNSIGNED_INT;
                property.ValueUInt = APDU.AppUInt32(bytes, offset);
                property.ToStringValue = property.ValueUInt.ToString();
            }
            else if (tag == 0x44)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_REAL;
                property.ValueSingle = APDU.AppSingle(bytes, offset);
                property.ToStringValue = property.ValueSingle.ToString();
            }
            else if (tag == 0x65)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_OCTET_STRING;
                property.ValueOctet = APDU.AppOctet(bytes, offset);
                //PEP Do this in the yet-to-be-written Octet Class
                string s = "";
                for (int i = 0; i < property.ValueOctet.Length; i++)
                    s = s + property.ValueOctet[i].ToString("X2");
                property.ToStringValue = s;
            }
            else if (tag == 0x74)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_CHARACTER_STRING;
                property.ValueString = APDU.AppSpecialString(bytes, offset);
                property.ToStringValue = property.ValueString;
            }
            else if (tag == 0x75)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_CHARACTER_STRING;
                property.ValueString = APDU.AppString(bytes, offset);
                property.ToStringValue = property.ValueString;
            }
            else if (tag == 0x91)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_ENUMERATED;
                property.ValueEnum = bytes[offset];
                property.ToStringValue = property.ValueEnum.ToString();

            }
            else if (tag == 0xC4)
            {
                property.Tag = BACnetEnums.BACNET_APPLICATION_TAG.BACNET_APPLICATION_TAG_OBJECT_ID;
                uint value = APDU.AppUInt32(bytes, offset);
                property.ValueObjectType = (BACnetEnums.BACNET_OBJECT_TYPE)(value >> 22);
                property.ValueObjectInstance = value & 0x3FFFFF;
                property.ToStringValue = property.ValueObjectInstance.ToString();
            }
            else
            {
                Console.WriteLine("WARNING: Unsupported property tag " + tag.ToString("X"));
            }
            return false;
        }

        public static uint /*APDU*/ SetPriority(ref byte[] bytes, uint pos, int priority)
        {
            // Convert priority into bytes
            bytes[pos++] = 0x49;  //PEP Why x49???
            bytes[pos++] = (byte)priority;
            return pos;
        }

    }
}
