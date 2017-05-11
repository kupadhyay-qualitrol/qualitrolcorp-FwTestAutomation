//**************************************************************
// Class Name  :  ObjectRepository  
// Purpose     :  This class contains variables which contains web elements id.
// Modification History:
//  Ver #        Date         Author/Modified By       Remarks
//--------------------------------------------------------------
//   1.0        10-May-17      Rahuldev Gupta           Initial   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject
{
    public class ObjectRepository
    {
        //tabindex
        public string tab_Configuration = "//*[contains(text(),'Configuration')]";
        public string tab_Calibration = "//*[@id='TabView1']/div[1]/a[3]";
        public string tab_Records = "//*[contains(text(),'Records')]";  //Xpath locator
        public string tab_Data = "//*[contains(text(),'Data')]";  //Xpath locator
        public string Frame_LDLicense = "//*[@id='TabView1']/div[2]/div[2]/iframe";
        public string Dfr = "//*[@id='TabView1']/div[1]/a[6]";
       // public string Dfr = "//*[contains(text(),'dfr']";
        public string Frame_Control_Data = "//*[@id='TabView1']/div[2]/div[6]/iframe";
        public string Frame_Sub_Control_Data = "/html/body/iframe";
        public string Expand_Control = "/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/a";
        public string Expand_Data = "/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a";
        public string Manual_Trigger = "/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a";
        public string MTigger_Delay = "/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[3]/span/a/input";
        public string Runtimeinfo = "/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a";
        public string MTrigger_interval="/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[2]/tbody/tr/td[3]/span/a/input";
        public string MTrigger_COTText = "/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[3]/tbody/tr/td[3]/span/a/input";
        public string MTrigger_No_Trigger = "/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[4]/tbody/tr/td[3]/span/a/input";
        //public string CommitChng = "//input[@value='Commit Changes']";
        public string CoomitChng="body > div > table > tbody > tr > td:nth-child(1) > form > input[type='submit']:nth-child(1)";

        public string Frame_Rec ="//*[@id='TabView1']/div[2]/div[4]/iframe";
        public string Frame__Rec_Sub = "/html/body/iframe";
        public string Frame__Rec_DFR = "/html/body/table[1]/tbody/tr/td/table[11]/tbody/tr/td/a";
        public string Rec_No = "/html/body/table[1]/tbody/tr/td/table[11]/tbody/tr/td/div/table[2]/tbody/tr/td[2]/a[3]";
        public string Rec_DFR_Config = "//*[@id='DfrForm']/tbody/tr/td/form/a/input[1]";
        public string Rec_DFR_Data = "//*[@id='DfrForm']/tbody/tr/td/form/a/input[3]";
        public string RecordNumber = "//*[@id='RecNo']";
        public string Runtime_RecordNumber = "/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[3]/tbody/tr/td[3]/span/a/input";
        
        //tabindex//Configuration

        public string ExterTrigger ="/html/body/form/table/tbody/tr/td/table[14]/tbody/tr/td/a";
        public string Exter_Trigger_PostFault = "/html/body/form/table/tbody/tr/td/table[14]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/span/a/input";
        public string Exter_Trigger_OpLimit = "/html/body/form/table/tbody/tr/td/table[14]/tbody/tr/td/div/table[2]/tbody/tr/td[2]/span/a/input";
        public string Prefault = "/html/body/form/table/tbody/tr/td/table[17]/tbody/tr/td[2]/span/a/input";
        public string Frame_Config_LDLicense = "//*[@id='TabView1']/div[2]/div[1]/iframe";



        //UpgradePage

        public string Select_lang = "/html/body/div/form[1]/input";
        public string Frame_CPU = "/html/body/iframe[1]";
        public string Frame_DSP = "/html/body/iframe[2]";
        public string Frame_FPGA = "/html/body/iframe[3]";
        public string Frame_PIC = "/html/body/iframe[4]";
        public string Frame_MMI = "/html/body/iframe[5]";
        public string Frame_toggle = "/html/body/iframe[9]";
            
        public string Select_File = "/html/body/center/form/input[1]";
        public string Send = "/html/body/center/form/input[2]";

        public string CPU_toggle = "/html/body/center/form/table/tbody/tr[1]/td[2]/input";
        public string DSP_toggle = "/html/body/center/form/table/tbody/tr[2]/td[2]/input";


        public string ToggleAll = "/html/body/center/form/p/input";


        //mfgindex

        public string Frame_FirmwareVer = "//*[@id='TabView1']/div[2]/div[1]/iframe";
        public string Firmware_ver_info = "//*[@id='sysinfopanel']/textarea[2]";
        public string Frame_Diagnostic_info = "//*[@id='diagIframe']";

        public string Diagnostic_Info = "//*[contains(text(),'Diagnostic Information')]";
        public string CPU_Ver = "/html/body/table[2]/tbody/tr[5]/td[2]";
        public string DSP_Ver = "/html/body/table[2]/tbody/tr[7]/td[2]";
        public string FPGA_Ver = "/html/body/table[2]/tbody/tr[8]/td[2]";
        public string PIC_Ver = "/html/body/table[2]/tbody/tr[9]/td[2]";
    }
}
