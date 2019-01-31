/*This file contains Test Cases related to Time Zone in the iQ+ and device*/

//USEUNIT CommonMethod
//USEUNIT DeviceTopologyPage
//USEUNIT AssertClass
//USEUNIT GeneralPage
//USEUNIT DeviceManagementPage


/*
BTC-95 & BTC-106:-Test all Timezone supported in Carrick & Cashel
*/
function BTC_95_BTC_106()
{
  try
  {
    Log.Message("Started TC:-Test all Timezone supported in Carrick & Cashel")
    var DataSheetName = Project.ConfigPath +"TestData\\TimeZone.xlsx"
    
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
    
    //Step4. 
    
    Log.Message("Pass:-Test all Timezone supported in Carrick & Cashel")
  }
  catch(ex)
  {
    Log.Message(ex.stack)
    Log.Error("Fail:-Test all Timezone supported in Carrick & Cashel")
  }
}