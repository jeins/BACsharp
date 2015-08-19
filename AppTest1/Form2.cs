using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
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

            lblBBMDStatus.BackColor = Color.Red;
            lblFDRegister.BackColor = Color.Red;
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
            bacnetDevice = new BACnetIpDevice(CreateIPEndPoint("10.35.8.43:47808"), 0, 23, 0, 0);//bacnetDevices[idx];
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

        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }

        private void listDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = listDevices.SelectedIndex;
            bacnetDevice = bacnetDevices[idx];
            lblDeviceIP.Text = bacnetDevice.IpAddress.ToString();

            if (BACService.IsBbmdEnabled(bacnetDevice.IpAddress))
            {
                lblBBMDStatus.Text = "True";
                lblBBMDStatus.BackColor = Color.Green;
            }
            else
            {
                lblBBMDStatus.Text = "False";
                lblBBMDStatus.BackColor = Color.Red;
            }

            if (BACService.IsFdRegistrationSupported(bacnetDevice.IpAddress))
            {
                lblFDRegister.Text = "True";
                lblFDRegister.BackColor = Color.Green;
            }
            else
            {
                lblBBMDStatus.Text = "False";
                lblBBMDStatus.BackColor = Color.Red;
            }
        }

        private void btnGetDeviceObj_Click(object sender, EventArgs e)
        {
            bacnetDevice = new BACnetIpDevice(CreateIPEndPoint("10.35.8.43:47808"), 0, 23, 0, 0);//bacnetDevices[idx];
            BACService.FindDeviceObjects(ref bacnetDevice);
            lblTotalProp.Text = bacnetDevice.DeviceObjects.Count.ToString();
            foreach (string values in bacnetDevice.DeviceObjects)
            {
                listDeviceObj.Items.Add(values);
            }
        }
    }
}
