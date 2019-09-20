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
var RadioBtn_RqstDeviceToSetDateTime = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.SetTimeView.STgrpContainer.STrboRequestTimeFromDevice
var RadioBtn_ForceDeviceToSetDateTime= Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.SetTimeView.STgrpContainer.STrboForceTimeForDevice
var Btn_SetTimeOK = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.SetTimeView.STbtnOK
var Btn_SetTimeCancel =Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.SetTimeView.STbtnCancel
var Edtbx_NoOfManualDFR= Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManualTriggerView.MNLTRGgrpContainer.nudNoManualTrigger.UpDownEdit
var Btn_CloseGooseWindow = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.GooseStatusView.GSVgrpContainer.GSVbtnClose
var GooseStatusView = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.GooseStatusView
var DDRCDirectory = Aliases.iQ_Plus.SDPContainer
var Dlg_Box_CleanMemory = Aliases.iQ_Plus.ModalDialogContainer
var CleanMemory_ConfirmBox = Aliases.iQ_Plus.dlgIQ
var Btn_DDRCDirectory_DownloadNow = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnDownloadDataNow
var Btn_Yes_CleanMemory_ConfirmBox = Aliases.iQ_Plus.dlgIQ.btnYes
var Btn_CleanMemoryPopUP_Execute = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CleanMemory.cmbtnExecute
var ChckBox_DDRC_CleanMemory = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CleanMemory.grpAll.grpTriggeredEvents.CMchkDDR
var Box_Start_Time = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.PQDIRLSTVWgrpContainer.PQDIRLSTVWdtpStartTime
var Box_End_Time = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.PQDIRLSTVWgrpContainer.PQDIRLSTVWdtpEndTime
var Btn_DDRCCancel = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnCancel
//
var retryGlobal;
//This method click on FR Manual Trigger under Device & Diagnostic Test in Data Retrieval pane
function ClickOnFRManualTrigger()
{
 if(CommonMethod.RibbonToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   OpenFRManualTriggerDialog()
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

function OpenFRManualTriggerDialog()
{
  //Clear Session Log
  SessionLogPage.ClearLog()
  CommonMethod.RibbonToolbar.ClickItem("Device &Management")
  CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
  aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
  CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|FR &Manual Trigger")
  Log.Message("Clicked on FR Manual Trigger Option")
  if(Btn_ManualDFRPopUP_OK.Exists)
  {
    Log.Message("FR Manual Trigger Dialog Opens")  
    return true
  }
  else
  {
    Log.Message("FR Manual Trigger Dialog Unable to Open")  
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
function GetLastestXDFRRecordNumbers(NoOfRecords)
{
  var RecordNumber
  var RecordNumberColumnIndex 
  var LatestRecordNumberArray = new Array();
  
  DataRetrievalPage.ClickOnDFRDirectory()
  if (DFRDirectory.Exists)
  { 
    Log.Message("DFR Directory window is visible")
    RecordNumberColumnIndex=GetColumnIndexByColumnName("Record #")
    if(RecordNumberColumnIndex!=null)
    {
      for(var iterator = 0; iterator < NoOfRecords; iterator++)
      {
        RecordNumber=DirectoryList.wItem(iterator,RecordNumberColumnIndex)    
        Log.Message("Record number " + (iterator+1)  + " is :- "+RecordNumber)
        LatestRecordNumberArray.push(RecordNumber);
      } 
      return LatestRecordNumberArray;
    }
    else
    {
      Log.Message("Record Number Column Index is wrong")
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
   retryCnt =5
   do
   {
    if(CommonMethod.CheckActivityLog("Device information displayed successfully"))
    {
      break
    }
    retryCnt = retryCnt-1
    Log.Message(retryCnt)
   }
   while (retryCnt>0)
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
  Log.Message("Current Date time is" + CurrentDateTime)
  
  SetDateTime = aqDateTime.AddDays(CurrentDateTime,-1)
  NewDateTime = aqConvert.DateTimeToFormatStr(SetDateTime, "%d/%m/%Y %H:%M");
  
  Log.Message("Device SaqConvertet Date time is"+NewDateTime)
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
function TimeQualityStatusFromDeviceStatus()
{
  var TimeQuality ="TIMESYNC = "
  if(DeviceStatusView.Exists)
  {
    Log.Message("Device Status View is visible and exists")
    var ClockKeywordPos = aqString.Find(DeviceStatusView.text.OleValue,TimeQuality)
    
    var Status = aqString.SubString(DeviceStatusView.text.OleValue, aqConvert.StrToInt(ClockKeywordPos)+10,50)

    if(aqString.Find(Status,"unlocked")!=-1)
    {
      Status = 0
    }
    else if (aqString.Find(Status,"locked")!=-1)
    {
      Status =1
    }
    else
    {
      Status =null
    }
    return Status
  }
  else
  {
    Log.Message("Device Status View doesn't exists")
    return null
  }
}

//This function is used to get the CurrentDateTime for the Device
function GetDeviceActualDateTime()
{
  var aString = "CURRENTDATE = ";
 
  var CurrentDateTimePos = aqString.Find(DeviceStatusView.text.OleValue,aString)
  Log.Message(CurrentDateTimePos) 
  var CurrentDateTime=aqString.SubString(DeviceStatusView.text.OleValue,aqConvert.StrToInt(CurrentDateTimePos)+13,20)
  //289 is the start position for date in text field and 19 is the length for the selected date. 
  Log.Message("Current Date time is" + CurrentDateTime)  
  CloseDeviceStatus.ClickButton()
  return CurrentDateTime  
}

//This function is used to Forecefully/Request to Set the Device Date Time
function SetDeviceTime(ForceORRequest)
{
  if(CommonMethod.RibbonToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
  {  
    //Clear Session Log
     SessionLogPage.ClearLog()
     CommonMethod.RibbonToolbar.ClickItem("Device &Management")
     CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
     aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
     CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|&Set Time")
     Log.Message("Clicked on Set Time")
     
     if(ForceORRequest=="Force")
     {
       RadioBtn_ForceDeviceToSetDateTime.ClickButton()
       Btn_SetTimeOK.ClickButton()
       CommonMethod.CheckActivityLog("Set Date/Time Command executed successfully")
       return true
     }
     else if(ForceORRequest=="Request")
     {
       RadioBtn_RqstDeviceToSetDateTime.ClickButton()
       Btn_SetTimeOK.ClickButton()
       CommonMethod.CheckActivityLog("Set Date/Time Command executed successfully")
       return true
     }
     else
     {     
       Log.Message("No option passed in argument")
       Btn_SetTimeCancel.ClickButton()
       return false
     }
  }
  else
  {
    Log.Message("Unable to click on Set Time")
    return false
  }
}

//This method is used to set number of Manual Triggers
function SetNoOfManualTrigger(NumberofManualTrigger)
{
  if(Btn_ManualDFRPopUP_OK.Exists)
  { 
    Edtbx_NoOfManualDFR.Text=NumberofManualTrigger
    Log.Message("Able to set the value")
    return true
  }
  else
  {
    Log.Message("Unable to set the DFR value")
    return false
  }
}

function GetCOTByRecordNumber(RecordNum)
{
  var COT
  var COTColumnName 
  if (DFRDirectory.Exists)
  { 
    Log.Message("DFR Directory window is visible")
    COTColumnName=GetColumnIndexByColumnName("Cause Of Trigger")
    var RowByRecord = GetRowIndexByRecordNumber(RecordNum)
    if(COTColumnName!=null)
    {
      COT=DirectoryList.wItem(RowByRecord,COTColumnName)
      //Unselect Default
      DirectoryList.Items.Item(0).set_Selected(false)
      //Select Now as per Record Number
      DirectoryList.Items.Item(RowByRecord).set_Selected(true)
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
    Log.Message("DFR Record doesn't exist in the device.")
    return null
  }
}


//This method is used to get the Trigger Priority from DFR Directory
function GetTriggerPriorityOnDFR()
{
  var TriggerPriority
  var PriorityColumnName 

  if (DFRDirectory.Exists)
  { 
    Log.Message("DFR Directory window is visible")
    PriorityColumnName=GetColumnIndexByColumnName("Priority")
    if(PriorityColumnName!=null)
    {
      TriggerPriority=DirectoryList.wItem(0,PriorityColumnName)    
      Log.Message("Trigger Priority value is "+TriggerPriority)    
      return TriggerPriority
    }
    else
    {
      Log.Message("Column Index is wrong")
      return null
    }
  }
  else
  { 
    Log.Message("DFR Record doesn't exist in the device.")
    return null
  } 
}

function GetRowIndexByRecordNumber(RecNumber)
{
  var DFRDirectoryRow
  var tempIndex
  var RecordColumnName=GetColumnIndexByColumnName("Record #")
  for(DFRDirectoryRow=0;DFRDirectoryRow<DirectoryList.wItemCount;DFRDirectoryRow++)
  {
    if(RecNumber==DirectoryList.wItem(DFRDirectoryRow,RecordColumnName))
    {
      tempIndex= DFRDirectoryRow  
      break
    }
    else
    {
      tempIndex=null
    }
  }
  Log.Message("Index for record :- "+RecNumber+" is "+tempIndex)
  return tempIndex
}
function GetCOTForLastestXDFRRecords(NoOfRecords)
{
  var COT
  var COTColumnIndex
  var COTArray = new Array();

  DataRetrievalPage.ClickOnDFRDirectory()
  if (DFRDirectory.Exists)
  { 
    Log.Message("DFR Directory window is visible")
    COTColumnIndex=GetColumnIndexByColumnName("Cause Of Trigger")
    if(COTColumnIndex!=null)
    {
      for(var iterator = 0; iterator < NoOfRecords; iterator++)
      {
        COT=DirectoryList.wItem(iterator,COTColumnIndex) 
        COTArray.push(COT) 
        Log.Message("Cause of Trigger is "+COT) 
      }
      DataRetrievalPage.CloseDFRDirectory()
      return COTArray
    }
    else
    {
      Log.Message("COT Column Index is wrong")
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

function OpenGooseStatusWindow()
{
  if(CommonMethod.RibbonToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   SessionLogPage.ClearLog()
   CommonMethod.RibbonToolbar.ClickItem("Device &Management")
   CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
   aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
   CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|&Goose Status")
   if(CommonMethod.CheckActivityLog("Goose status configuration file is successfully downloaded")==true)
   {
     return true
   }
   else
   {
     return false
   }
 }
 else
 {
   Log.Message("Device Management options is not available")
   return false
 } 
}
function CloseGooseStatusWindow()
{
  if(GooseStatusView.Exists)
  {
    Btn_CloseGooseWindow.ClickButton()
    Log.Message("Clicked on Close button of Goose Status window")
    return true
  }
  else
  {
    Log.Message("Not able to find Goose Status window")
    return false
  }
}

function ClickOnDDRCDirectory()
{
   if(CommonMethod.RibbonToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
   {
     //Clear Session Log
     SessionLogPage.ClearLog()
     CommonMethod.RibbonToolbar.ClickItem("Device &Management")
     CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Displa&y Device Directory")
     aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
     CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Displa&y Device Directory|DD&R-C (Slow Scan) Directory")
     Log.Message("Clicked on DDDRC (Slow Scan) Directory")
     return true
    }
   else
   {
     Log.Message("Unable to click on DDRC Directory")
     return false
   }
}

function CheckDDRCDirectoryOpen(retryCount1)
{
  var recordRetryCount = 0
  for(recordRetryCount=0;recordRetryCount<retryCount1;recordRetryCount++)
  {
   if(CommonMethod.CheckActivityLog("Directory list displayed successfully for device"))
   {
     Log.Message("DDRC directory list displayed successfully") 
     return true 
     break
   }
   else(CommonMethod.CheckActivityLog("DDR-C (Slow Scan) directory list not found"))
   {
     Log.Message("Trying to click on DDRC directory button again")
     ClickOnDDRCDirectory();
   }
  }
  if(recordRetryCount>=4)
  {
    Log.Message("DDRC recording is not started")
    return false
  }
}

function GetDDRCStartTime()
{
  var startDateTime
  if (DDRCDirectory.Exists)
  {
    Log.Message("DDRC directory displayed")    
    startDateTime = Box_Start_Time.get_Value().ToString();
    Log.Message("DDRC Start date and time is "+startDateTime) 
    return startDateTime 
  }
  else
  {
    Log.Message("DDRC directory displayed")    
  }
}
function ClickOnCleanMemory()
{
  SessionLogPage.ClearLog()
  CommonMethod.RibbonToolbar.ClickItem("Device &Management")
  CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
  aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
  CommonMethod.RibbonToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|Clean Memor&y")
  Log.Message("Clicked on Clean Memory Option")
  if(Btn_CleanMemoryPopUP_Execute.Exists)
  {
    Log.Message("Clean Memory Dialog Opens")  
    return true
  }
  else
  {
    Log.Message("Clean Memory Dialog Unable to Open")  
    return false
  }
}

function CheckDDRCCheckBox()
{
  if(Dlg_Box_CleanMemory.Exists)
  {
    ChckBox_DDRC_CleanMemory.wState = cbChecked;
    Log.Message("Checked DDRC check box")
    return true
  }
  else
  {
    Log.Message("Not able to check the DDRC checked box")
    return false
  }
}
function ClickOnExecuteButton()
{
  if(Dlg_Box_CleanMemory.Exists)
  {
    Btn_CleanMemoryPopUP_Execute.ClickButton();
    if (CleanMemory_ConfirmBox.Exists)
    {
      Btn_Yes_CleanMemory_ConfirmBox.ClickButton()
    }    
    Log.Message("Clicked on Execute button")
    return true
  }
  else
  {
    Log.Message("Not able to click Execute button")
    return false
  }
}

function SetDDRCStartTime()
{ 
  if (DDRCDirectory.Exists)
  {
    Log.Message("DDRC directory displayed")  
    var currentSystemDateTime = aqDateTime.Now(); 
    var startDateTimeDDRC = aqDateTime.AddMinutes(currentSystemDateTime, -2)
    StartDateTime = Box_Start_Time.set_Value(startDateTimeDDRC)
    Log.Message("DDRC Start date and time is set as "+StartDateTime) 
    return true
  }
  else
  {
    Log.Message("DDRC directory displayed")   
    return false 
  }
}

function ClickOnDDRCDownloadNowButton()
{ 
  if (DDRCDirectory.Exists)
  {
    Log.Message("DDRC directory displayed")  
    Btn_DDRCDirectory_DownloadNow.ClickButton();
    Log.Message("DDRC Download Now button clicked") 
    CommonMethod.CheckActivityLog("DDR-C (Slow Scan) records saved successfully for device")
    return true
  }
  else
  {
    Log.Message("DDRC directory not displayed")   
    return false 
  }
}
function ClickOnDDRCCancelButton()
{ 
  if (DDRCDirectory.Exists)
  {
    Log.Message("DDRC directory displayed")  
    Btn_DDRCCancel.ClickButton();
    Log.Message("DDRC Cancel button clicked") 
    return true
  }
  else
  {
    Log.Message("DDRC directory not displayed")   
    return false 
  }
}