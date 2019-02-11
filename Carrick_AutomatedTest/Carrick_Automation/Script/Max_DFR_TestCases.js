/*This unit contains test cases related to Max DFR functionality*/

//USEUNIT AssertClass
//USEUNIT CommonMethod
//USEUNIT DeviceTopologyPage
//USEUNIT GeneralPage
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecordingPage
//USEUNIT ConfigEditor_FinishPage
//USEUNIT DFR_Methods
//USEUNIT PDPPage
//USEUNIT ConfigEditor_TimeManagementPage
//USEUNIT ConfigEditor_Comms_NetworkServices

/*
CAM-727 Test to check the GUI(Text/Editbox) of iQ+ for Maximum DFR record length
PreCondition- iQ+ is launched already
*/
function CAM_727()
{
  try
  {
    Log.Message("Started TC:- Test to check the GUI(Text/Editbox) of iQ+ for Maximum DFR record length")
    var DataSheetName = Project.ConfigPath +"TestData\\MaxDFR.xlsx"
    
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
    
    //Step5.1. Check the Max DFR label.
    AssertClass.CompareString("Maximum Record Length:", aqString.Trim(ConfigEditor_FaultRecordingPage.GetMaxDFRLabel()),"Checking Max DFR label on UI")
    
    //Step5.2. Check the Max DFR Unit.
    AssertClass.CompareString("ms", aqString.Trim(ConfigEditor_FaultRecordingPage.GetMaxDFRUnit()),"Checking Max DFR unit label on UI")
    
    //Step6. Check Max DFR Editbox exist on UI.
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.Edtbx_MaxDFR.Exists,"Checking Editbox exists on UI")
    Log.Message("Pass:-Test to check the GUI(Text/Editbox) of iQ+ for Maximum DFR record length")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check the GUI(Text/Editbox) of iQ+ for Maximum DFR record length")
  }
  finally
  {
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close in Config Editor")
  }
}

/*
CAM-728 Test to check that DFR record length value get saved to database.
PreCondition- iQ+ is launched already
*/

function CAM_728()
{
  try
  {
    Log.Message("Started TC:-Test to check that DFR record length value get saved to database.")
    var DataSheetName = Project.ConfigPath +"TestData\\MaxDFR.xlsx"
    
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
    
    //Step5.0 //Enter PreFault
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime")),"Setting Prefault Time")
    
    //Step5. Enter & Check Max DFR value
    var MaxDFRLength =CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFRLength),"Setting and checking Max DFR")
    
    //Step6. Save to DB
    AssertClass.IsTrue(ConfigEditorPage.ClickSaveToDb(),"Clicked on Save to DB")
    
    //Step7. Click on Modify Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonModifyConfig(),"Clicked on Modify Config")
    
    //Step7.1 Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step8. Check the Max DFR Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetMaxDFR(),MaxDFRLength,"Checking Max DFR value")
    
    Log.Message("Pass:-Test to check that DFR record length value get saved to database.")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check that DFR record length value get saved to database.")
  }
  finally
  {
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close in Config Editor")
  }
}

/*
CAM-690 Test to check that minimum and maximum limit for DFR record length with non-Transco licenses
PreCondition- iQ+ is launched already
*/
function CAM_690()
{
  try
  {
    Log.Message("Started TC:- Test to check that minimum and maximum limit for DFR record length with non-Transco licenses ")
    var DataSheetName = Project.ConfigPath +"TestData\\MaxDFR.xlsx"
        
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
    
    //Step5.0 //Enter PreFault
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime")),"Setting Prefault Time")
    
    //Step5. Enter & Check Max DFR value
    var MaxDFRLength =CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFRLength),"Setting and checking Max DFR")
    
    //Step6. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step7. Retrieve Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
    //Step8. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step9. Check for Prefault and Max DFR Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetMaxDFR(), MaxDFRLength,"Checking for Max DFR Value")
    
    Log.Message("Pass:- Test to check that minimum and maximum limit for DFR record length with non-Transco licenses")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check that minimum and maximum limit for DFR record length with non-Transco licenses")
  }
  finally
  {
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close in Config Editor")
  }
}

/*
CAM-689 Test to check that user tries to set minimum DFR record length equal to Prefault time
PreCondition- iQ+ is launched already
*/
function CAM_689()
{
  try
  {
    Log.Message("Started TC:-Test to check that user tries to set minimum DFR record length equal to Prefault time")
    var DataSheetName = Project.ConfigPath +"TestData\\CAM_689.xlsx"
    
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
    
    //Step5.0 //Enter PreFault
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime")),"Setting Prefault Time")
    
    //Step5. Enter & Check Max DFR value
    var MaxDFRLength =CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFRLength),"Setting and checking Max DFR")
    
    //Step6. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step7. Check Error on Finish Pane
    AssertClass.CompareString("DFR maximum record length has to be at least 100ms more than the pre-fault.",ConfigEditor_FinishPage.GetErrorText("Fault Recording"),"Checking for Error Validation on Finish Pane.")
    
    Log.Message("Pass:-Test to check that user tries to set minimum DFR record length equal to Prefault time")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check that user tries to set minimum DFR record length equal to Prefault time")
  }
  finally
  {
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close in Config Editor")
  }
}

/*
CAM-688 Test to check limit DFR record length feature when Manual trigger(Pre+Post fault time) is equal to Maximum record length
PreCondition- iQ+ is launched already
*/
function CAM_686_687_688()
{
  try
  {
    Log.Message("Started TC:-Test to check limit DFR record length feature when Manual trigger(Pre+Post fault time) is equal to Maximum record length")    
    var DataSheetName = Project.ConfigPath +"TestData\\CAM_686_687_688.xlsx"
    
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
    
    //Step5.0 //Enter PreFault
    var Prefault=CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(Prefault),"Setting Prefault Time")
    
    //Step5.1 Enter & Check Max DFR value
    var MaxDFRLength =CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFRLength),"Setting and checking Max DFR")
    
    //Step5.2 Set Post fault
    var PostFault =CommonMethod.ReadDataFromExcel(DataSheetName,"PostFaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPostFault(PostFault),"Setting Post Fault time")
    
    //Step6. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step7. Trigger Manual DFR
    DFR_Methods.TriggerManualDFR()
    
    //Step8. Download Manual DFR
    AssertClass.IsTrue(DFR_Methods.DownloadManualDFR(),"Downloading DFR")
    
    //Step9. Get Prefault time
    var ActualPrefault = (PDPPage.GetRecordTriggerDateTime(0))-PDPPage.GetRecordStartDateTime(0)
    
    //Step10. Get Postfault time
    var ActualPostFault = PDPPage.GetRecordEndDateTime(0)-(PDPPage.GetRecordTriggerDateTime(0))
    
    //Step9. Check Record Length
    var RecordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(0))//FirstRow
    
    if(aqConvert.StrToInt64(Prefault)+aqConvert.StrToInt64(PostFault)<=aqConvert.StrToInt64(MaxDFRLength))
    {    
      AssertClass.CompareDecimalValues(aqConvert.StrToInt64(ActualPrefault)+aqConvert.StrToInt64(ActualPostFault),aqConvert.StrToInt64(RecordLength),1,"Validating Record Duration.")
    }
    else
    {
      AssertClass.CompareDecimalValues(aqConvert.StrToInt64(MaxDFRLength),aqConvert.StrToInt64(RecordLength),1,"Validating Record Duration.")
    }    
    Log.Message("Pass:-Test to check limit DFR record length feature when Manual trigger(Pre+Post fault time) is equal to Maximum record length")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check limit DFR record length feature when Manual trigger(Pre+Post fault time) is equal to Maximum record length")
  }
}

/*
CAM-725 Test to check that user tries to input DFR record length value less/greater than minimum/maximum value
PreCondition- iQ+ is launched already
*/
function CAM_725()
{
  try
  {
    Log.Message("Started TC:-Test to check that user tries to input DFR record length value less/greater than minimum/maximum value")
    var DataSheetName = Project.ConfigPath +"TestData\\CAM_725.xlsx";
    
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
    
    //Step5. Check the Max DFR Value
    var MaxDFR = ConfigEditor_FaultRecordingPage.GetMaxDFR();
    Log.Message("Max DFR value is" + MaxDFR);
    
    //Step6. //Enter MaxDFR_Min
    var MaxDFR_Min = CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR_Min")
    AssertClass.IsFalse(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFR_Min),"Setting and checking Max DFR")
    
    //Step7. Save to DB
    AssertClass.IsTrue(ConfigEditorPage.ClickSaveToDb(),"Clicked on Save to DB")
    
    //Step8. Click on Modify Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonModifyConfig(),"Clicked on Modify Config")
    
    //Step9. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step10. Check the Max DFR Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetMaxDFR(),MaxDFR,"Checking Max DFR value")

    //Step11. //Enter MaxDFR_Max
    var MaxDFR_Max =CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR_Max")
    AssertClass.IsFalse(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFR_Max),"Setting and checking Max DFR")
    
    //Step12. Save to DB
    AssertClass.IsTrue(ConfigEditorPage.ClickSaveToDb(),"Clicked on Save to DB")
    
    //Step13. Click on Modify Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonModifyConfig(),"Clicked on Modify Config")
    
    //Step14. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step15. Check the Max DFR Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetMaxDFR(),MaxDFR,"Checking Max DFR value")
    
    //Step16 //Enter PreFault
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime")),"Setting Prefault Time")
       
    //Step17. //Enter MaxDFR_Mid
    var MaxDFR_Mid =CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR_Mid")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFR_Mid),"Setting and checking Max DFR")
    
    //Step18. Save to DB
    AssertClass.IsTrue(ConfigEditorPage.ClickSaveToDb(),"Clicked on Save to DB")
    
    //Step19. Click on Modify Configuration
    AssertClass.IsTrue(DeviceManagementPage.ClickonModifyConfig(),"Clicked on Modify Config")
    
    //Step20. Click on Fault Recording
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFaultRecording(),"Clicked on Fault Recording")
    
    //Step21. Check the Max DFR Value
    AssertClass.CompareString(ConfigEditor_FaultRecordingPage.GetMaxDFR(),MaxDFR_Mid,"Checking Max DFR value")

    //Step22. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
  
    Log.Message("Pass:-Test to check that user tries to input DFR record length value less/greater than minimum/maximum value")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check that user tries to input DFR record length value less/greater than minimum/maximum value")
  }
}

/*
CAM-736 Test to check DFR record length with Cross Trigger
*/
function CAM_736()
{
  try
  {
    Log.Message("Start TC:-CAM-736 Test to check DFR record length with Cross Trigger")
    var DeviceSuffix =["1","2"]
    
    var DataSheetName = Project.ConfigPath +"TestData\\CAM_736.xlsx"
    
    for(let i=0;i< DeviceSuffix.length;i++ )
    {
      //Step0.Check whether device exists or not in the topology.    
      if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+(i+1)),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+(i+1)))!=true)
      {
        GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+(i+1)),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+(i+1)),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"+(i+1)),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"+(i+1)))
        DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+(i+1)),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+(i+1)))      
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
      var prefault =CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime"+(i+1))
      AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(prefault),"Validating Prefaul Time")
    
      //Step4. Set Post-fault time for External Triggers
      var postfault=CommonMethod.ReadDataFromExcel(DataSheetName,"PostFaultTime"+(i+1))
      AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPostFault(postfault),"Validating Post Faulttime")
    
      //Step4.1. Set Max DFR time
      var MaxDFR=CommonMethod.ReadDataFromExcel(DataSheetName,"MaxDFR"+(i+1))
      AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(MaxDFR),"Validating Max DFR")
    
      //Step5. Click on TimeManagement Page
      AssertClass.IsTrue(ConfigEditorPage.ClickOnTimeManagement(),"Clicked on Time Management")
    
      //Step6. Set Master/Slave Settings
      if(CommonMethod.ReadDataFromExcel(DataSheetName,"TimeMaster")== CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+(i+1)))
      {
        //Step6.1 Select Time Master
        AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeMaster(),"Selected Time Master")
        //Step6.2 Select Time Source
        AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeSourceSettings_Master(CommonMethod.ReadDataFromExcel(DataSheetName,"TimeMasterClock_Setting")),"Selected the time source for time sync")
      }
      else if (CommonMethod.ReadDataFromExcel(DataSheetName,"TimeSlave")== CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+(i+1)))
      {
        //Step6.3 Select Time Slave    
        AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeSlave(),"Selecting the Time Slave")
      
        //Step6.4 Set Master IP
        AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetTimeMasterIP_TimeSync(CommonMethod.ReadDataFromExcel(DataSheetName,"TimeSlave_Setting_Backup_IP")),"Setting Time Sync IP")
      
        //Step6.5 Select PPS INput
        AssertClass.IsTrue(ConfigEditor_TimeManagementPage.SetPPSInput(CommonMethod.ReadDataFromExcel(DataSheetName,"TimeSlave_Setting_PPS")),"Setting PPS Input")      
      }
      
      //Step7. Configure Cross Trigger
      //Step 7.1 Set UDP Port Number
      AssertClass.IsTrue(ConfigEditor_Comms_NetworkServices.SetUDPPort(CommonMethod.ReadDataFromExcel(DataSheetName,"UDPPortNumber"+(i+1))))
      //Step7.2 Set MaskID
      AssertClass.IsTrue(ConfigEditor_Comms_NetworkServices.SetGroupMaskID(CommonMethod.ReadDataFromExcel(DataSheetName,"GroupMaskID"+(i+1))))
      //Step7.3 Set Compatibility
      AssertClass.IsTrue(ConfigEditor_Comms_NetworkServices.SetCompatibility(CommonMethod.ReadDataFromExcel(DataSheetName,"Compatibility"+(i+1))))
      //Step7. Send to Device
      AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    }
        
    //Step8. Check Time Quality Status
    //Step8.1 Click on Device Status View
    var TimeStatusDevice1=""
    var TimeStatusDevice2=""
    do
    {
      aqUtils.Delay(5000) //Wait included so that time sync can happen
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+DeviceSuffix[0]),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+DeviceSuffix[0]))      
      AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView())
      TimeStatusDevice1 = DataRetrievalPage.TimeQualityStatusFromDeviceStatus()
      DataRetrievalPage.CloseDeviceStatus.ClickButton()
      
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+DeviceSuffix[1]),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+DeviceSuffix[1]))      
      AssertClass.IsTrue(DataRetrievalPage.ClickOnDeviceStatusView())
      TimeStatusDevice2 = DataRetrievalPage.TimeQualityStatusFromDeviceStatus()
      DataRetrievalPage.CloseDeviceStatus.ClickButton()
    }
    while (TimeStatusDevice1!="locked" && TimeStatusDevice2!="locked")
        
    //Step9. Generate Manual Trigger
    DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+DeviceSuffix[0]),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+DeviceSuffix[0]))
    
    //Step9.1 Trigger Manual DFR
    var NumberofTimes=0
    var IterationReq = CommonMethod.ReadDataFromExcel(DataSheetName,"NumberofTimes")
    var Delay = aqConvert.StrToInt64(CommonMethod.ReadDataFromExcel(DataSheetName,"Delay"))
    do
    {
      NumberofTimes=NumberofTimes+1    
      AssertClass.IsTrue(DataRetrievalPage.ClickOnFRManualTrigger(),"Clicked on FR Manual Trigger")
      AssertClass.IsTrue(DataRetrievalPage.ClickonOKManualDFRTrigger())
      aqUtils.Delay(Delay*1000)      
    }
    while (NumberofTimes!=IterationReq)
    
    //Step10. Check for Cross Trigger
    DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+DeviceSuffix[1]),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+DeviceSuffix[1]))
    
    //Step10.1 Check for COT
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory(),"Clicked on DFR Directory")
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDownloadDataNow(),"Clicked on Download Data")
  }
  catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Error:-CAM-736 Test to check DFR record length with Cross Trigger")  
  }
}