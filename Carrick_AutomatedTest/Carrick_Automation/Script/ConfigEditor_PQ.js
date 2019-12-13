/*This page contains methods and objectes related to PQ 10 min and Free interval Page*/

//Variables
var TABCOUNT
var PANE_PQ10MIN_CHANNELS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.WinFormsObject("ucPqContinuous10Min").WinFormsObject("tabContinuous")
var BTN_REMOVE_ALL_SELECTED_QUANTITIES_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnRemoveAll
var TAB_AVAILABLE_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.activeTab_2
var BTN_ADD_QUANTITIES_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnAddSelected

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

//This function used for getting the tab counts
function GetTabCountsPq10Min()
{
  if(PANE_PQ10MIN_CHANNELS.Visible)
  {
    var activeTab = TAB_AVAILABLE_PQ10MIN.get_TabControl().get_Tabs()
    TABCOUNT = activeTab.VisibleTabsCount
    Log.Message("Visible tab count are " + TABCOUNT)
  }
}

//This function is used for selecting the tabs
function ClickOnTabPq10Min(Index)
{
  if(PANE_PQ10MIN_CHANNELS.Visible)
  { 
    var item_Index = TAB_AVAILABLE_PQ10MIN.get_TabControl().get_Tabs().get_Item(Index)
    TAB_AVAILABLE_PQ10MIN.get_TabControl().set_SelectedTab(item_Index)
    Log.Message("Tab has been selected")
    return true
  }
  else
  {
    Log.Message("PQ 10 Min panel is not visible")
    return false
  }
}


//This function used for adding RMS quantities from available quantities
function AddRMSQuantitiesPq10Min()
{   
  if(PANE_PQ10MIN_CHANNELS.Visible)
  {     
    var available_Quantities_Pq10Min = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.lstAvailable.lvQuantities.wItemList
    var item_Count = available_Quantities_Pq10Min.wItemCount
    var sap = available_Quantities_Pq10Min
    aqString.ListSeparator = sap
    for (i = 0; i < aqString.GetListLength(item_Count); i++)
    {
      var temp = aqString.GetListItem(item_Count, i)
      if(aqString.StrMatches("RMS", temp))
      {
        available_Quantities_Pq10Min.ClickItem(temp)
        available_Quantities_Pq10Min.Click()
      }
    }
    Log.Message("PQ 10 Min RMS Quantities added") 
    return true
  }
  else
  {
    Log.Message("PQ 10 Min quantities not available")
    return false
  }
}
