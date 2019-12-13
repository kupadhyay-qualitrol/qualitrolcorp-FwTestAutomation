/*This file contains generic methods related to DDRC which can be used directly in Test Cases*/

//USEUNIT DataRetrievalPage
//USEUNIT AssertClass
//USEUNIT TICPage
//USEUNIT CommonMethod
//USEUNIT FavoritesPage
//USEUNIT PDPPage
//USEUNIT RMSDataValidationExePage
//USEUNIT ConfigEditor_PQ

function CompareDateTimePQ(retryCount)
{
  for(let recordRetryCount=0;recordRetryCount<retryCount;recordRetryCount++)
  {
    DataRetrievalPage.ClickOnDeviceStatusView()
    Log.Message("Clicked on Devcie status view")
    aqUtils.Delay(2000)
    var deviceDateTime = DataRetrievalPage.GetDeviceActualDateTime();
    Log.Message(deviceDateTime)
    aqUtils.Delay(1000)
    DataRetrievalPage.CloseDeviceStatus
    aqUtils.Delay(1000)
    DataRetrievalPage.ClickOnDDRCDirectory()
    DataRetrievalPage.CheckDDRCDirectoryOpen(retryCount)
    Log.Message("Clicked on DDRC Directory")
    aqUtils.Delay(2000)
    var startTimeDDRC = DataRetrievalPage.GetDDRCStartTime();
    if(startTimeDDRC!=null)
    {
    Log.Message(startTimeDDRC)
    DataRetrievalPage.CloseDFRDirectory()
    var dateTimeDifference = (aqConvert.StrToDateTime(deviceDateTime)-aqConvert.StrToDateTime(startTimeDDRC));
    var differenceInMinutes = dateTimeDifference/60000;
    Log.Message("The difference in Minutes is "+ differenceInMinutes);
    if (differenceInMinutes<5)
    {
      Log.Message("New record has been started to form")
      return true
      break
    }
    else if(differenceInMinutes>5)
    {
      Log.Message("Checking for the Start time again")
    }
    else
    {
      Log.Message("Not able to found the new Record")
      return false
    }
    }
    else
    {
      Log.Message("Not able to get DDRC Start time")
      return false
    }
  }
}
function CleanMemoryPQ()
{
  //Step.1 Clicked on Clean Memory
  AssertClass.IsTrue(DataRetrievalPage.ClickOnCleanMemory(),"Clicked on Clean Memory")
  
  //Step.2 Check the DDRC checked box
  AssertClass.IsTrue(DataRetrievalPage.CheckDDRCCheckBox(),"Checked the DDRC check box")
  
  //Step.3 Click on Execute button
  AssertClass.IsTrue(DataRetrievalPage.ClickOnExecuteButton(),"Clicked on Execute button")
  CommonMethod.CheckActivityLog("Device memory cleaned successfully for selected data types")
}
function SelectRMSChannelCircuitQuantitiesForPq10Min()
{
  ConfigEditor_PQ.GetTabCountsPq10Min()
  for (count=0; count < TABCOUNT; count++)
  {
    ConfigEditor_PQ.clickOnRemoveAllButtonForPq10Min()
    Log.Message("All the selected quantities are moved to Available quantities")
    AssertClass.IsTrue(ConfigEditor_PQ.ClickOnTabPq10Min(count),"Clicked on tab")
    ConfigEditor_PQ.AddRMSQuantitiesPq10Min    
  }
}

function SelectRMSChannelCircuitQuantitiesForPQFreeInterval()
{
  ConfigEditor_PQ.clickOnRemoveAllButtonForPq10Min()
  Log.Message("All the selected quantities are moved to Available quantities")
  ConfigEditor_PQ.GetTabCounts()
  for (count=0; count < Tab_Count; count++)
  {
    AssertClass.IsTrue(ConfigEditor_FaultRecording_DDRCChannels.ClickOnTab(count),"Clicked on tab");
    
    AssertClass.IsTrue(ConfigEditor_FaultRecording_DDRCChannels.AddRMSQuantities(),"RMS Quantities added to selected quantities from available quantities")    
  }
}



function SetDDRCStartTime()
{
  //Step.1 Close DDRC Directory
  AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory(),"DDRC Directroy get closed")
  
  //Step.2 Open Device Status view
  AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView(),"Device status view window opens")
  
  //Step.3 Get the current date time of device.
  DataRetrievalPage.GetDeviceCurrentDateTime()
  
  //Step.4 Open DDRC Directory
  AssertClass.IsTrue(DataRetrievalPage.ClickOnDDRCDirectory(),"DDRC Directory opens")
  
  //Step.5 Now set the time in DDRC Start time field.
  AssertClass.IsTrue(DataRetrievalPage.UpdateDDRCStartTime(),"DDRC start time has been set as per the current device time.")
  
  Log.Message("DDRC start time set as 2 minutes before of current system date and time")
}

function validateCSSData(rmsInjectedVoltage,rmsInjectedCurrent,voltageTolerance,currentTolerance)
{
  //Step.1 Export CSV data for DDRC Record
    var sysUserName = CommonMethod.GetSystemUsername()
    var ddrcRecordPath ="C:\\Users\\"+sysUserName+"\\Desktop\\DDRCRecord\\"
    if (!aqFileSystem.Exists(ddrcRecordPath))
    {
      aqFileSystem.CreateFolder(ddrcRecordPath) 
    }
    AssertClass.IsTrue(PDPPage.ExportTOCSVDDRC())
 
    AssertClass.IsTrue(CommonMethod.KillProcess("EXCEL")) //This method is used to kill the process
  
  //Step.2 Launch RMS data validation application
    AssertClass.IsTrue(RMSDataValidationExePage.LaunchRMSValidationApplication(), "RMS data validation app has been launched")
  
  //Step.3 Validate RMS data for DDRC
    var ddrcStoredPath = ddrcRecordPath+aqFileSystem.FindFiles(ddrcRecordPath, "*.csv").Item(0).Name
    var rmsDDRCValidationStatus= RMSDataValidationExePage.ValidateRMSData(ddrcStoredPath,rmsInjectedVoltage,rmsInjectedCurrent,voltageTolerance,currentTolerance)
    AssertClass.CompareString("PASS", rmsDDRCValidationStatus,"Checking RMS Validation" )
    
  //Step.4 Delete the downloaded file
    aqFileSystem.DeleteFile(ddrcStoredPath)
}