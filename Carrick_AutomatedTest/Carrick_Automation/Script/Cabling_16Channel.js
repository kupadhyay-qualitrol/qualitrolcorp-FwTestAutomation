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
//USEUNIT Circuit_Configuration
//USEUNIT Firmware_Tabindex_Methods
//USEUNIT IECBrowser_Test_Cases

var DataSetFolderPath = Project.ConfigPath +"TestData\\CablingDataSet\\CablingDataSet_16Channel\\"
var DeviceIP ="10.75.58.66"
var CashelType ="IDM-E"
var DeviceName ="IDC-DAU-66"
var DeviceSerialNo ="414638044"
var DriverInstance
var TestLog
var DeviceStatus
var Busbar1_Name ="Busbar 1"
var Busbar2_Name ="Busbar 2"
var Counter

function TestCablingIDME(DatasetFolderPath,CablingName,TestLog)
{
  try
  {
    Log.Message("Started TC:-Test to check " +CablingName+ " Cabling")
    var dataSheetName = DataSetFolderPath+ CablingName + ".xlsx"
    DriverInstance=SeleniumWebdriver.InitialiseWebdriver(DeviceIP)
    
    do
    {
      DeviceStatus=CommonMethod.GetDeviceStatusOnPing(DeviceIP)
    }
    while (DeviceStatus!="Success")
    
    //Step1. Upload Calibration
    switch (CablingName)
    {    
      case "3U":
        AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DeviceIP,DriverInstance,DataSetFolderPath+"3U_13I.cal"),"Uploading Calibration File")    
        break
      case "2M3U":
        AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DeviceIP,DriverInstance,DataSetFolderPath+"6U_10I.cal"),"Uploading Calibration File")    
        break
//      case "4U":
//        AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DeviceIP,DriverInstance,DataSetFolderPath+"4U_14I.cal"),"Uploading Calibration File")    
//        break
//      case "2M4U":
//        AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DeviceIP,DriverInstance,DataSetFolderPath+"8U_10I.cal"),"Uploading Calibration File")    
//        break
//      case "4U3U":
//        AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DeviceIP,DriverInstance,DataSetFolderPath+"7U_11I.cal"),"Uploading Calibration File")    
//        break
    }    
    //Step2. Check if iQ+ is running or not
    AssertClass.IsTrue(CommonMethod.IsExist("iQ-Plus"),"Checking if iQ+ is running or not")
    
    //Step3.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CashelType,DeviceName)!=true)
    {
      GeneralPage.CreateDevice(CashelType,DeviceName,DeviceSerialNo,DeviceIP)
      DeviceTopologyPage.ClickonDevice(CashelType,DeviceName)      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    
    //Step4. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step5. Click on Analog Inputs
    AssertClass.IsTrue(ConfigEditorPage.ClickOnAnalogInputs(),"Clicked on Analog Inputs")
    
    //Step6 Get RowCount
    var noOfChannels = ConfigEditor_DeviceOverview_AnalogInputs.GetChannelCount()
 
    //Step7. Click on Circuits ConfiguBusbar1ration
    AssertClass.IsTrue(ConfigEditorPage.ClickOnCircuits(),"Clicked on Circuits") 
    
    //Step8.1 Delete all circuits    
    while(ConfigEditor_Circuits.GetCircuitsCount()>0)
    {
      AssertClass.IsTrue(ConfigEditor_Circuits.ClickOnDeletePresentCircuit())
    }
    
    //Step8.2 Configure Circuit
    Circuit_Configuration.GetCircuitConfiguration(dataSheetName,"Cabling",noOfChannels)
    
    //Configure Busbar1
    if(Circuit_Configuration.Busbar1.length>0)
    {
      AssertClass.IsTrue(ConfigEditor_Circuits.ClickOnAddNewCircuit(),"Clicked on Add New Circuit")
      if(ConfigEditor_Circuits.GetBusbar_Name(0)!= Busbar1_Name)
      {
        if(ConfigEditor_Circuits.GetGroupName()!=ConfigEditor_Circuits.GetBusbar_Name(0))
        {
          AssertClass.IsTrue(ConfigEditor_Circuits.SwitchBusbar(GetBusbar_Name(0)),"Switched Busbar")  
        }
      
        AssertClass.IsTrue(ConfigEditor_Circuits.SetGroupName(Busbar1_Name),"Setting Busbar 1")
      }
      if (ConfigEditor_Circuits.GetBusbar_Name(1)!= Busbar2_Name)
      {
        if(ConfigEditor_Circuits.GetGroupName()!=ConfigEditor_Circuits.GetBusbar_Name(1))
        {
          AssertClass.IsTrue(ConfigEditor_Circuits.SwitchBusbar(GetBusbar_Name(1)),"Switched Busbar")  
        }   
        AssertClass.IsTrue(ConfigEditor_Circuits.SetGroupName(Busbar2_Name),"Setting Busbar 2")      
      }

      AssertClass.IsTrue(ConfigEditor_Circuits.SwitchBusbar(Busbar1_Name),"Switched to Busbar 1")

         
      AssertClass.IsTrue(Circuit_Configuration.SetBusbar1(),"Setting Busbar 1")
    
      //Configure Busbar 1 Feeders
      Circuit_Configuration.ConfigureBB1Feeder(Busbar1_Name)
      //Configure Busbar2
      if(Circuit_Configuration.Busbar2.length>0)
      {
        AssertClass.IsTrue(ConfigEditor_Circuits.ClickOnAddNewCircuit(),"Clicked on Add New Circuit")
        AssertClass.IsTrue(ConfigEditor_Circuits.SwitchBusbar(Busbar2_Name),"Switched to Busbar 1") 
              
        AssertClass.IsTrue(Circuit_Configuration.SetBusbar2(),"Setting Busbar 2")    
        //Configure Busbar2 Feeder
        Circuit_Configuration.ConfigureBB2Feeder(Busbar2_Name)
      }
    }
    
    //Step9. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step10. Wait for Device to go in reboot
    Counter =0
    do
    {
      DeviceStatus=CommonMethod.GetDeviceStatusOnPing(DeviceIP)
      Counter=Counter+1
      aqUtils.Delay(1000)
    }
    while (DeviceStatus=="Success" && Counter<=100)
    
    //Step11. Check if Device is up
    do
    {
      DeviceStatus=CommonMethod.GetDeviceStatusOnPing(DeviceIP)
    }
    while (DeviceStatus!="Success")

    Counter =0
    do
    {
      DeviceStatus=CommonMethod.GetDeviceStatusOnPing(DeviceIP)
      if(DeviceStatus=="Success")
      {
        Counter =Counter+1
      }
      aqUtils.Delay(1000)
    }
    while (Counter<=30)
    
    //Step12. Validate from Tabindex
    AssertClass.IsTrue(Firmware_Tabindex_Methods.ValidateCabling(DriverInstance,TestLog,DeviceIP,DataSetFolderPath,CablingName))   
    
    if(CablingName=="NOCIRCUIT")
    {
      AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DeviceIP,DriverInstance,DataSetFolderPath+"DefaultCalibration.cal"),"Uploading Calibration File")    
    }
    Log.Message("Pass:- Test to check Cabling:-"+CablingName)  
  }
  catch (ex)
  {
    Log.Message(ex.stack)    
    Log.Error("Fail:-Test to check Cabling:-"+CablingName)
  }
  finally
  {
    SeleniumWebdriver.TearDown() 
  }
}

function StartReportIDME()
{
  SeleniumWebdriver.StartReport(DeviceIP)
}

function EndReportIDME()
{
  SeleniumWebdriver.EndReport()
}

function SetAnalogChannelNameIDME()
{
    var datasheetname = DataSetFolderPath+ "NOCIRCUIT.xlsx"
    var channelNameChangeCounter =0
    if(DeviceTopologyPage.ClickonDevice(CashelType,DeviceName)!=true)
    {
      GeneralPage.CreateDevice(CashelType,DeviceName,DeviceSerialNo,DeviceIP)
      DeviceTopologyPage.ClickonDevice(CashelType,DeviceName)      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    
    //Step1. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    //Step2. Click on Analog Inputs
    AssertClass.IsTrue(ConfigEditorPage.ClickOnAnalogInputs(),"Clicked on Analog Inputs")
    
    //Step3.1 Get RowCount
    var noOfChannels = ConfigEditor_DeviceOverview_AnalogInputs.GetChannelCount()
    
    //Step3.2 Check Channel name & Set it if it is different from DataSheet
    for (let AnalogRows=0 ; AnalogRows< noOfChannels;AnalogRows++)
    {
      var DataSheetChannelName =CommonMethod.ReadDataFromExcel(datasheetname,"label","Cabling",AnalogRows)
    
      if(ConfigEditor_DeviceOverview_AnalogInputs.GetChannelName(AnalogRows)!= DataSheetChannelName)
      {
        AssertClass.IsTrue(ConfigEditor_DeviceOverview_AnalogInputs.SetChannelName(AnalogRows,DataSheetChannelName),"Sets the channel name for row:- "+AnalogRows)
        channelNameChangeCounter=channelNameChangeCounter+1
      }
    }
        
    if(channelNameChangeCounter>=0)
    {
      //Step4. Send to Device
      AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    }
    else
    {
      //Step5. Click on Close
      AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close")
    }
}

function TestIDMECablingNOCIRCUIT()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test NOCIRCUIT Cabling")
  TestCablingIDME(DataSetFolderPath,"NOCIRCUIT",TestLog)
}

function TestIDMECabling3U()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U Cabling")
  TestCablingIDME(DataSetFolderPath,"3U",TestLog)
}

function TestIDMECabling3U3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U3I Cabling")
  TestCablingIDME(DataSetFolderPath,"3U3I",TestLog)
}

function TestIDMECabling3U3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"3U3I3I",TestLog)
}

function TestIDMECabling3U3I3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U3I3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"3U3I3I3I",TestLog)
}

function TestIDMECabling3U3I3I3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U3I3I3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"3U3I3I3I3I",TestLog)
}

function TestIDMECabling1U3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 1U3I Cabling")
  TestCablingIDME(DataSetFolderPath,"1U3I",TestLog)
}

function TestIDMECabling1U3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 1U3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"1U3I3I",TestLog)
}

function TestIDMECabling1U3I3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 1U3I3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"1U3I3I3I",TestLog)
}

function TestCablingIDME1U3I3I3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 1U3I3I3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"1U3I3I3I3I",TestLog)
}

function TestCablingIDME2M3U()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M3U Cabling")
  TestCablingIDME(DataSetFolderPath,"2M3U",TestLog)
}

function TestCablingIDME2M3U3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M3U3I Cabling")
  TestCablingIDME(DataSetFolderPath,"2M3U3I",TestLog)
}

function TestCablingIDME2M3U3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M3U3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"2M3U3I3I",TestLog)
}

function TestCablingIDME2M3U3I3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M3U3I3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"2M3U3I3I3I",TestLog)
}
function TestCablingIDME1U1U3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 1U1U3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"1U1U3I3I",TestLog)
}

function TestCablingIDME1U1U3I3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 1U1U3I3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"1U1U3I3I3I",TestLog)
}

function TestCablingIDME3U1U3I3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U1U3I3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"3U1U3I3I3I",TestLog)
}

function TestCablingIDME3U3I1U3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U3I1U3I Cabling")
  TestCablingIDME(DataSetFolderPath,"3U3I1U3I",TestLog)
}

function TestCablingIDME3U3I1U3I3I()
{
  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U3I1U3I3I Cabling")
  TestCablingIDME(DataSetFolderPath,"3U3I1U3I3I",TestLog)
}

//function TestCabling3U3I1U3I3I3I()
//{
// TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U3I1U3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"3U3I1U3I3I3I",TestLog)
//}
//
//function TestCablingS1U1U3I3I3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test S1U1U3I3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"S1U1U3I3I3I3I",TestLog)
//}
//
//function TestCablingS3U1U3I3I3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test S3U1U3I3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"S3U1U3I3I3I3I",TestLog)
//}
//
//function TestCabling4U()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U Cabling")
//  TestCabling(DataSetFolderPath,"NOCIRCUIT",TestLog)
//  TestCabling(DataSetFolderPath,"4U",TestLog)
//}
//
//function TestCabling4U3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3I",TestLog)
//}
//
//function TestCabling4U3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3I3I",TestLog)
//}
//
//function TestCabling4U3I3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3I3I3I",TestLog)
//}
//
//function TestCabling4U3I3I3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3I3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3I3I3I3I",TestLog)
//}
//
//function TestCabling4U4I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U4I Cabling")
//  TestCabling(DataSetFolderPath,"4U4I",TestLog)
//}
//
//function TestCabling4U4I4I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U4I4I Cabling")
//  TestCabling(DataSetFolderPath,"4U4I4I",TestLog)
//}
//
//function TestCabling4U4I4I4I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U4I4I4I Cabling")
//  TestCabling(DataSetFolderPath,"4U4I4I4I",TestLog)
//}
//
//function TestCabling2M4U()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M4U Cabling")
//  TestCabling(DataSetFolderPath,"NOCIRCUIT",TestLog)
//  TestCabling(DataSetFolderPath,"2M4U",TestLog)
//}
//
//function TestCabling2M4U3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M4U3I Cabling")
//  TestCabling(DataSetFolderPath,"2M4U3I",TestLog)
//}
//
//function TestCabling2M4U3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M4U3I3I Cabling")
//  TestCabling(DataSetFolderPath,"2M4U3I3I",TestLog)
//}
//
//function TestCabling2M4U3I3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M4U3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"2M4U3I3I3I",TestLog)
//}
//
//function TestCabling2M4U4I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M4U4I Cabling")
//  TestCabling(DataSetFolderPath,"2M4U4I",TestLog)
//}
//
//function TestCabling2M4U4I4I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 2M4U4I4I Cabling")
//  TestCabling(DataSetFolderPath,"2M4U4I4I",TestLog)
//}
//
//function TestCabling4U3U()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3U Cabling")
//  TestCabling(DataSetFolderPath,"NOCIRCUIT",TestLog)
//  TestCabling(DataSetFolderPath,"4U3U",TestLog)
//}
//
//function TestCabling4U3U3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3U3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3U3I",TestLog)
//}
//
//function TestCabling4U3U3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3U3I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3U3I3I",TestLog)
//}
//
//function TestCabling4U3U3I3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3U3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3U3I3I3I",TestLog)
//}
//
//function TestCabling3U4U()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U4U Cabling")
//  TestCabling(DataSetFolderPath,"3U4U",TestLog)
//}
//
//function TestCabling3U4U3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U4U3I Cabling")
//  TestCabling(DataSetFolderPath,"3U4U3I",TestLog)
//}
//
//function TestCabling3U4U3I3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U4U3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"3U4U3I3I3I",TestLog)
//}
//
//function TestCabling4U3U4I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3U4I Cabling")
//  TestCabling(DataSetFolderPath,"4U3U4I",TestLog)
//}
//
//function TestCabling4U3U4I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3U4I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3U4I3I",TestLog)
//}
//
//function TestCabling4U3U4I4I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3U4I4I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3U4I4I3I",TestLog)
//}
//
//function TestCabling3U4U3I3I4I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 3U4U3I3I4I Cabling")
//  TestCabling(DataSetFolderPath,"3U4U3I3I4I",TestLog)
//}
//
//function TestCabling4U1U3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U1U3I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U1U3I3I",TestLog)
//}
//
//function TestCabling4U1U3I3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U1U3I3I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U1U3I3I3I",TestLog)
//}
//
////function TestCabling4U1U4I3I() //not Supported in iQ+
////{
////  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U1U4I3I Cabling")
////  TestCabling(DataSetFolderPath,"4U1U4I3I",TestLog)
////}
//
////function TestCabling4U1U4I4I3I() //Not Supported in iQ+
////{
////  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U1U4I4I3I Cabling")
////  TestCabling(DataSetFolderPath,"4U1U4I4I3I",TestLog)
////}
//
//function TestCabling4U3I1U3I3I()
//{
//  TestLog = SeleniumWebdriver.StartTestCaseReport("Test 4U3I1U3I3I Cabling")
//  TestCabling(DataSetFolderPath,"4U3I1U3I3I",TestLog)
//}