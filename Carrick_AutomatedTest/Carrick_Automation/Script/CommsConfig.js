//USEUNIT CommonMethod
//USEUNIT LoginPage
//USEUNIT DeviceTopologyPage
//USEUNIT DeviceManagementPage
//USEUNIT GeneralPage
//USEUNIT ConfigEditorPage
//USEUNIT ConfigEditor_FaultRecordingPage

var Edtbx_Username = Aliases.iQ_Plus.UserLogin.USRLOGINtxtUserName

//TC-Test to check retrieve,Send & Save to Db Config works fine after changing pre-fault & post-fault time.
function CommsConfig()
{
  try
  { 
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx"  
    //Step1.Launch iQ+  
    CommonMethod.Launch_iQ_Plus()
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Edtbx_Username.Exists)
    
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Edtbx_Username.Enabled)
    
    if(Edtbx_Username.Enabled)
    {
      Log.Message("Application launched successfully")
    }
    else
    {
      Log.Message("Application didn't launched successfully")
    }    
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
    
    //Step5. Change Pre-fault Time & Save to Db
    ConfigEditor_FaultRecordingPage.SetPrefault(CommonMethod.ReadDataFromExcel(DataSheetName,"PrefaultTime"))
    
    //Step6. Save to DB
    ConfigEditorPage.ClickSaveToDb()
    
    //Step7. Modify Configuration
    
    
    //*Terminates the iq+ application
    //CommonMethod.Close_iQ_Plus()
  }
  catch(ex)
  {
    
  }
}
