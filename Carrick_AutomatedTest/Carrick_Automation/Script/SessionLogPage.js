/*This page contains methods and objects related to Seesion Log*/

var Toolbar_SessionLog =Aliases.iQ_Plus.ShellForm.windowDockingArea1.dockableWindow3.ActivityLog.ActivityMonitor.zUserControlBase_Toolbars_Dock_Area_Top
//This method is used to clear the Session Log.
function ClearLog()
{
  if(!Toolbar_SessionLog.Exists)
  {
   Log.Message("Clear Log button doesn't exists")
   return false
  }
  else
  {
    Toolbar_SessionLog.ClickItem("ActivityLogToolbar|&Clear")
    Log.Message("Clicked on Clear button")
    return true
  }
}

function SaveLog()
{
  if(!Toolbar_SessionLog.Exists)
  {
    Log.Message("Clear Log button doesn't exists")
    return false
  }
  else
  {
    Toolbar_SessionLog.ClickItem("ActivityLogToolbar|&Save")
    Log.Message("Clicked on Save button")
    return true
  }
}