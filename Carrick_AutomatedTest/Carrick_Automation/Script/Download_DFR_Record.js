//USEUNIT CommonMethod
//USEUNIT DataRetrievalPage
//USEUNIT Trigger_ManualDFR
//USEUNIT PDPPage

//TC-Test to Download Manual DFR record
function DownloadManualDFR()
{
  try
  {
    Log.Message("Start:Test to to Download Manual DFR record")  
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx" 
      //Step1. Run Manual DFR Trigger script
      Trigger_ManualDFR.TriggerManualDFR(false)
      
      //Step2. Now Click on Download Now button
      DataRetrievalPage.ClickOnDFRDirectory()
      if(DataRetrievalPage.GetCOTForLatestDFRRecord()=="MANUAL")
      {     
      REC_DFR=DataRetrievalPage.GetLatestRecordnumber()
      PDPPage.ClickOnDownloadDataNow()
      CommonMethod.CheckActivityLog("DFR records saved successfully for device")
      DataRetrievalPage.CloseDFRDirectory() 
      Log.Message("DFR data download") 
      }
      else
      {
      {
            Log.Error("Failed:-Text is not there")
          }
          DataRetrievalPage.CloseDFRDirectory()        
       }
       //Step3. Click on device Status view option
       PDPPage.ClickOnDeviceStatusView()
       Log.Message("Device Status window is open")
       
       //Step4. Get the Current Date time from the device
       PDPPage.GetDeviceCurrentDateTime()
       Log.Message("Stores the Device Current date and time")
       
       //Step5. Set Start date time and End date time in IQ+
       PDPPage.SetDeviceDateTime()
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
  catch(ex)
  {
    Log.Error(ex.message)
    Log.Message("Error:Test to trigger Manual DFR and see in Display Directory")
  }
}


