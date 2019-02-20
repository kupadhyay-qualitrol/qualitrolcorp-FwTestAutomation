/*This file contains Test Cases related to Time Zone in the iQ+ and device*/

//USEUNIT CommonMethod
//USEUNIT DeviceTopologyPage
//USEUNIT AssertClass
//USEUNIT GeneralPage
//USEUNIT DeviceManagementPage
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_TimeManagementPage
//USEUNIT DataRetrievalPage
//USEUNIT LinuxOS_Methods


/*
BTC-95 & BTC-106:-Test all Timezone supported in Carrick & Cashel
*/
function BTC_95_BTC_106()
{
  try
  {  
    Log.Message("Started TC:-Test all Timezone supported in Carrick & Cashel")
    var DataSheetName = Project.ConfigPath +"TestData\\BTC_95_BTC106.xlsx"
    Log.Message("Test Timezone :- "+CommonMethod.ReadDataFromExcel(DataSheetName,"Timezone"))
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
    //Step2.1 Force Device to Sync Time
    if(Project.TestItems.Current.Iteration==1)
    {
      AssertClass.IsTrue(DataRetrievalPage.SetDeviceTime("Force"))
    }
    //Step3. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step4.Click on Time Management Tab
    AssertClass.IsTrue(ConfigEditorPage.ClickOnTimeManagement(),"Clicked on Time Management")
    
    //Step5. Set TimeZone as per Test Data
    AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeZone(CommonMethod.ReadDataFromExcel(DataSheetName,"Timezone")),"Sets the Time Zone")
    
    //Step6. Click on Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step7. Retrieve the Device Status
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView(),"Clicked on Device Status View")
        
    //Step8. Set PC Time to Test Data TimeZone
    CommonMethod.SetPCTimeZone(CommonMethod.ReadDataFromExcel(DataSheetName,"Timezone_path"))
    
    //Step9. Get Time
    var DeviceTime = DataRetrievalPage.GetDeviceActualDateTime()
    Log.Message("Date Time Shown in iQ+ :- "+DeviceTime)
    
    //Step10. Check TimeDifference between iq+ & PC  
    var DeviceDateTimeParse = new dotNET.System.DateTime.Parse(aqConvert.DateTimeToStr(DeviceTime)) 
    aqConvert.DateTimeToStr(DeviceDateTimeParse )   
    
    //Step11. Check TimeDifference between Device & PC 
    AssertClass.IsTrue(LinuxOS_Methods.ConnectToDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"),"root","qualcorpDec09"),"Making SSH Connection")    
    var LinuxOSTime = LinuxOS_Methods.SendCommand("date '+%d/%m/%Y %H:%M:%S.%Z'")
    
    //Step 12.Comparing Time from iQ+ and PC
    var TimeInterval=DeviceDateTimeParse.Subtract(dotNET.System.DateTime.Parse(aqConvert.DateTimeToStr(aqDateTime.Now())))
    
    Log.Message(aqConvert.TimeIntervalToStr(aqDateTime.TimeInterval(DeviceTime, aqDateTime.Now())))
    
    AssertClass.CompareDecimalValues(0,aqConvert.StrToInt64(TimeInterval.Days),0,"Checking for Days Difference")
    AssertClass.CompareDecimalValues(0,aqConvert.StrToInt64(TimeInterval.Hours),0,"Checking for Hours Difference")
    AssertClass.CompareDecimalValues(0,aqConvert.StrToInt64(TimeInterval.Minutes),5,"Checking for Minutes Difference")
    
    //Step 12.Comparing Time from Device and PC
    var LinuxDTime =LinuxOSTime.OleValue.split(".")
    
    Log.Message("Time Zone is :- "+ LinuxDTime[1])
    Log.Message("DateTime is :- "+ LinuxDTime[0])
    
    var LinuxDateTimeParse = new dotNET.System.DateTime.Parse(aqConvert.DateTimeToStr(LinuxDTime[0]))    
    var LinuxTimeInterval=LinuxDateTimeParse.Subtract(dotNET.System.DateTime.Parse(aqConvert.DateTimeToStr(aqDateTime.Now())))
    
    AssertClass.CompareDecimalValues(0,aqConvert.StrToInt64(LinuxTimeInterval.Days),0,"Checking for Days Difference")
    AssertClass.CompareDecimalValues(0,aqConvert.StrToInt64(LinuxTimeInterval.Hours),0,"Checking for Hours Difference")
    AssertClass.CompareDecimalValues(0,aqConvert.StrToInt64(LinuxTimeInterval.Minutes),5,"Checking for Minutes Difference")
    
    AssertClass.IsTrue(LinuxOS_Methods.DisconnectFromDevice(),"Disconnected Linux Connection")
    
    Log.Message("Pass:-Test all Timezone supported in Carrick & Cashel")
  }
  catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Fail:-Test all Timezone supported in Carrick & Cashel")
  }
}