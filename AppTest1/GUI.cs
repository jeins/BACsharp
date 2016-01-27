

namespace ConnectTools
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net;
    using System.Windows.Forms;

    using BACnet;

    public partial class Gui : Form
    {
        public readonly IBaCnetManager BacnetManager;
        public List<BaCnetDevice> BacnetDevices;
        private BaCnetDevice _bacnetDevice;

        public Gui()
        {
            InitializeComponent();

            BacnetManager = new BaCnetManager(System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[0]);
            _bacnetDevice = null;

            lblBBMDStatus.BackColor = Color.Red;
            lblFDRegister.BackColor = Color.Red;
            btnGetDeviceObj.Enabled = false;
            btnGetProp.Enabled = false;
        }

        private void btnGetDevice_Click(object sender, EventArgs e)
        {
            BacnetDevices = BacnetManager.FindBaCnetDevices();
            listDevices.Items.Clear();
            if (BacnetDevices.Count == 0)
            {
                MessageBox.Show("No Devices");
            }
            else
            {
                foreach (BaCnetDevice dev in BacnetDevices)
                {
                    listDevices.Items.Add(
                      dev.VendorIdentifier + ", " +
                      dev.Network + ", " +
                      dev.InstanceNumber + ", " +
                      dev.IpAddress + ":" +
                      dev.IpAddress.Port);
                }

                if (BacnetDevices.Count > 0)
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
            _bacnetDevice = BacnetDevices[listDevices.SelectedIndex]; //bacnetDevice = new BACnetDevice(CreateIPEndPoint("10.35.8.43:47808"), 0, 23, 0, 0);

            BacnetManager.FindDeviceProperties(ref _bacnetDevice);
            listDeviceProp.Items.Clear();
            listDeviceProp.Items.Add("IP Address: " + _bacnetDevice.IpAddress);
            listDeviceProp.Items.Add("Model Name: " + _bacnetDevice.ModelName);
            listDeviceProp.Items.Add("Software Version: " + _bacnetDevice.ApplicationSoftwareVersion);
            listDeviceProp.Items.Add("Firmware Revision: " + _bacnetDevice.FirmwareRevision);
            listDeviceProp.Items.Add("Protocol Revision: " + _bacnetDevice.ProtocolRevision);
            listDeviceProp.Items.Add("System Status: " + _bacnetDevice.SystemStatus);
            listDeviceProp.Items.Add("Instance Number: " + _bacnetDevice.InstanceNumber);
            listDeviceProp.Items.Add("Netwrok: " + _bacnetDevice.Network);
            listDeviceProp.Items.Add("Object Name: " + _bacnetDevice.ObjectName);
            listDeviceProp.Items.Add("Source Length: " + _bacnetDevice.SourceLength);
            listDeviceProp.Items.Add("Vendor Identifier: " + _bacnetDevice.VendorIdentifier);
        }

        private void listDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bacnetDevice = BacnetDevices[listDevices.SelectedIndex];

            lblDeviceIP.Text = _bacnetDevice.IpAddress.ToString();

            if (BacnetManager.IsBbmdEnabled(_bacnetDevice.IpAddress))
            {
                lblBBMDStatus.Text = "True";
                lblBBMDStatus.BackColor = Color.Green;
            }
            else
            {
                lblBBMDStatus.Text = "False";
                lblBBMDStatus.BackColor = Color.Red;
            }

            if (BacnetManager.IsFdRegistrationSupported(_bacnetDevice.IpAddress))
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
            _bacnetDevice = BacnetDevices[listDevices.SelectedIndex]; //bacnetDevice = new BACnetDevice(CreateIPEndPoint("10.35.8.43:47808"), 0, 23, 0, 0);

            BacnetManager.FindDeviceObjects(ref _bacnetDevice);
            lblTotalProp.Text = _bacnetDevice.DeviceObjects.Count.ToString();
            foreach (string values in _bacnetDevice.DeviceObjects)
            {
                listDeviceObj.Items.Add(values);
            }
        }
    }
}
