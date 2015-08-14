namespace AppTest1
{
    partial class Form2
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
            this.SuspendLayout();
            // 
            // btnGetDevice
            // 
            this.btnGetDevice.Location = new System.Drawing.Point(13, 24);
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
            this.listDevices.Location = new System.Drawing.Point(12, 90);
            this.listDevices.Name = "listDevices";
            this.listDevices.Size = new System.Drawing.Size(208, 355);
            this.listDevices.TabIndex = 1;
            // 
            // btnGetProp
            // 
            this.btnGetProp.Location = new System.Drawing.Point(424, 24);
            this.btnGetProp.Name = "btnGetProp";
            this.btnGetProp.Size = new System.Drawing.Size(212, 23);
            this.btnGetProp.TabIndex = 2;
            this.btnGetProp.Text = "Get Properties";
            this.btnGetProp.UseVisualStyleBackColor = true;
            this.btnGetProp.Click += new System.EventHandler(this.btnGetProp_Click);
            // 
            // listDeviceProp
            // 
            this.listDeviceProp.FormattingEnabled = true;
            this.listDeviceProp.Location = new System.Drawing.Point(424, 90);
            this.listDeviceProp.Name = "listDeviceProp";
            this.listDeviceProp.Size = new System.Drawing.Size(212, 355);
            this.listDeviceProp.TabIndex = 3;
            // 
            // lblDevices
            // 
            this.lblDevices.AutoSize = true;
            this.lblDevices.Location = new System.Drawing.Point(13, 71);
            this.lblDevices.Name = "lblDevices";
            this.lblDevices.Size = new System.Drawing.Size(46, 13);
            this.lblDevices.TabIndex = 4;
            this.lblDevices.Text = "Devices";
            // 
            // lblDeviceProp
            // 
            this.lblDeviceProp.AutoSize = true;
            this.lblDeviceProp.Location = new System.Drawing.Point(424, 70);
            this.lblDeviceProp.Name = "lblDeviceProp";
            this.lblDeviceProp.Size = new System.Drawing.Size(91, 13);
            this.lblDeviceProp.TabIndex = 5;
            this.lblDeviceProp.Text = "Device Properties";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 459);
            this.Controls.Add(this.lblDeviceProp);
            this.Controls.Add(this.lblDevices);
            this.Controls.Add(this.listDeviceProp);
            this.Controls.Add(this.btnGetProp);
            this.Controls.Add(this.listDevices);
            this.Controls.Add(this.btnGetDevice);
            this.Name = "Form2";
            this.Text = "Form2";
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
    }
}