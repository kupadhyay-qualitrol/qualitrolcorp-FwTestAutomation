/*This page contains methods and objectes related to FR Sensor Page*/

//Variables
var Grid_FRSensor = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucFrCircuitSensors.ugSensors
var Edtbx_FRSensor_Quantity = Aliases.iQ_Plus.FrSensor.tcFRParameters.tbGeneral.txtQuantity
var Drpdwn_FRSensorQuantity =Aliases.iQ_Plus.DropDownForm.ucFrSensorParamSelector.tabQuantities
var Item_FRQuantity =Aliases.iQ_Plus.DropDownForm.ucFrSensorParamSelector.tabQuantities.ultraTabSharedControlsPage1.lstQuantities.frmClient.lstQuantities.lvQuantities
var Btn_EditFRSensor_OK =Aliases.iQ_Plus.FrSensor.btnOk
var Btn_EditFRSensor_Quantity_OK =Aliases.iQ_Plus.DropDownForm.ucFrSensorParamSelector.btnOK
var Dialog_EditFRSensor =Aliases.iQ_Plus.FrSensor
var Drpdwn_SensorType =Aliases.iQ_Plus.FrSensor.tcFRParameters.tbGeneral.cmbType
var Drpdwn_SensorScalingType = Aliases.iQ_Plus.FrSensor.tcFRParameters.tbGeneral.cmbScalingType
var Edtbx_UpperThreshold =Aliases.iQ_Plus.FrSensor.tcFRParameters.tbGeneral.txtUpperThreshold
var Edtbx_PostFaultTime =Aliases.iQ_Plus.FrSensor.tcFRParameters.tbGeneral.gbxWhenTheSensorActivates.txtPostFault
var Edtbx_Oplimit =Aliases.iQ_Plus.FrSensor.tcFRParameters.tbGeneral.gbxWhenTheSensorActivates.txtOpLimit
//

//This method is used to Set the FR Sensor
function SelectFRSensorByName(sensorName)
{
  if(Dialog_EditFRSensor.Exists) 
   {  
    Edtbx_FRSensor_Quantity.wButtonsRight(0).DropDown()
    var sensorcount=Item_FRQuantity.wItemCount
    var sensorindex =-1
    for (let iteratesensor =0 ; iteratesensor<sensorcount ; iteratesensor++)
    {
      if(Item_FRQuantity.wItem(iteratesensor)==sensorName)
      {
        sensorindex =iteratesensor
        break
      }
    } 
    if(sensorindex!=-1)
    {
      var sensortobeselected=Item_FRQuantity.wItem(sensorindex)
      Item_FRQuantity.ClickItem(sensortobeselected)
      Btn_EditFRSensor_Quantity_OK.Click()
     // Btn_EditFRSensor_OK.Click()
      return true
    }
    else
    {
      Log.Message("Input FR Sensor Name doesnt match with Available")
      return false
    }
  }
  else
  {
    Log.Message("Edit FR sensor dialog doesn't exists")
    return false
  }
}

//This method is used to Open the FR sensor Editor
function OpenFRSensorEditor(row)
{
  if(Grid_FRSensor.Exists)
  {
    if(row!=null)
    {
      Grid_FRSensor.ClickCell(row,8)
      Log.Message("Opens FR Sensor Editor")
      return true
    }
    else
    {
      Log.Message("Row number is not mentioned")
      return false
    } 
  }
  else
  {
    Log.Message("FR Sensor grid doesn't exists in the device")
    return false
  }
}

//This method is used to Set the Type
function SetFRSensorType(sensorType)
{
  if(Dialog_EditFRSensor.Exists)
  {
    switch (sensorType)
    {
      case "Under":
        Drpdwn_SensorType.set_Text("Under")    
        return true  
        break;    
      case "Over":
        Drpdwn_SensorType.set_Text("Over")
        return true  
        break;
      case "Window":
        Drpdwn_SensorType.set_Text("Window")  
        return true
        break;
      case "Rate of Change":
        Drpdwn_SensorType.set_Text("Rate of Change")  
        return true
        break;
      case "Oscillation":
        Drpdwn_SensorType.set_Text("Oscillation")  
        return true
        break;
      default :
        Log.Message("No matching sensor Type")
        return false
        break
    }    
  }
  else
  {
    Log.Message("Edit FR sensor dialog doesn't exists")
    return false
  }
}

//This method is used to Set the Scaling Type
function SetScalingType(sensorScalingType)
{
  if(Drpdwn_SensorScalingType.Exists)
  {
    switch (sensorScalingType)
    {
      case "Primary":
        Drpdwn_SensorScalingType.set_Text("Primary")    
        return true  
        break;    
      case "Secondary":
        Drpdwn_SensorScalingType.set_Text("Secondary")
        return true  
        break;
      case "% of Nominal":
        Drpdwn_SensorScalingType.set_Text("% of Nominal")  
        return true
        break;
      default :
        Log.Message("No matching sensor Scaling Type")
        return false
        break
    }    
  }
  else
  {
    Log.Message("Edit FR sensor dialog doesn't exists")
    return false
  }
}

//This method is used to Set the Upper Threshold
function SetUpperThreshold(upperThreshold)
{
  if(Edtbx_UpperThreshold.Exists)
  {
    Edtbx_UpperThreshold.set_Text(upperThreshold)  
    return true
  }
  else
  {
    Log.Message("Edit FR sensor dialog doesn't exists")
    return false
  }
}

//This method is used to Set the Post Fault Time
function SetPostFaultTime(postFaultTime)
{
  if(Edtbx_PostFaultTime.Exists)
  {
    Edtbx_PostFaultTime.set_Text(postFaultTime)  
    return true
  }
  else
  {
    Log.Message("Edit FR sensor dialog doesn't exists")
    return false
  }
}

//This method is used to Set the Oplimit
function SetOplimit(oplimit)
{
  if(Edtbx_Oplimit.Exists)
  {
    Edtbx_Oplimit.set_Text(oplimit)  
    return true
  }
  else
  {
    Log.Message("Edit FR sensor dialog doesn't exists")
    return false
  }
}

