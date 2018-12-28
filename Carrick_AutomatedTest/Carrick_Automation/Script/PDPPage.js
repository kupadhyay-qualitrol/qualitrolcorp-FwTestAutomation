//USEUNIT CommonMethod
//USEUNIT SessionLogPage

/*This file contains methods and objects related to PDP Page*/
var DeviceManagementToolbar=CommonMethod.RibbonToolbar
var PDPContainerWorkspace=Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace
var ultraGrid = Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList.ugBaseGrid;
var EventsList = Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList


//This function is used to verify the Downloaded record in PDP
function VerifyDownloadedRecord()
{
  var REC
  if (PDPContainerWorkspace.Exists)
  {
    Log.Message("PDP window is visible")
    //22 is the Item number for column Rec.#(DFR) in PDP view
    REC=EventsList.ugBaseGrid.ActiveRow.Cells.Item(22).get_Value()
    Log.Message("Rec # is "+REC)    
    return REC
  }
  else
  {
    Log.Message("PDP window is not visible")
    return null
  }
}