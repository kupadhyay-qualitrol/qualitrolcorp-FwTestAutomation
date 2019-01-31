/*This file contains Test Cases related to Time Zone in the iQ+ and device*/

//USEUNIT CommonMethod
//USEUNIT DeviceTopologyPage
//USEUNIT AssertClass
//USEUNIT GeneralPage
//USEUNIT DeviceManagementPage
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_TimeManagementPage
//USEUNIT DataRetrievalPage


/*
BTC-95 & BTC-106:-Test all Timezone supported in Carrick & Cashel
*/
function BTC_95_BTC_106()
{
  try
  {
    Log.Message("Started TC:-Test all Timezone supported in Carrick & Cashel")
    var DataSheetName = Project.ConfigPath +"TestData\\BTC_95_BTC106.xlsx"
    
    //Step1.: Check if iQ-Plus is running or not.
    AssertClass.IsTrue(CommonMethod.IsExist("iQ-Plus"),"Checking if iQ+ is running or not")
    
    //Step2.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    
    //Step3. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step4.Click on Time Management Tab
    AssertClass.IsTrue(ConfigEditorPage.ClickOnTimeManagement(),"Clicked on Time Management")
    
    //Step5. Set TimeZone as per Test Data
    AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeZone(CommonMethod.ReadDataFromExcel(DataSheetName,"Timezone")),"Sets the Time Zone")
    
    //Step6. Click on Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice())
    
    //Step7. Retrieve the Device Status
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView())
        
    //Step8. Set PC Time to Test Data TimeZone
    CommonMethod.SetPCTimeZone(CommonMethod.ReadDataFromExcel(DataSheetName,"Timezone_path"))
    
    //Step9. Get Time
    var DeviceTime = DataRetrievalPage.GetDeviceActualDateTime()
    
    //Step10. Check TimeDifference between iq+ & PC
    var TimeInterval=aqDateTime.TimeInterval(DeviceTime, aqDateTime.Now())
    
    AssertClass.CompareDecimalValues(0,aqDateTime.GetDay(TimeInterval),0,"Checking for Days Difference")
    AssertClass.CompareDecimalValues(0,aqDateTime.GetHours(TimeInterval),0,"Checking for Hours Difference")
    AssertClass.CompareDecimalValues(0,aqDateTime.GetMinutes(TimeInterval),10,"Checking for Minutes Difference")
    
    Log.Message("Pass:-Test all Timezone supported in Carrick & Cashel")
  }
  catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Fail:-Test all Timezone supported in Carrick & Cashel")
  }
}