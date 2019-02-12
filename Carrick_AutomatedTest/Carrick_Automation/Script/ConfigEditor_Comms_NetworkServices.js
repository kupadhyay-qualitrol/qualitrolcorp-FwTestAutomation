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
    drpdwn_MaskID.Value=GroupMaskID
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
    drpdwn_Compatibility.Value=Compatiblity //0 for Not Compatible; 1 for Send and Recieve Cross Triggers
    if(Compatiblity=="0")
    {
      if(drpdwn_Compatibility.Text== "Not compatible")
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
    else if(Compatiblity=="1")
    {
      if(drpdwn_Compatibility.Text== "Send and receive cross triggers")
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
      Log.Message("Compatibility is not set")
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
    Edtbx_UDPPortNumber.Value=PortNumber
    
    if(Edtbx_UDPPortNumber.Text== PortNumber)
    {
      Log.Message("Able to set the port number")
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