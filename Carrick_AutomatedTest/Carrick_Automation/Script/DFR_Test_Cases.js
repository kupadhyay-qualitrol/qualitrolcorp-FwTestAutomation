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
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx"
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
    
    //Step6. Download Manual DFR Record
    DFR_Methods.DownloadManualDFR()
    
    //Step7. Check Record Length
    var RecordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(0))//FirstRow
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(prefault)+aqConvert.StrToInt64(postfault),aqConvert.StrToInt64(RecordLength),0,"Validating Record Duration.")
    
    //Step8. Check Time Quality Status
    //Step8.1 Click on Device Status View
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView(),"Clicked on Device Status View")
    
    //Step8.2 Check Time Quality Staus Actual
    AssertClass.CompareString(DataRetrievalPage.TimeQualityStatusFromDeviceStatus(),PDPPage.GetTimeQualityStatus(0).ToString().OleValue,"Comparing Time Quality from Device Status and from DFR record in PDP.")
    
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
    
    var RMSValidationStatus= RMSDataValidationExePage.ValidateRMSData(DFRRecordPath+aqFileSystem.FindFiles(DFRRecordPath, "*.csv").Item(0).Name,CommonMethod.ReadDataFromExcel(DataSheetName,"RMSInjectedVoltage"),CommonMethod.ReadDataFromExcel(DataSheetName,"RMSInjectedCurrent"))
    
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