/*This file contains methods and objects related to Data Retrieval Page*/

//USEUNIT CommonMethod
//USEUNIT SessionLogPage

//Variables
var DeviceManagementToolbar= CommonMethod.RibbonToolbar
var DFRDirectory=Aliases.iQ_Plus.SDPContainer
var DirectoryList=Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWDFRgrpContainer.DIRLSTVWlstDFRDirectoryList
var Btn_ManualDFRPopUP_OK =Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManualTriggerView.MNLTRGgrpContainer.MNLTRGbtnOk
var Btn_DFRDirectory_Close=Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnCancel
//

//This method click on FR Manual Trigger under Device & Diagnostic Test in Data Retrieval pane
function ClickOnFRManualTrigger()
{
 if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   //Clear Session Log
   SessionLogPage.ClearLog()
   DeviceManagementToolbar.ClickItem("Device &Management")
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|FR &Manual Trigger")
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
 if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   //Clear Session Log
   SessionLogPage.ClearLog()
   DeviceManagementToolbar.ClickItem("Device &Management")
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Displa&y Device Directory")
   aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Displa&y Device Directory|&DFR Directory ")
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