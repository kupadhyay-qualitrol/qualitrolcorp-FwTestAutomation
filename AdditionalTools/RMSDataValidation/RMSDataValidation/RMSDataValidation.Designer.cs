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
            this.CurrentTextBox.Location = new System.Drawing.Point(68, 80);
            this.CurrentTextBox.Name = "CurrentTextBox";
            this.CurrentTextBox.Size = new System.Drawing.Size(47, 20);
            this.CurrentTextBox.TabIndex = 3;
            this.CurrentTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Current";
            // 
            // VoltageTextBox
            // 
            this.VoltageTextBox.Location = new System.Drawing.Point(68, 47);
            this.VoltageTextBox.Name = "VoltageTextBox";
            this.VoltageTextBox.Size = new System.Drawing.Size(48, 20);
            this.VoltageTextBox.TabIndex = 2;
            this.VoltageTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Voltage";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(68, 113);
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
            this.ValidationResultLabel.Location = new System.Drawing.Point(505, 116);
            this.ValidationResultLabel.Name = "ValidationResultLabel";
            this.ValidationResultLabel.Size = new System.Drawing.Size(62, 20);
            this.ValidationResultLabel.TabIndex = 7;
            this.ValidationResultLabel.Text = "Status";
            this.ValidationResultLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // RMSDataValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 150);
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
    }
}

