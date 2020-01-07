/*This page contains methods and objectes related to PQ 10 min Page*/

//USEUNIT CommonMethod

//Variables
var TABCOUNT
var PANE_CHANNELS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous
var BTN_REMOVE_ALL_SELECTED_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnRemoveAll
var AVAILABLE_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.lstAvailable.lvQuantities
var BTN_ADD_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.frmPanel.btnAddSelected
var TAB_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuous10Min.tabContinuous.pg.pqQuantities.zucPqContinuous_Toolbars_Dock_Area_Top
var PANE_CONFIG = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane
var BTN_PQ = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CRCSTgrpCRStyles.CRCSTrboPQWaveform
var RADIO_BTN = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTgrpTimeInterval.CRPQPCSTpnlCRPQDataType.CRPQPCSTrdbtnFreeInterval
var CHKBX_VRMS = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamFirst.CRPQPCSTgrpCRPQParametersFirst.CRPQPCSTchkURMS
var CHKBX_IRMS = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamFirst.CRPQPCSTgrpCRPQParametersFirst.CRPQPCSTchkIRMS
var BTN_MORE = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTbtnMoreHide
var CHKBX_RMS = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamThird.CRPQPCSTgrpCRPQParametersThird.CRPQPCSTchkStandaloneRMS
var CHKBX_HARMONIC = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamThird.CRPQPCSTgrpCRPQParametersThird.CRPQPCSTchkStandaloneHarmonics
var CHKBX_INTERHARMONIC = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamThird.CRPQPCSTgrpCRPQParametersThird.CRPQPCSTchkStandaloneInterHarmonics
var EDTBX_FAVORITENAME = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.DACSTBgbOptions.DACSTBtxtFavoriteName
var BTN_SAVE = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.DACSTBgbSelection.DACSTBbtnSave
var EDTBX_HARMONIC = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamThird.CRPQPCSTgrpCRPQParametersThird.CRPQPCSTtxtStandaloneHarmonics
var EDTBX_INTERHARMONIC = Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.CustomizeCR.pnlCustomizeBase.CustomizeFavoritesStyleWorkspace.CustomizePQWaveform.CRPQPCSTpnlContainer.CRPQPCSTtbctrlParametersContainer.CRPQPCSTtbpgParamThird.CRPQPCSTgrpCRPQParametersThird.CRPQPCSTtxtStandaloneInterHarmonics
var MAINMENU_WAVEFORM_VIEWER = Aliases.iQ_Plus.MainForm
var RADIOBTN_ALL_DATA_POINTS = Aliases.iQ_Plus.WFVfrmExportToCSV.WFVgbxExportCSV.WFVrbtnAllDataPoints
var EXPORT_PATH_SELECTION =Aliases.iQ_Plus.WFVfrmExportToCSV.WFVgbxSelectPath.WFVtxtSelectPath
var OKBTN_PATH_SELECTION = Aliases.iQ_Plus.dlgBrowseForFolder.btnOK
var OKBTN_EXPORT_TO_CSV = Aliases.iQ_Plus.WFVfrmExportToCSV.WFVbtnOK
var SYNCHRONIZE_DATE_TIME = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2.TimeInterval.TimeIntervalControl.zUserControlBase_Toolbars_Dock_Area_Top.wItems.Item("Time Interval Control Toolbar").Items.Item("Synchronizes End Date Time to Current Date Time")
var START_DATE_TO_ONE = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2.TimeInterval.TimeIntervalControl.zUserControlBase_Toolbars_Dock_Area_Top.wItems.Item("Time Interval Control Toolbar").Items.Item(1)
var DEFAULT_FAV = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.DAFAVuexpbarFavorites
var DLG_BX_IQPLUS = Aliases.iQ_Plus.dlgIQ
var BTN_YES_DLG_BX_IQPLUS = Aliases.iQ_Plus.dlgIQ.btnYes
var BTN_OK_DLG_BX_IQPLUS = Aliases.iQ_Plus.dlgIQ.btnOK
//


//This function is used for Taking all the selected quantities back to available quantities for PQ 10Min Page
function clickOnRemoveAllButton()
{
  if(PANE_CHANNELS.Visible)
  {
    BTN_REMOVE_ALL_SELECTED_QUANTITIES.Click()
    Log.Message("All the selected quantities are removed in PQ Free Interval Page")
    return true
  }
  else
  {
    Log.Message("PQ Free Interval Channels pane is not displaying")
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
    Log.Message("Tab has been selected in PQ Free Interval Page")
    return true
  }
  else
  {
    Log.Message("PQ Free Interval panel is not visible")
    return false
  }
}



//This function used for adding quantities from available quantities in PQ 10Min Page
function addQuantitiesPq10Min(quantities)
{   
  if(PANE_CHANNELS.Visible)
  {     
    var item_List = AVAILABLE_QUANTITIES.wItemList
    var sap = AVAILABLE_QUANTITIES.wListSeparator
    aqString.ListSeparator = sap
    for (counter = 0; counter < aqString.GetListLength(item_List); counter++)
    {
      var availableQuantities = aqString.GetListItem(item_List, counter)
      if(aqString.StrMatches(quantities, availableQuantities))
      {
        AVAILABLE_QUANTITIES.ClickItem(availableQuantities)
        BTN_ADD_QUANTITIES.Click()
      }
    }
    return true
  }
  else
  {    Log.Message("PQ Free Interval quantities not available")
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
