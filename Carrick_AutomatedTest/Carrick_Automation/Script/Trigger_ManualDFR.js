//USEUNIT CommonMethod
//USEUNIT LoginPage
//USEUNIT DeviceTopologyPage
//USEUNIT GeneralPage
//USEUNIT DataRetrievalPage

//TC-Test to trigger Manual DFR and see in Display Directory
function TriggerManualDFR(blnCloseiQPlus)
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
      aqUtils.Delay(3000)
      
      //Step5. Find latest Record Number
      LastDFRRecord= DataRetrievalPage.GetLatestRecordnumber()
      Log.Message("Current Record Number is :- "+LastDFRRecord)
      aqUtils.Delay(3000)
      
      //Step5.1 Close DFR Directory
      DataRetrievalPage.CloseDFRDirectory()
      aqUtils.Delay(3000)
      
      //Step6. Generate Manual FR Trigger
      DataRetrievalPage.ClickOnFRManualTrigger()
      aqUtils.Delay(3000)
            
      //Step8. Check new record number
      for(RecordRetryCount=0;RecordRetryCount<10;RecordRetryCount++)
      {
        //Try 10 times to check for new record
        DataRetrievalPage.ClickOnDFRDirectory()
        aqUtils.Delay(3000)
        NewDFRRecord=DataRetrievalPage.GetLatestRecordnumber()
        if(aqConvert.StrToInt64(NewDFRRecord)!=aqConvert.StrToInt64(LastDFRRecord)+1)
        {
          DataRetrievalPage.CloseDFRDirectory()        
          aqUtils.Delay(30000)
        }
        else
        {
          Log.Message("Latest Record number is correct.It is:- "+NewDFRRecord)
            
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
          DataRetrievalPage.CloseDFRDirectory()        
          break
        }
      }      
      //Step10. Close iQ_Plus
      if(blnCloseiQPlus)
      {
      CommonMethod.Terminate_iQ_Plus()
      }
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