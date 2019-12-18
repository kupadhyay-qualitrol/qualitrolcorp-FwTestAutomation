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




//function SelectRMSChannelCircuitQuantitiesForPq10Min()
//{
//  ConfigEditor_PQ.GetTabCountsPq10Min()
//  for (count=0; count < TABCOUNT; count++)
//  {
//    ConfigEditor_PQ.clickOnRemoveAllButtonForPq10Min()
//    Log.Message("All the selected quantities are moved to Available quantities")
//    AssertClass.IsTrue(ConfigEditor_PQ.ClickOnTabPq10Min(count),"Clicked on tab")
//    ConfigEditor_PQ.AddRMSQuantitiesPq10Min()    
//  }
//}

function SelectRMSChannelCircuitQuantitiesForPQFreeInterval()
{
  ConfigEditor_PQ.clickOnRemoveAllButtonForPqFreeIntervalMin()
  Log.Message("All the selected quantities are moved to Available quantities in PQ Free Interval Page")
  ConfigEditor_PQ.GetTabCountsPqFreeInterval()
  for (count=0; count < PQFREEINTERVAL_TABCOUNT; count++)
  {
    AssertClass.IsTrue(ConfigEditor_PQ.ClickOnTabPqFreeInterval(count),"Clicked on tab")
    
    AssertClass.IsTrue(ConfigEditor_PQ.AddRMSQuantitiesPqFreeInterval(),"RMS Quantities added to selected quantities from available quantities")    
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