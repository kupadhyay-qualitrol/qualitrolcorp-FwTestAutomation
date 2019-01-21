/*This page contains objects and methods related to Configuration Editor
*/

//USEUNIT ConfigEditorPage

//Variables
var Item_ConfigurationEditor = Aliases.iQ_Plus.Form
var Edtbx_Prefault =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucFrGeneral.frmBackground.frmPrefaultTimeAndSampleRate.txtPrefault
var Edtbx_Postfault = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucFrGeneral.frmBackground.frmExternalTriggers.txtPostfault
var lbl_MaxDFR =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucFrGeneral.frmBackground.lblMaxDurationDFR
var lbl_MaxDFRUnit= Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucFrGeneral.frmBackground.lblMaxDFRUnits
var Edtbx_MaxDFR= Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucFrGeneral.frmBackground.txtMaxDFRDuration
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

//This method is used to Set Post Faultime for External Triggers
function SetPostFault(PostfaultTime)
{
  //Click on Fault Recording in Config Editor
  if(ConfigEditorPage.ClickOnFaultRecording())
  {  
    if(PostfaultTime!=null)
    {
      Edtbx_Postfault.Text=PostfaultTime
      Log.Message("Able to set the Post-fault time to:- "+PostfaultTime)
      Log.Picture(Item_ConfigurationEditor,"Snapshot for Post-fault time")
      return true
    }
    else
    {
     Log.Message("Post-Fault time is null.")
     return false
    }
  }
  else
  {
    Log.Picture(Item_ConfigurationEditor,"Sanpshot for debug")
    return false
  }
  
}

//This method is used to Get the Label of Max DFR
function GetMaxDFRLabel()
{
  //Click on Fault Recording in Config Editor
  if(ConfigEditorPage.ClickOnFaultRecording())
  {  
      Log.Message("Max DFR Label is :- "+lbl_MaxDFR.Text.OleValue)
      Log.Picture(Item_ConfigurationEditor,"Sanpshot for Maximum DFR label and Editbox")
      return lbl_MaxDFR.Text.OleValue
  }
  else
  {
    Log.Picture(Item_ConfigurationEditor,"Sanpshot for debug")
    return null
  } 
}

//This method is used to Get the unit of Max DFR
function GetMaxDFRUnit()
{
  //Click on Fault Recording in Config Editor
  if(ConfigEditorPage.ClickOnFaultRecording())
  {  
      Log.Message("Max DFR Unit Label is :- "+lbl_MaxDFRUnit.Text.OleValue)
      Log.Picture(Item_ConfigurationEditor,"Sanpshot for Maximum DFR Unit label and Editbox")
      return lbl_MaxDFRUnit.Text.OleValue
  }
  else
  {
    Log.Picture(Item_ConfigurationEditor,"Sanpshot for debug")
    return null
  }
}

