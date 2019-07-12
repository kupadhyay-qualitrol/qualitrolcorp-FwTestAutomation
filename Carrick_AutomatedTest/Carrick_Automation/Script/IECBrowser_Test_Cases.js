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
//USEUNIT DataRetrievalPage

//This test case is used to test device connection on IEC browser
function BTC_792()
{
  try
  {
    Log.Message("Started TC:-Test to Verify IEC browser connection and disconnection with the device")
    var DataSheetName = Project.ConfigPath +"TestData\\BTC_792.xlsx";
    aqUtils.Delay(150*Referesh_time)//Delay for IECBrowser service to restore as on IEC Browser there is no way to check if service is up or not.
      
    //Step1. Connect device to IEC browser
    IECBrowser_Methods.ConnectDeviceToIECBrowser(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"),"Device connected to IEC Browser")

    //Step2. Verify the connection of device on IEC Browser
    IECBrowser_Methods.VerifyDeviceConnection(),"Downloaded the CID file"
    
    //Step3. Disconnect the device on IECBrowser 
    AssertClass.IsTrue(IECBrowserPage.DisconnectDevice(),"Disconnected devices from IECBrowser")
    IECBrowserCommonMethod.CloseIECBrowser(),"IECBrowser closed successfully"
    Log.Message("Pass:-Test to Verify IEC browser connection and disconnection with the device")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to Verify IEC browser connection and disconnection with the device")
  }
}
