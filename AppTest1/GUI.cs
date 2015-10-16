using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using BACnet;

namespace AppTest1
{
    public partial class GUI : Form
    {
        public IBACnetManager BACnetManager;
        public List<BACnetDevice> bacnetDevices;
        private BACnetDevice bacnetDevice;

        public GUI()
        {
            InitializeComponent();

            BACnetManager = new BACnetManager(System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[0]);
            bacnetDevice = null;

            lblBBMDStatus.BackColor = Color.Red;
            lblFDRegister.BackColor = Color.Red;
            btnGetDeviceObj.Enabled = false;
            btnGetProp.Enabled = false;
        }

        private void btnGetDevice_Click(object sender, EventArgs e)
        {
            bacnetDevices = BACnetManager.FindBACnetDevices();
            listDevices.Items.Clear();
            if (bacnetDevices.Count == 0)
            {
                MessageBox.Show("No Devices");
            }
            else
            {
                foreach (BACnetDevice dev in bacnetDevices)
                {
                    listDevices.Items.Add(
                      dev.VendorIdentifier.ToString() + ", " +
                      dev.Network.ToString() + ", " +
                      dev.InstanceNumber.ToString() + ", " +
                      dev.IpAddress.ToString() + ":" +
                      dev.IpAddress.Port.ToString());
                }

                if (bacnetDevices.Count > 0)
                {
                    btnGetDeviceObj.Enabled = true;
                    btnGetProp.Enabled = true;
                }

                btnGetProp.Enabled = true;
                btnGetDeviceObj.Enabled = true;
            }
        }

        private void btnGetProp_Click(object sender, EventArgs e)
        {
            bacnetDevice = bacnetDevices[listDevices.SelectedIndex];

            BACnetManager.FindDeviceProperties(ref bacnetDevice);
            listDeviceProp.Items.Clear();
            listDeviceProp.Items.Add("IP Address: " + bacnetDevice.IpAddress.ToString());
            listDeviceProp.Items.Add("Model Name: " + bacnetDevice.ModelName.ToString());
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
            bacnetDevice = bacnetDevices[listDevices.SelectedIndex];

            lblDeviceIP.Text = bacnetDevice.IpAddress.ToString();

            if (BACnetManager.IsBbmdEnabled(bacnetDevice.IpAddress))
            {
                lblBBMDStatus.Text = "True";
                lblBBMDStatus.BackColor = Color.Green;
            }
            else
            {
                lblBBMDStatus.Text = "False";
                lblBBMDStatus.BackColor = Color.Red;
            }

            if (BACnetManager.IsFdRegistrationSupported(bacnetDevice.IpAddress))
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
            bacnetDevice = bacnetDevices[listDevices.SelectedIndex];

            BACnetManager.FindDeviceObjects(ref bacnetDevice);
            lblTotalProp.Text = bacnetDevice.DeviceObjects.Count.ToString();
            foreach (string values in bacnetDevice.DeviceObjects)
            {
                listDeviceObj.Items.Add(values);
            }
        }
    }
}
