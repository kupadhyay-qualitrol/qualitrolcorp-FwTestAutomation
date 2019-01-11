/*This file contains methods and Objects related to FRViewer*/

//USEUNIT AssertClass

//Variables
var FRViewerFileOption = Aliases.iQ_Plus
var Edtbx_SaveAs_Name=Aliases.iQ_Plus.dlgSaveAs.DUIViewWndClassName.Explorer_Pane.FloatNotifySink.ComboBox
var Dialog_SaveAs=Aliases.iQ_Plus.dlgSaveAs
var Btn_Save =Aliases.iQ_Plus.dlgSaveAs.btnSave
//
//This function is used to save the cdf file 
function SaveToCDFFile(Path)
{
  FRViewerFileOption.MainForm.StripMainMenu.Click("File|Save As")
  if(Dialog_SaveAs.Exists)
  {
    Edtbx_SaveAs_Name.SetText(aqString.Trim(Path+"DFRRecord_"+aqString.Replace(aqConvert.TimeIntervalToStr(aqDateTime.Time()),":","_")))
    Btn_Save.ClickButton()
    FRViewerFileOption.MainForm.Close()
    return true
  }
  else
  {
    Log.Message("Save As dialog is not open.")
    return false
  }  
}