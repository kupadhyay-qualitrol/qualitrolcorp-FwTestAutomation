//USEUNIT DeviceManagementPage

//Variables
var RibbonToolbar=Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top
var StartDateTime = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2;
var DateTimePicker = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2.TimeInterval.TimeIntervalControl.UserControlBase_Fill_Panel.TICtplInnerMostLayout1.TICdtpStartTime
var SetDateTime;
var NewDateTime;



//This function is used to set the CurrentDateTime for the Device
function SetDeviceDateTime()
{
  if (StartDateTime.VisibleOnScreen) 
  {
    DateTimePicker.wDate=aqDateTime.SetDateElements(aqDateTime.GetYear(NewDateTime),aqDateTime.GetMonth(NewDateTime),aqDateTime.GetDay(NewDateTime))
    Log.Message("Start Date time is set for one month ahead as per the Current date time of device")
    
    EndDateTime=StartDateTime.TimeInterval.TimeIntervalControl.WinFormsObject("_UserControlBase_Toolbars_Dock_Area_Top").wItems.Item(0).Items.Item("Synchronizes End Date Time to Current Date Time").Click()
    Log.Message("End Date time is set for Current date time of PC")
  }
  else
  {
    RibbonToolbar.ClickItem("&View|[0]|&Time Interval");
    Log.Message("Time interval window open")
    if (StartDateTime.VisibleOnScreen)
    {
      SetDeviceDateTime();
    }
    else
    {
      return false;
      Log.Message("System not allowed to open time interval window")
    }
  }
}
