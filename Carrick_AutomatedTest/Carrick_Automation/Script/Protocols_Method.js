//This file contains methods and objects variable related to Protocols page

//USEUNIT ConfigEditor_Protocols
//USEUNIT AssertClass
//USEUNIT ConfigEditor_ConfigurationPage
//USEUNIT ConfigEditorPage
//USEUNIT DeviceManagementPage
//USEUNIT DeviceTopologyPage

//This method is used to add protocols in a device
function EnableProtocols()
{
  
  var dataSheetName = Project.ConfigPath +"TestData\\BTC_792.xlsx";
  var noOfProtocols
  //Step Check for the device in the device tree
  if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"))      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
  //Step0. Retrieve Configuration
  AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
  
  //Step1. Click on Advanced under view of configeditor
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickAdvanceInView(),"Clicked on Advanced in View of configeditor")
  
  //Step2. Click on Protocols
  AssertClass.IsTrue(ConfigEditorPage.ClickOnProtocols(),"Clicked on Protocols in config editor tree view")
  
  //Step3. Remove all the added protocols
  for (let noOfProtocols=0;noOfProtocols<6;noOfProtocols++ )
  {
    if(ConfigEditor_Protocols.Btn_Remove_Protocol.Enabled)
    {
      AssertClass.IsTrue(ConfigEditor_Protocols.ClickOnRemoveProtocol(),"Removed all the added protocol")
    }
    else
    {
      Log.Message("remove Protocol button is disable")
      break
    }
  }
  //Step4. Click on Add protocols
  AssertClass.IsTrue(ConfigEditor_Protocols.ClickOnAddProtocol(),"Clicked on Add Protocol")
  
  //Step5. Select Protocols from the dropdown and click on ok
  AssertClass.IsTrue(ConfigEditor_Protocols.SelectProtocol(CommonMethod.ReadDataFromExcel(dataSheetName,"Protocol_Name")),"Protocol has been selected")
  
  //Step6. Click on send to device button
  AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Configuration has been sent to device")
}