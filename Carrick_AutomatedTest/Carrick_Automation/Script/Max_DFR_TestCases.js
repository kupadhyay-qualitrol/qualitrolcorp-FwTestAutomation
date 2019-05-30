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
//USEUNIT CrossTrigger_Methods
//USEUNIT TimeSync_Methods
//USEUNIT ConfigEditor_FaultRecording_FRSensorPage
//USEUNIT OmicronStateSeqPage

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
    Log.Message("Start TC:-CAM-736- Test to check DFR record length with Cross Trigger")
    var DeviceSuffix =["1","2"]
    var DeviceLRecordNum=[]
    DeviceLRecordNum.length =2
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
    
      //Step 5. Configure Time Master Slave
      TimeSync_Methods.Configure_TimeMaster_Slave(DataSheetName,(i+1))
      
      //Step6. Configure Cross Trigger
      AssertClass.IsTrue(ConfigEditorPage.ClickonAdvance(),"Clicked on Advance")
      CrossTrigger_Methods.Configure_CrossTrigger(DataSheetName,(i+1))
      
      //Step7.1 Send to Device
      AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
      //Step7.2 Get Both Device Latest Record Number
      AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory(),"Clicked on DFR Directory")
      DeviceLRecordNum[i] = DataRetrievalPage.GetLatestRecordnumber()
      AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory(),"Clicked on Close button")
    }
        
    //Step8. Check Time Quality Status
    TimeSync_Methods.CheckTimeQualityInMasterSlave(DataSheetName, DeviceSuffix[0], DeviceSuffix[1])
        
    //Step9. Generate Manual Trigger
    DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+DeviceSuffix[0]),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+DeviceSuffix[0]))
    
    AssertClass.IsTrue(DataRetrievalPage.OpenFRManualTriggerDialog(),"Clicked on FR Manual Trigger")
    AssertClass.IsTrue(DataRetrievalPage.SetNoOfManualTrigger(CommonMethod.ReadDataFromExcel(DataSheetName,"NoOfManualTrigger")),"Setting No. of Manual Triggers")
    AssertClass.IsTrue(DataRetrievalPage.ClickonOKManualDFRTrigger(),"Clicked on OK button")
    
    //Step10. Check for Cross Trigger
    DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"+DeviceSuffix[1]),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"+DeviceSuffix[1]))
    
    //Step10.1 Check for New DFR Record
    AssertClass.IsTrue(DFR_Methods.IsNewRecordFound(30,DeviceLRecordNum[1]),"Checking for New Record")
    //Step10.2 Check for COT
    AssertClass.CompareString("XTRIG", DataRetrievalPage.GetCOTByRecordNumber(NewDFRRecord),"Checking COT for DFR")
    
    //Step10.3 Download DFR
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDownloadDataNow(),"Downloadind Record Now")
    AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory(),"Closed DFR Directory Popup") 
    //Step10.4 View Record on PDP
    AssertClass.IsTrue(DFR_Methods.ViewDFROnPDP(NewDFRRecord),"Checking for New record on PDP")
    
    //Step 11 Validate Record Length
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(CommonMethod.ReadDataFromExcel(DataSheetName,"ExpectedRecordLength")),CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(0)),0,"Checking Record Duration")
    Log.Message("Pass:- CAM-736 Test to check DFR record length with Cross Trigger")
  }
  catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Error:-CAM-736 Test to check DFR record length with Cross Trigger")  
  }
}

/*
CAM-729 Test to check limit DFR record length feature when FR trigger(Pre+Oplimit+Post fault time) is equal to Maximum record length.
CAM-730 Test to check limit DFR record length functionality when FR trigger(Pre+Oplimit+Post fault time) is over Maximum record length.
CAM-731 Test to check limit DFR record length feature when FR trigger(Pre+Oplimit+Post fault time) is within Maximum record length.
CAM-733 Test to check DFR record with continuous FR Trigger in Post Fault state.
*/
function CAM_729_730_731_733()
{
  try
  {
    Log.Message("Start:-Test to check limit DFR record length feature when FR trigger(Pre+Oplimit+Post fault time) is within Maximum record length.")
    var dataSheetName = Project.ConfigPath +"TestData\\CAM_729_730_731_733.xlsx"
    //Step0.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"))      
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
    var prefault =CommonMethod.ReadDataFromExcel(dataSheetName,"PrefaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(prefault),"Validating Prefault Time")
    
    //Step3.1. Set Max DFR time
    var maxDFR=CommonMethod.ReadDataFromExcel(dataSheetName,"MaxDFR")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(maxDFR),"Validating Max DFR") 
    
    //Step3.2 Click on FR Sensor
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFRSensor(),"Clicked on FR Sensor")
    
    //Step4 Set Post Fault,Oplimit for FR Sensor
    AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.OpenFRSensorEditor(0),"Open up FR Sensor Editor") //Setting First FR Sensor
    var frsensorNameFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"FRSensorName")
    var frsensorTypeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"Type")
    var frsensorScalingTypeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"ScalingType")
    var frsensorUpperThresholdFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"UpperThreshold")
    var frsensorPostFaultTimeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"PostFaultTime")
    var frsensorOplimitFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"Oplimit")
    var frsensorRecordDurationFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"RecordDuration")
    
    DFR_Methods.SetFRSensor(frsensorNameFromTestData,frsensorTypeFromTestData,frsensorScalingTypeFromTestData,frsensorUpperThresholdFromTestData,frsensorPostFaultTimeFromTestData,frsensorOplimitFromTestData)
    
    //Step5. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step6. Click on DFR Directory under Display Device Directory
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory() ,"Clicked on DFR Directory")           
      
    //Step7. Find latest Record Number
    var lastDFRRecord= DataRetrievalPage.GetLatestRecordnumber()
    Log.Message("Current Record Number is :- "+lastDFRRecord)      
      
    //Step8. Close DFR Directory
    AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory() ,"Close DFR Directory") 
    
    //Step9 Start Omicron Injection
    OmicronStateSeqPage.RunSeqFile(Project.ConfigPath+"TestData\\"+CommonMethod.ReadDataFromExcel(dataSheetName,"OmicronFile"))
      
    AssertClass.IsTrue(DFR_Methods.IsNewRecordFound(10,lastDFRRecord),"Checking for new Record")
    
    AssertClass.CompareString("FRSENSOR",DataRetrievalPage.GetCOTForLatestDFRRecord(),"Checking COT") 
    
    //Step11. Click on Download Data Now
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDownloadDataNow(),"Clicked on Download Data Now")
    CommonMethod.CheckActivityLog("DFR records saved successfully for device")
    AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory(),"Closed DFR Directory")
    DFR_Methods.ViewDFROnPDP(aqConvert.StrToInt64(lastDFRRecord)+1)
    
    //Step12. Check Record Length
    var recordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(0))//FirstRow
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(frsensorRecordDurationFromTestData),aqConvert.StrToInt64(recordLength),0,"Validating Record Duration.")
    
    //Step13. Check Prefault time
    var actualPrefault = (PDPPage.GetRecordTriggerDateTime(0))-PDPPage.GetRecordStartDateTime(0)
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(prefault),actualPrefault,0,"Prefault calculated from PDP is :-"+actualPrefault)
    
    //Step14. Export to CDF.
    if (aqFileSystem.Exists(Project.ConfigPath+"DFRRecordResults"))
    {
      AssertClass.IsTrue(PDPPage.ExportTOCDF(Project.ConfigPath+"DFRRecordResults\\"))
    }
    else
    {
      aqFileSystem.CreateFolder(Project.ConfigPath+"DFRRecordResults")
      AssertClass.IsTrue(PDPPage.ExportTOCDF(Project.ConfigPath+"DFRRecordResults\\"))
    }    
    //Step15. Export to CSV
    var sysUserName = CommonMethod.GetSystemUsername()
    var dfrRecordPath ="C:\\Users\\"+sysUserName+"\\Desktop\\DFRRecord\\"
    if (aqFileSystem.Exists(dfrRecordPath))
    {
      AssertClass.IsTrue(PDPPage.ExportTOCSV())    
    }
    else
    {
      aqFileSystem.CreateFolder(dfrRecordPath)
      AssertClass.IsTrue(PDPPage.ExportTOCSV())
    }
    AssertClass.IsTrue(CommonMethod.KillProcess("EXCEL")) //This method is used to kill the process    
    Log.Message("Pass:-Test to check limit DFR record length feature when FR trigger(Pre+Oplimit+Post fault time) is within Maximum record length.")
  }
  catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Error:-Test to check limit DFR record length feature when FR trigger(Pre+Oplimit+Post fault time) is within Maximum record length.")  
  }
  finally
  {
    OmicronStateSeqPage.CloseStateSeq()
  }
}

//CAM-734 Test to check DFR record length when Trigger comes at end of first record.
function CAM_734()
{
 try
  {
    Log.Message("Start:-Test to check DFR record length when Trigger comes at end of first record..")
    var dataSheetName = Project.ConfigPath +"TestData\\CAM_734.xlsx"
    //Step0.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"))      
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
    var prefault =CommonMethod.ReadDataFromExcel(dataSheetName,"PrefaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(prefault),"Validating Prefault Time")
    
    //Step3.1. Set Max DFR time
    var maxDFR=CommonMethod.ReadDataFromExcel(dataSheetName,"MaxDFR")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(maxDFR),"Validating Max DFR") 
    
    //Step3.2 Click on FR Sensor
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFRSensor(),"Clicked on FR Sensor")
    
    //Step4 Set Post Fault,Oplimit for FR Sensor
    AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.OpenFRSensorEditor(0),"Open up FR Sensor Editor") //Setting First FR Sensor
    var frsensorNameFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"FRSensorName")
    var frsensorTypeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"Type")
    var frsensorScalingTypeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"ScalingType")
    var frsensorUpperThresholdFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"UpperThreshold")
    var frsensorPostFaultTimeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"PostFaultTime")
    var frsensorOplimitFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"Oplimit")
    var expectedRecordDuration1 = CommonMethod.ReadDataFromExcel(dataSheetName,"ExpectedRecordDuration_1")
    var expectedRecordDuration2 = CommonMethod.ReadDataFromExcel(dataSheetName,"ExpectedRecordDuration_2")
    var expectedRecordDuration3 = CommonMethod.ReadDataFromExcel(dataSheetName,"ExpectedRecordDuration_3")
    var expectedRecordDuration4 = CommonMethod.ReadDataFromExcel(dataSheetName,"ExpectedRecordDuration_4")
            
    DFR_Methods.SetFRSensor(frsensorNameFromTestData,frsensorTypeFromTestData,frsensorScalingTypeFromTestData,frsensorUpperThresholdFromTestData,frsensorPostFaultTimeFromTestData,frsensorOplimitFromTestData)
     
    //Step4.1 Set Post fault, oplimit for FR sensor
    AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.OpenFRSensorEditor(1),"Open up FR Sensor Editor") //Setting Second FR Sensor
    var frsensorName1FromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"FRSensorName1")
    
    DFR_Methods.SetFRSensor(frsensorName1FromTestData,frsensorTypeFromTestData,frsensorScalingTypeFromTestData,frsensorUpperThresholdFromTestData,frsensorPostFaultTimeFromTestData,frsensorOplimitFromTestData)
    
    //Step5. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    CAM_734_Verification("OmicronFile_1",expectedRecordDuration1,expectedRecordDuration2,dataSheetName,prefault,100);
    CAM_734_Verification("OmicronFile_2",expectedRecordDuration3,expectedRecordDuration4,dataSheetName,prefault);
       
    Log.Message("Pass:-Test to check DFR record length when Trigger comes at end of first record.")
  } 
    catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Error:-Test to check DFR record length when Trigger comes at end of first record.")  
  }
}

//This Function Gets Latest DFR record number, Injects Omicron seq file , 
//verify if two records are generated and if so, perform record duration verification & exports them to CDF & CSV
function CAM_734_Verification(OmirconSeqFile,expectedRecordDurationPrevRec,expectedRecordDurationLatestRec,dataSheetName,expectedPrefault,prefaultBuffer=0)
{
 try
  {
    Log.Message("Log Start:-Test to check DFR record length when Trigger comes at end of first record with Seq ." + OmirconSeqFile)
    //Step6. Click on DFR Directory under Display Device Directory
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory() ,"Clicked on DFR Directory")           
      
    //Step7. Find latest Record Number
    var lastDFRRecord= DataRetrievalPage.GetLatestRecordnumber()
    Log.Message("Current Record Number is :- "+lastDFRRecord)      
      
    //Step8. Close DFR Directory
    AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory() ,"Close DFR Directory") 
    
    //Step9 Start Omicron Injection
    OmicronStateSeqPage.RunSeqFile(Project.ConfigPath+"TestData\\"+CommonMethod.ReadDataFromExcel(dataSheetName,OmirconSeqFile))
      
    AssertClass.IsTrue(DFR_Methods.IsMultipleRecordFound(10,2,lastDFRRecord),"Checking for new Record")
    
    AssertClass.CompareString("FRSENSOR",DataRetrievalPage.GetCOTForLastestXDFRRecords(2)[0],"Checking COT")
     
    //Step11. Click on Download Data Now
    AssertClass.IsTrue(DFR_Methods.DownloadMultipleRecords(),"Clicked on Download Data Now")
    CommonMethod.CheckActivityLog("DFR records saved successfully for device")
    AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory(),"Closed DFR Directory")
    DFR_Methods.ViewDFROnPDP(aqConvert.StrToInt64(lastDFRRecord)+2)
    
        //Step12. Check Record Length
    var recordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(0))//FirstRow
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(expectedRecordDurationLatestRec),aqConvert.StrToInt64(recordLength),10,"Validating Record Duration.")
    var recordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(1))//SecondRow
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(expectedRecordDurationPrevRec),aqConvert.StrToInt64(recordLength),0,"Validating Record Duration.")
    
    //Step13. Check Prefault time
    var actualPrefault = (PDPPage.GetRecordTriggerDateTime(0))-PDPPage.GetRecordStartDateTime(0)
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(expectedPrefault)-prefaultBuffer,actualPrefault,0,"Prefault calculated from PDP is :-"+actualPrefault)
    var actualPrefault = (PDPPage.GetRecordTriggerDateTime(1))-PDPPage.GetRecordStartDateTime(1)
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(expectedPrefault),actualPrefault,0,"Prefault calculated from PDP is :-"+actualPrefault)
    
    //Step14. Export to CDF for first record.
    if (aqFileSystem.Exists(Project.ConfigPath+"DFRRecordResults"))
    {
      AssertClass.IsTrue(PDPPage.ExportMultipleTOCDF(Project.ConfigPath+"DFRRecordResults\\",0))
    }
    else
    {
      aqFileSystem.CreateFolder(Project.ConfigPath+"DFRRecordResults")
      AssertClass.IsTrue(PDPPage.ExportMultipleTOCDF(Project.ConfigPath+"DFRRecordResults\\",0))
    }    
    //Step14.1. Export to CDF for second record.
    if (aqFileSystem.Exists(Project.ConfigPath+"DFRRecordResults"))
    {
      AssertClass.IsTrue(PDPPage.ExportMultipleTOCDF(Project.ConfigPath+"DFRRecordResults\\",1))
    }
    else
    {
      aqFileSystem.CreateFolder(Project.ConfigPath+"DFRRecordResults")
      AssertClass.IsTrue(PDPPage.ExportMultipleTOCDF(Project.ConfigPath+"DFRRecordResults\\",1))
    }    
    //Step15. Export to CSV for first record
    var sysUserName = CommonMethod.GetSystemUsername()
    var dfrRecordPath ="C:\\Users\\"+sysUserName+"\\Desktop\\DFRRecord\\"
    if (aqFileSystem.Exists(dfrRecordPath))
    {
      AssertClass.IsTrue(PDPPage.ExportMultipleTOCSV(0))    
    }
    else
    {
      aqFileSystem.CreateFolder(dfrRecordPath)
      AssertClass.IsTrue(PDPPage.ExportMultipleTOCSV(0))
    }
    AssertClass.IsTrue(CommonMethod.KillProcess("EXCEL")) //This method is used to kill the process    
  //Step15.1. Export to CSV for second record
    var sysUserName = CommonMethod.GetSystemUsername()
    var dfrRecordPath ="C:\\Users\\"+sysUserName+"\\Desktop\\DFRRecord\\"
    if (aqFileSystem.Exists(dfrRecordPath))
    {
      AssertClass.IsTrue(PDPPage.ExportMultipleTOCSV(1))    
    }
    else
    {
      aqFileSystem.CreateFolder(dfrRecordPath)
      AssertClass.IsTrue(PDPPage.ExportMultipleTOCSV(1))
    }
    AssertClass.IsTrue(CommonMethod.KillProcess("EXCEL")) //This method is used to kill the process 
    Log.Message("Log End:-Test to check DFR record length when Trigger comes at end of first record with Seq ." + OmirconSeqFile)
  } 
  catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Error:-Test to check DFR record length when Trigger comes at end of first record with Seq." + OmirconSeqFile)  
  }
  finally
  {
    OmicronStateSeqPage.CloseStateSeq()
  }
}

// CAM-732 Test to check DFR record length with continuous FR Trigger within oplimit.
function CAM_732()
{
  try
  {
    Log.Message("Start:-Test to check DFR record length with continuous FR Trigger within oplimit.")
    var dataSheetName = Project.ConfigPath +"TestData\\CAM_732.xlsx"
    //Step0.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(dataSheetName,"DeviceName"))      
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
    var prefault =CommonMethod.ReadDataFromExcel(dataSheetName,"PrefaultTime")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetPrefault(prefault),"Validating Prefault Time")
    
    //Step3.1. Set Max DFR time
    var maxDFR=CommonMethod.ReadDataFromExcel(dataSheetName,"MaxDFR")
    AssertClass.IsTrue(ConfigEditor_FaultRecordingPage.SetMaxDFR(maxDFR),"Validating Max DFR") 
    
    //Step3.2 Click on FR Sensor
    AssertClass.IsTrue(ConfigEditorPage.ClickOnFRSensor(),"Clicked on FR Sensor")
    
    //Step4 Set Post Fault,Oplimit for FR Sensor
    AssertClass.IsTrue(ConfigEditor_FaultRecording_FRSensorPage.OpenFRSensorEditor(0),"Open up FR Sensor Editor") //Setting First FR Sensor
    var frsensorNameFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"FRSensorName")
    var frsensorTypeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"Type")
    var frsensorScalingTypeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"ScalingType")
    var frsensorUpperThresholdFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"UpperThreshold")
    var frsensorPostFaultTimeFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"PostFaultTime")
    var frsensorOplimitFromTestData = CommonMethod.ReadDataFromExcel(dataSheetName,"Oplimit")
    var expectedRecordDuration1 = CommonMethod.ReadDataFromExcel(dataSheetName,"ExpectedRecordDuration_1")     
    
    DFR_Methods.SetFRSensor(frsensorNameFromTestData,frsensorTypeFromTestData,frsensorScalingTypeFromTestData,frsensorUpperThresholdFromTestData,frsensorPostFaultTimeFromTestData,frsensorOplimitFromTestData)
    
    //Step5. Send to Device
    AssertClass.IsTrue(ConfigEditorPage.ClickSendToDevice(),"Clicked on Send to Device")
    
    //Step6. Click on DFR Directory under Display Device Directory
    AssertClass.IsTrue(DataRetrievalPage.ClickOnDFRDirectory() ,"Clicked on DFR Directory")           
      
    //Step7. Find latest Record Number
    var lastDFRRecord= DataRetrievalPage.GetLatestRecordnumber()
    Log.Message("Current Record Number is :- "+lastDFRRecord)      
      
    //Step8. Close DFR Directory
    AssertClass.IsTrue(DataRetrievalPage.CloseDFRDirectory() ,"Close DFR Directory") 
    
    //Step9 Start Omicron Injection
    OmicronStateSeqPage.RunSeqFile(Project.ConfigPath+"TestData\\"+CommonMethod.ReadDataFromExcel(dataSheetName,"OmicronFile_1"))  
    AssertClass.IsTrue(DFR_Methods.IsMultipleRecordFound(10,1,lastDFRRecord),"Checking for new Record")    
     
    //Step11. Click on Download Data Now
    AssertClass.IsTrue(DFR_Methods.DownloadManualDFR(),"Clicked on Download Data Now")
    
    //Step12. Check Record Length
    var recordLength= CommonMethod.ConvertTimeIntoms(PDPPage.GetRecordDuration(0))//FirstRow
    AssertClass.CompareDecimalValues(aqConvert.StrToInt64(expectedRecordDuration1),aqConvert.StrToInt64(recordLength),10,"Validating Record Duration.")
    
    Log.Message("Pass:-Test to check DFR record length with continuous FR Trigger within oplimit.")
  }
  catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Fail:-Test to check DFR record length with continuous FR Trigger within oplimit. ")
  }
  finally
  {
    OmicronStateSeqPage.CloseStateSeq()
  }
}