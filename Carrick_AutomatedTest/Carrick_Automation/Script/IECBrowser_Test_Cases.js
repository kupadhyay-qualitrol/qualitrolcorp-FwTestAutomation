/*This file contains Test Cases related to IECBrowser*/

//USEUNIT CommonMethod
//USEUNIT Firmware_Mfgindex_Methods
//USEUNIT AssertClass
//USEUNIT SeleniumWebdriver
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_DeviceOverview_AnalogInputs
//USEUNIT ConfigEditor_Circuits
//USEUNIT DeviceTopologyPage
//USEUNIT GeneralPage
//USEUNIT DeviceManagementPage
//USEUNIT Circuit_Configuration
//USEUNIT Firmware_Tabindex_Methods
//USEUNIT ConfigEditor_Protocols
//USEUNIT Protocols_Method
//USEUNIT IECBrowserPage
//USEUNIT IECBrowser_Methods

//This test case is used to test device connection on IEC browser
function BTC_792()
{
  try
  {
    Log.Message("Started TC:-Test to Verify IEC browser connection and disconnection with the device")
    var DataSheetName = Project.ConfigPath +"TestData\\BTC_792.xlsx";
    
    //Step1. Check if iQ-Plus is running or not.
    AssertClass.IsTrue(CommonMethod.IsExist("iQ-Plus"),"Checking if iQ+ is running or not")
    
    //Step2.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    //Step3. Enable protocol in IQ+
    Protocols_Method.EnableProtocols(CommonMethod.ReadDataFromExcel(DataSheetName,"Protocol_Name")),"Protocols enable in the device"
    
    //Step3. Connect device to IEC browser
    AssertClass.IsTrue(IECBrowser_Methods.ConnectDeviceToIECBrowser(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"),"Device connected to IEC Browser"))
    
    //Step4. Verify the connection of device on IEC Browser
    AssertClass.IsTrue(IECBrowser_Methods.VerifyDeviceConnection(),"Downloaded the CID file")
    
    //Step5. Disconnect the device on IECBrowser 
    AssertClass.IsTrue(IECBrowserPage.DisconnectDevice(),"Disconnected devices from IECBrowser")
    AssertClass.IsTrue(IECBrowserCommonMethod.CloseIECBrowser(),"IECBrowser closed successfully")
    Log.Message("Pass:-Test to Verify IEC browser connection and disconnection with the device")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to Verify IEC browser connection and disconnection with the device")
  }
}
