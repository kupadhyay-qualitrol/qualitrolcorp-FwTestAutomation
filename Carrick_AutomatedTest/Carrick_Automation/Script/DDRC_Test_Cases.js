﻿//USEUNIT CommonMethod
//USEUNIT DataRetrievalPage
//USEUNIT DeviceManagementPage
//USEUNIT AssertClass
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecording_DDRCChannels
//USEUNIT DDRC_Methods
//USEUNIT DeviceTopologyPage
//USEUNIT OmicronQuickCMCPage
//USEUNIT GeneralPage

function DDRCRecordingDownload(deviceType,deviceName,deviceSerialNo,deviceIPAdd,storageRate,retryCountDateTime,retryCountDDRCOpen)
{
  try
  {
   Log.Message("Test to check for the DDRC recording started or not and download DDRC record") 
   
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
    //Step.3 Clean memory of DDRC
    DDRC_Methods.CleanMemoryDDRC()
    aqUtils.Delay(3000)//Applying delay so that clean memory window get closed
    Log.Message("Clean the DDRC records from the data")
    
    //Step.4 Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step.5 Click on DDRC Channnels
    AssertClass.IsTrue(ConfigEditorPage.ClickOnDDRCChannels(),"Clicked on DDRC Channles")
    
    //Step.6 Set Storage rate 
    AssertClass.IsTrue(ConfigEditor_FaultRecording_DDRCChannels.SetStorageRate(storageRate))
    
    //Step.7 Send to device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step.8 Compare Device time and DDRC Start time
    AssertClass.IsTrue(DDRC_Methods.CompareDateTimeDDRC(retryCountDateTime),"Device time and DDRC time compared and new record started to form")
    
    //Step.9 Click on DDRC button
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCDirectory(),"Clicked on DDRC Directory")
    
    //Step.9.1 Check if DDRC Directroy opens or not
    AssertClass.IsTrue(DataRetrievalPage.CheckDDRCDirectoryOpen(retryCountDDRCOpen),"DDRC directory pane open and displayed")
    
    //Step.10 Set Start date and time for DDRC
    AssertClass.IsTrue(DDRC_Methods.SetDDRCStartTime(),"DDRC start time set as 2 minutes before of current system date and time")
    
    //Step.11 Download DDRC record
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCDownloadNowButton(),"Downloaded DDRC Record")
    
    //Step.12 Close DDRC directory
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCCancelButton(),"DDRC directroy closed")
  }
    catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for the DDRC recording started or not and download DDRC record")
  }
}

function DDRCRecordingDownloadWithQuantities(deviceType,deviceName,deviceSerialNo,deviceIPAdd,storageRate,retryCountDateTime,retryCountDDRCOpen,fileName)
{
  try
  {
   Log.Message("Test to check for the DDRC recording started or not and download DDRC record with quantities") 
   
    //Step.0 Inject voltage and current using omicron
    OmicronQuickCMCPage.InjectVoltCurrent(Project.ConfigPath+fileName)
   
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
    //Step.3 Clean memory of DDRC
    DDRC_Methods.CleanMemoryDDRC()
    aqUtils.Delay(3000)//Applying delay so that clean memory window get closed
    Log.Message("Clean the DDRC records from the data")
    
    //Step.4 Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step.5 Click on DDRC Channnels
    AssertClass.IsTrue(ConfigEditorPage.ClickOnDDRCChannels(),"Clicked on DDRC Channels")
    
    //Step.6 Set Storage rate 
    AssertClass.IsTrue(ConfigEditor_FaultRecording_DDRCChannels.SetStorageRate(storageRate),"Storage rate has been set")
    
    //Step.7 Select RMS values from the Available quantities
    DDRC_Methods.SelectRMSChannelCircuitQuantities()
    
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
    AssertClass.IsTrue(OmicronQuickCMCPage.CloseQuickCMC(),"Quick CMC got closed")
  }
    catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for the DDRC recording started or not and download DDRC record with quantities")
  }
}