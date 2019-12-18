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


function pqFreeIntervalRecordingDownloadWithQuantities(deviceType,deviceName,deviceSerialNo,deviceIPAdd,retryCountDateTime,retryCountPQOpen,fileName)
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
    PQ_Methods.SelectRMSChannelCircuitQuantitiesForPQFreeInterval()
    
    
    //Step.8 Send to device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step.9 Configure Favorite for PQ Free Interval
    
    //Step.12 Compare Device time and PQ Free Interval
    //AssertClass.IsTrue(DDRC_Methods.CompareDateTimeDDRC(retryCountDateTime),"Device time and DDRC time compared and new record started to form")
    
    
    //Step.15 Set Start date and time for PQ Free Interval
    //DDRC_Methods.SetDDRCStartTime()
    
    //Step.16 Download PQ Free Interval
    //AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCDownloadNowButton(),"Downloaded DDRC Record")
    
    //Step.17 Close PQ Free Interval directory
    //AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCCancelButton(),"DDRC directory closed")
    
    
    //Step.19 Download PQ Free Interval data
    //AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCDownloadNowButton(),"Downloaded DDRC Record")
    
    //Step.20 Close PQ Free Interval directory
    //AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCCancelButton(),"DDRC directory closed")
    
    //Step.21 Close Omicron CMC file
    //AssertClass.IsTrue(OmicronQuickCMCPage.CloseQuickCMC(),"Quick CMC got closed")
  }
    catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for the PQ recording for 10 min and free interval started or not and download PQ record with quantities")
  }
}