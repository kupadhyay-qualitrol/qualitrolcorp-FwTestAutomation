//USEUNIT CommonMethod
//USEUNIT LoginPage
//USEUNIT DeviceTopologyPage
//USEUNIT GeneralPage
//USEUNIT DataRetrievalPage

//TC-Test to trigger Manual DFR and see in Display Directory
function TriggerManualDFR()
{
  try
  {
    Log.Message("Start:Test to trigger Manual DFR and see in Display Directory")  
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx" 
    var LastDFRRecord
    var NewDFRRecord
    var RecordRetryCount
    //Step1.Launch iQ+  
    if(CommonMethod.Launch_iQ_Plus())
    {
      //Step2.Login iQ+ with Admin role
      LoginPage.Login(CommonMethod.ReadDataFromExcel(DataSheetName,"Username"),CommonMethod.ReadDataFromExcel(DataSheetName,"Password"))
      Log.Message("Login to the iQ+ successfully.")
      //Step3.Check whether device exists or not in the topology.    
      if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))!=true)
      {
        GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
        DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
      }
      else
      {
        Log.Message("Device exist in the tree topology.")
      }
      //Step4. Click on DFR Directory under Display Device Directory
      DataRetrievalPage.ClickOnDFRDirectory()
      
      //Step5. Find latest Record Number
      LastDFRRecord= DataRetrievalPage.GetLatestRecordnumber()
      Log.Message("Current Record Number is :- "+LastDFRRecord)
      
      //Step5.1 Close DFR Directory
      DataRetrievalPage.CloseDFRDirectory()
      
      //Step6. Generate Manual FR Trigger
      DataRetrievalPage.ClickOnFRManualTrigger()
            
      //Step8. Check new record number
      for(RecordRetryCount=0;RecordRetryCount<10;RecordRetryCount++)
      {
        //Try 10 times to check for new record
        DataRetrievalPage.ClickOnDFRDirectory()
        if(aqConvert.StrToInt64(DataRetrievalPage.GetLatestRecordnumber())!=aqConvert.StrToInt64(LastDFRRecord)+1)
        {
          DataRetrievalPage.CloseDFRDirectory()        
          aqUtils.Delay(30000)
        }
        else
        {
          DataRetrievalPage.CloseDFRDirectory()        
          break
        }
      }
      //Step8.1 Retrieve DFRDirectory
      DataRetrievalPage.ClickOnDFRDirectory()
      
      NewDFRRecord=DataRetrievalPage.GetLatestRecordnumber()
      if(aqConvert.StrToInt64(LastDFRRecord)!= aqConvert.StrToInt64(NewDFRRecord)-1)
      {
        Log.Error("Failed:-Latest Record number is not correct.It is :- "+NewDFRRecord) 
      }
      else
      {
        Log.Message("Latest Record number is correct.It is:- "+NewDFRRecord)
      }
      
      //Step9. Check COT is Manual
      if(DataRetrievalPage.GetCOTForLatestDFRRecord()=="MANUAL")
      {
        Log.Message("Cause of trigger is correct.")
        Log.Message("Passed:Test to trigger Manual DFR and see in Display Directory")
      }
      else
      {
        Log.Error("Failed:-Cause of Trigger is wrong :-"+DataRetrievalPage.GetCOTForLatestDFRRecord())
      }
      //Step9.1 Close DFR Directory
      DataRetrievalPage.CloseDFRDirectory()
      
      //Step10. Close iQ_Plus
      CommonMethod.Terminate_iQ_Plus()
    }
    else
    {
      Log.Error("Unable to launch iQ+")
      Log.Message("Failed:Test to trigger Manual DFR and see in Display Directory")
    }

  }
  catch(ex)
  {
    Log.Error(ex.message)
    Log.Message("Error:Test to trigger Manual DFR and see in Display Directory")
  }
}