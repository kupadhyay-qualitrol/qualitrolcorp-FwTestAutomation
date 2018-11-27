﻿/*This file contains test cases related to Installation of iQ+
*/

//USEUNIT CommonMethod

var InstallShieldwizard =Aliases.setup.dlgQualitrolIQInstallShieldWizard
var Btn_InstallShieldwizard_OK= Aliases.setup.dlgQualitrolIQInstallShieldWizard.btnOK
var Btn_InstallShieldwizard_Next= Aliases.setup.dlgQualitrolIQInstallShieldWizard.btnNext
var RadioBtn_InstallShieldwizard_AcceptLicense =Aliases.setup.dlgQualitrolIQInstallShieldWizard.AcceptLicense
var Btn_InstallShieldwizard_Install=Aliases.setup.dlgQualitrolIQInstallShieldWizard.btnInstall
var Btn_TaskRunner_Next=Aliases.setup.FrmTaskRunner.btnNext
var Btn_SetPassword_Next= Aliases.setup.FrmSetPasswordAndLanguage.btnNext
var Btn_Finish =Aliases.setup.dlgQualitrolIQInstallShieldWizard.btnFinish
var Edtbx_Username=Aliases.iQ_Plus.UserLogin.USRLOGINtxtUserName
var Btn_DBConfigurationAssistant_Next =Aliases.setup.FrmServiceSettings.btnNext
var Btn_DBServerSelection_Next=Aliases.setup.FrmConfigSqlServer.btnNext

//TC-Install IQ+ Application in the PC with default steps
function Install_iQ_Plus()
{
  Log.Message("Start:TC-Install IQ+ Application in the PC with default settings.")
  //Step1. Copy the latest build from the server to local path.
  var DriveName="C:\\My_Documents\\Projects\\4.15_Sprint_1\\Des1\\"
  
  if(CommonMethod.CreateDirectory(Project.Path+"Builds\\")!=null)
  {
    if(CommonMethod.LastModifiedFile(DriveName)!=CommonMethod.LastModifiedFile(Project.Path+"Builds\\"))
    {
      if(aqFile.Copy(DriveName+ "\\" + CommonMethod.LastModifiedFile(DriveName), Project.Path+"Builds\\",true))
      {
        Log.Message("File Copied Successfully")
      }
      else
      {
        Log.Message("File didn't copied")
      }
    }
    //Step2. Launch iQ+ Application Setup
    CommonMethod.LaunchApplication(Project.Path+"Builds\\"+CommonMethod.LastModifiedFile(Project.Path+"Builds\\"))
  
    //Step3.Press OK to start installation process
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_InstallShieldwizard_OK.Exists) 
    Btn_InstallShieldwizard_OK.ClickButton()
  
    //Step4.Press Next to continue Installation
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_InstallShieldwizard_Next.Exists)
    Btn_InstallShieldwizard_Next.ClickButton()
  
    //Step5.Accept License
    do
    {
      aqUtils.Delay(2000)
    }
    while (!RadioBtn_InstallShieldwizard_AcceptLicense.Exists)
    RadioBtn_InstallShieldwizard_AcceptLicense.Click()
  
    //Step6.Press Next to continue Installation
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_InstallShieldwizard_Next.Exists)
    Btn_InstallShieldwizard_Next.ClickButton()
  
    //Step7.Press Next to continue Installation
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_InstallShieldwizard_Next.Exists)
    Btn_InstallShieldwizard_Next.ClickButton()
  
    //Step8.Setup Type:-Press Next to continue Installation
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_InstallShieldwizard_Next.Exists)
    Btn_InstallShieldwizard_Next.ClickButton()
  
    //Step9.DatabaseConfiguration Assistant:-Press Next to continue Installation
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_DBConfigurationAssistant_Next.Exists)
    Btn_DBConfigurationAssistant_Next.ClickButton()
  
    //Step10.DatabaseServer Selection:-Press Next to continue Installation
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_DBServerSelection_Next.Exists)
    Btn_DBServerSelection_Next.ClickButton()
  
    //Step11. Ready to install
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_InstallShieldwizard_Install.Exists)
    Btn_InstallShieldwizard_Install.ClickButton()
  
    //Step12.Database created
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_TaskRunner_Next.Exists)
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_TaskRunner_Next.Enabled)
    Btn_TaskRunner_Next.ClickButton()
  
    //Step13.Press button Next
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_SetPassword_Next.Exists)
    Btn_SetPassword_Next.ClickButton()
  
    //Steps14.Press Finish button
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_Finish.Exists)
    Btn_Finish.ClickButton()
  
    //Step15.Check iQ+ Client is up
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Edtbx_Username.Exists)
    
    if(Edtbx_Username.Exists)
    {
      Log.Message("Application launched successfully")
    }
    else
    {
      Log.Message("Application didn't launched successfully")
    }  
    Log.Message("Completed:TC-Install IQ+ Application in the PC with default settings.")
  }
}