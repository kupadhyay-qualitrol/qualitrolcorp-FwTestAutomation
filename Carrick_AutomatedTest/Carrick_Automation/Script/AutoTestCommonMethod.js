/*This file contain methods related to the AutoTest tool
*common functionality which can be accessed by other TestScripts and are generic. 
*/

//Global Variables
//var OpenConfigSheetButton = Aliases.AutoTest_00_TestShell.MainForm.splitContainer1.SplitterPanel.testPanel.groupBoxSettings.WinFormsObject("btnOpenConfigurationSheet")

//This method launches AutoTest application from installed folder path.
function Launch_auto_Test()
{
  try 
  {
    //Before launching AutoTest. Terminate if any instance of AutoTest is running.
    Terminate_auto_Test()
  
    //Launch AutoTest application.
    if(TestedApps.AutoTest_00_TestShell.Run())
    {
    
      Log.Message("Application launched successfully")
      return true
    }
  }
  catch(ex)
  {
    //Post the message to log file  
    Log.Error(ex.message)
    return false
  }
}


//Terminate AutoTest Tool
function Terminate_auto_Test()
{
  //Check whether the AutoTest is running or not?
  var autoTest_process=Sys.WaitProcess("AutoTest.00.TestShell")
  
  if (autoTest_process.Exists)
  {
    autoTest_process.Terminate() 
    Log.Message("Terminated AutoTest Application")
  }  
  
  else {
    Log.Message("AutoTest Application is not running")
  }
}