//USEUNIT CommonMethod
//USEUNIT DataRetrievalPage
//USEUNIT DeviceManagementPage
//USEUNIT AssertClass
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecording_DDRCChannels
//USEUNIT PQ_Methods
//USEUNIT DeviceTopologyPage
//USEUNIT OmicronQuickCMCPage
//USEUNIT GeneralPage


function pqRecordingDownloadWithQuantities(deviceType,deviceName,deviceSerialNo,deviceIPAdd,storageRate,retryCountDateTime,retryCountPQOpen,fileName)
{
  try
  {
   Log.Message("Test to check for the PQ recording for 10 min and free interval started or not and download PQ record with quantities") 
   
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
    //Step.3 Clean memory of PQ
    PQ_Methods.CleanMemoryPQ()
    aqUtils.Delay(3000)//Applying delay so that clean memory window get closed
    Log.Message("Clean the PQ records from the data")
    
    //Step.4 Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step.5 Click on PQ 10 Min
    AssertClass.IsTrue(ConfigEditorPage.clickPq10Min(),"Clicked on PQ 10 Min")
    
    //Step.6 Set Storage rate 
    AssertClass.IsTrue(ConfigEditor_FaultRecording_DDRCChannels.SetStorageRate(storageRate),"Storage rate has been set")
    
    //Step.7 Select RMS values from the Available quantities
    PQ_Methods.SelectRMSChannelCircuitQuantities()
    
    //Step.8 Check the DDRT check box
    ConfigEditor_FaultRecording_DDRCChannels.CheckDDRTCheckBoxSelectedQuantities()
    
    //Step.9 Send to device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step.10 Compare Device time and DDRC Start time
    AssertClass.IsTrue(DDRC_Methods.CompareDateTimeDDRC(retryCountDateTime),"Device time and DDRC time compared and new record started to form")
    
    //Step.11 Click on DDRC button
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCDirectory(),"Clicked on DDRC Directory")
    
    //Step.12 Check if DDRC Directory opens or not
    AssertClass.IsTrue(DataRetrievalPage.CheckDDRCDirectoryOpen(retryCountDDRCOpen),"DDRC directory pane open and displayed")
    
    //Step.13 Set Start date and time for DDRC
    DDRC_Methods.SetDDRCStartTime()
    
    //Step.14 Download DDRC record
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCDownloadNowButton(),"Downloaded DDRC Record")
    
    //Step.15 Close DDRC directory
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCCancelButton(),"DDRC directory closed")
    
    //Step.16 Close Omicron CMC file
    //AssertClass.IsTrue(OmicronQuickCMCPage.CloseQuickCMC(),"Quick CMC got closed")
  }
    catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for the PQ recording for 10 min and free interval started or not and download PQ record with quantities")
  }
}