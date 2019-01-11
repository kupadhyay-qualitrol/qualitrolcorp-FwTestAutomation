//USEUNIT CommonMethod
//USEUNIT DataRetrievalPage
//USEUNIT Trigger_ManualDFR
//USEUNIT PDPPage
//USEUNIT DeviceManagementPage
//USEUNIT TICPage
//USEUNIT AssertClass
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecordingPage
//USEUNIT FavoritesPage

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
      DataRetrievalPage.ClickOnDownloadDataNow()
      CommonMethod.CheckActivityLog("DFR records saved successfully for device")
      DataRetrievalPage.CloseDFRDirectory() 
      Log.Message("DFR data download")
      //Step3. Click on device Status view option
       DataRetrievalPage.ClickOnDeviceStatusView()
       Log.Message("Device Status window is open")
       
       //Step4. Get the Current Date time from the device
       NewDateTime=DataRetrievalPage.GetDeviceCurrentDateTime()
       Log.Message("Stores the Device Current date and time")
       
       //Step5. Set Start date time and End date time in IQ+
       TICPage.SetDeviceDateTime(NewDateTime)
       Log.Message("Start Date time and End date time is updated in IQ+")
       
       //Step5.1 Click on All FR Record Default Favorites
       AssertClass.IsTrue(FavoritesPage.ClickOnAllFRTriggeredRecord(),"Clicked on All FR Triggered Record")
       aqUtils.Delay(3000)
      //Step6. Verify downloaded record on PDP
        REC=PDPPage.VerifyDownloadedRecord()
        if(REC==REC_DFR)
        {
          Log.Message("Pass: DFR latest record downloaded and verified on PDP")       
        }
        else
        {
          Log.Error("Fail: DFR latest record not able to downloaded and verified on PDP")
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
    //Step0.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    
    //Step1. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig())
    
    //Step2. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording())
    
    //Step3. Set pre-fault for External Triggers
    var prefault =CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(prefault))
    
    //Step4. Set Post-fault time for External Triggers
    var postfault=CommonMethod.ReadDataFromExcel(DataSheetName,"PostFaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPostFault(postfault))
    
    //Step5. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice())
    
    //Step6. Download Manual DFR Record
    DownloadManualDFR()
    
    //Step7. Check Record Length
    var RecordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(0))//FirstRow
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(prefault)+aqConvert.StrToInt64(postfault),aqConvert.StrToInt64(RecordLength),0,"Validating Record Duration.")
    
    //Step8. Check Time Quality Status
    //Step8.1 Click on Device Status View
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView())
    
    //Step8.2 Check Time Quality Staus Actual
    AssertClass.CompareString(DataRetrievalPage.TimeQualityStatusFromDeviceStatus(),PDPPage.GetTimeQualityStatus(0).ToString().OleValue,"Comparing Time Quality from Device Status and from DFR record in PDP.")
    
    //Step8.3
    DataRetrievalPage.CloseDeviceStatus.ClickButton()
    //Step9. Check Prefault time
    var ActualPrefault = (PDPPage.GetRecordTriggerDateTime(0))-PDPPage.GetRecordStartDateTime(0)
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(prefault),ActualPrefault,0,"Prefault calculated from PDP is :-"+ActualPrefault)
    
    //Step10. Check Postfault value
    var ActualPostFault = PDPPage.GetRecordEndDateTime(0)-(PDPPage.GetRecordTriggerDateTime(0))
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(postfault),ActualPostFault,0,"Postfault calculated from PDP is :-"+ActualPostFault)
    
    Log.Message("Pass:-Test to Validate Prefault,Post fault time and record length in the DFR record.")
  }
  catch(ex)
  {
    Log.Message(ex.message)
    Log.Error("Error:-Test to Validate Prefault,Post fault time and record length in the DFR record.")  
  }
}