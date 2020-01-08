using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace RMSDataValidation
{
    public partial class RMSDataValidation : Form
    {
        delegate void UpdateStatusDelegate(string value);
        delegate void Mainthread(bool value);
        Stopwatch _stopWatch = new Stopwatch();
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        public RMSDataValidation()
        {
            InitializeComponent();
            StartButton.Enabled = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _stopWatch.Reset();
            _stopWatch.Start();

            _timer.Tick += Timer_Tick;
            _timer.Interval = 1000;

            ChangeState(false);
            bool isValidationPass = false;
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            Thread work = new Thread(() =>
            {
                try
                {
                    if (this.InvokeRequired)
                    {
                        UpdateStatusDelegate updateStatusDelegate = new UpdateStatusDelegate(UpdateStatus);
                        this.Invoke(updateStatusDelegate, "In Progress");
                    }

                    string filePath = FilePathTextBox.Text.ToString();
                    double voltage = Double.Parse(VoltageTextBox.Text);
                    double current = Double.Parse(CurrentTextBox.Text);
                    double voltageTolerance;
                    Double.TryParse(VoltageToleranceTextBox.Text, out voltageTolerance);
                    double currentTolerance;
                    Double.TryParse(CurrentToleranceTextBox.Text, out currentTolerance);

                    RMSValidator.RMSValidator validation = new RMSValidator.RMSValidator(filePath, voltage, current, voltageTolerance, currentTolerance);

                    if (string.IsNullOrEmpty(Type.Text))
                    {
                        validation.Validate();
                    }
                    else
                    {
                        switch (Type.Text.ToUpper())
                        {
                            case "PQ":
                                isValidationPass = validation.PQValidate();
                                break;
                            default:
                                isValidationPass = validation.Validate();
                                break;
                        }
                    }
                }
                finally
                {
                    manualResetEvent.Set();
                }
            });

            work.Start();

            Thread OnCompletion = new Thread(() =>
            {
                manualResetEvent.WaitOne();
                string result = isValidationPass ? "PASS" : "FAIL";
                if (this.InvokeRequired)
                {
                    UpdateStatusDelegate updateStatusDelegate = new UpdateStatusDelegate(UpdateStatus);
                    this.Invoke(updateStatusDelegate, result);
                }
                ChangeState(true);
            });
            OnCompletion.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Text = (_stopWatch.ElapsedMilliseconds / 1000).ToString();
        }

        void UpdateStatus(string value)
        {
            if (value == "In Progress")
            {
                _stopWatch.Start();
                _timer.Start();
            }
            else
            {
                _stopWatch.Stop();
                _timer.Stop();
            }

            ValidationResultLabel.Text = value;
        }

        void ChangeState(bool value)
        {
            if (this.InvokeRequired)
            {
                Mainthread dlgThread = new Mainthread(ChangeState);
                this.Invoke(dlgThread, value);
            }
            else
            {
                StartButton.Enabled = value;
                FilePathTextBox.Enabled = value;
                VoltageTextBox.Enabled = value;
                CurrentTextBox.Enabled = value;
                VoltageToleranceTextBox.Enabled = value;
                CurrentToleranceTextBox.Enabled = value;
            }
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
