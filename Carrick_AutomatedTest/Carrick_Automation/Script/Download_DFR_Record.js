//USEUNIT CommonMethod
//USEUNIT DataRetrievalPage
//USEUNIT Trigger_ManualDFR
//USEUNIT PDPPage
//USEUNIT DeviceManagementPage
//USEUNIT TICPage
//USEUNIT AssertClass
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecordingPage

//TC-Test to Download Manual DFR record
function DownloadManualDFR()
{
  try
  {
    var REC_DFR;
    var REC;
  
    Log.Message("Start:Test to to Download Manual DFR record")  
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx" 
      //Step1. Run Manual DFR Trigger script
      Trigger_ManualDFR.TriggerManualDFR(false)
      
      //Step2. Now Click on Download Now button
      DataRetrievalPage.ClickOnDFRDirectory()
      if(DataRetrievalPage.GetCOTForLatestDFRRecord()=="MANUAL")
{     
      REC_DFR=DataRetrievalPage.GetLatestRecordnumber()
      DeviceManagementPage.ClickOnDownloadDataNow()
      CommonMethod.CheckActivityLog("DFR records saved successfully for device")
      DataRetrievalPage.CloseDFRDirectory() 
      Log.Message("DFR data download")
      //Step3. Click on device Status view option
       DeviceManagementPage.ClickOnDeviceStatusView()
       Log.Message("Device Status window is open")
       
       //Step4. Get the Current Date time from the device
       NewDateTime=DeviceManagementPage.GetDeviceCurrentDateTime()
       Log.Message("Stores the Device Current date and time")
       
       //Step5. Set Start date time and End date time in IQ+
       TICPage.SetDeviceDateTime(NewDateTime)
       Log.Message("Start Date time and End date time is updated in IQ+")
       
     
      //Step6. Verify downloaded record on PDP
        REC=PDPPage.VerifyDownloadedRecord()
        if(REC==REC_DFR)
        {
          Log.Message("Pass: DFR latest record downloaded and verified on PDP")       
        }
        else
        {
          Log.Message("Fail: DFR latest record not able to downloaded and verified on PDP")
        }  
}
      else
      {
            Log.Error("Latest DFR record found is not of Manual type")
            DataRetrievalPage.CloseDFRDirectory()        
       }    
}
  catch(ex)
  {
    Log.Error(ex.message)
    Log.Message("Error:Test to Download Manual DFR and verify in PDP is fail")
  }
}


//TC-Test to Validate Prefault,Post fault time and record length in the DFR record.

function Validate_RecordTime()
{
  try
  {
    Log.Message("Start:-Test to Validate Prefault,Post fault time and record length in the DFR record.")
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx"
    
    //Step1. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig())
    
    //Step2. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording())
    
    //Step3. Set pre-fault for External Triggers
    var prefault =CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(prefault))
    
    //Step4. Set Post-fault time for External Triggers
    var postfault=CommonMethod.ReadDataFromExcel(DataSheetName,"PostfaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPostFault(postfault))
    
    //Step5. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice())
    
    //Step6. Download Manual DFR Record
    Download_DFR_Record()
    
    //Step7. Check Record Length
    var RecordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration())
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(prefault)+aqConvert.StrToInt64(postfault),RecordLength)
    
    
    //Step2.
    Log.Message("Pass:-Test to Validate Prefault,Post fault time and record length in the DFR record.")
  }
  catch(ex)
  {
    Log.Message(ex.message)
    Log.Error("Error:-Test to Validate Prefault,Post fault time and record length in the DFR record.")
  
  }
}


