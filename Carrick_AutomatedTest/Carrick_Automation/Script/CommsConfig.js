//USEUNIT CommonMethod
//USEUNIT LoginPage
//USEUNIT DeviceTopologyPage
//USEUNIT DeviceManagementPage
//USEUNIT GeneralPage

function CommsConfig()
{
  try
  { 
    var DataSheetName = Project.ConfigPath +"TestData\\SmokeTestData.xlsx"
  
    //1.Launch iQ+  
    CommonMethod.Launch_iQ_Plus()
    //2.Login iQ+ with Admin role
    LoginPage.Login(CommonMethod.ReadDataFromExcel(DataSheetName,"Username"),CommonMethod.ReadDataFromExcel(DataSheetName,"Password"))
    //3.Check whether device exists or not in the topology.    
    if(DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))!=true)
    {
      GeneralPage.CreateDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceSerialNo"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceIPAdd"))
      DeviceTopologyPage.ClickonDevice(CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceType"),CommonMethod.ReadDataFromExcel(DataSheetName,"DeviceName"))      
    }
    //4.Retrieve Configuration
    //CommonMethod.AssertIsTrue(true,DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    //*Terminates the iq+ application
    //CommonMethod.Close_iQ_Plus()
  }
  catch(ex)
  {
    
  }
}
