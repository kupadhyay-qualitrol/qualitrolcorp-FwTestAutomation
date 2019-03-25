/*This file contains methods & objects related to ConfigEditor_Circuits*/

//Variables
var Form_Circuits = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits
var Form_Circuit_Container = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer
var Lnk_AddNewCircuit =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.llbAddNewCircuit
var Lnk_DeletePresentCircuit =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.llbDelete
var Btn_PopuDialog_Yes =Aliases.iQ_Plus.dlgQualitrolCashelMConfigurator.btnYes
var Edtbx_CircuitName = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.txtCircuitName
var Drpdwn_GroupName = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxVoltages.cmbGroupName
var Lnk_RenameGroupName = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxVoltages.llblRenameBusbar
var Edtbx_ModifyGroupName =Aliases.iQ_Plus.RenameBusbar.txtGroupName
var Btn_ModifyGroupName_OK = Aliases.iQ_Plus.RenameBusbar.btnModify
var Drpdwn_VL1=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxVoltages.ugbxVoltageChannels.cmbVa
var Drpdwn_VL2=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxVoltages.ugbxVoltageChannels.cmbVb
var Drpdwn_VL3=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxVoltages.ugbxVoltageChannels.cmbVc
var Drpdwn_VN=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxVoltages.ugbxVoltageChannels.cmbVn
var Drpdwn_IL1 = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxCurrents.ugbxCurrentChannels.cmbIa
var Drpdwn_IL2 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxCurrents.ugbxCurrentChannels.cmbIb
var Drpdwn_IL3 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxCurrents.ugbxCurrentChannels.cmbIc
var Drpdwn_IN =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucCircuits.tcCircuitContainer.ultraTabSharedControlsPage2.circuitEditor.ugbxCurrents.ugbxCurrentChannels.cmbIn
//

//This method is used to get the circuits count
function GetCircuitsCount()
{
  var circuitCount=0
  if (Form_Circuits.Exists)
  {
    Log.Message("Circuits Form is visible")
    if(Form_Circuit_Container.Exists)
    {
      Log.Message("Circuit Container Exists")
      circuitCount= Form_Circuit_Container.DataSource.List.Count
      Log.Message("No. of Circuit Exists:- "+circuitCount)
      return circuitCount
    }
    else
    {
      Log.Message("Circuit Container doesn't Exists")
      return circuitCount
    }
  }
  else
  {
    Log.Message("Circuits Form is not visible")
    return null
  }
}

function ClickOnAddNewCircuit()
{
  if (Lnk_AddNewCircuit.Enabled)
  {
    Lnk_AddNewCircuit.Click()
    return true  
  }
  else
  {
    Log.Message("Add New Circuit Link is not enable")  
    return false
  }
}

function ClickOnDeletePresentCircuit()
{
  if (Lnk_DeletePresentCircuit.Enabled)
  {
    Lnk_DeletePresentCircuit.Click()
    Btn_PopuDialog_Yes.ClickButton()
    return true  
  }
  else
  {
    Log.Message("Delete Present Circuit Link is not enable")  
    return false
  }
}

function SwitchBusbar(BusbarName)
{
 if(BusbarName!=null)
 {
   var eventargs = dotNET.System.EventArgs.zctor()   
   Drpdwn_GroupName.set_Text(BusbarName)
   Drpdwn_GroupName.FireSelectionChangeCommitted(eventargs)  //This event makes changes to the circuit values
   Log.Message("Selected Busbar as :-"+ BusbarName)
   return true
 }
 else
 {
   Log.Message("Input Parameter is null") 
   return false
 }
}

function GetBusbar_Name(index)
{
  if(Drpdwn_GroupName.Exists)
  {
    return Drpdwn_GroupName.get_Items().Item(index).DisplayText.OleValue
  }
  else
  {
    Log.Message("Config Editor doesn't exists")
    return null
  }
}

function SwitchCircuits(CircuitName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    //Get Index
    var circuitCount = GetCircuitsCount()
    var index
    for (let circuitindex=0;circuitindex<circuitCount;circuitindex++)
    {
      if(CircuitName== Form_Circuit_Container.Tabs.Item_2(circuitindex).Text.OleValue)
      {
        index=circuitindex
        break
      }
    
    }
    //Switch Circuit
    Form_Circuit_Container.Tabs.Item_2(index).Selected=true    
  }
  else
  {
    Log.Message("No Circuits exists")
  }
}

function GetCircuitIndexByName(CircuitName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    var index =null
    var circuitCount =GetCircuitsCount()
    //Get Index
    for (let circuitindex=0;circuitindex<circuitCount;circuitindex++)
    {
      if(Form_Circuit_Container.DataSource.List.Item(circuitindex).Label.OleValue==CircuitName)
      {
        index=circuitindex
        Log.Message("Index for Circuit "+CircuitName+ " is "+index)
        break     
      }    
    } 
    return index   
  }
  else
  {
    Log.Message("No Circuits exists")
    return null
  }
}

function SetCircuitName(CircuitName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    var circuitCount =GetCircuitsCount()
    Edtbx_CircuitName.SetText(CircuitName)
    return true  
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }
}

function GetGroupName()
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    return Drpdwn_GroupName.Text.OleValue
  }
  else
  {
    Log.Message("No Circuits exists")
    return null
  }
}

function SetGroupName(GroupName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Lnk_RenameGroupName.Click()
    Edtbx_ModifyGroupName.SetText(GroupName)
    Btn_ModifyGroupName_OK.Click()
    Log.Message("Successfully Sets the Voltage GroupName")
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}

function SetVL1(ChannelName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Drpdwn_VL1.set_Text(ChannelName)
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}

function SetVL2(ChannelName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Drpdwn_VL2.set_Text(ChannelName)
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}

function SetVL3(ChannelName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Drpdwn_VL3.set_Text(ChannelName)
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}

function SetVN(ChannelName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Drpdwn_VN.set_Text(ChannelName)
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}

function SetIL1(ChannelName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Drpdwn_IL1.set_Text(ChannelName)
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}

function SetIL2(ChannelName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Drpdwn_IL2.set_Text(ChannelName)
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}

function SetIL3(ChannelName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Drpdwn_IL3.set_Text(ChannelName)
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}

function SetILN(ChannelName)
{
  if(Form_Circuit_Container.Exists)
  {
    Log.Message("Circuit Container Exists")
    Drpdwn_IN.set_Text(ChannelName)
    return true
  }
  else
  {
    Log.Message("No Circuits exists")
    return false
  }  
}