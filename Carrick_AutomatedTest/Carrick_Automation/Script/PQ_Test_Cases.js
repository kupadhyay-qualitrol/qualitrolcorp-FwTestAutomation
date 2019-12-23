//USEUNIT CommonMethod
//USEUNIT DataRetrievalPage
//USEUNIT DeviceManagementPage
//USEUNIT AssertClass
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_PQ
//USEUNIT PQ_Methods
//USEUNIT DeviceTopologyPage
//USEUNIT OmicronQuickCMCPage
//USEUNIT GeneralPage


function pqFreeIntervalRecordingDownloadWithQuantities(deviceType,deviceName,deviceSerialNo,deviceIPAdd,retryCountDateTime,retryCountPqOpen,filePath)
{
  try
  {
   Log.Message("Test to check for the PQ recording for free interval started or not and download PQ record with quantities") 
   
    //Step.0 Inject voltage and current using omicron
    //OmicronQuickCMCPage.InjectVoltCurrent(Project.ConfigPath+fileName)
   
    //Step1. Check if iQ-Plus is running or not.
    AssertClass.IsTrue(CommonMethod.IsExist("iQ-Plus"),"Checking if iQ+ is running or not")
    
    //Step2.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(deviceType,deviceName)!=true)
    {
      GeneralPage.CreateDevice(deviceType,deviceName,deviceSerialNo,deviceIPAdd)
      DeviceTopologyPage.ClickonDevice(deviceType,deviceName)      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    //Step.3 Clean memory of PQ Free Interval
    PQ_Methods.cleanMemoryPqFreeInterval()
    aqUtils.Delay(3000)//Applying delay so that clean memory window get closed
    Log.Message("Clean the PQ Free Interval records from the device data")
    
    //Step.4 Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step.5 Click on PQ Free Interval
    AssertClass.IsTrue(ConfigEditorPage.clickPqFreeInterval(),"Clicked on PQ Free Interval")
    
    //Step.7 Select RMS values from the Available quantities for PQ Free Inerval
    PQ_Methods.selectRMSChannelCircuitQuantitiesForPQFreeInterval()
    
    
    //Step.8 Send to device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    
    //Step.9 Compare Device time and PQ Start time
    AssertClass.IsTrue(PQ_Methods.compareDateTimePq(retryCountDateTime),"Device time and PQ Free Interval time compared and new record started to form")
    
    //Step.10 Click on PQ Free Interval button
    AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalDirectory(),"Clicked on PQ Free Interval Directory")
    
    //Step.10.1 Check if PQ Free Interval Directroy opens or not
    AssertClass.IsTrue(DataRetrievalPage.checkPqFreeIntervalDirectoryOpen(retryCountPqOpen),"PQ Free Interval directory pane open and displayed")
    
    //Step.11 Set Start date and time for PQ Free Interval
    AssertClass.IsTrue(PQ_Methods.setPqFreeIntervalStartTime(),"PQ Free Interval start time set as 2 minutes before of current system date and time")
    
    //Step.12 Download DDRC record
    AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalDownloadNowButton(),"Downloaded PQ Free Interval Record")
    
    //Step.13 Close DDRC directory
    AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalCloseButton(),"PQ Free Interval directroy closed")
 
    //Step.14 Check if PQ Free Interval Favorite is available if not then Configure Favorite for PQ Free Interval
    AssertClass.IsTrue(PQ_Methods.checkForPqFreeIntervalFavorite(),"Check if PQ Free Interval Favorite is available if not then Configure Favorite for PQ Free Interval")
    
    //Step.15 Export PQ Free Interval Data
    AssertClass.IsTrue(PQ_Methods.exportToCsvPqFreeIntervalData(filePath), "PQ Free Interval Data is exported to CSV")
    
    //Step.21 Close Omicron CMC file
    //AssertClass.IsTrue(OmicronQuickCMCPage.CloseQuickCMC(),"Quick CMC got closed")
  }
    catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for the PQ recording for 10 min and free interval started or not and download PQ record with quantities")
  }
}