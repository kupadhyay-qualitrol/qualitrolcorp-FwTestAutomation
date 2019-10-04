/*This page contains methods and objectes related to DDRC channels Page*/

//Variables
var Tab_Count
var Drp_Dwn_Storage_Rate = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.cmbSamplingRate
var Pane_DDRC_Channels = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane
var Box_Pre_Fault = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.frmDdrtDuration.txtPrefault
var Box_Post_Fault = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.frmDdrtDuration.txtPostfault
var Chk_Box_DDRC_Group1 = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.qtGroupBox1.chkGroup1
var Chk_Box_DDRC_Group2 = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.qtGroupBox1.chkGroup2
var Tab_Available = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.tabAvailable
var Btn_Add_Quantities = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.btnAddSelected
var Tab_Selected = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.frmSelected.lstSelected;
var Btn_Remove_All_Selected_Quantities = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.btnRemoveAll

function SetStorageRate(StorageRate)
{
  if(Pane_DDRC_Channels.Visible)
  {
    Drp_Dwn_Storage_Rate.Click;
    Log.Message("Clicked on Storage rate dorpdown")
    Drp_Dwn_Storage_Rate.set_Text(StorageRate);
    Log.Message("Storage rate has been set")
    return true
  }
  else
  {
    Log.Message("DDRC Channels pane is not displaying")
    return false
  }
}

//This function is to set Pre-Fault and Post-Fault for DDRC channels
function SetPreFaultPostFault(prefault,postfault)
{
  if(Pane_DDRC_Channels.Visible)
  {
    Box_Pre_Fault.set_Text(prefault);
    Log.Message("Pre Fault has been set for DDRC channels")
    Box_Post_Fault.set_Text(postfault);
    Log.Message("Post Fault has been set for DDRC channels")
    return true
  }
  else
  {
    Log.Message("DDRC Channels pane is not displaying")
    return false
  }
}
//This Function is to check DDRC and DDRT
function CheckDDRCDDRTCheckBox() 
{
  if(Pane_DDRC_Channels.Visible)
  {
    if (Chk_Box_DDRC_Group1.get_Checked()== true && Chk_Box_DDRC_Group2.get_Checked()== true)
    {      
      Log.Message("DDRC Groups checked box are already checked")
      return true
    }
    else
    {
      if(Chk_Box_DDRC_Group1.get_Checked()!= true)      
      {
        Chk_Box_DDRC_Group1.Click();
        Log.Message("Clicked on Check box for Group 1")
      }
      
      if(Chk_Box_DDRC_Group2.get_Checked()!= true)      
      {
        Chk_Box_DDRC_Group2.Click();
       Log.Message("Clicked on Check box for Group 2")  
      }
      return true
    }
  }
  else
  {
    Log.Message("DDRC Channels pane is not displaying")
    return false
  }
}
//This function used for getting the tab counts
function GetTabCounts()
{
  if(Pane_DDRC_Channels.Visible)
  {
    var Active_Tab = Tab_Available.ActiveTab.get_TabControl().get_Tabs()
    Tab_Count = Active_Tab.VisibleTabsCount
    Log.Message("Visible tab count are" + Tab_Count)
    return Tab_Count
  }
}
//This function used for adding RMS quantities from available quantities
function AddRMSQuantities()
{  
  if(Pane_DDRC_Channels.Visible)
  {
    var Available_quantities = Tab_Available.ultraTabSharedControlsPage1.lstAvailable.frmClient.lstQuantities.lvQuantities
    var item_list = Available_quantities.wItemList;
    var sap = Available_quantities.wListSeparator;
    aqString.ListSeparator = sap; 
    for (i = 0; i < aqString.GetListLength(item_list); i++)
    {
      var temp = aqString.GetListItem(item_list, i);
      if(aqString.StrMatches("RMS", temp))
      {
        Available_quantities.ClickItem(temp);
        Btn_Add_Quantities.Click();
      }
    }
    Log.Message("RMS Quantities added") 
    return true
  }
  else
  {
    Log.Message("DDRC Pannel is not visible")
    return false
  }
}
//This function is used for selecting the tabs
function ClickOnTab(Index)
{
  if(Pane_DDRC_Channels.Visible)
  {
    var Item_Index = Tab_Available.get_VisibleTabs().get_Item(Index)
    Tab_Available.set_SelectedTab(Item_Index)
    Log.Message("Tab has been selected")
    return true
  }
  else
  {
    Log.Message("DDRC Pannel is not visible")
    return false
  }
}
//This function is used for checking DDRT check box in selected quantities
function CheckDDRTCheckBoxSelectedQuantities()
{
  var Selected_Quantities_Item_Count = Tab_Selected.wRowCount
  Log.Message("Count is" + Selected_Quantities_Item_Count)
  for (Item_Count = 0; Item_Count < Selected_Quantities_Item_Count; Item_Count++)
  {
     Tab_Selected.ClickCell(Item_Count,1)
     Log.Message("DDRC check box clicked for RMS quantity"+ Item_Count)
  }
}
//This function is used for Taking all the selected quantities back to available quantities
function ClickOnRemoveAllButton()
{
  if(Pane_DDRC_Channels.Visible)
  {
    Btn_Remove_All_Selected_Quantities.Click()
    Log.Message("All the selected quantities are removed")
    return true
  }
  else
  {
    Log.Message("DDRC Channels pane is not displaying")
    return false
  } 
}