/*This page contains methods and objectes related to PQ 10 min and Free interval Page*/

//USEUNIT CommonMethod

//Variables
var TABCOUNT
var PANE_CHANNELS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous
var BTN_REMOVE_ALL_SELECTED_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous.pg.pqQuantities.frmPanel.btnRemoveAll
var AVAILABLE_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous.pg.pqQuantities.frmPanel.lstAvailable.lvQuantities
var BTN_ADD_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous.pg.pqQuantities.frmPanel.btnAddSelected
var TAB_QUANTITIES = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucPqContinuousFreeInterval.tabContinuous.pg.pqQuantities.zucPqContinuous_Toolbars_Dock_Area_Top
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
//


//This function is used for Taking all the selected quantities back to available quantities for PQ Free Interval Page
function clickOnRemoveAllButtonForPqFreeIntervalMin()
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


//This function used for getting the tab counts from PQ Free Interval Page
function getTabCountsPqFreeInterval()
{
  if(PANE_CHANNELS.Visible)
  {
    var activeTab = PANE_CHANNELS.activeTab_2.get_TabControl().get_Tabs()
    TABCOUNT = activeTab.VisibleTabsCount
    Log.Message("Visible tab count are " + TABCOUNT)
  }
}


//This function is used for selecting the tabs in PQ Free Interval Page
function clickOnTabPqFreeInterval(Index)
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



//This function used for adding RMS quantities from available quantities in PQ Free Interval Page
function addRMSQuantitiesPqFreeInterval()
{   
  if(PANE_CHANNELS.Visible)
  {     
    var item_List = AVAILABLE_QUANTITIES.wItemList
    var sap = AVAILABLE_QUANTITIES.wListSeparator
    aqString.ListSeparator = sap
    for (i = 0; i < aqString.GetListLength(item_List); i++)
    {
      var availableQuantities = aqString.GetListItem(item_List, i)
      if(aqString.StrMatches("RMS", availableQuantities))
      {
        AVAILABLE_QUANTITIES.ClickItem(availableQuantities)
        BTN_ADD_QUANTITIES.Click()
      }
    }
    return true
  }
  else
  {    Log.Message("PQ Free Interval Min quantities not available")
    return false
  }
}

//This function used for adding Harmonic and IntraHarmonic in PQ Free Interval Page
function addHarmonicIntraHarmonicPqFreeInterval(){
  if(PANE_CHANNELS.Visible)
  {     
    var item_List = AVAILABLE_QUANTITIES.wItemList
    var sap = AVAILABLE_QUANTITIES.wListSeparator
    aqString.ListSeparator = sap
    
    for (i = 0; i < aqString.GetListLength(item_List); i++)
    {
      var availableQuantities = aqString.GetListItem(item_List, i)
      if(aqString.StrMatches("H01", availableQuantities))
      {
        AVAILABLE_QUANTITIES.ClickItem(availableQuantities)
        BTN_ADD_QUANTITIES.Click()
      }
    }
    Log.Message("Harmonic and IntraHarmonic Channel Cirtcuit Quantities added for PQ Free Interval") 
    return true
  }
  else
  {    Log.Message("Harmonic and IntraHarmonic quantities not available for PQ Free Interval")
    return false
  }

}


//This function is used to click on Harmonics and Intra Harmonics on PQ Free Interval Page
function clickOnHarmonicsIntraharmonicsTabPqFreeInterval() 
{
  if(TAB_QUANTITIES.Exists)  
  {
    TAB_QUANTITIES.wItems.Item('tbFilters').Items.Item("Harmonic").Click()
    TAB_QUANTITIES.wItems.Item('tbFilters').Items.Item("InterHarmonic").Click()
    return true
  }

  else
  {
    Log.Message("Unable to find Harmonic")
    return false
  }
  
}


//This method will configure new favorite for PQ free interval
function createNewFavoriteForPqFreeInterval() 
{
    var pqFreeIntervalFavorite = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow4.Favorites.DAFavoriteView.zUserControlBase_Toolbars_Dock_Area_Top.wItems.Item("utFavoriteToolbar").Items.Item(1)
    pqFreeIntervalFavorite.Click()
    aqUtils.Delay(2000)
    BTN_PQ.Click()
    RADIO_BTN.Click()
    CHKBX_VRMS.Click()
    CHKBX_IRMS.Click()
    BTN_MORE.Click()
    BTN_MORE.Click()
    aqUtils.Delay(2000)
    CHKBX_RMS.Click()
    CHKBX_HARMONIC.Click()
    EDTBX_HARMONIC.SetText("1")
    CHKBX_INTERHARMONIC.Click()
    EDTBX_INTERHARMONIC.SetText("1")
    EDTBX_FAVORITENAME.SetText("PQ Free Interval")
    aqUtils.Delay(2000)
    BTN_SAVE.Click()
    Log.Message("PQ Free Interval new Favorite has been configured")
    aqUtils.Delay(2000)
}

//This function will export PQ Free Interval Data to CSV
function exportPqFreeIntervalDataToCsv() 
{
  //Synchronize date and time for time interval
  SYNCHRONIZE_DATE_TIME.Click()
  START_DATE_TO_ONE.Click()
  
  //Click on PQ Free Interval Favorite
  DEFAULT_FAV.ClickItem("Default Favorites", "PQ Free Interval")
  Log.Message("PQ Free Interval Data Opened")
  aqUtils.Delay(7000)
  
  var sysUserName = CommonMethod.GetSystemUsername()
  var folderName ="C:\\Users\\"+sysUserName+"\\Desktop\\PQFreeInterval\\"
  //Check if export folder available or not if not then create folder
  if(aqFileSystem.Exists(folderName)== false)
  {
    aqFileSystem.CreateFolder(folderName)
  }
  
  //Export PQ Free Interval data to CSV
  MAINMENU_WAVEFORM_VIEWER.Keys("~{F}")
  for(i=0; i<10 ; i++)
  {
  LLPlayer.KeyDown(VK_DOWN,1000)  
  }
  
  LLPlayer.KeyDown(VK_RETURN,1000)
  
  for(i=0; i<3 ; i++)
  {
  LLPlayer.KeyDown(VK_DOWN,1000)  
  }
  LLPlayer.KeyDown(VK_RETURN,1000)
  RADIOBTN_ALL_DATA_POINTS.Click()
  EXPORT_PATH_SELECTION.Click()
  var selectFolderToExport = Aliases.iQ_Plus.dlgBrowseForFolder.SHBrowseForFolderShellNameSpaceControl.TreeView.wItems.Item(0).Items.Item("PQFreeInterval")
  selectFolderToExport.Click()
  OKBTN_PATH_SELECTION.Click()
  OKBTN_EXPORT_TO_CSV.Click()
  if (DLG_BX_IQPLUS.Exists)
  {
    BTN_YES_DLG_BX_IQPLUS.Click()
  }
  Log.Message("Export data in CSV format")
  MAINMENU_WAVEFORM_VIEWER.Close()
}

