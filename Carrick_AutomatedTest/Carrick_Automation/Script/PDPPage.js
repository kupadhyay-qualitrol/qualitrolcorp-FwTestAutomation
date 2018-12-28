//USEUNIT CommonMethod
//USEUNIT SessionLogPage

/*This file contains methods and objects related to PDP Page*/
var DeviceManagementToolbar=CommonMethod.RibbonToolbar
var RibbonToolbar=Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top
var Activitylog = Aliases.iQ_Plus.ShellForm.windowDockingArea1.dockableWindow3.ActivityLog.ActivityMonitor.ACTYLOGtxtLog
var StartDateTime = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2;
var DateTimePicker = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2.TimeInterval.TimeIntervalControl.UserControlBase_Fill_Panel.TICtplInnerMostLayout1.TICdtpStartTime
var SetDateTime;
var NewDateTime ;
var Btn_DFRDirectory_DownloadDataNow = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnDownloadDataNow
var DFRDirectory=Aliases.iQ_Plus.SDPContainer
var DeviceStatusView = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DeviceStatusView.txtDeviceStatus
var CloseDeviceStatus = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DeviceStatusView.DEVSTATUSbtnCancel
var PDPContainerWorkspace=Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace
var ultraGrid = Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList.ugBaseGrid;
var EventsList = Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList


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

//This function is used to verify the Downloaded record in PDP
function VerifyDownloadedRecord()
{
  var REC
if (PDPContainerWorkspace.Exists)
{
Log.Message("PDP window is visible")
      REC=EventsList.ugBaseGrid.ActiveRow.Cells.Item(22).get_Value()
      //22 is the Item number for column Rec.#(DFR) in PDP view
      Log.Message("Rec # is "+REC)    
      return REC
}
    else
    {
      Log.Message("PDP window is not visible")
      return null
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
  var CurrentDateTime=aqString.SubString(DeviceStatusView.text.OleValue,CurrentDateTimePos,19)
  //289 is the start position for date in text field and 19 is the length for the selected date.
  
  var deviceCurrentDateTime = aqConvert.StrToDateTime(CurrentDateTime);
 
  Log.Message("Current Date time is" + deviceCurrentDateTime)
  
  SetDateTime = aqDateTime.AddDays(deviceCurrentDateTime,-30);
  NewDateTime = aqConvert.DateTimeToFormatStr(SetDateTime, "%d/%m/%Y %H:%M");
  
  Log.Message("Device Set Date time is"+NewDateTime)
  CloseDeviceStatus.ClickButton();
}

//This function is used to set the CurrentDateTime for the Device
function SetDeviceDateTime()
{
  if (StartDateTime.VisibleOnScreen) 
  {
    DateTimePicker.wDate=aqDateTime.SetDateElements(aqDateTime.GetYear(NewDateTime),aqDateTime.GetMonth(NewDateTime),aqDateTime.GetDay(NewDateTime))
    Log.Message("Start Date time is set for one month ahead as per the Current date time of device")
    
    EndDateTime=StartDateTime.TimeInterval.TimeIntervalControl.WinFormsObject("_UserControlBase_Toolbars_Dock_Area_Top").wItems.Item(0).Items.Item("Synchronizes End Date Time to Current Date Time").Click()
    Log.Message("End Date time is set for Current date time of PC")
  }
  else
  {
    RibbonToolbar.ClickItem("&View|[0]|&Time Interval");
    Log.Message("Time interval window open")
    if (StartDateTime.VisibleOnScreen)
    {
      SetDeviceDateTime();
    }
    else
    {
    return false;
    Log.Message("System not allowed to open time interval window")
    }
  }
}
