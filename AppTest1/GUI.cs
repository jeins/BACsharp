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

namespace ConnectTools
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using BACnet;

    public partial class Gui : Form
    {
        private readonly IBacnetManager _bacnetManager;
        private List<BacnetDevice> _bacnetDevices;
        private BacnetDevice _bacnetDevice;


        /// <summary>
        /// Initializes a new instance of the <see cref="Gui"/> class.
        /// </summary>
        public Gui()
        {
            InitializeComponent();

            _bacnetManager = new BacnetManager(System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[0]);
            _bacnetDevice = null;

            lblBBMDStatus.BackColor = Color.Red;
            lblFDRegister.BackColor = Color.Red;
            btnGetDeviceObj.Enabled = false;
            btnGetProp.Enabled = false;
        }

        private void btnGetDevice_Click(object sender, EventArgs e)
        {
            _bacnetDevices = _bacnetManager.FindBaCnetDevices();
            listDevices.Items.Clear();
            if (_bacnetDevices.Count == 0)
            {
                MessageBox.Show("No Devices");
            }
            else
            {
                foreach (BacnetDevice dev in _bacnetDevices)
                {
                    listDevices.Items.Add(
                      dev.VendorIdentifier + ", " +
                      dev.Network + ", " +
                      dev.InstanceNumber + ", " +
                      dev.IpAddress + ":" +
                      dev.IpAddress.Port);
                }

                if (_bacnetDevices.Count > 0)
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
            _bacnetDevice = _bacnetDevices[listDevices.SelectedIndex]; 

            _bacnetManager.FindDeviceProperties(ref _bacnetDevice);
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
            _bacnetDevice = _bacnetDevices[listDevices.SelectedIndex];

            lblDeviceIP.Text = _bacnetDevice.IpAddress.ToString();

            if (_bacnetManager.IsBbmdEnabled(_bacnetDevice.IpAddress))
            {
                lblBBMDStatus.Text = @"True";
                lblBBMDStatus.BackColor = Color.Green;
            }
            else
            {
                lblBBMDStatus.Text = @"False";
                lblBBMDStatus.BackColor = Color.Red;
            }

            if (_bacnetManager.IsFdRegistrationSupported(_bacnetDevice.IpAddress))
            {
                lblFDRegister.Text = @"True";
                lblFDRegister.BackColor = Color.Green;
            }
            else
            {
                lblBBMDStatus.Text = @"False";
                lblBBMDStatus.BackColor = Color.Red;
            }
        }

        private void btnGetDeviceObj_Click(object sender, EventArgs e)
        {
            _bacnetDevice = _bacnetDevices[listDevices.SelectedIndex]; 

            _bacnetManager.FindDeviceObjects(ref _bacnetDevice);

            lblTotalProp.Text = _bacnetDevice.DeviceObjects.Count.ToString();

            listDeviceObj.Items.Clear();

            foreach (string values in _bacnetDevice.DeviceObjects)
            {
                listDeviceObj.Items.Add(values);
            }
        }
    }
}
