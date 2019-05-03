/*This file contains test cases related to Uninstall iQ+
*/

//USEUNIT CommonMethod

//Variables
var InstallShieldwizard =Aliases.setup.dlgQualitrolIQInstallShieldWizard
var Btn_InstallShieldwizard_Yes= Aliases.setup.dlgQualitrolIQInstallShieldWizard.btnYes
var Form_Uninstall =Aliases.setup.FrmUninstall
var Chkbox_DbBackup =Aliases.setup.FrmUninstall.grBxBackData.chbxBackupDB
var Chkbox_RemoveDb =Aliases.setup.FrmUninstall.chbxRemoveDB
var Btn_Form_Next=Aliases.setup.FrmUninstall.btnNext
var Btn_TaskRunner_Next=Aliases.setup.FrmTaskRunner.btnNext
var Btn_Finish =Aliases.setup.dlgQualitrolIQInstallShieldWizard.btnFinish
//

//TC-Uninstall IQ+ Application from the PC without taking the backup of the Database.
function Uninstall_iQ_Plus()
{
  Log.Message("Start:TC-Uninstall IQ+ Application from the PC without taking the backup of the Database.")
  //Step1. Close the application before starting
  CommonMethod.Terminate_iQ_Plus()
  
  //Step2. Close the server
  CommonMethod.Terminate_iQ_Plus_Server()
  
  //Steps3. Run the uninstall application to uninstall iq+
  if(aqFile.Exists(TestedApps.Uninstall_iQ.FullFileName))
  {
    TestedApps.Uninstall_iQ.Run()
  
    //Step4. Wait till the uninstall shield opens
    do
    {
      aqUtils.Delay(1000)
    }
    while (!InstallShieldwizard.Exists)
  
    //Step5. Click on Yes to start uninstallation process
    Btn_InstallShieldwizard_Yes.ClickButton()
  
    //Step6. Wait till Form to uninstall is up
    do
    {
      aqUtils.Delay(1000)
    }
    while (!Form_Uninstall.Exists)
  
    //Step7. Uncheck DB backup option 
    if(Chkbox_DbBackup.Checked==true)
    {
      Chkbox_DbBackup.Click()
    }
  
    //Step8. Check Remove Database and files.
    if(Chkbox_RemoveDb.Checked==false)
    {
      Chkbox_RemoveDb.Click()
    }
  
    do
    {
      aqUtils.Delay(1000)
    }
    while (!Btn_Form_Next.Exists)
    //Step9. Press Next button
    Btn_Form_Next.ClickButton()
  
    do
    {
      aqUtils.Delay(1000)
    }
    while (!Btn_TaskRunner_Next.Exists)
    
    do
    {
      aqUtils.Delay(1000)
    }
    while (!Btn_TaskRunner_Next.Enabled)
    //Step10. Press Next button
    Btn_TaskRunner_Next.ClickButton()
    
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Btn_Finish.Exists)  
  
    //Step11. Press Finish Button
    Btn_Finish.ClickButton()
  }
  Log.Message("Completed:TC-Uninstall IQ+ Application from the PC without taking the backup of the Database.")
  
}