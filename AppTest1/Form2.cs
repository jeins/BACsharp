using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Windows.Forms;
using AppTest1.BACnet;
using BACnet;

namespace AppTest1
{
    public partial class Form2 : Form
    {
        public IBACnetService BACService;
        public List<BACnetIpDevice> bacnetDevices;
        private BACnetIpDevice bacnetDevice;

        public Form2()
        {
            InitializeComponent();

            BACService = new Service(47808);
            bacnetDevice = null;

            btnGetProp.Enabled = false;
            btnGetDeviceObj.Enabled = false;
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
                btnGetDeviceObj.Enabled = true;
            }
        }

        private void btnGetProp_Click(object sender, EventArgs e)
        {
            BACService.FindDeviceProperties(ref bacnetDevice);
            listDeviceProp.Items.Clear();
            listDeviceProp.Items.Add("IP Address: " + bacnetDevice.IpAddress.ToString());
            listDeviceProp.Items.Add("Model Name: " + bacnetDevice.ModelName.ToString());
            listDeviceProp.Items.Add("Vendor Name: " + bacnetDevice.VendorName.ToString());
            listDeviceProp.Items.Add("Software Version: " + bacnetDevice.ApplicationSoftwareVersion.ToString());
            listDeviceProp.Items.Add("Firmware Revision: " + bacnetDevice.FirmwareRevision.ToString());
            listDeviceProp.Items.Add("Protocol Revision: " + bacnetDevice.ProtocolRevision.ToString());
            listDeviceProp.Items.Add("System Status: " + bacnetDevice.SystemStatus.ToString());
            listDeviceProp.Items.Add("Instance Number: " + bacnetDevice.InstanceNumber.ToString());
            listDeviceProp.Items.Add("Netwrok: " + bacnetDevice.Network.ToString());
            listDeviceProp.Items.Add("Object Name: " + bacnetDevice.ObjectName.ToString());
            listDeviceProp.Items.Add("Source Length: " + bacnetDevice.SourceLength.ToString());
            listDeviceProp.Items.Add("Vendor Identifier: " + bacnetDevice.VendorIdentifier.ToString());
        }

        private void listDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = listDevices.SelectedIndex;
            bacnetDevice = bacnetDevices[idx];
            lblDeviceIP.Text = bacnetDevice.IpAddress.ToString();
        }

        private void btnGetDeviceObj_Click(object sender, EventArgs e)
        {
            BACService.FindDeviceObjects(ref bacnetDevice);
            foreach (string values in bacnetDevice.DeviceObjects)
            {
                listDeviceObj.Items.Add(values);
            }
        }
    }
}
