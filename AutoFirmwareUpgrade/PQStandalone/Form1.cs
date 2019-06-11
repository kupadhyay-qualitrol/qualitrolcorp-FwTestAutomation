using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CashelFirmware.Reporting;

namespace PQStandalone
{
    public partial class PQStandaloneData : Form
    {
        
        public PQStandaloneData()
        {
            InitializeComponent();
            ReportGeneration.StartReport_dotNet("PQStandalone_"+ DateTime.Now.ToString(@"dd_MM_yyyy_hh_mm_ss"));
        }

        private void DeviceIP_Click(object sender, EventArgs e)
        {

        }

        private void Edtbx_DeviceIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void EnableDisableConfigureCablingButton(bool enabled)
        {
            if (btn_ConfigureCabling.InvokeRequired)
            {
                btn_ConfigureCabling.BeginInvoke((MethodInvoker)delegate () { btn_ConfigureCabling.Enabled = enabled; });
            }
            else
            {
                btn_ConfigureCabling.Enabled = enabled;
            }
        }

        private void btn_ConfigureCabling_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                PQStandaloneDataConfiguration Cabling = new PQStandaloneDataConfiguration();
                Cabling.ConfigureCabling(Edtbx_DeviceIP.Text, Edtbx_CablingType.Text, Edtbx_PQDuration.Text, Edtbx_PQDurUnit.Text);
                EnableDisableConfigureCablingButton(true);
            });
            EnableDisableConfigureCablingButton(false);

        }

        private void Edtbx_PQFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void Edtbx_RecordStartTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void EnableDisableDownloadPQButton(bool enabled)
        {
            if (btn_DownloadPQData.InvokeRequired)
            {
                btn_DownloadPQData.BeginInvoke((MethodInvoker)delegate () { btn_DownloadPQData.Enabled = enabled; });
            }
            else
            {
                btn_DownloadPQData.Enabled = enabled;
            }
        }

        private void btn_DownloadPQData_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                PQStandaloneDataConfiguration Cabling = new PQStandaloneDataConfiguration(Edtbx_CablingType.Text);
                Cabling.DonwloadPQData(Edtbx_DeviceIP.Text, Edtbx_CablingType.Text, Edtbx_RecordStartTime.Text);
                EnableDisableDownloadPQButton(true);
            });
            EnableDisableDownloadPQButton(false);
        }

        private void PQStandaloneData_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReportGeneration.EndReprot();
        }

        private void Edtbx_PQDuration_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
