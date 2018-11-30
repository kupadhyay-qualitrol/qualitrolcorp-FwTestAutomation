/*This page contains objects and methods related to Configuration Editor
*/

//This method is used to Click on Fault Recording
var Item_ConfigurationEditor = Aliases.iQ_Plus.Form
var Item_ConfigTree=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel.configTree
function ClickOnFaultRecording()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Fault Recording")
    Log.Message("Clicked on Fault Recording")
    return true
  }
  else
  {
    Log.Message("Unable to click on Fault Recording")
    return false
  }
}

var Edtbx_Prefault =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucFrGeneral.frmBackground.frmPrefaultTimeAndSampleRate.txtPrefault
//This method is used to Set Prefault time for DFR.
function SetPrefault(PrefaultTime)
{
  //Click on Fault Recording in Config Editor
  if(ClickOnFaultRecording())
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