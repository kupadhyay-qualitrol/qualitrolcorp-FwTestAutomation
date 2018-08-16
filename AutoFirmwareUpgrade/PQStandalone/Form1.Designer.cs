namespace PQStandalone
{
    partial class PQStandaloneData
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
            this.components = new System.ComponentModel.Container();
            this.DeviceIP = new System.Windows.Forms.Label();
            this.Edtbx_DeviceIP = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lbl_DataSetFilePath = new System.Windows.Forms.Label();
            this.Edtbx_CablingType = new System.Windows.Forms.TextBox();
            this.lbl_RecordStartTime = new System.Windows.Forms.Label();
            this.Edtbx_RecordStartTime = new System.Windows.Forms.TextBox();
            this.btn_ConfigureCabling = new System.Windows.Forms.Button();
            this.btn_DownloadPQData = new System.Windows.Forms.Button();
            this.PQFreeIntervalDuration = new System.Windows.Forms.Label();
            this.PQFreeIntervalDurationUnit = new System.Windows.Forms.Label();
            this.Edtbx_PQDuration = new System.Windows.Forms.TextBox();
            this.Edtbx_PQDurUnit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DeviceIP
            // 
            this.DeviceIP.AutoSize = true;
            this.DeviceIP.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.DeviceIP.Location = new System.Drawing.Point(13, 28);
            this.DeviceIP.Name = "DeviceIP";
            this.DeviceIP.Size = new System.Drawing.Size(96, 20);
            this.DeviceIP.TabIndex = 0;
            this.DeviceIP.Text = "DeviceIP :-";
            this.DeviceIP.Click += new System.EventHandler(this.DeviceIP_Click);
            // 
            // Edtbx_DeviceIP
            // 
            this.Edtbx_DeviceIP.Location = new System.Drawing.Point(223, 28);
            this.Edtbx_DeviceIP.Name = "Edtbx_DeviceIP";
            this.Edtbx_DeviceIP.Size = new System.Drawing.Size(191, 26);
            this.Edtbx_DeviceIP.TabIndex = 1;
            this.Edtbx_DeviceIP.TextChanged += new System.EventHandler(this.Edtbx_DeviceIP_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lbl_DataSetFilePath
            // 
            this.lbl_DataSetFilePath.AutoSize = true;
            this.lbl_DataSetFilePath.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbl_DataSetFilePath.Location = new System.Drawing.Point(13, 77);
            this.lbl_DataSetFilePath.Name = "lbl_DataSetFilePath";
            this.lbl_DataSetFilePath.Size = new System.Drawing.Size(123, 20);
            this.lbl_DataSetFilePath.TabIndex = 3;
            this.lbl_DataSetFilePath.Text = "CablingType :-";
            // 
            // Edtbx_CablingType
            // 
            this.Edtbx_CablingType.Location = new System.Drawing.Point(223, 74);
            this.Edtbx_CablingType.Name = "Edtbx_CablingType";
            this.Edtbx_CablingType.Size = new System.Drawing.Size(191, 26);
            this.Edtbx_CablingType.TabIndex = 4;
            this.Edtbx_CablingType.TextChanged += new System.EventHandler(this.Edtbx_PQFilePath_TextChanged);
            // 
            // lbl_RecordStartTime
            // 
            this.lbl_RecordStartTime.AutoSize = true;
            this.lbl_RecordStartTime.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbl_RecordStartTime.Location = new System.Drawing.Point(13, 132);
            this.lbl_RecordStartTime.Name = "lbl_RecordStartTime";
            this.lbl_RecordStartTime.Size = new System.Drawing.Size(161, 20);
            this.lbl_RecordStartTime.TabIndex = 5;
            this.lbl_RecordStartTime.Text = "RecordStartTime :-";
            // 
            // Edtbx_RecordStartTime
            // 
            this.Edtbx_RecordStartTime.Location = new System.Drawing.Point(223, 129);
            this.Edtbx_RecordStartTime.Name = "Edtbx_RecordStartTime";
            this.Edtbx_RecordStartTime.Size = new System.Drawing.Size(191, 26);
            this.Edtbx_RecordStartTime.TabIndex = 6;
            this.Edtbx_RecordStartTime.TextChanged += new System.EventHandler(this.Edtbx_RecordStartTime_TextChanged);
            // 
            // btn_ConfigureCabling
            // 
            this.btn_ConfigureCabling.Location = new System.Drawing.Point(17, 257);
            this.btn_ConfigureCabling.Name = "btn_ConfigureCabling";
            this.btn_ConfigureCabling.Size = new System.Drawing.Size(157, 29);
            this.btn_ConfigureCabling.TabIndex = 7;
            this.btn_ConfigureCabling.Text = "ConfigureCabling";
            this.btn_ConfigureCabling.UseVisualStyleBackColor = true;
            this.btn_ConfigureCabling.Click += new System.EventHandler(this.btn_ConfigureCabling_Click);
            // 
            // btn_DownloadPQData
            // 
            this.btn_DownloadPQData.Location = new System.Drawing.Point(213, 257);
            this.btn_DownloadPQData.Name = "btn_DownloadPQData";
            this.btn_DownloadPQData.Size = new System.Drawing.Size(191, 29);
            this.btn_DownloadPQData.TabIndex = 8;
            this.btn_DownloadPQData.Text = "DownloadPQData";
            this.btn_DownloadPQData.UseVisualStyleBackColor = true;
            this.btn_DownloadPQData.Click += new System.EventHandler(this.btn_DownloadPQData_Click);
            // 
            // PQFreeIntervalDuration
            // 
            this.PQFreeIntervalDuration.AutoSize = true;
            this.PQFreeIntervalDuration.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.PQFreeIntervalDuration.Location = new System.Drawing.Point(12, 179);
            this.PQFreeIntervalDuration.Name = "PQFreeIntervalDuration";
            this.PQFreeIntervalDuration.Size = new System.Drawing.Size(192, 20);
            this.PQFreeIntervalDuration.TabIndex = 9;
            this.PQFreeIntervalDuration.Text = "PQ Free Int. Duration:-";
            // 
            // PQFreeIntervalDurationUnit
            // 
            this.PQFreeIntervalDurationUnit.AutoSize = true;
            this.PQFreeIntervalDurationUnit.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.PQFreeIntervalDurationUnit.Location = new System.Drawing.Point(13, 216);
            this.PQFreeIntervalDurationUnit.Name = "PQFreeIntervalDurationUnit";
            this.PQFreeIntervalDurationUnit.Size = new System.Drawing.Size(230, 20);
            this.PQFreeIntervalDurationUnit.TabIndex = 10;
            this.PQFreeIntervalDurationUnit.Text = "PQ Free Int. Duration Unit:-";
            // 
            // Edtbx_PQDuration
            // 
            this.Edtbx_PQDuration.Location = new System.Drawing.Point(249, 179);
            this.Edtbx_PQDuration.Name = "Edtbx_PQDuration";
            this.Edtbx_PQDuration.Size = new System.Drawing.Size(165, 26);
            this.Edtbx_PQDuration.TabIndex = 11;
            this.Edtbx_PQDuration.TextChanged += new System.EventHandler(this.Edtbx_PQDuration_TextChanged);
            // 
            // Edtbx_PQDurUnit
            // 
            this.Edtbx_PQDurUnit.Location = new System.Drawing.Point(249, 216);
            this.Edtbx_PQDurUnit.Name = "Edtbx_PQDurUnit";
            this.Edtbx_PQDurUnit.Size = new System.Drawing.Size(165, 26);
            this.Edtbx_PQDurUnit.TabIndex = 12;
            // 
            // PQStandaloneData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 298);
            this.Controls.Add(this.Edtbx_PQDurUnit);
            this.Controls.Add(this.Edtbx_PQDuration);
            this.Controls.Add(this.PQFreeIntervalDurationUnit);
            this.Controls.Add(this.PQFreeIntervalDuration);
            this.Controls.Add(this.btn_DownloadPQData);
            this.Controls.Add(this.btn_ConfigureCabling);
            this.Controls.Add(this.Edtbx_RecordStartTime);
            this.Controls.Add(this.lbl_RecordStartTime);
            this.Controls.Add(this.Edtbx_CablingType);
            this.Controls.Add(this.lbl_DataSetFilePath);
            this.Controls.Add(this.Edtbx_DeviceIP);
            this.Controls.Add(this.DeviceIP);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "PQStandaloneData";
            this.Text = "PQStandalone";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PQStandaloneData_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DeviceIP;
        private System.Windows.Forms.TextBox Edtbx_DeviceIP;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lbl_DataSetFilePath;
        private System.Windows.Forms.TextBox Edtbx_CablingType;
        private System.Windows.Forms.Label lbl_RecordStartTime;
        private System.Windows.Forms.TextBox Edtbx_RecordStartTime;
        private System.Windows.Forms.Button btn_ConfigureCabling;
        private System.Windows.Forms.Button btn_DownloadPQData;
        private System.Windows.Forms.Label PQFreeIntervalDuration;
        private System.Windows.Forms.Label PQFreeIntervalDurationUnit;
        private System.Windows.Forms.TextBox Edtbx_PQDuration;
        private System.Windows.Forms.TextBox Edtbx_PQDurUnit;
    }
}

