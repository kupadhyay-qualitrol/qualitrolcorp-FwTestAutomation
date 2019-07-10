/*This file contain methods related to the IECBrowser common functionality which can be accessed by other TestScripts and are generic. 
*/

//USEUNIT ConfigEditor_ConfigurationPage

//Global Variables
var IECToolBar = Aliases.Iec_Browser.mainForm.toolBar1
var Trace_Log = Aliases.Iec_Browser.mainForm.panel1.textBox_Output
var Main_Menu = Aliases.Iec_Browser.mainForm
var Referesh_Time = 1000;

function LaunchIECBrowser()
{
  try
    {
      if(TestedApps.Iec_Browser.Run())
    {
      do
      {
        aqUtils.Delay(2*Referesh_time)
      }
      while (!IECToolBar.Exists)
      Log.Message("Application launched successfully")
      return true
    }
  }
  catch(ex)
  {
    //Post the message to log file  
    Log.Error(ex.stack)
    return false
  }
}

function CloseIECBrowser()
{
  try 
  {
    //Close iec browser application.
    Main_Menu.Close()
    Log.Message("Application Closed successfully")    
  }
  catch(ex)
  {
    //Post the message to log file  
    Log.Error("Exception raised:- "+ex.message)
  }
}

//Method to check progress from activity log
function CheckTraceLog(logMessage)
{
 var trace_ExecutionCount=0
 do
 {
  aqUtils.Delay(2000)
  trace_ExecutionCount = trace_ExecutionCount + 1
  }
 while (aqString.FindLast(Trace_Log.Text,logMessage)==-1 && trace_ExecutionCount<=10) 
 
 if(trace_ExecutionCount==11)
 {
   return false
 }
 else
 {
   return true
 } 
}