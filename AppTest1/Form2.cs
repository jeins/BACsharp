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
        public IBACnetStack BACStack;
        public Form2()
        {
            InitializeComponent();

            // Create the BACNet stack
            BACStack = new BACnetStack();
        }

        private void btnGetDevice_Click(object sender, EventArgs e)
        {
            BACStack.GetDevices(1);
            listDevices.Items.Clear();
            if (BACnetData.Devices.Count == 0)
            {
                MessageBox.Show("No Devices");
            }
            else
            {
                foreach (Device dev in BACnetData.Devices)
                {
                    listDevices.Items.Add(
                      dev.VendorID.ToString() + ", " +
                      dev.Network.ToString() + ", " +
                      dev.Instance.ToString() + ", " +
                      dev.ServerEP.Address.ToString() + ":" +
                      dev.ServerEP.Port.ToString());
                }
            }
        }

        private void btnGetProp_Click(object sender, EventArgs e)
        {

        }
    }
}
