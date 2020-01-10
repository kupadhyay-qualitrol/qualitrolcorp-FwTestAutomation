using RMSValidator;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace RMSDataValidation
{
    public partial class RMSDataValidation : Form
    {
        delegate void UpdateStatusDelegate(string progress, string errorMessage);
        delegate void Mainthread(bool value);
        Stopwatch _stopWatch = new Stopwatch();
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        const string INPROGRESS_MESSAGE = "In Progress";
        const string POWER_QUALITY = "PQ";

        public RMSDataValidation()
        {
            InitializeComponent();
            StartButton.Enabled = false;
            _timer.Tick += Timer_Tick;
            _timer.Interval = 1000;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
                       
            ChangeState(false);
            ValidationResultLabel.Text = INPROGRESS_MESSAGE;

            bool isValidationPass = false;
            string errorMessage = string.Empty;
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            Thread work = new Thread(() =>
            {
                try
                {
                    string filePath = FilePathTextBox.Text.ToString();
                    double voltage = Double.Parse(VoltageTextBox.Text);
                    double current = Double.Parse(CurrentTextBox.Text);
                    double voltageTolerance;
                    Double.TryParse(VoltageToleranceTextBox.Text, out voltageTolerance);
                    double currentTolerance;
                    Double.TryParse(CurrentToleranceTextBox.Text, out currentTolerance);

                    RMSValidator.RMSValidator validation = new RMSValidator.RMSValidator(filePath, voltage, current, voltageTolerance, currentTolerance);
                    if (!string.IsNullOrEmpty(Type.Text) && string.Compare(Type.Text, POWER_QUALITY, true) == 0)                    
                        isValidationPass = validation.PQValidate(out errorMessage);
                    else
                        isValidationPass = validation.Validate();                    
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
                string result = isValidationPass ? Constants.PASS_MESSAGE : Constants.FAIL_MESSAGE;
                string error = errorMessage;
                if (this.InvokeRequired)
                {
                    UpdateStatusDelegate updateStatusDelegate = new UpdateStatusDelegate(UpdateStatus);
                    this.Invoke(updateStatusDelegate, new object[] { result, error });
                }
                ChangeState(true);
            });
            OnCompletion.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Text = (_stopWatch.ElapsedMilliseconds / 1000).ToString();
        }

        void UpdateStatus(string progress, string errorMessage)
        {
            _stopWatch.Stop();
            _timer.Stop();
            ValidationResultLabel.Text = progress;
            ErrorMessage.Text = errorMessage;
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
                Type.Enabled = value;
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
