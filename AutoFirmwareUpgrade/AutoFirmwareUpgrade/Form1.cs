using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AutoFirmwareUpgrade
{
    
    public partial class AutoFWUpgrade : Form
    {
        public static string FWBinPath=string.Empty;
        string DeviceFilePath = string.Empty;
        string Errormessage = string.Empty;
        string[] deviceIP = new string[0];
        int i = 0;
        string line;
        string CablingDataset = string.Empty;
        
        public static string firmwareBinpathCPU = string.Empty;
        public static string CPUFileName = string.Empty;

        public static string firmwareBinpathDSP = string.Empty;
        public static string DSPFileName = string.Empty;

        public static string firmwareBinpathFPGA = string.Empty;
        public static string FPGAFileName = string.Empty;

        public static string firmwareBinpathPIC = string.Empty;
        public static string PICFilename = string.Empty;

        public static string firmwareBinpathMMI = string.Empty;
        public static string MMIFileName = string.Empty;

        
        public FirmwareUpgrade UpgradeFirmware = new FirmwareUpgrade();
        public AutoFWUpgrade()
        {
            InitializeComponent();
        }

        private void Edtbox_FirmwareBinPath_TextChanged(object sender, EventArgs e)
        {
           // FWBinPath = Edtbox_FirmwareBinPath.Text;
            //firmwareBinpathCPU = Directory.GetFiles(FWBinPath + "\\CPU").Last();
            //firmwareBinpathDSP = Directory.GetFiles(FWBinPath + "\\DSP").Last();
            //firmwareBinpathFPGA = Directory.GetFiles(FWBinPath + "\\FPGA").Last();        //Commented out as not required for Package Upgrade
            //firmwareBinpathPIC = Directory.GetFiles(FWBinPath + "\\PIC").Last();
            //firmwareBinpathMMI = Directory.GetFiles(FWBinPath + "\\MMI").Last();            
        }

        private void Edtbx_DeviceFilePath_TextChanged(object sender, EventArgs e)
        {
            //DeviceFilePath = Edtbx_DeviceFilePath.Text;
            //StreamReader file = new StreamReader(DeviceFilePath); //To read the file
            
            //while ((line = file.ReadLine()) != null)
            //{
            //    Array.Resize(ref deviceIP, deviceIP.Length + 1);
            //    deviceIP[deviceIP.Length - 1] = line;
            //}
        }

        private void lbl_Status_Click(object sender, EventArgs e)
        {
            
        }
        private void updatelabel(string label)
        {            
            if (lbl_Status.InvokeRequired)
            {
                lbl_Status.BeginInvoke((MethodInvoker)delegate() { lbl_Status.Text = label; });
            }
            else
            {
                lbl_Status.Text = label;
            }
        }

        private void EnableDisablePQ10MinButton(bool enabled)
        {
            if (DownloadPQ10min.InvokeRequired)
            {
                DownloadPQ10min.BeginInvoke((MethodInvoker)delegate () { DownloadPQ10min.Enabled = enabled; });
            }
            else
            {
                DownloadPQ10min.Enabled = enabled;
            }
        }

        private void EnableDisableCabling3U(bool enabled)
        {
            if (Cabling3U.InvokeRequired)
            {
                DownloadPQ10min.BeginInvoke((MethodInvoker)delegate () { Cabling3U.Enabled = enabled; });
            }
            else
            {
                Cabling3U.Enabled = enabled;
            }
        }

        private void Btn_UpgradeFW_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Edtbox_FirmwareBinPath.Text))
                {
                    FWBinPath = Edtbox_FirmwareBinPath.Text;
                    //firmwareBinpathCPU = Directory.GetFiles(FWBinPath + "\\CPU").Last();
                    //firmwareBinpathDSP = Directory.GetFiles(FWBinPath + "\\DSP").Last();
                    //firmwareBinpathFPGA = Directory.GetFiles(FWBinPath + "\\FPGA").Last();        //Commented out as not required for Package Upgrade
                    //firmwareBinpathPIC = Directory.GetFiles(FWBinPath + "\\PIC").Last();
                    //firmwareBinpathMMI = Directory.GetFiles(FWBinPath + "\\MMI").Last();            

                }
                else
                {
                    MessageBox.Show("Firmware Binary Path can not be empty");
                    return;
                }

                if (!String.IsNullOrEmpty(Edtbx_DeviceFilePath.Text))
                {
                    DeviceFilePath = Edtbx_DeviceFilePath.Text;
                    StreamReader file = new StreamReader(DeviceFilePath); //To read the file

                    while ((line = file.ReadLine()) != null)
                    {
                        Array.Resize(ref deviceIP, deviceIP.Length + 1);
                        deviceIP[deviceIP.Length - 1] = line;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Please add device FileName path to be upgraded");
                    return;
                }
                updatelabel( "Started" );
                this.UseWaitCursor = true;

               // FirmwareUpgrade UpgradeFirmware = new FirmwareUpgrade();

                Task.Factory.StartNew(() =>
                {
                    for (i = 0; i < deviceIP.Length; i++)
                    {

                        UpgradeFirmware.UpgradePackage(deviceIP[i]);

                        //updatelabel("Completed : " + ((int)(i + 1)).ToString() + " , Total : " + deviceIP.Length);

                    }
                   updatelabel("Completed");
                });

                this.UseWaitCursor = false;
                updatelabel("Started");
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                //Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           // UpgradeFirmware.ConfigureCabling_3U(DeviceIPAddress.Text,Fil);
            //UpgradeFirmware.GetCalcNo(PQFilepath.Text,DeviceIPAddress.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void DeviceIPAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cabling3U_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                UpgradeFirmware.ConfigureCabling_3U(DeviceIPAddress.Text,PQFilepath.Text);
                EnableDisableCabling3U(true);
            });
            Cabling3U.Enabled = false;
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
                    }

        private void Edtbx_StartTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void DownloadPQ10min_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                UpgradeFirmware.DownloadPQ10min(DeviceIPAddress.Text, Edtbx_StartTime.Text,CablingLabel.Text);
                EnableDisablePQ10MinButton(true);
            });
            DownloadPQ10min.Enabled = false;
        }

        private void CablingLabel_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
