/*This file contains generic methods related to DDRC which can be used directly in Test Cases*/

//USEUNIT DataRetrievalPage
//USEUNIT AssertClass
//USEUNIT TICPage
//USEUNIT CommonMethod
//USEUNIT FavoritesPage
//USEUNIT PDPPage
//USEUNIT RMSDataValidationExePage
//USEUNIT ConfigEditor_FaultRecording_DDRCChannels

function CompareDateTimeDDRC(retryCount)
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
function CleanMemoryDDRC()
{
  //Step.1 Clicked on Clean Memory
  AssertClass.IsTrue(DataRetrievalPage.ClickOnCleanMemory(),"Clicked on Clean Memory")
  
  //Step.2 Check the DDRC checked box
  AssertClass.IsTrue(DataRetrievalPage.CheckDDRCCheckBox(),"Checked the DDRC check box")
  
  //Step.3 Click on Execute button
  AssertClass.IsTrue(DataRetrievalPage.ClickOnExecuteButton(),"Clicked on Execute button")
  CommonMethod.CheckActivityLog("Device memory cleaned successfully for selected data types")
}
function SelectRMSChannelCircuitQuantities()
{
  ConfigEditor_FaultRecording_DDRCChannels.ClickOnRemoveAllButton()
  Log.Message("All the selected quantities are moved to Available quantities")
  ConfigEditor_FaultRecording_DDRCChannels.GetTabCounts()
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

function ValidateCSSData(RMSInjectedVoltage,RMSInjectedCurrent,VoltageTolerance,CurrentTolerance)
{
  //Step.1 Export CSV data for DDRC Record
    var SysUserName = CommonMethod.GetSystemUsername()
    var DDRCRecordPath ="C:\\Users\\"+SysUserName+"\\Desktop\\DDRCRecord\\"
    if (aqFileSystem.Exists(DDRCRecordPath))
    {
      AssertClass.IsTrue(PDPPage.ExportTOCSVDDRC())    
    }
    else
    {
      aqFileSystem.CreateFolder(DDRCRecordPath)
      AssertClass.IsTrue(PDPPage.ExportTOCSVDDRC())
    }
    AssertClass.IsTrue(CommonMethod.KillProcess("EXCEL")) //This method is used to kill the process
  
  //Step.2 Launch RMS data validation application
    AssertClass.IsTrue(RMSDataValidationExePage.LaunchRMSValidationApplication(), "RMS data validation app has been launched")
  
  //Step.3 Validate RMS data for DDRC
    var DDRCStoredPath = DDRCRecordPath+aqFileSystem.FindFiles(DDRCRecordPath, "*.csv").Item(0).Name
    var RMSDDRCValidationStatus= RMSDataValidationExePage.ValidateRMSData(DDRCStoredPath,RMSInjectedVoltage,RMSInjectedCurrent,VoltageTolerance,CurrentTolerance)
    AssertClass.CompareString("PASS", RMSDDRCValidationStatus,"Checking RMS Validation" )
    
  //Step.4 Delete the downloaded file
    aqFileSystem.DeleteFile(DDRCStoredPath)
}