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
    var DeviceDateTime = DataRetrievalPage.GetDeviceActualDateTime();
    Log.Message(DeviceDateTime)
    aqUtils.Delay(1000)
    DataRetrievalPage.CloseDeviceStatus
    aqUtils.Delay(1000)
    DataRetrievalPage.ClickOnDDRCDirectory()
    Log.Message("Clicked on DDRC Directory")
    aqUtils.Delay(2000)
    var DDRCStartTime = DataRetrievalPage.GetDDRCStartTime();
    Log.Message(DDRCStartTime)
    DataRetrievalPage.CloseDFRDirectory()
    var DateTimeDifference = (aqConvert.StrToDateTime(DeviceDateTime)-aqConvert.StrToDateTime(DDRCStartTime));
    var DifferenceInMinutes = DateTimeDifference/60000;
    Log.Message("The difference in Minutes is "+ DifferenceInMinutes);
    if (DifferenceInMinutes<5)
    {
      Log.Message("New record has been started to form")
      return true
      break
    }
    else if(DifferenceInMinutes>5)
    {
      Log.Message("Checking for the Start time again")
    }
    else
    {
      return false
      Log.Message("Not able to found the new Record")
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