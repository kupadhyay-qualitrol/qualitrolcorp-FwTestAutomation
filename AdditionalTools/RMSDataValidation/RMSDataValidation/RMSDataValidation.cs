using System;
using System.Windows.Forms;

namespace RMSDataValidation
{
    public partial class RMSDataValidation : Form
    {
        public RMSDataValidation()
        {
            InitializeComponent();
            StartButton.Enabled = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ChangeState(false);

            string filePath = FilePathTextBox.Text.ToString();
            double voltage = Double.Parse(VoltageTextBox.Text);
            double current = Double.Parse(CurrentTextBox.Text);
            double voltageTolerance = Double.Parse(VoltageToleranceTextBox.Text);
            double currentTolerance = Double.Parse(CurrentToleranceTextBox.Text);

            RMSValidator.RMSValidator local = new RMSValidator.RMSValidator(filePath, voltage, current, voltageTolerance, currentTolerance);

            bool isValidationPass = local.Validate();

            ValidationResultLabel.Text = isValidationPass ? "PASS" : "FAIL";

            ChangeState(true);
        }

        void ChangeState(bool value)
        {
            StartButton.Enabled = value;
            FilePathTextBox.Enabled = value;
            VoltageTextBox.Enabled = value;
            CurrentTextBox.Enabled = value;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Validation();
        }

        void Validation()
        {
            bool isValidated = false;

            if (!string.IsNullOrEmpty(FilePathTextBox.Text))
            {
                double local;

                if (Double.TryParse(VoltageTextBox.Text, out local))
                {
                    if (Double.TryParse(CurrentTextBox.Text, out local))
                    {
                        isValidated = true;
                    }
                }
            }

            StartButton.Enabled = isValidated;
        }
    }
}
