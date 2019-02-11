/*This file contains methods and objects related to Network Services under Communication*/

//USEUNIT ConfigEditorPage

//Variables
var drpdwn_MaskID = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.ugbxCrossTriggerSettings.cmbGroupMaskId
var drpdwn_Compatibility=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.ugbxCrossTriggerSettings.cmbIdmCompatibility
var Edtbx_UDPPortNumber =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.ugbxCrossTriggerSettings.txtUDPPortNumber
//

//This method is used to set the Groupmask id
function SetGroupMaskID(GroupMaskID)
{
  if(drpdwn_MaskID.Exists)
  {
    drpdwn_MaskID.SetText(GroupMaskID)
    if(drpdwn_MaskID.Text==GroupMaskID)
    {
      Log.Message("Able to set Group Mask ID")    
      return true
    }
    else
    {
      Log.Message("Unable to set Group Mask ID")
      return false
    }
    
  }
  else
  {
    Log.Message("Unable to locate Group Mask ID box")
    return false
  }
}

//This method is used to set the Compatibility
function SetCompatibility(Compatiblity)
{
  if(drpdwn_Compatibility.Exists)
  {
    drpdwn_Compatibility.SetText(Compatiblity)
    if(drpdwn_Compatibility.Text== Compatiblity)
    {
      Log.Message("Able to set compatibility")
      return true
    }
    else
    {
      Log.Message("Unable to set Compatibility")    
      return false
    }
  }
  else
  {
    Log.Message("Unable to locate Compatibility Checkbox")
    return false
  }
}

//This method is used to set the UDPPort
function SetUDPPort(PortNumber)
{
  if(Edtbx_UDPPortNumber.Exists)
  {
    Edtbx_UDPPortNumber.Text= PortNumber
    
    if(Edtbx_UDPPortNumber.Text== PortNumber)
    {
      Log.Message("Success to set the port number")
      return true
    }
    else
    {
      Log.Message("Unable to set Port Number")
      return false
    }
  }
  else
  {
    Log.Message("Unable to locate UDP Port Control")
    return false
  }
}