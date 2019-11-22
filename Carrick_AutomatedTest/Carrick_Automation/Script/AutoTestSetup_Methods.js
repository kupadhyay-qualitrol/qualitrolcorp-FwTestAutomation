/*This file contain methods related to the AutoTest tool setup for test settings
*common functionality which can be accessed by other TestScripts and are generic. 
*/
//USEUNIT AutoTestCommonMethod


//Global Variables
var ModifyTestSettingsButton = Aliases.AutoTest_00_TestShell.MainForm.splitContainer1.SplitterPanel.testPanel.groupBoxSettings.btnModifySettings
var DeviceConfigSheetButton = Aliases.AutoTest_00_TestShell.TestSettingsDiag.testSettingsControl.groupBoxSettings.btnDeviceConfigSheet
var OKButton = Aliases.AutoTest_00_TestShell.dlgOpenFile.Open_File.CtrlNotifySink.btnOK
var ConfigFileLocation = Aliases.AutoTest_00_TestShell.dlgOpenADeviceConfigurationSheet.cbxFileName.ComboBox.Edit
var OpenFileButton = Aliases.AutoTest_00_TestShell.dlgOpenADeviceConfigurationSheet.btnOpen
var ApplyTestSettingsButton = Aliases.AutoTest_00_TestShell.TestSettingsDiag.testSettingsControl.btnApply

//This method Setup Test Settings for  AutoTest application
function Setup_auto_Test()
{  
  Launch_auto_Test()
  aqUtils.Delay(2000)
  if(ModifyTestSettingsButton.Exists) {
    Log.Message("Test Settings button is visible")
    aqUtils.Delay(2000)
    ModifyTestSettingsButton.Click()
    Log.Message("Test Settings button is clicked")
  }
  
  else{
    Log.Error("Test Settings button is not available")
  }
  
  if(DeviceConfigSheetButton.Exists) {
    Log.Message("Device Config Sheet button is visible")
    aqUtils.Delay(2000)
    DeviceConfigSheetButton.Click()
    Log.Message("Device Config Sheet button is clicked")
    OKButton.Click()
    ConfigFileLocation.SetText("DeviceSheet");
    OpenFileButton.Click()
  }
  
   else{
    Log.Error("Device Config sheet button is not available")
  }
  
  if(ApplyTestSettingsButton.Exists) {
    Log.Message("Apply Test Settings Button is visible")
    ApplyTestSettingsButton.Click()
    Log.Message("Apply Test Settings Button is clicked")
  }
  
  else{
    Log.Error("Apply Test Settings Button is not available")
  }
 
   
}

