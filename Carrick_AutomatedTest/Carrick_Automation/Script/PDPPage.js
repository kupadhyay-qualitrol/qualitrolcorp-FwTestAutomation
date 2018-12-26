/*This file contains methods and objects related to PDP Page*/


var DFRDirectory=Aliases.iQ_Plus.SDPContainer
var DeviceStatusView = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DeviceStatusView.txtDeviceStatus
var DirectoryList=Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWDFRgrpContainer.DIRLSTVWlstDFRDirectoryList
var Btn_ManualDFRPopUP_OK =Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManualTriggerView.MNLTRGgrpContainer.MNLTRGbtnOk
var Btn_DFRDirectory_Close=Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnCancel
var Btn_DFRDirectory_DownloadDataNow=Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnDownloadDataNow
var PDP_StatusBar=Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRusbPDPStatusBar
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
  var ColumnIndex 
if (PDPContainerWorkspace.Exists)
{
Log.Message("PDP window is visible")
      REC=EventsList.ugBaseGrid.ActiveRow.Cells.Item(22).get_Value()
      Log.Message("Rec # is "+REC)    
      return REC
    }
    else
    {
      Log.Message("Column Index is wrong")
      return null
    }
}
//This function is used to get the CurrentDateTime for the Device
function GetDeviceCurrentDateTime()
{
  var aString = "CURRENTDATE";
 
  var Temp = aqString.Find(DeviceStatusView.text.OleValue,aString)
  Log.Message(Temp) 
  var CurrentDateTime=aqString.SubString(DeviceStatusView.text.OleValue,289,19)

  
  Log.Message("Current Date time is" + CurrentDateTime)
}