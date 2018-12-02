//USEUNIT CommonMethod
//USEUNIT LoginPage
//USEUNIT DeviceTopologyPage
//USEUNIT GeneralPage

//TC-Test to trigger Manual DFR and see in Display Directory
function TriggerManualDFR()
{
  try
  {
    Log.Message("Start:Test to trigger Manual DFR and see in Display Directory")  
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
      //Step4. Check last record number
      
      
    }
  }
  catch(ex)
  {
    
  }
}