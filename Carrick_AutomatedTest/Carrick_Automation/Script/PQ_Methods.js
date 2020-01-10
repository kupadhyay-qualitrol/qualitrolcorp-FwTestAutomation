/*This file contains generic methods related to PQ which can be used directly in Test Cases*/

//USEUNIT DataRetrievalPage
//USEUNIT AssertClass
//USEUNIT TICPage
//USEUNIT CommonMethod
//USEUNIT FavoritesPage
//USEUNIT PDPPage
//USEUNIT RMSDataValidationExePage
//USEUNIT ConfigEditor_PQ
//USEUNIT ConfigEditor_PQ_10Mins

function cleanMemoryPq()
{
  //Step.1 Clicked on Clean Memory
  AssertClass.IsTrue(DataRetrievalPage.ClickOnCleanMemory(),"Clicked on Clean Memory")
  
  //Step.2 Check the PQ Free Interval checked box
  AssertClass.IsTrue(DataRetrievalPage.checkPqFreeIntervalCheckBox(),"Checked the PQ Free Interval check box")
  
  //Step.3 Check the PQ 10min checked box
  AssertClass.IsTrue(DataRetrievalPage.checkPq10MinCheckBox(),"Checked the PQ 10min check box")
  
  //Step.4 Click on Execute button
  AssertClass.IsTrue(DataRetrievalPage.ClickOnExecuteButton(),"Clicked on Execute button")
  CommonMethod.CheckActivityLog("Device memory cleaned successfully for selected data types")
}

function selectRMSChannelCircuitQuantitiesForPQ()
{
  if(ConfigEditor_PQ.PANE_CONFIG.WndCaption=="Free Interval")
  {
    AssertClass.IsTrue(ConfigEditor_PQ.clickOnHarmonicsIntraharmonicsTabPqFreeInterval(),"Clicked on Harmonics and Intra Harmonics tab") 
    ConfigEditor_PQ.getTabCountsPqFreeInterval()
    for (count=0; count < TABCOUNT; count++)
    {
    AssertClass.IsTrue(ConfigEditor_PQ.clickOnTabPqFreeInterval(count),"Clicked on tab")
    ConfigEditor_PQ.clickOnRemoveAllButtonForPqFreeInterval()
    Log.Message("All the selected quantities are moved to Available quantities in PQ Free Interval Page")
    AssertClass.IsTrue(ConfigEditor_PQ.addQuantitiesPqFreeInterval("RMS"),"RMS Channel Cirtcuit Quantities added for PQ Free Interval")
    AssertClass.IsTrue(ConfigEditor_PQ.addQuantitiesPqFreeInterval("H01"),"Harmonic and IntraHarmonic Channel Cirtcuit Quantities added for PQ Free Interval")   
    }
    Log.Message("RMS Channel Cirtcuit Quantities added for PQ Free Interval")
  }
  else if(ConfigEditor_PQ.PANE_CONFIG.WndCaption=="PQ 10 Min")
  {
    AssertClass.IsTrue(ConfigEditor_PQ_10Mins.clickOnAllTabsPQ10Min(),"Clicked on Harmonics and Intra Harmonics tab") 
    var tabcount = ConfigEditor_PQ_10Mins.getTabCountsPq10Min()
    for (count=0; count < tabcount; count++)
    {
    AssertClass.IsTrue(ConfigEditor_PQ_10Mins.clickOnAllTabsPQ10Min(),"Clicked on tab")
    AssertClass.IsTrue(ConfigEditor_PQ_10Mins.clickOnRemoveAllButton(),"All Selected quantities are removed from the selected quantities")
    AssertClass.IsTrue(ConfigEditor_PQ_10Mins.addQuantitiesPq10Min("RMS"),"RMS Channel Cirtcuit Quantities added for PQ Free Interval")
    AssertClass.IsTrue(ConfigEditor_PQ_10Mins.addQuantitiesPq10Min("H01"),"Harmonic and IntraHarmonic Channel Cirtcuit Quantities added for PQ Free Interval")   
    }
    Log.Message("RMS Channel Cirtcuit Quantities added for PQ Free Interval") 
  }
}


function compareDateTimePq(retryCount)
{
  for(let recordRetryCount=0;recordRetryCount<retryCount;recordRetryCount++)
  {
    DataRetrievalPage.ClickOnDeviceStatusView()
    Log.Message("Clicked on Device status view")
    aqUtils.Delay(2000)
    var deviceDateTime = DataRetrievalPage.GetDeviceActualDateTime();
    Log.Message(deviceDateTime)
    aqUtils.Delay(1000)
    DataRetrievalPage.CloseDeviceStatus
    aqUtils.Delay(2000)
    DataRetrievalPage.clickOnPqFreeIntervalDirectory()
    DataRetrievalPage.checkPqFreeIntervalDirectoryOpen(retryCount)
    Log.Message("Clicked on PQ Free interval Directory")
    aqUtils.Delay(2000)
    var startTimePqFreeInterval = DataRetrievalPage.getPqFreeIntervalStartTime();
    if(startTimePqFreeInterval!=null)
    {
    Log.Message(startTimePqFreeInterval)
    DataRetrievalPage.CloseDFRDirectory()
    var dateTimeDifference = (aqConvert.StrToDateTime(deviceDateTime)-aqConvert.StrToDateTime(startTimePqFreeInterval));
    var differenceInMinutes = dateTimeDifference/60000;
    Log.Message("The difference in Minutes is "+ differenceInMinutes);
    if (differenceInMinutes<5)
    {
      Log.Message("New record has been started to form")
      return true
    }
    else if(differenceInMinutes>5)
    {
      Log.Message("Checking for the PQ Free IntervalStart time again")
    }
    else
    {
      Log.Message("Not able to found the new Record for PQ Free Interval")
      return false
    }
    }
    else
    {
      Log.Message("Not able to get PQ Free Interval Start time")
      return false
    }
  }
}

function setPqFreeIntervalStartTime()
{
  //Step.1 Close PQ Free Interval Directory
  AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory(),"PQ Free Interval Directroy get closed")
  
  //Step.2 Open Device Status view
  AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView(),"Device status view window opens")
  
  //Step.3 Get the current date time of device.
  DataRetrievalPage.GetDeviceCurrentDateTime()
  
  //Step.4 Open PQ Free Interval Directory
  AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalDirectory(),"PQ Free Interval Directory opens")
  
  //Step.5 Now set the time in PQ Free Interval Start time field.
  AssertClass.IsTrue(DataRetrievalPage.UpdateDDRCStartTime(),"PQ Free Interval start time has been set as per the current device time.")
  Log.Message("PQ Free Interval start time set as 2 minutes before of current system date and time")
}

function checkForPqFreeIntervalFavorite()
{ 
  //Navigate to Favorites under Conitnouous Recording for PQ Free Interval
  CommonMethod.RibbonToolbar.wItems.Item("&Data Analysis").Click()
  CommonMethod.RibbonToolbar.ClickItem("&Data Analysis|Data Analysis Views|&Continuous Recording")
  
  try 
  { 
    var defaultFavoriteItemCount = DEFAULT_FAV.wItemCount("Default Favorites")
    var myFavoriteItemCount = DEFAULT_FAV.wItemCount("My Favorites")
    var defaultFavoriteItems = []
    var myFavoriteItems = []
    for (counter=0;counter<defaultFavoriteItemCount;counter++)
    {
       defaultFavoriteItems = DEFAULT_FAV.wItem(0, counter)
    }
    
    for (counter=0;counter<myFavoriteItemCount;counter++)
    {
       myFavoriteItems = DEFAULT_FAV.wItem(1, counter)
    }
    
    
    if(defaultFavoriteItems.includes("PQ Free Interval")|| myFavoriteItems.includes("PQ Free Interval"))
    {
      Log.Message("PQ Free Interval Favorite Exists")
      return true
    }
    
    else
    {
      ConfigEditor_PQ.createNewFavoriteForPqFreeInterval()
      Log.Message("Configured new Favorite for PQ Free Interval")    
      return true 
    }
    
  }  
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for the PQ Free Interval Favorite")
  }
  
}

function setTimeIntervalForPqDataExport()
{ 
  
  try {

  DataRetrievalPage.ClickOnDeviceStatusView()
  Log.Message("Clicked on Device status view")
  aqUtils.Delay(2000)
  var deviceDateTime = DataRetrievalPage.GetDeviceActualDateTime();
  Log.Message(deviceDateTime)
  aqUtils.Delay(2000)
  var getEndDateTime = aqConvert.StrToDateTime(deviceDateTime)
  Log.Message(getEndDateTime)
  var getStartDateTime = aqDateTime.AddMinutes(getEndDateTime, -10);
  var startDateTime = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2.TimeInterval.TimeIntervalControl.UserControlBase_Fill_Panel.TICtplInnerMostLayout1.TICdtpStartTime
  var endDateTime = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2.TimeInterval.TimeIntervalControl.UserControlBase_Fill_Panel.TICtplInnerMostLayout2.TICdtpEndTime
  startDateTime.set_Value(getStartDateTime)
  endDateTime.set_Value(getEndDateTime)
  }
  
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to set time interval for PQ data") 
  }
}


function exportToCsvPqFreeIntervalData()
{ 

 try {
  
  ConfigEditor_PQ.exportPqFreeIntervalDataToCsv()
  Log.Message("PQ Free Interval Data is exported to CSV")
  }
  
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to export PQ Free Interval Data to CSV")
  }
   
   
}


function validatePqData(rmsInjectedVoltage,validationType,rmsInjectedCurrent,voltageTolerance,currentTolerance)
{
  //Step.1 Find CSV data for PQ Record
    var sysUserName = CommonMethod.GetSystemUsername()
    var pqRecordPath ="C:\\Users\\"+sysUserName+"\\Desktop\\PQ\\"

  //Step.2 Launch RMS data validation application
    AssertClass.IsTrue(RMSDataValidationExePage.LaunchRMSValidationApplication(), "RMS data validation app has been launched")
  
  //Step.3 Validate RMS data for PQ
    var pqStoredPath = pqRecordPath+aqFileSystem.FindFiles(pqRecordPath, "*.csv").Item(0).Name
    var rmsPQValidationStatus= RMSDataValidationExePage.validatePqRmsData(pqStoredPath,validationType,rmsInjectedVoltage,rmsInjectedCurrent,voltageTolerance,currentTolerance)
    AssertClass.CompareString("PASS", rmsPQValidationStatus ,"Checking PQ RMS Validation" )
    
  //Step.4 Delete the downloaded file
    aqFileSystem.DeleteFile(pqStoredPath)
}
