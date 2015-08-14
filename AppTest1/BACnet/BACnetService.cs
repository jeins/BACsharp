using System;
using System.Collections.Generic;
using System.Net;
using AppTest1.BACnet;
using BACnet;

namespace BACnet
{
    //-----------------------------------------------------------------------------------------------
    // The Stack
    public class Service : IBACnetScout
    {
        public UInt16 UdpPort { get; set; }
        private IBACnetStack BACStack = null;

        public Service(UInt16 Port)
        {
            UdpPort = Port;
            BACStack = new BACnetStack();
        }

        public List<BACnetIpDevice> FindBACnetDevices(IPEndPoint IpAddress)
        {
            
            List<Device> FoundDevices = BACStack.GetDevices( 2000 );
            List<BACnetIpDevice> FoundIpBacnetDevices = new List<BACnetIpDevice>();
            for (UInt16 i = 0; i < FoundDevices.Count; i++)
            {
                FoundIpBacnetDevices.Add(new BACnetIpDevice(FoundDevices[i].ServerEP, FoundDevices[i].Network, FoundDevices[i].Instance, FoundDevices[i].VendorID, FoundDevices[i].SourceLength ) );

            }
        }

        public bool FindDeviceProperties(ref BACnetDevice device)
        {
            
        }

        public List<BACnetDeviceWithBBMD> FindBACnetBBMDs(IPAddress ip)
        {
            
        }

        public int IsBACnetIpDevice(IPEndPoint IpAddress)
        {
            
        }

        public bool IsBbmdEnabled(IPEndPoint IpAddress)
        {
            
        }

        public bool IsFdRegistrationSupported(IPEndPoint IpAddress)
        {
            
        }

    }




}
