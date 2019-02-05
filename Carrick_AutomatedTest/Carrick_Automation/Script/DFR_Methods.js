/*This file contains generic methods related to DFR which can be used directly in Test Cases*/

//USEUNIT DataRetrievalPage
//USEUNIT AssertClass
//USEUNIT TICPage
//USEUNIT CommonMethod
//USEUNIT FavoritesPage
//USEUNIT PDPPage

function TriggerManualDFR()
{
  //Step1. Click on DFR Directory under Display Device Directory
  AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory() ,"Clicked on DFR Directory")           
      
  //Step2. Find latest Record Number
  LastDFRRecord= DataRetrievalPage.GetLatestRecordnumber()
  Log.Message("Current Record Number is :- "+LastDFRRecord)      
      
  //Step3. Close DFR Directory
  AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory() ,"Closed on DFR Directory")      
      
  //Step4. Generate Manual FR Trigger
  AssertClass.IsTrue(DataRetrievalPage.ClickOnFRManualTrigger(),"Clicked on FR Manual Trigger")
            
  //Step5. Check new record number
  for(RecordRetryCount=0;RecordRetryCount<10;RecordRetryCount++)
  {
    //Try 10 times to check for new record
    DataRetrievalPage.ClickOnDFRDirectory()
       
    var NewDFRRecord=DataRetrievalPage.GetLatestRecordnumber()
    if(aqConvert.StrToInt64(NewDFRRecord)!=aqConvert.StrToInt64(LastDFRRecord)+1)
      {
        DataRetrievalPage.CloseDFRDirectory()
      }
      else
      {
        Log.Message("Latest Record number is correct.It is:- "+NewDFRRecord)
        break
      }      
  }
  AssertClass.CompareString("MANUAL",DataRetrievalPage.GetCOTForLatestDFRRecord(),"Checking COT")  
  AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory(),"Closed DFR Directory")
}

function DownloadManualDFR()
{   
    //Step 1. Click on DFR Directory
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory(),"Clicked on DFR Directory")
    var REC_DFR=DataRetrievalPage.GetLatestRecordnumber()
    //Step2. Click on Download Data Now
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDownloadDataNow(),"Clicked on Download Data Now")
    CommonMethod.CheckActivityLog("DFR records saved successfully for device")
    //Step3. Click on Close DFR Directory
    DataRetrievalPage.CloseDFRDirectory() 
    Log.Message("DFR data download")
  
    //Step4. Click on device Status view option
    DataRetrievalPage.ClickOnDeviceStatusView()
    Log.Message("Device Status window is open")
       
    //Step5. Get the Current Date time from the device
    var NewDateTime=DataRetrievalPage.GetDeviceCurrentDateTime()
    Log.Message("Stores the Device Current date and time")
      
    //Step6. Set Start date time and End date time in IQ+
    TICPage.SetDeviceDateTime(NewDateTime)
    Log.Message("Start Date time and End date time is updated in IQ+")
    
    //Step6.1 Set EndDateTime
    TICPage.SetTICEndDateTime(TICPage.GetTICEndDateTime(),1,0) //Set the EndDate Time with offsetof +1 month and 0 days
    aqUtils.Delay(2000)
    
    //Step7. Click on All FR Record Default Favorites
    AssertClass.IsTrue(FavoritesPage.ClickOnAllFRTriggeredRecord(),"Clicked on All FR Triggered Record")
    aqUtils.Delay(3000)
    
    //Step8. Verify downloaded record on PDP
    REC=PDPPage.VerifyDownloadedRecord()
    if(REC==REC_DFR)
    {
      return true 
    }
    else
    {
      return false
    }
}