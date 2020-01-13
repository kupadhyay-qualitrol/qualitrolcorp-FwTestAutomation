//USEUNIT CommonMethod

var WAVEFORM_VIEWER_PQ = Aliases.iQ_Plus.MainForm
var DLG_BOX_EXPORT = Aliases.iQ_Plus.WFVfrmExportToCSV
var RADIO_BTN_ALL_POINTS = Aliases.iQ_Plus.WFVfrmExportToCSV.WFVgbxExportCSV.WFVrbtnAllDataPoints
var BTN_BROWSER_PATH = Aliases.iQ_Plus.WFVfrmExportToCSV.WFVgbxSelectPath.WFVbtnBrowsePath
var FOLDER_BROWSE = Aliases.iQ_Plus.dlgBrowseForFolder
var TREE_SYSTEM_BROWSE = Aliases.iQ_Plus.dlgBrowseForFolder.SHBrowseForFolderShellNameSpaceControl.TreeView
var BTN_OK_BROWSE_FOLDER = Aliases.iQ_Plus.dlgBrowseForFolder.btnOK
var BTN_OK_EXPORT_FOLDER = Aliases.iQ_Plus.WFVfrmExportToCSV.WFVbtnOK


//This function is used to Click on Export button of PQ10Min
function clickOnExportToCSVButton()
{
  if(WAVEFORM_VIEWER_PQ.Visible)
  {
    STRIP_MAIN_MENU.Click("File|Export|To CSV")
    Log.Message("Clicked on Export to CSV button")
    return true
  }
  else
  {
    Log.Message("Waveform viewer is not visible")
    return false
  }
}
//This function is used to Click on Radio button of All points of channels
function clickOnAllPointsRadioChannel()
{
  if(WAVEFORM_VIEWER_PQ.Visible)
  {
    RADIO_BTN_ALL_POINTS.ClickButton()
    Log.Message("Clicked on Radio button of All points of channels")
    return true
  }
  else
  {
    return false
    Log.Message("Waveform viewer is not visible")
  }
}
//This function is used to Click on Radio button of All points of channels
function clickOnBrowse()
{
  if(WAVEFORM_VIEWER_PQ.Visible)
  {
    BTN_BROWSER_PATH.ClickButton()
    Log.Message("Clicked on Browser path button")
    return true
  }
  else
  {
    return false
    Log.Message("Waveform viewer is not visible")
  }
}
//This function is used to select folder in Browse folder
function selectPQFolder()
{
  if(FOLDER_BROWSE.Visible)
  {
  TREE_SYSTEM_BROWSE.ClickItem("|Desktop|PQ")
  BTN_OK_BROWSE_FOLDER.ClickButton()
  Log.Message("Folder selected and clicked on Ok button")
  return true
  }
  else
  {
    Log.Message("Browse folder is not visible")
    return false
  }
}
//This function is used to click on ok button of export pop up
function clickOnExportOkButton()
{
  if(DLG_BOX_EXPORT.Visible)
  {
    BTN_OK_EXPORT_FOLDER.ClickButton()
    Log.Message("Ok button clicked and data exported")
    return true
  }
  else
  {
    Log.Message("Export folder is not visible")
    return false
  }  
}