namespace AutoFirmwareUpgrade
{
    partial class AutoFWUpgrade
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
            this.lbl_FWBinPath = new System.Windows.Forms.Label();
            this.Edtbox_FirmwareBinPath = new System.Windows.Forms.TextBox();
            this.lbl_DeviceFile = new System.Windows.Forms.Label();
            this.Edtbx_DeviceFilePath = new System.Windows.Forms.TextBox();
            this.Btn_UpgradeFW = new System.Windows.Forms.Button();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.PQFilepath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DeviceIPAddress = new System.Windows.Forms.TextBox();
            this.Cabling3U = new System.Windows.Forms.Button();
            this.Start_Time = new System.Windows.Forms.Label();
            this.Edtbx_StartTime = new System.Windows.Forms.TextBox();
            this.DownloadPQ10min = new System.Windows.Forms.Button();
            this.CablingLabel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_FWBinPath
            // 
            this.lbl_FWBinPath.AutoSize = true;
            this.lbl_FWBinPath.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FWBinPath.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbl_FWBinPath.Location = new System.Drawing.Point(13, 13);
            this.lbl_FWBinPath.Name = "lbl_FWBinPath";
            this.lbl_FWBinPath.Size = new System.Drawing.Size(164, 19);
            this.lbl_FWBinPath.TabIndex = 0;
            this.lbl_FWBinPath.Text = "Firmware Binary Path :";
            // 
            // Edtbox_FirmwareBinPath
            // 
            this.Edtbox_FirmwareBinPath.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Edtbox_FirmwareBinPath.Location = new System.Drawing.Point(184, 11);
            this.Edtbox_FirmwareBinPath.Name = "Edtbox_FirmwareBinPath";
            this.Edtbox_FirmwareBinPath.Size = new System.Drawing.Size(191, 26);
            this.Edtbox_FirmwareBinPath.TabIndex = 1;
            this.Edtbox_FirmwareBinPath.TextChanged += new System.EventHandler(this.Edtbox_FirmwareBinPath_TextChanged);
            // 
            // lbl_DeviceFile
            // 
            this.lbl_DeviceFile.AutoSize = true;
            this.lbl_DeviceFile.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DeviceFile.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbl_DeviceFile.Location = new System.Drawing.Point(13, 51);
            this.lbl_DeviceFile.Name = "lbl_DeviceFile";
            this.lbl_DeviceFile.Size = new System.Drawing.Size(172, 19);
            this.lbl_DeviceFile.TabIndex = 2;
            this.lbl_DeviceFile.Text = "Device File Path          :  ";
            // 
            // Edtbx_DeviceFilePath
            // 
            this.Edtbx_DeviceFilePath.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Edtbx_DeviceFilePath.Location = new System.Drawing.Point(184, 44);
            this.Edtbx_DeviceFilePath.Name = "Edtbx_DeviceFilePath";
            this.Edtbx_DeviceFilePath.Size = new System.Drawing.Size(191, 26);
            this.Edtbx_DeviceFilePath.TabIndex = 3;
            this.Edtbx_DeviceFilePath.TextChanged += new System.EventHandler(this.Edtbx_DeviceFilePath_TextChanged);
            // 
            // Btn_UpgradeFW
            // 
            this.Btn_UpgradeFW.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_UpgradeFW.Location = new System.Drawing.Point(17, 97);
            this.Btn_UpgradeFW.Name = "Btn_UpgradeFW";
            this.Btn_UpgradeFW.Size = new System.Drawing.Size(168, 36);
            this.Btn_UpgradeFW.TabIndex = 4;
            this.Btn_UpgradeFW.Text = "Upgrade Firmware";
            this.Btn_UpgradeFW.UseVisualStyleBackColor = true;
            this.Btn_UpgradeFW.Click += new System.EventHandler(this.Btn_UpgradeFW_Click);
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.Location = new System.Drawing.Point(259, 97);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(51, 19);
            this.lbl_Status.TabIndex = 5;
            this.lbl_Status.Text = "Status";
            this.lbl_Status.Click += new System.EventHandler(this.lbl_Status_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Get Calc No";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PQFilepath
            // 
            this.PQFilepath.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PQFilepath.Location = new System.Drawing.Point(184, 168);
            this.PQFilepath.Name = "PQFilepath";
            this.PQFilepath.Size = new System.Drawing.Size(191, 26);
            this.PQFilepath.TabIndex = 7;
            this.PQFilepath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(12, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Device IP ";
            // 
            // DeviceIPAddress
            // 
            this.DeviceIPAddress.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceIPAddress.Location = new System.Drawing.Point(184, 212);
            this.DeviceIPAddress.Name = "DeviceIPAddress";
            this.DeviceIPAddress.Size = new System.Drawing.Size(191, 26);
            this.DeviceIPAddress.TabIndex = 10;
            this.DeviceIPAddress.TextChanged += new System.EventHandler(this.DeviceIPAddress_TextChanged);
            // 
            // Cabling3U
            // 
            this.Cabling3U.Location = new System.Drawing.Point(400, 60);
            this.Cabling3U.Name = "Cabling3U";
            this.Cabling3U.Size = new System.Drawing.Size(75, 23);
            this.Cabling3U.TabIndex = 11;
            this.Cabling3U.Text = "Cabling3U";
            this.Cabling3U.UseVisualStyleBackColor = true;
            this.Cabling3U.Click += new System.EventHandler(this.Cabling3U_Click);
            // 
            // Start_Time
            // 
            this.Start_Time.AutoSize = true;
            this.Start_Time.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_Time.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Start_Time.Location = new System.Drawing.Point(396, 14);
            this.Start_Time.Name = "Start_Time";
            this.Start_Time.Size = new System.Drawing.Size(101, 19);
            this.Start_Time.TabIndex = 13;
            this.Start_Time.Text = "Start_Time   :";
            // 
            // Edtbx_StartTime
            // 
            this.Edtbx_StartTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Edtbx_StartTime.Location = new System.Drawing.Point(522, 14);
            this.Edtbx_StartTime.Name = "Edtbx_StartTime";
            this.Edtbx_StartTime.Size = new System.Drawing.Size(191, 26);
            this.Edtbx_StartTime.TabIndex = 14;
            this.Edtbx_StartTime.TextChanged += new System.EventHandler(this.Edtbx_StartTime_TextChanged);
            // 
            // DownloadPQ10min
            // 
            this.DownloadPQ10min.Location = new System.Drawing.Point(17, 279);
            this.DownloadPQ10min.Name = "DownloadPQ10min";
            this.DownloadPQ10min.Size = new System.Drawing.Size(137, 23);
            this.DownloadPQ10min.TabIndex = 15;
            this.DownloadPQ10min.Text = "DownloadPQ10min";
            this.DownloadPQ10min.UseVisualStyleBackColor = true;
            this.DownloadPQ10min.Click += new System.EventHandler(this.DownloadPQ10min_Click);
            // 
            // CablingLabel
            // 
            this.CablingLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CablingLabel.Location = new System.Drawing.Point(184, 277);
            this.CablingLabel.Name = "CablingLabel";
            this.CablingLabel.Size = new System.Drawing.Size(191, 26);
            this.CablingLabel.TabIndex = 16;
            this.CablingLabel.TextChanged += new System.EventHandler(this.CablingLabel_TextChanged);
            // 
            // AutoFWUpgrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 314);
            this.Controls.Add(this.CablingLabel);
            this.Controls.Add(this.DownloadPQ10min);
            this.Controls.Add(this.Edtbx_StartTime);
            this.Controls.Add(this.Start_Time);
            this.Controls.Add(this.Cabling3U);
            this.Controls.Add(this.DeviceIPAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PQFilepath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.Btn_UpgradeFW);
            this.Controls.Add(this.Edtbx_DeviceFilePath);
            this.Controls.Add(this.lbl_DeviceFile);
            this.Controls.Add(this.Edtbox_FirmwareBinPath);
            this.Controls.Add(this.lbl_FWBinPath);
            this.Name = "AutoFWUpgrade";
            this.Text = "AutoFWUpgrade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_FWBinPath;
        private System.Windows.Forms.TextBox Edtbox_FirmwareBinPath;
        private System.Windows.Forms.Label lbl_DeviceFile;
        private System.Windows.Forms.TextBox Edtbx_DeviceFilePath;
        private System.Windows.Forms.Button Btn_UpgradeFW;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox PQFilepath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DeviceIPAddress;
        private System.Windows.Forms.Button Cabling3U;
        private System.Windows.Forms.Label Start_Time;
        private System.Windows.Forms.TextBox Edtbx_StartTime;
        private System.Windows.Forms.Button DownloadPQ10min;
        private System.Windows.Forms.TextBox CablingLabel;
    }
}

