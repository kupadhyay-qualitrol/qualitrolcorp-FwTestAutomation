/*This page contains methods and objectes related to PQ 10 min and Free interval Page*/

//Variables
var PANE_PQ10MIN_CHANNELS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.lstAvailable.lvQuantities
var BTN_REMOVE_ALL_SELECTED_QUANTITIES_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnRemoveAll

//This function is used for Taking all the selected quantities back to available quantities
function clickOnRemoveAllButtonForPq10Min()
{
  if(PANE_PQ10MIN_CHANNELS.Visible)
  {
    BTN_REMOVE_ALL_SELECTED_QUANTITIES_PQ10MIN.Click()
    Log.Message("All the selected quantities are removed")
    return true
  }
  else
  {
    Log.Message("PQ 10 Min Channels pane is not displaying")
    return false
  } 
}