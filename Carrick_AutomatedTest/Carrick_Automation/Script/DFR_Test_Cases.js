//USEUNIT CommonMethod
//USEUNIT DataRetrievalPage
//USEUNIT Trigger_ManualDFR
//USEUNIT PDPPage
//USEUNIT DeviceManagementPage
//USEUNIT TICPage
//USEUNIT AssertClass
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecordingPage
//USEUNIT FavoritesPage
//USEUNIT RMSDataValidationExePage
//USEUNIT OmicronQuickCMCPage
//USEUNIT DFR_Methods

/*
BTC-125 Check for DFR record length and time stamp validation
*/
function BTC_125()
{
  try
  {
    Log.Message("Start:-Test to Validate Prefault,Post fault time and record length in the DFR record.")
    var DataSheetName = Project.ConfigPath +"TestData\\BTC_125.xlsx"
    //Step0.1 Start Omicron Injection
    OmicronQuickCMCPage.InjectVoltCurrent(Project.ConfigPath+"TestData\\"+CommonMethod.ReadDataFromExcel(DataSheetName,"OmicronFile"))
    //Step0.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
    }
    else
    {
      Log.Message("Device exist in the tree topology.")
    }
    
    //Step1. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step2. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step3. Set pre-fault for External Triggers
    var prefault =CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(prefault),"Validating Prefaul Time")
    
    //Step4. Set Post-fault time for External Triggers
    var postfault=CommonMethod.ReadDataFromExcel(DataSheetName,"PostFaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPostFault(postfault),"Validating Post Faulttime")
    
    //Step4.1. Set Max DFR time
    var MaxDFR=CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFR),"Validating Max DFR") 
    
    //Step5. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step6. Trigger & Download Manual DFR Record
    DFR_Methods.TriggerManualDFR()
    
    //Step6.1 Download Manual DFR Record
    DFR_Methods.DownloadManualDFR()
    
    //Step7. Check Record Length
    var RecordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(0))//FirstRow
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(prefault)+aqConvert.StrToInt64(postfault),aqConvert.StrToInt64(RecordLength),0,"Validating Record Duration.")
    
    //Step8. Check Time Quality Status
    //Step8.1 Click on Device Status View
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView(),"Clicked on Device Status View")
    
    //Step8.2 Check Time Quality Staus Actual
    AssertClass.CompareDecimalValues(DataRetrievalPage.TimeQualityStatusFromDeviceStatus(),PDPPage.GetTimeQualityStatus(0),0,"Comparing Time Quality from Device Status and from DFR record in PDP.")
    
    //Step8.3
    DataRetrievalPage.CloseDeviceStatus.ClickButton()
    
    //Step9. Check Prefault time
    var ActualPrefault = (PDPPage.GetRecordTriggerDateTime(0))-PDPPage.GetRecordStartDateTime(0)
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(prefault),ActualPrefault,0,"Prefault calculated from PDP is :-"+ActualPrefault)
    
    //Step10. Check Postfault value
    var ActualPostFault = PDPPage.GetRecordEndDateTime(0)-(PDPPage.GetRecordTriggerDateTime(0))
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(postfault),ActualPostFault,0,"Postfault calculated from PDP is :-"+ActualPostFault)
    
    //Step11. Export to CDF.
    if (aqFileSystem.Exists(Project.ConfigPath+"DFRRecordResults"))
    {
      AssertClass.IsTrue(PDPPage.ExportTOCDF(Project.ConfigPath+"DFRRecordResults\\"))
    }
    else
    {
      aqFileSystem.CreateFolder(Project.ConfigPath+"DFRRecordResults")
      AssertClass.IsTrue(PDPPage.ExportTOCDF(Project.ConfigPath+"DFRRecordResults\\"))
    }    
    //Step12. Export to CSV
    var SysUserName = CommonMethod.GetSystemUsername()
    var DFRRecordPath ="C:\\Users\\"+SysUserName+"\\Desktop\\DFRRecord\\"
    if (aqFileSystem.Exists(DFRRecordPath))
    {
      AssertClass.IsTrue(PDPPage.ExportTOCSV())    
    }
    else
    {
      aqFileSystem.CreateFolder(DFRRecordPath)
      AssertClass.IsTrue(PDPPage.ExportTOCSV())
    }
    AssertClass.IsTrue(CommonMethod.KillProcess("EXCEL")) //This method is used to kill the process
    
    //Step 13. Validate RMS Data
    AssertClass.IsTrue(RMSDataValidationExePage.LaunchRMSValidationApplication())
    
    var RMSValidationStatus= RMSDataValidationExePage.ValidateRMSData(DFRRecordPath+aqFileSystem.FindFiles(DFRRecordPath, "*.csv").Item(0).Name,CommonMethod.ReadDataFromExcel(DataSheetName,"RMSInjectedVoltage"),CommonMethod.ReadDataFromExcel(DataSheetName,"RMSInjectedCurrent"),CommonMethod.ReadDataFromExcel(DataSheetName,"VoltageTolerance"),CommonMethod.ReadDataFromExcel(DataSheetName,"CurrentTolerance"))
    
    AssertClass.IsTrue(aqFile.Move(DFRRecordPath+aqFileSystem.FindFiles(DFRRecordPath, "*.csv").Item(0).Name,Project.ConfigPath+"DFRRecordResults"),"Moving CSV file to Project Folder")//Move the file to the Project path folder
    AssertClass.CompareString("PASS", RMSValidationStatus,"Checking RMS Validation" )    
    Log.Message("Pass:-Test to Validate Prefault,Post fault time and record length in the DFR record.")
  }
  catch(ex)
  {
    Log.Message(ex.message)
    Log.Error("Error:-Test to Validate Prefault,Post fault time and record length in the DFR record.")  
  }
  finally
  {
    AssertClass.IsTrue(OmicronQuickCMCPage.CloseQuickCMC(),"Close Quick CMC Application")
  }
}

/*
BTC-114 Test to check for Trigger priority verification on device configuration for boundary value 0-99
*/
function BTC_639()
{
  try
  {
    Log.Message("Started TC:-Test to check for Trigger priority verification on device configuration for boundary value 0-99")
    var DataSheetName = Project.ConfigPath +"TestData\\BTC_639.xlsx";
    
    //Step1. Check if iQ-Plus is running or not.
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
    
    //Step4. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step5. Check the Max TriggerPriority Value
    var TriggerPriority = ConfigEditor_FaultRecordingPage.GetTriggerPriority();
    Log.Message("Trigger Priority value is" + TriggerPriority);
    
    //Step6. Enter TriggerPriority_Max
    var TriggerPriority_MaxPlusOne = CommonMethod.ReadDataFromExcel(DataSheetName,"TriggerPriority_Max")
    AssertClass.IsFalse(ConfigEditor_FaultRecordingPage.SetTriggerPriority(TriggerPriority_MaxPlusOne),"Setting and checking Trigger Priority")
    
    //Step7. Save to DB
    AssertClass.IsTrue(ConfigEditorPage.ClickSaveToDb(),"Clicked on Save to DB")
    
    //Step8. Click on Modify Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonModifyConfig(),"Clicked on Modify Config")
    
    //Step9. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step10. Check the Max Trigger Priority Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetTriggerPriority(),TriggerPriority,"Checking Trigger Priority value")

    //Step11. //Enter TriggerPriority_Min
    var TriggerPriority_MinMinusOne =CommonMethod.ReadDataFromExcel(DataSheetName,"TriggerPriority_Min")
    AssertClass.IsFalse(ConfigEditor_FaultRecordingPage.SetTriggerPriority(TriggerPriority_MinMinusOne),"Setting and checking Max DFR")
    
    //Step12. Save to DB
    AssertClass.IsTrue(ConfigEditorPage.ClickSaveToDb(),"Clicked on Save to DB")
    
    //Step13. Click on Modify Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonModifyConfig(),"Clicked on Modify Config")
    
    //Step14. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step15. Check the Min Trigger Priority Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetTriggerPriority(),TriggerPriority,"Checking Trigger Priority value")
    
    //Step16. //Enter TriggerPriority_Mid
    var TriggerPriority_Mid =CommonMethod.ReadDataFromExcel(DataSheetName,"TriggerPriority_Mid")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetTriggerPriority(TriggerPriority_Mid),"Setting and checking TriggerPriority")
    
    //Step17. Save to DB
    AssertClass.IsTrue(ConfigEditorPage.ClickSaveToDb(),"Clicked on Save to DB")
    
    //Step18. Click on Modify Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonModifyConfig(),"Clicked on Modify Config")
    
    //Step19. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step20. Check the Trigger Priority Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetTriggerPriority(),TriggerPriority_Mid,"Checking TriggerPriority_Mid value")

    //Step21. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step22. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step23. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step24. Check the Trigger Priority Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetTriggerPriority(),TriggerPriority_Mid,"Checking TriggerPriority_Mid value")
   
    Log.Message("Pass:-Test to check for Trigger priority verification on device configuration for boundary value 0-99")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for Trigger priority verification on device configuration for boundary value 0-99")
  }
}

/*
BTC- Test to check for Trigger Priority
*/
function BTC_114()
{
  try
  {
    Log.Message("Started TC:-Test to check for Trigger Priority on PDP page and DFR Directory pop-up")
    var DataSheetName = Project.ConfigPath +"TestData\\BTC_114.xlsx";
    
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
    
    //Step4. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step5. //Enter TriggerPriority
    var TriggerPriority = CommonMethod.ReadDataFromExcel(DataSheetName,"TriggerPriority")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetTriggerPriority(TriggerPriority),"Setting and checking Trigger Priority")
    
    //Step6. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step7. Trigger Manual DFR
    DFR_Methods.TriggerManualDFR()
    
    //Step8. Click on DFR directoy
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory(),"DFR Directory pop up is open")
    
    //Step9. Check Trigger Priority on DFR Directory pop-up
    AssertClass.CompareString(aqConvert.IntToStr(DataRetrievalPage.GetTriggerPriorityOnDFR()),aqConvert.IntToStr(TriggerPriority),"Trigger Priority recorded for the latest DFR Record")
    
    //Step10. Close DFR Directory popup
    AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory(),"DFR Directory get closed")
    
    //Step11. Download Manual DFR
    AssertClass.IsTrue(DFR_Methods.DownloadManualDFR(),"Downloading DFR")
    
    //Step12. Verify Trigger Priority on PDP Page
    AssertClass.CompareString(aqConvert.IntToStr(PDPPage.GetTriggerPriority(0)),aqConvert.IntToStr(TriggerPriority),"Trigger Priority for the latest record is return")
    
    //Step13. Export to CDF.
    if (aqFileSystem.Exists(Project.ConfigPath+"DFRRecordResults"))
    {
      AssertClass.IsTrue(PDPPage.ExportTOCDF(Project.ConfigPath+"DFRRecordResults\\"))
    }
    else
    {
      aqFileSystem.CreateFolder(Project.ConfigPath+"DFRRecordResults")
      AssertClass.IsTrue(PDPPage.ExportTOCDF(Project.ConfigPath+"DFRRecordResults\\"))
    }    
    //Step14. Export to CSV
    var SysUserName = CommonMethod.GetSystemUsername()
    var DFRRecordPath ="C:\\Users\\"+SysUserName+"\\Desktop\\DFRRecord\\"
    if (aqFileSystem.Exists(DFRRecordPath))
    {
      AssertClass.IsTrue(PDPPage.ExportTOCSV())    
    }
    else
    {
      aqFileSystem.CreateFolder(DFRRecordPath)
      AssertClass.IsTrue(PDPPage.ExportTOCSV())
    }
    AssertClass.IsTrue(CommonMethod.KillProcess("EXCEL")) //This method is used to kill the process
    
    Log.Message("Pass:-Test to check for Trigger Priority")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check for Trigger Priority")
  }
}