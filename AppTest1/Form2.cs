using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppTest1.BACnet;
using BACnet;

namespace AppTest1
{
    public partial class Form2 : Form
    {
        public IBACnetScout BACService;
        public List<BACnetIpDevice> bacnetDevices;
        private BACnetIpDevice bacnetDevice;

        public Form2()
        {
            InitializeComponent();

            // Create the BACNet stack
            BACService = new Service(47808);
            btnGetProp.Enabled = false;
            bacnetDevice = null;
        }

        private void btnGetDevice_Click(object sender, EventArgs e)
        {
            bacnetDevices = BACService.FindBACnetDevices();
            listDevices.Items.Clear();
            if (bacnetDevices.Count == 0)
            {
                MessageBox.Show("No Devices");
            }
            else
            {
                foreach (BACnetIpDevice dev in bacnetDevices)
                {
                    listDevices.Items.Add(
                      dev.VendorIdentifier.ToString() + ", " +
                      dev.Network.ToString() + ", " +
                      dev.InstanceNumber.ToString() + ", " +
                      dev.IpAddress.ToString() + ":" +
                      dev.IpAddress.Port.ToString());
                }
                btnGetProp.Enabled = true;
            }
        }

        private void btnGetProp_Click(object sender, EventArgs e)
        {
            BACService.FindDeviceProperties(ref bacnetDevice);
            listDeviceProp.Items.Clear();
            listDeviceProp.Items.Add(bacnetDevice.ModelName.ToString());
            listDeviceProp.Items.Add(bacnetDevice.VendorName.ToString());
            listDeviceProp.Items.Add(bacnetDevice.ApplicationSoftwareVersion.ToString());
            listDeviceProp.Items.Add(bacnetDevice.FirmwareRevision.ToString());
            listDeviceProp.Items.Add(bacnetDevice.ProtocolRevision.ToString());
            listDeviceProp.Items.Add(bacnetDevice.SystemStatus.ToString());
        }

        private void listDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = listDevices.SelectedIndex;
            bacnetDevice = bacnetDevices[idx];
            lblDeviceIP.Text = bacnetDevice.IpAddress.ToString();
        }
    }
}
