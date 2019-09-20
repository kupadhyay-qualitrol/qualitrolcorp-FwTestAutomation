/*This file contains generic methods related to DDRC which can be used directly in Test Cases*/

//USEUNIT DataRetrievalPage
//USEUNIT AssertClass
//USEUNIT TICPage
//USEUNIT CommonMethod
//USEUNIT FavoritesPage
//USEUNIT PDPPage
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