/*This file contains methods related to Time Sync in the device using iQ+*/

//USEUNIT AssertClass
//USEUNIT ConfigEditor_TimeManagementPage
//USEUNIT ConfigEditorPage
//USEUNIT CommonMethod
//USEUNIT DeviceTopologyPage
//USEUNIT DataRetrievalPage

function Configure_TimeMaster_Slave(DataSheetName,deviceIndex)
{
  //Step1. Click on TimeManagement Page
  AssertClass.IsTrue(ConfigEditorPage.ClickOnTimeManagement(),"Clicked on Time Management")
  
  //Step2. Set Master/Slave Settings
  if(CommonMethod.ReadDataFromExcel(DataSheetName,"TimeMaster")== CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+(deviceIndex)))
  {
    //Step2.1 Select Time Master
    AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeMaster(),"Selected Time Master")
    //Step2.2 Select Time Source
    AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeSourceSettings_Master(CommonMethod.ReadDataFromExcel(DataSheetName,"TimeMasterClock_Setting")),"Selected the time source for time sync")
  }
  else if (CommonMethod.ReadDataFromExcel(DataSheetName,"TimeSlave")== CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+(deviceIndex)))
  {
    //Step2.3 Select Time Slave    
    AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeSlave(),"Selecting the Time Slave")
      
    //Step2.4 Set Master IP
    AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeMasterIP_TimeSync(CommonMethod.ReadDataFromExcel(DataSheetName,"TimeSlave_Setting_Backup_IP")),"Setting Time Sync IP")
      
    //Step2.5 Select PPS INput
    AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetPPSInput(CommonMethod.ReadDataFromExcel(DataSheetName,"TimeSlave_Setting_PPS")),"Setting PPS Input")      
  }
}

function CheckTimeQualityInMasterSlave(DataSheetName,MasterIndex,SlaveIndex)
{
    var TimeStatusDevice1=""
    var TimeStatusDevice2=""
    do
    {
      aqUtils.Delay(5000) //Wait included so that time sync can happen
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+MasterIndex),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+MasterIndex))      
      AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView())
      TimeStatusDevice1 = DataRetrievalPage.TimeQualityStatusFromDeviceStatus()
      DataRetrievalPage.CloseDeviceStatus.ClickButton()
      
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+SlaveIndex),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+SlaveIndex))      
      AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView())
      TimeStatusDevice2 = DataRetrievalPage.TimeQualityStatusFromDeviceStatus()
      DataRetrievalPage.CloseDeviceStatus.ClickButton()
    }
    while (TimeStatusDevice1!="1" || TimeStatusDevice2!="1")
}