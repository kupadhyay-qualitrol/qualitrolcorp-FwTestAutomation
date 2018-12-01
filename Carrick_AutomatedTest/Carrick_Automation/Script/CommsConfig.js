//USEUNIT CommonMethod
//USEUNIT LoginPage
//USEUNIT DeviceTopologyPage
//USEUNIT DeviceManagementPage
//USEUNIT GeneralPage
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecordingPage
//USEUNIT SessionLogPage


//TC-Test to check Save to Db Config works fine after changing pre-fault time.
function Test_SaveToDB_Prefault()
{
  try
  { 
    Log.Message("Start:Test to check Save to Db Config works fine after changing pre-fault time.")  
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx"  
    //Step1.Launch iQ+  
    if(CommonMethod.Launch_iQ_Plus())
    {    
      //Step2.Login iQ+ with Admin role
      LoginPage.Login(CommonMethod.ReadDataFromExcel(DataSheetName,"Username"),CommonMethod.ReadDataFromExcel(DataSheetName,"Password"))
      Log.Message("Login to the iQ+ successfully.")
      //Step3.Check whether device exists or not in the topology.    
      if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))!=true)
      {
        GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
        DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
      }
      else
      {
        Log.Message("Device exist in the tree topology.")
      }
      //Step4.Retrieve Configuration
      CommonMethod.AssertIsTrue(true,DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
      //Step5. Change Pre-fault Time.
      ConfigEditor_FaultRecordingPage.SetPrefault(CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime"))
    
      //Step6. Save to DB
      ConfigEditorPage.ClickSaveToDb()
    
      //Step7. Modify Configuration
      DeviceManagementPage.ClickonModifyConfig()
    
      //Step8. Check prefaultime
      if(ConfigEditor_FaultRecordingPage.GetPrefault()!=CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime"))
      {
        Log.Error("Prefault time doesn't match after save to Db.")
      }
      else
      {
        Log.Message("Prefault time before and After saving to DB is same.")
      }
      //Step9.Close the Config Editor
      ConfigEditorPage.ClickOnClose()
      //Step10.Terminates the iq+ application
      CommonMethod.Close_iQ_Plus()
      Log.Message("Passed:-Test to check Save to Db Config works fine after changing pre-fault time.")
    }
    else
    {
      Log.Message("Unable to launch iQ+.")
      Log.Message("Failed:-Test to check Save to Db Config works fine after changing pre-fault time.")
    }
  }
  catch(ex)
  {
    Log.Error(ex.message)
    Log.Message("Failed:-Test to check Save to Db Config works fine after changing pre-fault time.")
  }
}

//TC-Test to check Send to Device & Retrieve works fine after changing pre-fault time.
function Test_SendToDevice_Prefault()
{
  try
  { 
    Log.Message("Start:-Test to check Send to Device & Retrieve works fine after changing pre-fault time.")  
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx"  
    //Step1.Launch iQ+  
    if(CommonMethod.Launch_iQ_Plus())
    {    
      //Step2.Login iQ+ with Admin role
      LoginPage.Login(CommonMethod.ReadDataFromExcel(DataSheetName,"Username"),CommonMethod.ReadDataFromExcel(DataSheetName,"Password"))
      Log.Message("Login to the iQ+ successfully.")
      //Step3.Check whether device exists or not in the topology.    
      if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))!=true)
      {
        GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
        DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
      }
      else
      {
        Log.Message("Device exist in the tree topology.")
      }
      //Step4.Retrieve Configuration     
      CommonMethod.AssertIsTrue(true,DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
      //Step5. Change Pre-fault Time
      ConfigEditor_FaultRecordingPage.SetPrefault(CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime"))
      
      //Step6. Send to Device
      ConfigEditorPage.ClickSendToDevice()
      
      //Step7.Retrieve Configuration
      CommonMethod.AssertIsTrue(true,DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
      
      //Step8.Check pre-fault time
      if(ConfigEditor_FaultRecordingPage.GetPrefault()!=CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime"))
      {
        Log.Error("Prefault time doesn't match after save to Db.")
      }
      else
      {
        Log.Message("Prefault time before and After saving to DB is same.")
      }
      //Step9.Close the Config Editor
      ConfigEditorPage.ClickOnClose()
      //Step10.Terminates the iq+ application
      CommonMethod.Close_iQ_Plus()      
      Log.Message("Passed:-Test to check Send to Device & Retrieve works fine after changing pre-fault time.")
    }
    else
    {
      Log.Message("Unable to launch iQ+.")
      Log.Message("Failed:-Test to check Save to Db Config works fine after changing pre-fault time.")
    }
  }
  catch(ex)
  {  
    Log.Error(ex.message)
    Log.Message("Failed:-Test to check Send to Device & Retrieve works fine after changing pre-fault time.")
  }
}
