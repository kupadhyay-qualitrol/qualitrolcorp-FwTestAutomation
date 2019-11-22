/*This file contain methods related to the AutoTest Tests Page elements 
*common functionality which can be accessed by other TestScripts and are generic. 
*/



//Global Variables
var ReadDeviceConfigButton = Aliases.AutoTest_00_TestShell.MainForm.splitContainer1.SplitterPanel.testPanel.groupBoxTest.tableLayoutPanelTestbuttons.btn01ReadDevice0
var CheckBoxConfirm = Aliases.AutoTest_00_TestShell.ValidationResultsForm.checkBoxConfirm
var PassButton = Aliases.AutoTest_00_TestShell.ValidationResultsForm.buttonPassed
var TestResult = Aliases.AutoTest_00_TestShell.MainForm.splitContainer1.SplitterPanel2.tabControl.tabPageText.tbxTestTextResults

//This Method will Perform Test on Read Device Config 
function Read_Device_Config_Test () {
  
  if (ReadDeviceConfigButton.Exists) {
   ReadDeviceConfigButton.Click()
   Log.Message("Read Device Config test button is clicked")
   aqUtils.Delay(30000)
   CheckBoxConfirm.Click()
   PassButton.Click()
  }

  else {
   Log.Error("Read Device Config test button is not visible")    
  }
  
  if (TestResult.Exists){
   var result = TestResult.get_Text();
   Log.Message(result)
   Log.Message("Read Device Testcase Passed")
  }
  
  else {
    Log.Error("Read Device Testcase Failed")
  }
 
}
