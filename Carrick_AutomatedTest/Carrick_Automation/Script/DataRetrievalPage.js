/*This file contains methods and objects related to Data Retrieval Page*/

//USEUNIT CommonMethod
//USEUNIT SessionLogPage

//Variables
var DFRDirectory=Aliases.iQ_Plus.SDPContainer
var DirectoryList=Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWDFRgrpContainer.DIRLSTVWlstDFRDirectoryList
var Btn_ManualDFRPopUP_OK =Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManualTriggerView.MNLTRGgrpContainer.MNLTRGbtnOk
var Btn_DFRDirectory_Close=Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnCancel
var Btn_DFRDirectory_DownloadDataNow=Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnDownloadDataNow
var PDP_StatusBar=Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRusbPDPStatusBar
var PDPContainerWorkspace=Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace
var ultraGrid = Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList.ugBaseGrid;
var DeviceStatusView = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DeviceStatusView.txtDeviceStatus
var CloseDeviceStatus = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DeviceStatusView.DEVSTATUSbtnCancel
var NewDateTime
var SetDateTime
//

//This method click on FR Manual Trigger under Device & Diagnostic Test in Data Retrieval pane
function ClickOnFRManualTrigger()
{
 if(CommonMethod.RibbonToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   //Clear Session Log
   SessionLogPage.ClearLog()
   CommonMethod.RibbonToolbar.ClickItem("Device &Management")
   CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
   aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
   CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|FR &Manual Trigger")
   Log.Message("Clicked on FR Manual Trigger Option")
   if(ClickonOKManualDFRTrigger())
   {
     Log.Message("Clicke on OK button on Popup menu for FR Manual Trigger")
     CommonMethod.CheckActivityLog("FR Manual Trigger Command executed successfully for device")  
     return true
   }
   else
   {
     Log.Message("Popup to click on OK button for ManualFR trigger is not available.")   
     return false
   }
 }
 else
 {
   Log.Message("Unable to Click on FR Manual Trigger")
   return false
 }
}

//This method click on DFR Directory under Display Device Directory in Data Retrieval Pane
function ClickOnDFRDirectory()
{
 if(CommonMethod.RibbonToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   //Clear Session Log
   SessionLogPage.ClearLog()
   CommonMethod.RibbonToolbar.ClickItem("Device &Management")
   CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Displa&y Device Directory")
   aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
   CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Displa&y Device Directory|&DFR Directory ")
   Log.Message("Clicked on DFR Directory")
   CommonMethod.CheckActivityLog("Directory list displayed successfully")
   return true
 }
 else
 {
   Log.Message("Unable to click on DFR Directory")
   return false
 }
}

//This method is used to Get Latest DFR record number
function GetLatestRecordnumber()
{
  var LatestRecordNumber
  var ColumnName 

  if (DFRDirectory.Exists)
  { 
    Log.Message("DFR Directory window is visible")
    ColumnName=GetColumnIndexByColumnName("Record #")
    if(ColumnName!=null)
    {
      LatestRecordNumber=DirectoryList.wItem(0,ColumnName)    
      Log.Message("Latest Record number is :- "+LatestRecordNumber)    
      return LatestRecordNumber
    }
    else
    {
      Log.Message("Column Index is wrong")
      return null
    }
  }
  else
  { 
    CommonMethod.CheckActivityLog("")
    Log.Message("DFR Record doesn't exist in the device.")
    return null
  }
}

//This method is used to get column index by column name for DFR Directory
function GetColumnIndexByColumnName(ColumnName)
{
  var DFRDirectoryColumn
  var tempIndex
  for(DFRDirectoryColumn=0;DFRDirectoryColumn<DirectoryList.Columns.Count;DFRDirectoryColumn++)
  {
    if(ColumnName==DirectoryList.Columns.Item(DFRDirectoryColumn).Text.OleValue)
    {
      tempIndex= DFRDirectoryColumn  
      break
    }
    else
    {
      tempIndex=null
    }
  }
  Log.Message("Index for "+ColumnName+" is "+tempIndex)
  return tempIndex
}

//This method is used to get the Cause of Trigger from DFR Directory
function GetCOTForLatestDFRRecord()
{
  var COT
  var ColumnName 

  if (DFRDirectory.Exists)
  { 
    Log.Message("DFR Directory window is visible")
    ColumnName=GetColumnIndexByColumnName("Cause Of Trigger")
    if(ColumnName!=null)
    {
      COT=DirectoryList.wItem(0,ColumnName)    
      Log.Message("Cause of Trigger is "+COT)    
      return COT
    }
    else
    {
      Log.Message("Column Index is wrong")
      return null
    }
  }
  else
  { 
    CommonMethod.CheckActivityLog("")
    Log.Message("DFR Record doesn't exist in the device.")
    return null
  }
  
}

//This method is used to Click on Manual DFR Trigger-Popup
function ClickonOKManualDFRTrigger()
{
  if(Btn_ManualDFRPopUP_OK.Exists)
  { 
    Btn_ManualDFRPopUP_OK.ClickButton()
    Log.Message("Clicked on OK button available on Manaul Trigger Popup.")
    return true
  }
  else
  {
    Log.Message("OK button not available on Manual Trigger Popup")
    return false
  }
}

//This method is used to close DFR Directory Popup.
function CloseDFRDirectory()
{
  if(DFRDirectory.Exists)
  {
    Btn_DFRDirectory_Close.ClickButton()
    Log.Message("Clicked on Close button on DFR Directory")
    return true
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
 if(CommonMethod.RibbonToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   SessionLogPage.ClearLog()
   CommonMethod.RibbonToolbar.ClickItem("Device &Management")
   CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
   aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
   CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|D&evice Status")
   Log.Message("Clicked on Device Status Option")
   CommonMethod.CheckActivityLog("Device information displayed successfully")
   return true
 }
 else
 {
   CommonMethod.RibbonToolbar.ClickItem("Device &Management");
   Log.Message("Device Management options is not available")
   return false
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
  return NewDateTime;
  
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

//This method is used to get TimeSync status from Device Status
function TimeQualityStatus()
{
  if(DeviceStatusView.
}
