namespace AppTest1
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetDevice = new System.Windows.Forms.Button();
            this.listDevices = new System.Windows.Forms.ListBox();
            this.btnGetProp = new System.Windows.Forms.Button();
            this.listDeviceProp = new System.Windows.Forms.ListBox();
            this.lblDevices = new System.Windows.Forms.Label();
            this.lblDeviceProp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDeviceIP = new System.Windows.Forms.Label();
            this.listDeviceObj = new System.Windows.Forms.ListBox();
            this.btnGetDeviceObj = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalProp = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBBMDStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFDRegister = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGetDevice
            // 
            this.btnGetDevice.Location = new System.Drawing.Point(13, 88);
            this.btnGetDevice.Name = "btnGetDevice";
            this.btnGetDevice.Size = new System.Drawing.Size(207, 23);
            this.btnGetDevice.TabIndex = 0;
            this.btnGetDevice.Text = "Get Devices";
            this.btnGetDevice.UseVisualStyleBackColor = true;
            this.btnGetDevice.Click += new System.EventHandler(this.btnGetDevice_Click);
            // 
            // listDevices
            // 
            this.listDevices.FormattingEnabled = true;
            this.listDevices.Location = new System.Drawing.Point(12, 137);
            this.listDevices.Name = "listDevices";
            this.listDevices.Size = new System.Drawing.Size(208, 355);
            this.listDevices.TabIndex = 1;
            this.listDevices.SelectedIndexChanged += new System.EventHandler(this.listDevices_SelectedIndexChanged);
            // 
            // btnGetProp
            // 
            this.btnGetProp.Location = new System.Drawing.Point(515, 88);
            this.btnGetProp.Name = "btnGetProp";
            this.btnGetProp.Size = new System.Drawing.Size(339, 23);
            this.btnGetProp.TabIndex = 2;
            this.btnGetProp.Text = "Get Properties";
            this.btnGetProp.UseVisualStyleBackColor = true;
            this.btnGetProp.Click += new System.EventHandler(this.btnGetProp_Click);
            // 
            // listDeviceProp
            // 
            this.listDeviceProp.FormattingEnabled = true;
            this.listDeviceProp.Location = new System.Drawing.Point(515, 137);
            this.listDeviceProp.Name = "listDeviceProp";
            this.listDeviceProp.Size = new System.Drawing.Size(339, 355);
            this.listDeviceProp.TabIndex = 3;
            // 
            // lblDevices
            // 
            this.lblDevices.AutoSize = true;
            this.lblDevices.Location = new System.Drawing.Point(13, 118);
            this.lblDevices.Name = "lblDevices";
            this.lblDevices.Size = new System.Drawing.Size(46, 13);
            this.lblDevices.TabIndex = 4;
            this.lblDevices.Text = "Devices";
            // 
            // lblDeviceProp
            // 
            this.lblDeviceProp.AutoSize = true;
            this.lblDeviceProp.Location = new System.Drawing.Point(515, 117);
            this.lblDeviceProp.Name = "lblDeviceProp";
            this.lblDeviceProp.Size = new System.Drawing.Size(91, 13);
            this.lblDeviceProp.TabIndex = 5;
            this.lblDeviceProp.Text = "Device Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Device IP/Port:";
            // 
            // lblDeviceIP
            // 
            this.lblDeviceIP.AutoSize = true;
            this.lblDeviceIP.Location = new System.Drawing.Point(437, 28);
            this.lblDeviceIP.Name = "lblDeviceIP";
            this.lblDeviceIP.Size = new System.Drawing.Size(0, 13);
            this.lblDeviceIP.TabIndex = 7;
            // 
            // listDeviceObj
            // 
            this.listDeviceObj.FormattingEnabled = true;
            this.listDeviceObj.Location = new System.Drawing.Point(266, 137);
            this.listDeviceObj.Name = "listDeviceObj";
            this.listDeviceObj.Size = new System.Drawing.Size(208, 355);
            this.listDeviceObj.TabIndex = 8;
            // 
            // btnGetDeviceObj
            // 
            this.btnGetDeviceObj.Location = new System.Drawing.Point(266, 88);
            this.btnGetDeviceObj.Name = "btnGetDeviceObj";
            this.btnGetDeviceObj.Size = new System.Drawing.Size(208, 23);
            this.btnGetDeviceObj.TabIndex = 9;
            this.btnGetDeviceObj.Text = "Get Device Objects";
            this.btnGetDeviceObj.UseVisualStyleBackColor = true;
            this.btnGetDeviceObj.Click += new System.EventHandler(this.btnGetDeviceObj_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(266, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Device Properties: ";
            // 
            // lblTotalProp
            // 
            this.lblTotalProp.AutoSize = true;
            this.lblTotalProp.Location = new System.Drawing.Point(364, 118);
            this.lblTotalProp.Name = "lblTotalProp";
            this.lblTotalProp.Size = new System.Drawing.Size(0, 13);
            this.lblTotalProp.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "BBMD Enabled ?";
            // 
            // lblBBMDStatus
            // 
            this.lblBBMDStatus.AutoSize = true;
            this.lblBBMDStatus.Location = new System.Drawing.Point(227, 51);
            this.lblBBMDStatus.Name = "lblBBMDStatus";
            this.lblBBMDStatus.Size = new System.Drawing.Size(32, 13);
            this.lblBBMDStatus.TabIndex = 13;
            this.lblBBMDStatus.Text = "False";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Foreign Device Registration?";
            // 
            // lblFDRegister
            // 
            this.lblFDRegister.AutoSize = true;
            this.lblFDRegister.Location = new System.Drawing.Point(588, 51);
            this.lblFDRegister.Name = "lblFDRegister";
            this.lblFDRegister.Size = new System.Drawing.Size(32, 13);
            this.lblFDRegister.TabIndex = 15;
            this.lblFDRegister.Text = "False";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 508);
            this.Controls.Add(this.lblFDRegister);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblBBMDStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotalProp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGetDeviceObj);
            this.Controls.Add(this.listDeviceObj);
            this.Controls.Add(this.lblDeviceIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDeviceProp);
            this.Controls.Add(this.lblDevices);
            this.Controls.Add(this.listDeviceProp);
            this.Controls.Add(this.btnGetProp);
            this.Controls.Add(this.listDevices);
            this.Controls.Add(this.btnGetDevice);
            this.Name = "BACnet";
            this.Text = "BACnet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetDevice;
        private System.Windows.Forms.ListBox listDevices;
        private System.Windows.Forms.Button btnGetProp;
        private System.Windows.Forms.ListBox listDeviceProp;
        private System.Windows.Forms.Label lblDevices;
        private System.Windows.Forms.Label lblDeviceProp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDeviceIP;
        private System.Windows.Forms.ListBox listDeviceObj;
        private System.Windows.Forms.Button btnGetDeviceObj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalProp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBBMDStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFDRegister;
    }
}