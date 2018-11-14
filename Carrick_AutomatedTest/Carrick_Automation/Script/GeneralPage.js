/*This file contains method and variables for General toolbar available on iQ+ GUI.*/
//USEUNIT CommonMethod
//USEUNIT DeviceTopologyPage
//USEUNIT DeviceManagementPage

function CreateDevice(devicetype,devicename,deviceSerialNo,deviceIPAdd)
{
  if(DeviceManagementPage.DeviceManagementToolbar.Enabled)
  {
    DeviceManagementPage.DeviceManagementToolbar.ClickItem("Device &Management")
    CommonMethod.AssertIsTrue(true,DeviceTopologyPage.ClickonAllDevices(),"Clicked on All Devices Button")
    DeviceManagementPage.DeviceManagementToolbar.ClickItem("Device &Management|General|&Create Device")
    Log.Message("Clicked on Create Device page")
    SelectDeviceType(devicetype)
    EnterDeviceDetails(devicetype,devicename,deviceSerialNo,deviceIPAdd)
    return true
  }
  else
  {
    Log.Error("iQ+ GUI is not visible")
  }
}
var DeviceTypePage=Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.panel1.MDEVtbParent
var DeviceType=DeviceTypePage.MDEVtbPDeviceTypes.MDEVpnlDeviceTypes.MDEVlsvDeviceTypes
var BtnNext_DeviceType= Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.MDEVbtnNavigation
function SelectDeviceType(devicetype)
{
  if(DeviceTypePage.Enabled)
  {  
    DeviceType.ClickItem(devicetype)
    Log.Message("Selected device type:- "+devicetype)
    BtnNext_DeviceType.ClickButton()
  }
  else
  {
    Log.Message("Device Type page is not enabled")
  }
}

var DeviceName=Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.panel1.MDEVtbParent.MDEVtbpDevice.MDEVpnlDevice.DEVdwsDecDeviceDetails.ManagePMDADevices.DEVgrpCashelDeviceDetails.DEVtxtCashelDeviceName
var DeviceSerialNo=Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.panel1.MDEVtbParent.MDEVtbpDevice.MDEVpnlDevice.DEVdwsDecDeviceDetails.ManagePMDADevices.DEVgrpCashelDeviceDetails.DEVtxtDeviceSerialNo
var DeviceIPAdd0=Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.panel1.MDEVtbParent.MDEVtbpDevice.MDEVpnlDevice.DEVdwsDecDeviceDetails.ManagePMDADevices.DEVgrpCommunicationDetails.DEVpnlEthernetSettings.DEVuMEEthernetIP.FieldControl0
var DeviceIPAdd1=Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.panel1.MDEVtbParent.MDEVtbpDevice.MDEVpnlDevice.DEVdwsDecDeviceDetails.ManagePMDADevices.DEVgrpCommunicationDetails.DEVpnlEthernetSettings.DEVuMEEthernetIP.FieldControl1
var DeviceIPAdd2=Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.panel1.MDEVtbParent.MDEVtbpDevice.MDEVpnlDevice.DEVdwsDecDeviceDetails.ManagePMDADevices.DEVgrpCommunicationDetails.DEVpnlEthernetSettings.DEVuMEEthernetIP.FieldControl2
var DeviceIPAdd3=Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.panel1.MDEVtbParent.MDEVtbpDevice.MDEVpnlDevice.DEVdwsDecDeviceDetails.ManagePMDADevices.DEVgrpCommunicationDetails.DEVpnlEthernetSettings.DEVuMEEthernetIP.FieldControl3
var BtnSave_DeviceDetails=Aliases.iQ_Plus.ModalDialogContainer.MDLGCTRpnlContainer.ModelDialogContainerWorkspace.ManageDevices.MDEVbtnSave
function EnterDeviceDetails(devicetype,deviceName,deviceSerialNo,deviceIPAdd)
{
  IPSec0= aqString.Find(deviceIPAdd,".")
  IPAdd0= aqString.SubString(deviceIPAdd,0,IPSec0)
  IPSec1=aqString.Find((aqString.SubString(deviceIPAdd,IPSec0+1,aqString.GetLength(deviceIPAdd))),".")
  IPAdd1= aqString.SubString(aqString.SubString(deviceIPAdd,IPSec0+1,aqString.GetLength(deviceIPAdd)),0,IPSec1)
  IPSec2=aqString.Find((aqString.SubString(deviceIPAdd,IPSec1+1,aqString.GetLength(deviceIPAdd))),".")
  IPAdd2= aqString.SubString(aqString.SubString(deviceIPAdd,IPSec1+1,aqString.GetLength(deviceIPAdd)),0,IPSec2)
  IPSec3=aqString.Find((aqString.SubString(deviceIPAdd,IPSec2+1,aqString.GetLength(deviceIPAdd))),".")
  IPAdd3= aqString.SubString(aqString.SubString(deviceIPAdd,IPSec2+1,aqString.GetLength(deviceIPAdd)),0,IPSec3)  
  switch (devicetype)
  {
    case "IDM+18":
        {
          DeviceName.SetText(deviceName)
          Log.Message("Sets the device name")
          DeviceSerialNo.SetText(deviceSerialNo)
          Log.Message("Sets the device Serial No")
          DeviceIPAdd0.SetText(IPAdd0)   
          DeviceIPAdd1.SetText(IPAdd1)  
          DeviceIPAdd2.SetText(IPAdd2)
          DeviceIPAdd3.SetText(IPAdd3)  
          BtnSave_DeviceDetails.ClickButton()
          Log.Message("Device Created successfully.")      
        }
      break;
  }
  
}