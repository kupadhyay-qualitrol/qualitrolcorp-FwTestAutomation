/*This unit contains test cases related to Max DFR functionality*/

//USEUNIT AssertClass
//USEUNIT CommonMethod
//USEUNIT DeviceTopologyPage
//USEUNIT GeneralPage
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecordingPage
//USEUNIT ConfigEditor_FinishPage

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
    
    //Step7.Close Config Editor
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close in Config Editor")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check the GUI(Text/Editbox) of iQ+ for Maximum DFR record length")
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
    
    //Step9. Close the ConfigEditor
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Closed the Config Editor")
    
    Log.Message("Pass:-Test to check that DFR record length value get saved to database.")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check that DFR record length value get saved to database.")
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
    
    //Step10. Close Configuration Editor
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close on config Editor")    
    
    Log.Message("Pass:- Test to check that minimum and maximum limit for DFR record length with non-Transco licenses")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check that minimum and maximum limit for DFR record length with non-Transco licenses")
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
    
    //Step8. Click on Close
    AssertClass.IsTrue(ConfigEditorPage.ClickOnClose(),"Clicked on Close")
    
    Log.Message("Pass:-Test to check that user tries to set minimum DFR record length equal to Prefault time")
  }
  catch(ex)
  {
    Log.Error(ex.stack)
    Log.Error("Fail:-Test to check that user tries to set minimum DFR record length equal to Prefault time")
  }
}