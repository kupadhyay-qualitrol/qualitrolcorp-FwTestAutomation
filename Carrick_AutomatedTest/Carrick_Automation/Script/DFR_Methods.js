/*This file contains generic methods related to DFR which can be used directly in Test Cases*/

//USEUNIT DataRetrievalPage
//USEUNIT AssertClass
//USEUNIT TICPage
//USEUNIT CommonMethod
//USEUNIT FavoritesPage
//USEUNIT PDPPage
//USEUNIT ConfigEditor_FaultRecording_FRSensorPage

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
  return ViewDFROnPDP(REC_DFR)
}

function ViewDFROnPDP(DownloadedDFRNum)
{
  //Step1. Click on device Status view option
  AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView(),"Clicked on Device Status")
  Log.Message("Device Status window is open")
       
  //Step2. Get the Current Date time from the device
  var NewDateTime=DataRetrievalPage.GetDeviceCurrentDateTime()
  Log.Message("Stores the Device Current date and time")
      
  //Step3. Set Start date time and End date time in IQ+
  TICPage.SetDeviceDateTime(NewDateTime)
  Log.Message("Start Date time and End date time is updated in IQ+")
    
  //Step3.1 Set EndDateTime
  TICPage.SetTICEndDateTime(TICPage.GetTICEndDateTime(),1,0) //Set the EndDate Time with offsetof +1 month and 0 days
  aqUtils.Delay(2000)
    
  //Step4. Click on All FR Record Default Favorites
  AssertClass.IsTrue(FavoritesPage.ClickOnAllFRTriggeredRecord(),"Clicked on All FR Triggered Record")
  aqUtils.Delay(3000)
    
  //Step5. Verify downloaded record on PDP
  REC=PDPPage.VerifyDownloadedRecord()
  if(REC==DownloadedDFRNum)
  {
    Log.Message("DFR Record Number is correct on PDP also.")  
    return true 
  }
  else
  {
    Log.Message("DFR Record Number is not correct on PDP.Record number on PDP is :- "+REC+" & on Downloaded one is :- "+DownloadedDFRNum)    
    return false
  }
}

function SetFRSensor(frsensorName,frsensorType,frsensorScalingType,frsensorUpperThreshold,frsensorPostfaultTime,frsensorOplimit)
{
  AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.SelectFRSensorByName(frsensorName),"Setting FR Sensor Name")
  AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.SetFRSensorType(frsensorType),"Setting FR sensor Type")
  AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.SetScalingType(frsensorScalingType),"Setting FR Sensor Scaling Type")
  AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.SetUpperThreshold(frsensorUpperThreshold),"Setting FR sensor Upper Threshold")
  AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.SetPostFaultTime(frsensorPostfaultTime),"Setitng FR sensor Post fault")
  AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.SetOplimit(frsensorOplimit),"Setting FR sensor Oplimit")
  AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.ClickOnOkEditFRSensor() ,"Clicked on OK Button on Edit FR Sensor")
}

function IsNewRecordFound(retryCount,lastRecordNumber)
{
  for(let recordRetryCount=0;recordRetryCount<retryCount;recordRetryCount++)
  {
    DataRetrievalPage.ClickOnDFRDirectory()      
    var newDFRRecord=DataRetrievalPage.GetLatestRecordnumber()
    if((aqConvert.StrToInt64(newDFRRecord)-(aqConvert.StrToInt64(lastRecordNumber)+1))<0)
    {
      DataRetrievalPage.CloseDFRDirectory()
      aqUtils.Delay(20000) 
    }
    else if((aqConvert.StrToInt64(newDFRRecord)-(aqConvert.StrToInt64(lastRecordNumber)+1))>0)
    {
      Log.Message("Multiple Triggers found")
      DataRetrievalPage.CloseDFRDirectory()
      return false
      break
    }
    else
    {   
      Log.Message("Latest Record number is :- "+newDFRRecord)
      return true
    }   
  }
}
function IsMultipleRecordFound(retryCount,lastRecordNumber)
{
  for(let recordRetryCount=0;recordRetryCount<retryCount;recordRetryCount++)
  {     
    var newDFRRecord=DataRetrievalPage.GetMultipleLatestRecordnumber()
    if((aqConvert.StrToInt64(newDFRRecord[0])-(aqConvert.StrToInt64(lastRecordNumber)+2))<0)
    {
      DataRetrievalPage.CloseDFRDirectory()
      aqUtils.Delay(20000) 
    }
    else if((aqConvert.StrToInt64(newDFRRecord[0])-(aqConvert.StrToInt64(lastRecordNumber)+2))>0)
    {
      Log.Message("Multiple Triggers found")
      DataRetrievalPage.CloseDFRDirectory()
      return false
      break
    }
    else
    {   
      Log.Message("Latest Record number is :- "+newDFRRecord[0])
      Log.Message("Latest Record number is :- "+newDFRRecord[1])
      return true
    }   
  }
}
function DownloadMultipleRecords()
{ 
  var NewDFRRecord=DataRetrievalPage.GetMultipleLatestRecordnumber()  
  DirectoryList.ClickItem(NewDFRRecord[1], 0, skCtrl);
  Log.Message("selected multiple records")
  aqUtils.Delay(2000)
  DataRetrievalPage.ClickOnDownloadDataNow()
  return true
}