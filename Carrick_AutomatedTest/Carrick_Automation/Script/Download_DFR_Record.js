//USEUNIT CommonMethod
//USEUNIT LoginPage
//USEUNIT DeviceTopologyPage
//USEUNIT GeneralPage
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
      aqUtils.Delay(10000)
      DataRetrievalPage.CloseDFRDirectory() 
      Log.Message("DFR data download") 
//      ActiveRowRecordNumber =     
//      Log.Message("Downloaded DFR record no. is"+ActiveRowRecordNumber)   
      //DataRetrievalPage.CloseDFRDirectory() 
      }
      else
      {
      {
            Log.Error("Failed:-Text is not there")
          }
          DataRetrievalPage.CloseDFRDirectory()        
       }
      //Step3. Verify downloaded record on PDP
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


