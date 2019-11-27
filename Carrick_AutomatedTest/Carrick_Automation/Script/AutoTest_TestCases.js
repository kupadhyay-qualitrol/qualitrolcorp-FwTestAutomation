/*This file contains Test Cases related to AutoTest tool*/

//USEUNIT AutoTestSetup_Methods
//USEUNIT AutoTest_TestsPage
//USEUNIT AssertClass

function ReadDeviceConfigTest(){
  
  Log.Message("Started TC:-Test Read Device Config")
  //Step-1 Setup Test Settings for AutoTest tool 
  AutoTestSetup_Methods.Setup_auto_Test()
  
  //Step-2 Perform Read Device Cofig Test
  Log.Message("Verify that Read Device Config test is performed")
  AssertClass.IsTrue(AutoTest_TestsPage.Read_Device_Config_Test(),"Read Device Config Test button is clicked")

}


