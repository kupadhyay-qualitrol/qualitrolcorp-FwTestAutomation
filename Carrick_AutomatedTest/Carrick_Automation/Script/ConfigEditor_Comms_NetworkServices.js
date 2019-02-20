/*This file contains methods and objects related to Network Services under Communication*/

//USEUNIT ConfigEditorPage

//Variables
var drpdwn_Compatibility=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.cmbIdmCompatibility
var Edtbx_UDPPortNumber =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.ugbxCrossTriggerSettings.txtUDPPortNumber
var Chkbx_Group1 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.ugbxCrossTriggerSettings.chkGroup1
var Chkbx_Group2 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.ugbxCrossTriggerSettings.chkGroup2
var Chkbx_Group3 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.ugbxCrossTriggerSettings.chkGroup3
var Chkbx_Group4 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucNetworkServices.chkGroup4
//

//This method is used to set the Groupmask id
function SetGroupMaskID(Grp1,Grp2,Grp3,Grp4)
{
  if(Chkbx_Group1.Exists)
  {  
    Chkbx_Group1.wState =Grp1
    Chkbx_Group2.wState =Grp2
    Chkbx_Group3.wState =Grp3
    Chkbx_Group4.Checked =Grp4
    if(Chkbx_Group1.wState==Grp1 && Chkbx_Group2.wState==Grp2 && Chkbx_Group3.wState==Grp3 && Chkbx_Group4.wState==Grp4)
    {
      Log.Message("Checked the Checkbox in Groups")
      return true
    }
    else
    {
      Log.Message("Unable to Check the checkbox in Groups")
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