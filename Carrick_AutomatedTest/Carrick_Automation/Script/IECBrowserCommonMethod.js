/*This file contain methods related to the IECBrowser common functionality which can be accessed by other TestScripts and are generic. 
*/

//USEUNIT ConfigEditor_ConfigurationPage

//Global Variables
var IECtoolbar = Aliases.Iec_Browser.mainForm.toolBar1
var Trace_Log = Aliases.Iec_Browser.mainForm.panel1.textBox_Output

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
    if(TestedApps.Iec_Browser.Terminate())
    {
    Log.Message("Application Closed successfully")    
    }
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
 do
 {
 aqUtils.Delay(2000)
 }
 while (aqString.FindLast(Trace_Log.Text,logmessage)==-1)  
}