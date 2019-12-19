/*This page contains methods and objectes related to PQ 10 min and Free interval Page*/

//Variables
var PQFREEINTERVAL_TABCOUNT
//var PANE_PQ10MIN_CHANNELS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.WinFormsObject("ucPqContinuous10Min").WinFormsObject("tabContinuous")
//var BTN_REMOVE_ALL_SELECTED_QUANTITIES_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnRemoveAll
//var TAB_AVAILABLE_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.activeTab_2
//var BTN_ADD_QUANTITIES_PQ10MIN = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnAddSelected
var PANE_PQFREEINTERVAL_CHANNELS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous
var BTN_REMOVE_ALL_SELECTED_QUANTITIES_PQFREEINTERVAL = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous.pg.pqQuantities.frmPanel.btnRemoveAll
var AVAILABLE_QUANTITIES_PQFREEINTERVAL = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous.pg.pqQuantities.frmPanel.lstAvailable.lvQuantities
var BTN_ADD_QUANTITIES_PQFREEINTERVAL = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous.pg.pqQuantities.frmPanel.btnAddSelected
var TAB_QUANTITIES_PQFREEINTERVAL = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous.pg.pqQuantities.zucPqContinuous_Toolbars_Dock_Area_Top
var PANE_CONFIG_PQFREEINTERVAL = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane

//This function is used for Taking all the selected quantities back to available quantities for PQ Free Interval Page
function clickOnRemoveAllButtonForPqFreeIntervalMin()
{
  if(PANE_PQFREEINTERVAL_CHANNELS.Visible)
  {
    BTN_REMOVE_ALL_SELECTED_QUANTITIES_PQFREEINTERVAL.Click()
    Log.Message("All the selected quantities are removed in PQ Free Interval Page")
    return true
  }
  else
  {
    Log.Message("PQ Free Interval Channels pane is not displaying")
    return false
  } 
}


//This function used for getting the tab counts from PQ Free Interval Page
function getTabCountsPqFreeInterval()
{
  if(PANE_PQFREEINTERVAL_CHANNELS.Visible)
  {
    var activeTab = PANE_PQFREEINTERVAL_CHANNELS.activeTab_2.get_TabControl().get_Tabs()
    PQFREEINTERVAL_TABCOUNT = activeTab.VisibleTabsCount
    Log.Message("Visible tab count are " + PQFREEINTERVAL_TABCOUNT)
  }
}


//This function is used for selecting the tabs in PQ Free Interval Page
function clickOnTabPqFreeInterval(Index)
{
  if(PANE_PQFREEINTERVAL_CHANNELS.Visible)
  { 
    var item_Index = PANE_PQFREEINTERVAL_CHANNELS.activeTab_2.get_TabControl().get_Tabs().get_Item(Index)
    PANE_PQFREEINTERVAL_CHANNELS.activeTab_2.get_TabControl().set_SelectedTab(item_Index)
    Log.Message("Tab has been selected in PQ Free Interval Page")
    return true
  }
  else
  {
    Log.Message("PQ Free Interval panel is not visible")
    return false
  }
}



//This function used for adding RMS quantities from available quantities in PQ Free Interval Page
function addRMSQuantitiesPqFreeInterval()
{   
  if(PANE_PQFREEINTERVAL_CHANNELS.Visible)
  {     
    var item_List = AVAILABLE_QUANTITIES_PQFREEINTERVAL.wItemList
    var sap = AVAILABLE_QUANTITIES_PQFREEINTERVAL.wListSeparator
    aqString.ListSeparator = sap
    for (i = 0; i < aqString.GetListLength(item_List); i++)
    {
      var temp = aqString.GetListItem(item_List, i)
      if(aqString.StrMatches("RMS", temp))
      {
        AVAILABLE_QUANTITIES_PQFREEINTERVAL.ClickItem(temp)
        BTN_ADD_QUANTITIES_PQFREEINTERVAL.Click()
      }
    }
    for (i = 0; i < aqString.GetListLength(item_List); i++)
    {
      var temp = aqString.GetListItem(item_List, i)
      if(aqString.StrMatches("H01", temp))
      {
        AVAILABLE_QUANTITIES_PQFREEINTERVAL.ClickItem(temp)
        BTN_ADD_QUANTITIES_PQFREEINTERVAL.Click()
      }
    }
    
    for (i = 0; i < aqString.GetListLength(item_List); i++)
    {
      var temp = aqString.GetListItem(item_List, i)
      if(aqString.StrMatches("IH01", temp))
      {
        AVAILABLE_QUANTITIES_PQFREEINTERVAL.ClickItem(temp)
        BTN_ADD_QUANTITIES_PQFREEINTERVAL.Click()
      }
    }
    Log.Message("PQ Free Interval RMS Quantities added") 
    return true
  }
  else
  {    Log.Message("PQ Free Interval Min quantities not available")
    return false
  }
}

//This function is used to click on Harmonics and Intra Harmonics on PQ Free Interval Page
function clickOnHarmonicsIntraharmonicsTabPqFreeInterval() {
 if(TAB_QUANTITIES_PQFREEINTERVAL.Exists)  {
    TAB_QUANTITIES_PQFREEINTERVAL.wItems.Item('tbFilters').Items.Item("Harmonic").Click()
    TAB_QUANTITIES_PQFREEINTERVAL.wItems.Item('tbFilters').Items.Item("InterHarmonic").Click()
    return true
 }

  else
  {
    Log.Message("Unable to find Harmonic")
    return false
  }
  
}












//
//
//
////This function is used for Taking all the selected quantities back to available quantities
//function clickOnRemoveAllButtonForPq10Min()
//{
//  if(PANE_PQ10MIN_CHANNELS.Visible)
//  {
//    BTN_REMOVE_ALL_SELECTED_QUANTITIES_PQ10MIN.Click()
//    Log.Message("All the selected quantities are removed")
//    return true
//  }
//  else
//  {
//    Log.Message("PQ 10 Min Channels pane is not displaying")
//    return false
//  } 
//}
//
////This function used for getting the tab counts
//function GetTabCountsPq10Min()
//{
//  if(PANE_PQ10MIN_CHANNELS.Visible)
//  {
//    var activeTab = TAB_AVAILABLE_PQ10MIN.get_TabControl().get_Tabs()
//    TABCOUNT = activeTab.VisibleTabsCount
//    Log.Message("Visible tab count are " + TABCOUNT)
//  }
//}
//
////This function is used for selecting the tabs
//function ClickOnTabPq10Min(Index)
//{
//  if(PANE_PQ10MIN_CHANNELS.Visible)
//  { 
//    var item_Index = TAB_AVAILABLE_PQ10MIN.get_TabControl().get_Tabs().get_Item(Index)
//    TAB_AVAILABLE_PQ10MIN.get_TabControl().set_SelectedTab(item_Index)
//    Log.Message("Tab has been selected")
//    return true
//  }
//  else
//  {
//    Log.Message("PQ 10 Min panel is not visible")
//    return false
//  }
//}
//
//
////This function used for adding RMS quantities from available quantities
//function AddRMSQuantitiesPq10Min()
//{   
//  if(PANE_PQ10MIN_CHANNELS.Visible)
//  {     
//    var available_Quantities_Pq10Min = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.lstAvailable.lvQuantities
//    var item_List = available_Quantities_Pq10Min.wItemList
//    var sap = available_Quantities_Pq10Min
//    aqString.ListSeparator = sap
//    for (i = 0; i < aqString.GetListLength(item_Count); i++)
//    {
//      var temp = aqString.GetListItem(item_Count, i)
//      if(aqString.StrMatches("RMS", temp))
//      {
//        available_Quantities_Pq10Min.ClickItem(temp)
//        available_Quantities_Pq10Min.Click()
//      }
//    }
//    Log.Message("PQ 10 Min RMS Quantities added") 
//    return true
//  }
//  else
//  {
//    Log.Message("PQ 10 Min quantities not available")
//    return false
//  }
//}
