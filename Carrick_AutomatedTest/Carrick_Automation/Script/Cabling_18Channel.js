/*This file contains Test Cases related to Cabling Configuration*/

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

function TestNOCIRCUITCabling()
{
  try
  {
    Log.Message("Started TC:-Test to check NOCIRCUIT Cabling")
    var DataSheetName = Project.ConfigPath +"TestData\\CablingDataSet\\CablingDataSet_18Channel\\NOCIRCUIT.xlsx"
    var CalibrationFilePath = Project.ConfigPath +"TestData\\CablingDataSet\\CablingDataSet_18Channel\\DefaultCalibration.cal"
    var DeviceIP="10.75.58.51"
    //Step0. Start Report
    var DriverInstance=SeleniumWebdriver.InitialiseWebdriver(DeviceIP)
    AssertClass.IsTrue(SeleniumWebdriver.StartReport(DeviceIP),"Starting Report")
    //Step1. Initialise ChromeDriver     
    AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DeviceIP,DriverInstance,CalibrationFilePath),"Uploading Calibration File")
    
    SeleniumWebdriver.StartTestCaseReport("Test NOCIRCUIT Cabling")
    //Step2. Check if iQ+ is running or not
    AssertClass.IsTrue(CommonMethod.IsExist("iQ-Plus"),"Checking if iQ+ is running or not")
    
    //Step3.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType","DeviceInfo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName","DeviceInfo"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    
    //Step4. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step5. Click on Analog Inputs
    AssertClass.IsTrue(ConfigEditorPage.ClickOnAnalogInputs(),"Clicked on Analog Inputs")
    
    //Step6. Set Channel Name
    //Step6.1 Get RowCount
    var DeviceType = ConfigEditor_DeviceOverview_AnalogInputs.GetChannelCount()
    
    //Step6.2 Check Channel name & Set it if it is different from DataSheet
    for (AnalogRows=0 ; AnalogRows< DeviceType;AnalogRows++)
    {
      var DataSheetChannelName =CommonMethod.ReadDataFromExcel(DataSheetName,"label","Cabling",AnalogRows)
    
      if(ConfigEditor_DeviceOverview_AnalogInputs.GetChannelName(AnalogRows)!= DataSheetChannelName)
      {
        AssertClass.IsTrue(ConfigEditor_DeviceOverview_AnalogInputs.SetChannelName(AnalogRows,DataSheetChannelName),"Sets the channel name for row:- "+AnalogRows)
      }
    }
    
    //Step7. Click on Circuits Configuration
    AssertClass.IsTrue(ConfigEditorPage.ClickOnCircuits(),"Clicked on Circuits")
    
    //Step8. Delete All Circuits
    //Step8.1 Get Circuit Count
    var CiruitsCount=ConfigEditor_Circuits.GetCircuitsCount()
    
    //Step8.2 Delete all circuits
    if(CiruitsCount>0)
    {
      AssertClass.IsTrue(ConfigEditor_Circuits.ClickOnDeletePresentCircuit())
    }
    
    //Step9. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step10. Wait for Device to go in reboot
    aqUtils.Delay(40000)
    
    //Step11. Check if Device is up
    var DeviceStatus
    do
    {
      DeviceStatus=CommonMethod.GetDeviceStatusOnPing("10.75.58.51")
    }
    while (DeviceStatus!="Success")
    
    //Step12. Wait for device to be stable
    aqUtils.Delay(120000)
    
    //Step13. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step14. Click on Circuits
    AssertClass.IsTrue(ConfigEditorPage.ClickOnCircuits(),"Clicked on Circuits")
    
    //Step14. Get Circuit Count
    AssertClass.CompareDecimalValues(0, ConfigEditor_Circuits.GetCircuitsCount(),0,"Checking Circuits Counts")
    
    //Step15. Close Config Editor
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close Config Editor")
    
    Log.Message("Pass:- Test to check NOCIRCUIT Cabling")  
  }
  catch (ex)
  {
    Log.Message(ex.stack)
    Log.Error("Fail:-Test to check NOCIRCUIT Cabling")
  }
  finally
  {
    SeleniumWebdriver.TearDown(DeviceIP)
    SeleniumWebdriver.EndReport(DeviceIP)
  }
}