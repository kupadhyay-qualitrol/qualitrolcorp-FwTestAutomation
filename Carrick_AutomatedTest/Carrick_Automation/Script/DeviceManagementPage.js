/*This file contains methods and objects variable related to device Management page
* on iQ+ UI. 
*/
//USEUNIT CommonMethod
//USEUNIT SessionLogPage

//Variables
var DeviceManagementToolbar= CommonMethod.RibbonToolbar
var Btn_PopUpConfigCheck_No=Aliases.iQ_Plus.CheckConfigVersion.btnNo
var DeviceStatusView = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DeviceStatusView.txtDeviceStatus
var CloseDeviceStatus = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DeviceStatusView.DEVSTATUSbtnCancel
var Btn_DFRDirectory_DownloadDataNow = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnDownloadDataNow
var DFRDirectory=Aliases.iQ_Plus.SDPContainer
var RibbonToolbar=Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top
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

// This function is used to Click on Download button of DFR directory pop up
function ClickOnDownloadDataNow()
{
  if(DFRDirectory.Exists)
  {
    if(Btn_DFRDirectory_DownloadDataNow.Exists)
    { 
      Btn_DFRDirectory_DownloadDataNow.ClickButton()
      Log.Message("Clicked on Download Now button available on DFR directory Popup.")
      return true
    }
    else
    {
      Log.Message("Download Now button not available on DFR directory Popup")
      return false
  }
}
  else
  {
    Log.Message("DFR Directory not visible")
    return false
  }
}

//This function is used to get the Device Status View window 
function ClickOnDeviceStatusView()
{
 if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   SessionLogPage.ClearLog()
   DeviceManagementToolbar.ClickItem("Device &Management")
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
   aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|D&evice Status")
   Log.Message("Clicked on Device Status Option")
   CommonMethod.CheckActivityLog("Device information displayed successfully")
   return true
 }
 else
 {
   RibbonToolbar.ClickItem("Device &Management");
   Log.Message("Device Management options is selected")
   ClickOnDeviceStatusView();
 }
}

//This function is used to get the CurrentDateTime for the Device
function GetDeviceCurrentDateTime()
{
  var aString = "CURRENTDATE = ";
 
  var CurrentDateTimePos = aqString.Find(DeviceStatusView.text.OleValue,aString)
  Log.Message(CurrentDateTimePos) 
  var CurrentDateTime=aqString.SubString(DeviceStatusView.text.OleValue,aqConvert.StrToInt(CurrentDateTimePos)+13,19)
  //289 is the start position for date in text field and 19 is the length for the selected date.
  
  var deviceCurrentDateTime = aqConvert.StrToDateTime(CurrentDateTime);
 
  Log.Message("Current Date time is" + deviceCurrentDateTime)
  
  SetDateTime = aqDateTime.AddDays(deviceCurrentDateTime,-30);
  NewDateTime = aqConvert.DateTimeToFormatStr(SetDateTime, "%d/%m/%Y %H:%M");
  
  Log.Message("Device Set Date time is"+NewDateTime)
  CloseDeviceStatus.ClickButton();
}