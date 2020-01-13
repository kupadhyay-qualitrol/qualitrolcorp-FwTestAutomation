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
//USEUNIT PQ_Waveform_Viewer_Page
//USEUNIT FavoritesPage


function pqFreeIntervalRecordingDownloadWithQuantities(deviceType,deviceName,deviceSerialNo,deviceIPAdd,retryCountDateTime,retryCountPqOpen)
{
  try
  {
   Log.Message("Test to check for the PQ recording for free interval started or not and download PQ record with quantities") 
   
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
    PQ_Methods.cleanMemoryPq()
    Log.Message("Clean the PQ Free Interval records from the device data")
    
    //Step.4 Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step.5 Click on PQ Free Interval
    AssertClass.IsTrue(ConfigEditorPage.clickPqFreeInterval(),"Clicked on PQ Free Interval")
    
    //Step.7 Select RMS values from the Available quantities for PQ Free Inerval
    PQ_Methods.selectRMSChannelCircuitQuantitiesForPQ()
    
    //Step.8 Send to device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step.9 Compare Device time and PQ Start time
    AssertClass.IsTrue(PQ_Methods.compareDateTimePq(retryCountDateTime),"Device time and PQ Free Interval time compared and new record started to form")
    
    //Step.10 Click on PQ Free Interval button
    AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalDirectory(),"Clicked on PQ Free Interval Directory")
    
    //Step.10.1 Check if PQ Free Interval Directroy opens or not
    AssertClass.IsTrue(DataRetrievalPage.checkPqFreeIntervalDirectoryOpen(retryCountPqOpen),"PQ Free Interval directory pane open and displayed")
    
    //Step.11 Set Start date and time for PQ Free Interval
    PQ_Methods.setPqFreeIntervalStartTime()
    Log.Message("PQ Free Interval Start Time has been set")
    
    //Step.12 Download PQ Free Interval record
    AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalDownloadNowButton(),"Downloaded PQ Free Interval Record")
    
    //Step.13 Close PQ Free Interval directory
    AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalCloseButton(),"PQ Free Interval directroy closed")
    
    //Step.13a Set time interval for PQ Free Interval data exort to CSV
    PQ_Methods.setTimeIntervalForPqDataExport()
    
    //Step.14 Check if PQ Free Interval Favorite is available if not then Configure Favorite for PQ Free Interval
    PQ_Methods.checkForPqFreeIntervalFavorite()
    
    //Step.15 Export PQ Free Interval Data
    PQ_Methods.exportToCsvPqFreeIntervalData()
    Log.Message("PQ Free Interval Data has been exported")
    
  }
    catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for the PQ recording for free interval started or not and download PQ record with quantities")
  }
}

function pq10MinRecordingDownloadWithQuantities(deviceType,deviceName,deviceSerialNo,deviceIPAdd,retryCountDateTime,retryCountPqOpen)
{
  try
  {
   Log.Message("Test to check for the PQ recording for 10 Min started or not and download PQ record with quantities") 
   
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
    
    //Step.3 Clean memory of PQ 10Min
    PQ_Methods.cleanMemoryPq()
    Log.Message("Clean the PQ records from the device data")
    
    //Step.4 Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step.5 Click on PQ 10Min
    AssertClass.IsTrue(ConfigEditorPage.clickPq10Min(),"Clicked on PQ 10Min")
    
    //Step.7 Select RMS values from the Available quantities for PQ 10Min
    PQ_Methods.selectRMSChannelCircuitQuantitiesForPQ()
    
    //Step.8 Send to device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step.9 Check for PQ favorite and Create PQ10min favorite
    
    
//    
//    //Step.10 Click on PQ 10Min button
//    AssertClass.IsTrue(DataRetrievalPage.clickPq10Min(),"Clicked on PQ 10Min Directory")
//    
//    //Step.10.1 Check if PQ 10Min Directroy opens or not
//    AssertClass.IsTrue(DataRetrievalPage.CheckPQ10MinDirectoryOpen(retryCountPqOpen),"PQ 10Min directory pane open and displayed")
    
    //Step.11 Set Start date and time for PQ 10MIn
    PQ_Methods.setPq10MinStartTime()
    Log.Message("PQ 10Min Start Time has been set")
    
    //Step.12 Download PQ 10Min record
    AssertClass.IsTrue(DataRetrievalPage.clickOnPq10MinDownloadNowButton(),"Downloaded PQ 10Min Record")
    
    //Step.13 Close PQ 10Min directory
    AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalCloseButton(),"PQ 10Min directroy closed")
    
    //Step.13a Set time interval for PQ 10Min data exort to CSV
    PQ_Methods.setPq10MinStartTime(),"PQ 10 min set for 30 mins before"
    
    //Step.14 Check if PQ 10Min Favorite is available if not then Configure Favorite for PQ Free Interval

    
    //Step.15 Export PQ 10Min Data
    PQ_Methods.exportToCsvPqFreeIntervalData()
    Log.Message("PQ Min Data has been exported")
    
  }
    catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for the PQ recording for 10Min started or not and download PQ record with quantities")
  }
} 