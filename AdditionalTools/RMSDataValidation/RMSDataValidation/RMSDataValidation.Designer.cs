namespace RMSDataValidation
{
    partial class RMSDataValidation
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
            this.label1 = new System.Windows.Forms.Label();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.CurrentTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VoltageTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.ValidationResultLabel = new System.Windows.Forms.Label();
            this.VoltageToleranceTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CurrentToleranceTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Type = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ErrorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Path";
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Location = new System.Drawing.Point(68, 14);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(499, 20);
            this.FilePathTextBox.TabIndex = 1;
            this.FilePathTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // CurrentTextBox
            // 
            this.CurrentTextBox.Location = new System.Drawing.Point(68, 106);
            this.CurrentTextBox.Name = "CurrentTextBox";
            this.CurrentTextBox.Size = new System.Drawing.Size(47, 20);
            this.CurrentTextBox.TabIndex = 3;
            this.CurrentTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Current";
            // 
            // VoltageTextBox
            // 
            this.VoltageTextBox.Location = new System.Drawing.Point(68, 73);
            this.VoltageTextBox.Name = "VoltageTextBox";
            this.VoltageTextBox.Size = new System.Drawing.Size(48, 20);
            this.VoltageTextBox.TabIndex = 2;
            this.VoltageTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Voltage";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(68, 139);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(99, 23);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ValidationResultLabel
            // 
            this.ValidationResultLabel.AutoSize = true;
            this.ValidationResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValidationResultLabel.Location = new System.Drawing.Point(304, 75);
            this.ValidationResultLabel.Name = "ValidationResultLabel";
            this.ValidationResultLabel.Size = new System.Drawing.Size(62, 20);
            this.ValidationResultLabel.TabIndex = 7;
            this.ValidationResultLabel.Text = "Status";
            this.ValidationResultLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // VoltageToleranceTextBox
            // 
            this.VoltageToleranceTextBox.Location = new System.Drawing.Point(231, 73);
            this.VoltageToleranceTextBox.Name = "VoltageToleranceTextBox";
            this.VoltageToleranceTextBox.Size = new System.Drawing.Size(48, 20);
            this.VoltageToleranceTextBox.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Voltage Tolerance";
            // 
            // CurrentToleranceTextBox
            // 
            this.CurrentToleranceTextBox.Location = new System.Drawing.Point(231, 106);
            this.CurrentToleranceTextBox.Name = "CurrentToleranceTextBox";
            this.CurrentToleranceTextBox.Size = new System.Drawing.Size(48, 20);
            this.CurrentToleranceTextBox.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(133, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Current Tolerance";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Location = new System.Drawing.Point(430, 43);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(0, 20);
            this.TimeLabel.TabIndex = 25;
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(304, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 20);
            this.label6.TabIndex = 26;
            this.label6.Text = "Time Elapsed";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // Type
            // 
            this.Type.Location = new System.Drawing.Point(68, 43);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(48, 20);
            this.Type.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Type";
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.AutoSize = true;
            this.ErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorMessage.Location = new System.Drawing.Point(304, 105);
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Size = new System.Drawing.Size(81, 20);
            this.ErrorMessage.TabIndex = 29;
            this.ErrorMessage.Text = "Message";
            this.ErrorMessage.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // RMSDataValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 186);
            this.Controls.Add(this.ErrorMessage);
            this.Controls.Add(this.Type);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.CurrentToleranceTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.VoltageToleranceTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ValidationResultLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.VoltageTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CurrentTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RMSDataValidation";
            this.Text = "RMS Validator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.TextBox CurrentTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox VoltageTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label ValidationResultLabel;
        private System.Windows.Forms.TextBox VoltageToleranceTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CurrentToleranceTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Type;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label ErrorMessage;
    }
}

