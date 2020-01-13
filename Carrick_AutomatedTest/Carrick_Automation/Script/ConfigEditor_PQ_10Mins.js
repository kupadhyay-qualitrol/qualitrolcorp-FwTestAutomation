/*This page contains methods and objectes related to PQ 10 min Page*/

//USEUNIT CommonMethod

//Variables
var TABCOUNT
var PANE_CHANNELS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous
var BTN_REMOVE_ALL_SELECTED_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnRemoveAll
var AVAILABLE_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.lstAvailable.lvQuantities
var BTN_ADD_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnAddSelected
var TAB_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.zucPqContinuous_Toolbars_Dock_Area_Top

//


//This function is used for Taking all the selected quantities back to available quantities for PQ 10Min Page
function clickOnRemoveAllButton()
{
  if(PANE_CHANNELS.Visible)
  {
    BTN_REMOVE_ALL_SELECTED_QUANTITIES.Click()
    Log.Message("All the selected quantities are removed in PQ 10Min Page")
    return true
  }
  else
  {
    Log.Message("PQ 10Min Channels pane is not displaying")
    return false
  } 
}


//This function used for getting the tab counts from PQ 10Min Page
function getTabCountsPq10Min()
{
  if(PANE_CHANNELS.Visible)
  {
    var activeTab = PANE_CHANNELS.activeTab_2.get_TabControl().get_Tabs()
    TABCOUNT = activeTab.VisibleTabsCount
    Log.Message("Visible tab count are " + TABCOUNT)
  }
}


//This function is used for selecting the tabs in PQ 10Min Page
function clickOnTabPq10Min(Index)
{
  if(PANE_CHANNELS.Visible)
  { 
    var item_Index = PANE_CHANNELS.activeTab_2.get_TabControl().get_Tabs().get_Item(Index)
    PANE_CHANNELS.activeTab_2.get_TabControl().set_SelectedTab(item_Index)
    Log.Message("Tab has been selected in PQ 10Min Page")
    return true
  }
  else
  {
    Log.Message("PQ 10Min panel is not visible")
    return false
  }
}



//This function used for adding quantities from available quantities in PQ 10Min Page
function addQuantitiesPq10Min(quantities)
{   
  if(PANE_CHANNELS.Visible)
  {     
    var itemList = AVAILABLE_QUANTITIES.wItemList
    var sap = AVAILABLE_QUANTITIES.wListSeparator
    aqString.ListSeparator = sap
    for (counter = 0; counter < aqString.GetListLength(itemList); counter++)
    {
      var availableQuantities = aqString.GetListItem(itemList, counter)
      if(aqString.StrMatches(quantities, availableQuantities))
      {
        AVAILABLE_QUANTITIES.ClickItem(availableQuantities)
        BTN_ADD_QUANTITIES.Click()
      }
    }
    return true
  }
  else
  {    Log.Message("PQ 10 min pane is not available")
    return false
  }
}


//This function is used to click on Harmonics and Intra Harmonics on PQ 10Min Page
function clickOnAllTabsPQ10Min() 
{
  if(TAB_QUANTITIES.Exists)  
  {
    TAB_QUANTITIES.ClickItem("tbFilters|Harmonic")
    TAB_QUANTITIES.ClickItem("tbFilters|InterHarmonic")
    return true
  }

  else
  {
    Log.Message("Unable to find Quantities Tab")
    return false
  }
}
