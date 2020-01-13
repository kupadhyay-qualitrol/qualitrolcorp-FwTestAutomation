/*This file contains methods and object variables related Data Analysis View page*/

var TOP_TOOLBAR = Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top

//This method is used for clicking on Continuous Recording
function clickOnContnuousRecording()
{
  if(TOP_TOOLBAR.Visible)
  {
    TOP_TOOLBAR.ClickItem("&Data Analysis|Data Analysis Views|&Continuous Recording")
    Log.Message("Clicked on Continuous Recording Favorites")
    return true
  }
  else
  {
    Log.Message("ToolBar dock is not visible")
    return false
  }
}