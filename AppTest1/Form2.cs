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

        public Form2()
        {
            InitializeComponent();

            // Create the BACNet stack
            BACService = new Service(47808);
        }

        private void btnGetDevice_Click(object sender, EventArgs e)
        {
            List<BACnetIpDevice> bacnetDevices = BACService.FindBACnetDevices();
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
            }
        }

        private void btnGetProp_Click(object sender, EventArgs e)
        {

        }
    }
}
