﻿/*This file contains generic methods related to DFR which can be used directly in Test Cases*/

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
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory(),"Clicked on DFR Directory")
    var REC_DFR=DataRetrievalPage.GetLatestRecordnumber()
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDownloadDataNow(),"Clicked on Download Data Now")
    CommonMethod.CheckActivityLog("DFR records saved successfully for device")
  
    DataRetrievalPage.CloseDFRDirectory() 
    Log.Message("DFR data download")
  
    //Step3. Click on device Status view option
    DataRetrievalPage.ClickOnDeviceStatusView()
    Log.Message("Device Status window is open")
       
    //Step4. Get the Current Date time from the device
    var NewDateTime=DataRetrievalPage.GetDeviceCurrentDateTime()
    Log.Message("Stores the Device Current date and time")
      
    //Step5. Set Start date time and End date time in IQ+
    TICPage.SetDeviceDateTime(NewDateTime)
    Log.Message("Start Date time and End date time is updated in IQ+")
    aqUtils.Delay(2000)
    
    //Step5.1 Click on All FR Record Default Favorites
    AssertClass.IsTrue(FavoritesPage.ClickOnAllFRTriggeredRecord(),"Clicked on All FR Triggered Record")
    aqUtils.Delay(3000)
    
    //Step6. Verify downloaded record on PDP
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