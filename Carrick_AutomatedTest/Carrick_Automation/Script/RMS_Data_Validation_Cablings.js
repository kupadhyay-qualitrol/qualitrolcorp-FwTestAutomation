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
//USEUNIT Cabling_18Channel

var DATASETFOLDERPATH = Project.ConfigPath +"TestData\\CablingDataSet\\RMS_Data_Validation_Cablings\\"
var DEVICEIP ="10.75.58.51"
var CASHELTYPE ="IDM+18"
var DEVICENAME ="IND_DAU_51"
var DEVICESERIALNO ="409026540"
var DRIVEINSTANCE
var TESTLOG
var DEVICESTATUS
var BUSBAR1_NAME ="Busbar 1"
var BUSBAR2_NAME ="Busbar 2"
var COUNTER


function RMSValidation(cablingName)
{
  try
  {
    Log.Message("Started TC:-Test to check " +cablingName+ " Cabling")
    var dataSheetName = DATASETFOLDERPATH+ cablingName + ".xlsx"
    DRIVEINSTANCE=SeleniumWebdriver.InitialiseWebdriver(DEVICEIP)
    
    do
    {
      DEVICESTATUS=CommonMethod.GetDeviceStatusOnPing(DEVICEIP)
    }
    while (DEVICESTATUS!="Success")
    
    //Step1. Upload Calibration
    switch (cablingName)
    {    
      case "3U":
        AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DEVICEIP,DRIVEINSTANCE,DATASETFOLDERPATH+"Default.cal"),"Uploading Calibration File")//Default calibration isn uploading because we are not going to change calibration file in this function    
        break
    }    
    //Step2. Check if iQ+ is running or not
    AssertClass.IsTrue(CommonMethod.IsExist("iQ-Plus"),"Checking if iQ+ is running or not")
    
    //Step3.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CASHELTYPE,DEVICENAME)!=true)
    {
      GeneralPage.CreateDevice(CASHELTYPE,DEVICENAME,DEVICESERIALNO,DEVICEIP)
      DeviceTopologyPage.ClickonDevice(CASHELTYPE,DEVICENAME)      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    
    //Step4. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step5. Click on Analog Inputs
    AssertClass.IsTrue(ConfigEditorPage.ClickOnAnalogInputs(),"Clicked on Analog Inputs")
    
    //Step6. Get Channel Count
    var deviceType = ConfigEditor_DeviceOverview_AnalogInputs.GetChannelCount()
 
    //Step7. Click on Circuits
    AssertClass.IsTrue(ConfigEditorPage.ClickOnCircuits(),"Clicked on Circuits") 
    
    //Step8.1 Delete all circuits    
    while(ConfigEditor_Circuits.GetCircuitsCount()>0)
    {
      AssertClass.IsTrue(ConfigEditor_Circuits.ClickOnDeletePresentCircuit())
    }
    
    //Step8.2 Configure Circuit
    Circuit_Configuration.GetCircuitConfiguration(dataSheetName,"Cabling",deviceType)
    
    //Configure Busbar1
    if(Circuit_Configuration.Busbar1.length>0)
    {
      AssertClass.IsTrue(ConfigEditor_Circuits.ClickOnAddNewCircuit(),"Clicked on Add New Circuit")
      if(ConfigEditor_Circuits.GetBusbar_Name(0)!= BUSBAR1_NAME)
      {
        if(ConfigEditor_Circuits.GetGroupName()!=ConfigEditor_Circuits.GetBusbar_Name(0))
        {
          AssertClass.IsTrue(ConfigEditor_Circuits.SwitchBusbar(GetBusbar_Name(0)),"Switched Busbar")  
        }
      
        AssertClass.IsTrue(ConfigEditor_Circuits.SetGroupName(BUSBAR1_NAME),"Setting Busbar 1")
      }
      if (ConfigEditor_Circuits.GetBusbar_Name(1)!= BUSBAR2_NAME)
      {
        if(ConfigEditor_Circuits.GetGroupName()!=ConfigEditor_Circuits.GetBusbar_Name(1))
        {
          AssertClass.IsTrue(ConfigEditor_Circuits.SwitchBusbar(GetBusbar_Name(1)),"Switched Busbar")  
        }   
        AssertClass.IsTrue(ConfigEditor_Circuits.SetGroupName(BUSBAR2_NAME),"Setting Busbar 2")      
      }

      AssertClass.IsTrue(ConfigEditor_Circuits.SwitchBusbar(BUSBAR1_NAME),"Switched to Busbar 1")

         
      AssertClass.IsTrue(Circuit_Configuration.SetBusbar1(),"Setting Busbar 1")
    
      //Configure Busbar 1 Feeders
      Circuit_Configuration.ConfigureBB1Feeder(BUSBAR1_NAME)
      //Configure Busbar2
      if(Circuit_Configuration.Busbar2.length>0)
      {
        AssertClass.IsTrue(ConfigEditor_Circuits.ClickOnAddNewCircuit(),"Clicked on Add New Circuit")
        AssertClass.IsTrue(ConfigEditor_Circuits.SwitchBusbar(BUSBAR2_NAME),"Switched to Busbar 2") 
              
        AssertClass.IsTrue(Circuit_Configuration.SetBusbar2(),"Setting Busbar 2")    
        //Configure Busbar2 Feeder
        Circuit_Configuration.ConfigureBB2Feeder(BUSBAR2_NAME)
      }
    }
    
    //Step9. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step10. Wait for Device to go in reboot
    COUNTER =0
    do
    {
      DEVICESTATUS=CommonMethod.GetDeviceStatusOnPing(DEVICEIP)
      COUNTER=COUNTER+1
      aqUtils.Delay(1000)
    }
    while (DEVICESTATUS=="Success" && COUNTER<=100)
    
    //Step11. Check if Device is up
    do
    {
      DEVICESTATUS=CommonMethod.GetDeviceStatusOnPing(DEVICEIP)
    }
    while (DEVICESTATUS!="Success")

    COUNTER =0
    do
    {
      DEVICESTATUS=CommonMethod.GetDeviceStatusOnPing(DEVICEIP)
      if(DEVICESTATUS=="Success")
      {
        COUNTER =COUNTER+1
      }
      aqUtils.Delay(1000)
    }
    while (COUNTER<=30)
    
    //Step12. Validate from Tabindex
    AssertClass.IsTrue(Firmware_Tabindex_Methods.ValidateCabling(DRIVEINSTANCE,TESTLOG,DEVICEIP,DATASETFOLDERPATH,cablingName,FwVersion))   
    
    if(cablingName=="NOCIRCUIT")
    {
      AssertClass.IsTrue(Firmware_Mfgindex_Methods.UploadCalibration(DEVICEIP,DRIVEINSTANCE,DATASETFOLDERPATH+"Default.cal"),"Uploading Calibration File")    
    }//No circuit is kept here because when we upload calibration file we have to make circuit as NoCircuit so that device won't get bad configuration.
    Log.Message("Pass:- Test to check Cabling:-"+cablingName)  
  }
  catch (ex)
  {
    Log.Message(ex.stack)    
    Log.Error("Fail:-Test to check Cabling:-"+cablingName)
  }
  finally
  {
    SeleniumWebdriver.TearDown() 
  }
}

function StartReport()
{
  SeleniumWebdriver.StartReport(DEVICEIP)
}

function EndReport()
{
  SeleniumWebdriver.EndReport()
}

function SetAnalogChannelName()
{
    var datasheetname = DATASETFOLDERPATH+ "NOCIRCUIT.xlsx"
    var channelNameChangeCounter =0
    if(DeviceTopologyPage.ClickonDevice(CASHELTYPE,DEVICENAME)!=true)
    {
      GeneralPage.CreateDevice(CASHELTYPE,DEVICENAME,DEVICESERIALNO,DEVICEIP)
      DeviceTopologyPage.ClickonDevice(CASHELTYPE,DEVICENAME)      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    
    //Step0. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    //Step1. Click on Analog Inputs
    AssertClass.IsTrue(ConfigEditorPage.ClickOnAnalogInputs(),"Clicked on Analog Inputs")
    
    //Step2. Set Channel Name
    //Step2.1 Get RowCount
    var deviceType = ConfigEditor_DeviceOverview_AnalogInputs.GetChannelCount()
    
    //Step3. Check Channel name & Set it if it is different from DataSheet
    for (let analogRows=0 ; analogRows< deviceType;analogRows++)
    {
      var dataSheetChannelName =CommonMethod.ReadDataFromExcel(datasheetname,"label","Cabling",analogRows)
    
      if(ConfigEditor_DeviceOverview_AnalogInputs.GetChannelName(analogRows)!= dataSheetChannelName)
      {
        AssertClass.IsTrue(ConfigEditor_DeviceOverview_AnalogInputs.SetChannelName(analogRows,dataSheetChannelName),"Sets the channel name for row:- "+analogRows)
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

function RMSValidationNOCIRCUIT()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test NOCIRCUIT Cabling")
  RMSValidation("NOCIRCUIT")
}

function RMSValidation3U()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test 3U Cabling")
  RMSValidation("NOCIRCUIT")
  RMSValidation("3U")
}

function RMSValidation3U3I()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test 3U3I Cabling")
  RMSValidation("3U3I")
}
function RMSValidation3U3I3I()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test 3U3I3I Cabling")
  RMSValidation("3U3I3I")
}

function RMSValidation3U3I3I3I()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test 3U3I3I3I Cabling")
  RMSValidation("3U3I3I3I")
}
function RMSValidation4U()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test 4U Cabling")
  RMSValidation("4U")
}

function RMSValidation4U3I()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test 4U3I Cabling")
  RMSValidation("4U3I")
}

function RMSValidation4U3I3I()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test 4U3I3I Cabling")
  RMSValidation("4U3I3I")
}

function RMSValidation4U3I3I3I()
{
  TESTLOG = SeleniumWebdriver.StartTestCaseReport("Test 4U3I3I3I Cabling")
  RMSValidation("4U3I3I3I")
}