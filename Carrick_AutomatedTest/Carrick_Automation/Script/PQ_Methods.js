﻿/*This file contains generic methods related to DDRC which can be used directly in Test Cases*/

//USEUNIT DataRetrievalPage
//USEUNIT AssertClass
//USEUNIT TICPage
//USEUNIT CommonMethod
//USEUNIT FavoritesPage
//USEUNIT PDPPage
//USEUNIT RMSDataValidationExePage
//USEUNIT ConfigEditor_PQ

function cleanMemoryPqFreeInterval()
{
  //Step.1 Clicked on Clean Memory
  AssertClass.IsTrue(DataRetrievalPage.ClickOnCleanMemory(),"Clicked on Clean Memory")
  
  //Step.2 Check the PQ Free Interval checked box
  AssertClass.IsTrue(DataRetrievalPage.checkPqFreeIntervalCheckBox(),"Checked the PQ Free Interval check box")
  
  //Step.3 Click on Execute button
  AssertClass.IsTrue(DataRetrievalPage.ClickOnExecuteButton(),"Clicked on Execute button")
  CommonMethod.CheckActivityLog("Device memory cleaned successfully for selected data types")
}



function selectRMSChannelCircuitQuantitiesForPQFreeInterval()
{
    AssertClass.IsTrue(ConfigEditor_PQ.clickOnHarmonicsIntraharmonicsTabPqFreeInterval(),"Clicked on Harmonics and Intra Harmonics tab") 
    ConfigEditor_PQ.getTabCountsPqFreeInterval()
  for (count=0; count < PQFREEINTERVAL_TABCOUNT; count++)
  {
    
    AssertClass.IsTrue(ConfigEditor_PQ.clickOnTabPqFreeInterval(count),"Clicked on tab")
    ConfigEditor_PQ.clickOnRemoveAllButtonForPqFreeIntervalMin()
    Log.Message("All the selected quantities are moved to Available quantities in PQ Free Interval Page")
    AssertClass.IsTrue(ConfigEditor_PQ.addRMSQuantitiesPqFreeInterval(),"RMS Quantities added to selected quantities from available quantities")    
    AssertClass.IsTrue(ConfigEditor_PQ.addRMSQuantitiesPqFreeInterval(),"Harmonic and IntraHarmonic quantities added to selected quantities from available quantities")
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
      break
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
  
  //Step.4 Open DDRC Directory
  AssertClass.IsTrue(DataRetrievalPage.clickOnPqFreeIntervalDirectory(),"PQ Free Interval Directory opens")
  
  //Step.5 Now set the time in DDRC Start time field.
  AssertClass.IsTrue(DataRetrievalPage.UpdateDDRCStartTime(),"PQ Free Interval start time has been set as per the current device time.")
  
  Log.Message("DDRC start time set as 2 minutes before of current system date and time")
}