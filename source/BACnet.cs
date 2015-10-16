/**************************************************************************
*
* THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
* OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
* CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
* SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*
*********************************************************************/

/* COPYRIGHT
 -------------------------------------------
 Copyright (C) 2013-2015 Plus 1 Micro, Inc.

 This program is free software; you can redistribute it and/or
 modify it under the terms of the GNU General Public License
 as published by the Free Software Foundation; either version 2
 of the License, or (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program; if not, write to:
 The Free Software Foundation, Inc.
 59 Temple Place - Suite 330
 Boston, MA  02111-1307, USA.

 As a special exception, if other files instantiate templates or
 use macros or inline functions from this file, or you compile
 this file and link it with other works to produce a work based
 on this file, this file does not by itself cause the resulting
 work to be covered by the GNU General Public License. However
 the source code for this file must still be made available in
 accordance with section (3) of the GNU General Public License.

 This exception does not invalidate any other reasons why a work
 based on this file might be covered by the GNU General Public
 License.
 -------------------------------------------
*/

using System;
using System.Collections.Generic;

namespace BACnet
{

  //===============================================================================================
  // A .NET Implementaion of the BACnet Stack
  // Class Abstract:
  //    A BACnetStack is a Client-Server interface protocol implementation of the BACnet 
  //    Specification, allowing a connection between the Appication Layer and BACnet Devices
  //    Application Entity => Application Layer <--> BACnet Devices
  //    Specifically, it is a "BACnet User Element"
  //    Members include:
  //      Packet Creation and Processing
  //      Services (Who-Is, I-Am, ReadProperty, WriteProperty, Reject, etc)
  //      Objects (Device, etc)
  //      Network Layer Protocol
  //      Application Layer Protocol
  //      Transactions

    //-----------------------------------------------------------------------------------------------
    // BACnet Services
    class BACnetService
    {
    }

    class BACnetServiceRequest : BACnetService
    {
    }

    class BACnetServiceResponse : BACnetService
    {
    }

    class BACnetServiceIndication : BACnetService
    {
    }

    class BACnetServiceConfirm : BACnetService
    {
    }

    //-----------------------------------------------------------------------------------------------
    // BACnetTag Routines
    public static class BACnetTag
    {
        public static byte TagNumber(byte tag)
        {
            int x = ((int)tag >> 4) & 0x0F;
            return (byte)x;
        }

        public static byte Class(byte tag)
        {
            int x = ((int)tag >> 3) & 0x01;
            return (byte)x;
        }
        public static byte LenValType(byte tag)
        {
            int x = (int)tag & 0x07;
            return (byte)x;
        }
    }

    public static class BACnetData
    {
        public static List<Device> Devices;   // A list of BACnet devices after the WhoIs
        public static int DeviceIndex;        // The current BACnet device selected
        public static UInt32 PacketRetryCount;
    }


}