/*This file contain methods related to the IECBrowser common functionality which can be accessed by other TestScripts and are generic. 
*/

//USEUNIT ConfigEditor_ConfigurationPage

//Global Variables
var IECtoolbar = Aliases.Iec_Browser.mainForm.toolBar1
var Trace_Log = Aliases.Iec_Browser.mainForm.panel1.textBox_Output
var Main_Menu = Aliases.Iec_Browser.mainForm
var Referesh_time = 1000;

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
      while (!IECtoolbar.Exists)
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
function CheckTraceLog(logmessage)
{
 var trace_executioncount=0
 do
 {
  aqUtils.Delay(2000)
  trace_executioncount = trace_executioncount + 1
  }
 while (aqString.FindLast(Trace_Log.Text,logmessage)==-1 && trace_executioncount<=10) 
 
 if(trace_executioncount==11)
 {
   return false
 }
 else
 {
   return true
 } 
}