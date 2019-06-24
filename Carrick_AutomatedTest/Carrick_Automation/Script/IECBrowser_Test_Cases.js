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
    var retrycount_61850 = 16
    
    //Step0. Check whether IEC61850 is up or not
    do
    {
    GooseWIndow=DataRetrievalPage.OpenGooseStatusWindow()
    retrycount_61850 = retrycount_61850-1
    }
    while (retrycount_61850>1 && GooseWIndow==false)
    AssertClass.IsTrue(DataRetrievalPage.CloseGooseStatusWindow(),"Closed Goose Status Window")
    
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
