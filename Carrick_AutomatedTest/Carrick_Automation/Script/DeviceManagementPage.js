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
function ExportConfiguration()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    //Clear Session log  
    SessionLogPage.ClearLog()
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved successfully from device")
    aqUtils.Delay(5000)
    Item_ConfigurationEditor.SetFocus()
    Item_ConfigurationEditor.StripMainMenu.Click("Configuration|Export|As a File")
    Log.Message("Export as a file button clicked")
    export_window.SetFocus()
//    Address_bar.Click()
//    Address_bar.SetText(aqString.Trim(ConfigurationFilePath))
    ViewFileName.SetText(aqString.Trim("Configuration"))
    dlg.btnSave.Click()
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

