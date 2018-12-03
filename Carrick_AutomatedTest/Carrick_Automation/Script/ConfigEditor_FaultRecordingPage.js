/*This page contains objects and methods related to Configuration Editor
*/

//USEUNIT ConfigEditorPage

//Variables
var Item_ConfigurationEditor = Aliases.iQ_Plus.Form
var Edtbx_Prefault =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucFrGeneral.frmBackground.frmPrefaultTimeAndSampleRate.txtPrefault
//

//This method is used to Set Prefault time for DFR.
function SetPrefault(PrefaultTime)
{
  //Click on Fault Recording in Config Editor
  if(ConfigEditorPage.ClickOnFaultRecording())
  {  
    if(PrefaultTime!=null)
    {
      Edtbx_Prefault.Text=PrefaultTime
      Log.Message("Able to set the Prefault time to:- "+PrefaultTime)
      Log.Picture(Item_ConfigurationEditor,"Snapshot for Prefault time")
      return true
    }
    else
    {
     Log.Message("Prefault time is null.")
     return false
    }
  }
  else
  {
    Log.Picture(Item_ConfigurationEditor,"Sanpshot for debug")
    return false
  }  
}

//This method is used to Get Prefault time for DFR.
function GetPrefault()
{
  //Click on Fault Recording in Config Editor
  if(ConfigEditorPage.ClickOnFaultRecording())
  {  
      Log.Message("Prefaul time is :- "+Edtbx_Prefault.Text)
      return Edtbx_Prefault.Text
  }
  else
  {
    Log.Picture(Item_ConfigurationEditor,"Sanpshot for debug")
    return null
  } 
}