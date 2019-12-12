/*This page contains methods and objectes related to PQ 10 min and Free interval Page*/

//Variables
var TABCOUNT
var PANE_PQ10MIN_CHANNELS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.WinFormsObject("ucPqContinuous10Min").WinFormsObject("tabContinuous")
var BTN_REMOVE_ALL_SELECTED_QUANTITIES_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnRemoveAll
var TAB_AVAILABLE_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.activeTab_2
var AVAILABLE_QUANTITIES= Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.lstAvailable.lvQuantities


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
    var item_Index = TAB_AVAILABLE_PQ10MIN.get_TabControl().get_VisibleTabs().get_Item(Index)
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
    var item_List = AVAILABLE_QUANTITIES.wItemList;
    var sap = available_Quantities.wListSeparator;
    aqString.ListSeparator = sap; 
    for (i = 0; i < aqString.GetListLength(item_List); i++)
    {
      var temp = aqString.GetListItem(item_List, i);
      if(aqString.StrMatches("RMS", temp))
      {
        available_Quantities.ClickItem(temp);
        Btn_Add_Quantities.Click();
      }
    }
    Log.Message("PQ 10 Min RMS Quantities added") 
    return true
  }
  else
  {
    Log.Message("PQ 10 Min panel is not visible")
    return false
  }
}
