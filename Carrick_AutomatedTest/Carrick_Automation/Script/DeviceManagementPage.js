/*This file contains methods and objects variable related to device Management page
* on iQ+ UI. 
*/
//USEUNIT CommonMethod
//USEUNIT SessionLogPage
//USEUNIT ConfigEditorPage

//Variables
var DeviceManagementToolbar= CommonMethod.RibbonToolbar
var Btn_PopUpConfigCheck_No=Aliases.iQ_Plus.CheckConfigVersion.btnNo
var Btn_DFRDirectory_DownloadDataNow = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnDownloadDataNow
var dlg = Aliases.iQ_Plus.dlg;
var ViewFileName = Aliases.iQ_Plus.dlg.DUIViewWndClassName.Explorer_Pane.FloatNotifySink2.ComboBox.Edit
var Item_ConfigurationEditor = Aliases.iQ_Plus.Form
var ConfigurationDropDown = Aliases.iQ_Plus.ToolStripDropDownMenu2
var ConfigOptions = Aliases.iQ_Plus.ToolStripDropDownMenu
var Address_bar = Aliases.iQ_Plus.dlg.WorkerW.ReBarWindow32.AddressBandRoot.progress.BreadcrumbParent.toolbarAddressDBelfast
var Confirm_Save_As = Aliases.iQ_Plus.dlgConfirmSaveAs
var Save_As_Button = Aliases.iQ_Plus.dlgConfirmSaveAs.Confirm_Save_As.CtrlNotifySink.btnYes
var export_window = Aliases.iQ_Plus.dlg
var Import_window = Aliases.iQ_Plus.dlgSelectACashelMConfigurationFile
var Save_as_template = Aliases.iQ_Plus.Form2
var ok_button = Aliases.iQ_Plus.Form2.Save_As_Template.SaveAsTemplate.btnOk
var Template_Name_txt_box = Aliases.iQ_Plus.Form2.Save_As_Template.SaveAsTemplate.txtUsername
var Template = Aliases.iQ_Plus.Form3.Import_Template.ImportTemplate.gbDefaultConfigurations.lstDefaultTemplates
var Copy_button = Aliases.iQ_Plus.Form3.Import_Template.ImportTemplate.btnImport
//

//This method is to Click on Retrieve CConfiguration button
function ClickonRetrieveConfig()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    //Clear Session log  
    SessionLogPage.ClearLog()  
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved successfully from device")    
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}

//This method is to Click on Modify CConfiguration button
function ClickonModifyConfig()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    //Clear Session log  
    SessionLogPage.ClearLog()  
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|Modify Co&nfiguration")
    Log.Message("Clicked on Modify Configuration Page")
    if(Btn_PopUpConfigCheck_No.Exists)
    {
      Btn_PopUpConfigCheck_No.Click()
      Log.Message("Clicked on NO on Check Config version Popup")
    }
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}
// This method is to export config from device
function ExportConfigurationAsFile()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    //Clear Session log  
    SessionLogPage.ClearLog()
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved successfully from device")
    Item_ConfigurationEditor.SetFocus()
    Item_ConfigurationEditor.StripMainMenu.Click("Configuration|Export|As a File")
    Log.Message("Export as a file button clicked")
    Save_as_template.SetFocus()
    ViewFileName.SetText(aqString.Trim("Configuration"))
    ok_button.Click()
    if(Confirm_Save_As.Exists)
    {
      
      Save_As_Button.Click()
      Log.Message("New configuration is replaced with the previous configuration")
    }
    ConfigEditorPage.ClickOnClose()
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}
// This method is to import config from device
function ImportConfigurationAsFile()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    //Clear Session log  
    SessionLogPage.ClearLog()
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved successfully from device")
    Item_ConfigurationEditor.SetFocus()
    Item_ConfigurationEditor.StripMainMenu.Click("Configuration|Import|From File")
    Log.Message("Import from file button clicked")
    Import_window.SetFocus()
    Import_window.OpenFile("D:\\Belfast\\Configuration\\Configuration.cfg", "CFG files (*.cfg)")
    ConfigEditorPage.ClickSendToDevice()
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}
function ExportConfigurationAsTemplate()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    //Clear Session log  
    SessionLogPage.ClearLog()
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved successfully from device")
    Item_ConfigurationEditor.SetFocus()
    Item_ConfigurationEditor.StripMainMenu.Click("Configuration|Export|As a Template")
    Log.Message("Export as a template button clicked")
    Save_as_template.SetFocus()
    Template_Name_txt_box.SetText(aqString.Trim("Device_51"))
    //Template_Name_txt_box.wText(aqString.Trim("Device_51"))
    ok_button.ClickButton()
    Item_ConfigurationEditor.SetFocus()
    ConfigEditorPage.ClickOnClose()
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}

function ImportConfigurationAsTemplate()
{
 if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    //Clear Session log  
    SessionLogPage.ClearLog()
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved successfully from device")
    Item_ConfigurationEditor.SetFocus()
    Item_ConfigurationEditor.StripMainMenu.Click("Configuration|Import|From Template")
    Template.ClickItem("Device_51")
    Copy_button.ClickButton()
    ConfigEditorPage.ClickSendToDevice()
    return true
  }  
 else
 {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}