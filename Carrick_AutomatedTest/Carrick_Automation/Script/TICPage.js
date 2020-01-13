//USEUNIT CommonMethod
//USEUNIT DataRetrievalPage

//Variables
var StartDateTime = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2;
var DateTimePicker = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2.TimeInterval.TimeIntervalControl.UserControlBase_Fill_Panel.TICtplInnerMostLayout1.TICdtpStartTime
var Edtbx_EndDateTime = Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow2.TimeInterval.TimeIntervalControl.UserControlBase_Fill_Panel.TICtplInnerMostLayout2.TICdtpEndTime
//This function is used to set the CurrentDateTime for the Device
function SetDeviceDateTime(NewDateTime)
{
  if (StartDateTime.VisibleOnScreen) 
  {
    DateTimePicker.wDate=aqDateTime.SetDateElements(aqDateTime.GetYear(NewDateTime),aqDateTime.GetMonth(NewDateTime),aqDateTime.GetDay(NewDateTime))
    Log.Message("Start Date time is set for one month ahead as per the Current date time of device")
    if(StartDateTime.TimeInterval.TimeIntervalControl.WinFormsObject("_UserControlBase_Toolbars_Dock_Area_Top").wItems.Item(0).Items.Item("Synchronizes End Date Time to Current Date Time").State =="0") //0 means unchecked
    {
      EndDateTime=StartDateTime.TimeInterval.TimeIntervalControl.WinFormsObject("_UserControlBase_Toolbars_Dock_Area_Top").wItems.Item(0).Items.Item("Synchronizes End Date Time to Current Date Time").Click()
    }    
    Log.Message("End Date time is set for Current date time of PC")
  }
  else
  {
    CommonMethod.RibbonToolbar.ClickItem("&View|[0]|&Time Interval");
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

//This function is used to Set the EndDate Time in TIC
function SetTICEndDateTime(DateTime,Monthoffset,Daysoffset)
{
  if(Edtbx_EndDateTime.Exists)
  {
    Log.Message("Time Interval Pane is visible on screen")
    if(DateTime!=null && Monthoffset!=null && Daysoffset!=null)
    {
      var DateAfterMonthOffset = aqDateTime.AddMonths(DateTime,Monthoffset)    
      var DateAfterDayOffset = aqDateTime.AddDays(DateAfterMonthOffset,Daysoffset)      
      Edtbx_EndDateTime.wDate=aqDateTime.SetDateElements(aqDateTime.GetYear(DateAfterDayOffset), aqDateTime.GetMonth(DateAfterDayOffset), aqDateTime.GetDay(DateAfterDayOffset))
    }
    else
    {
      Log.Message("Value of one of the argument is null")
      return null
    }
  }
  else
  {
    Log.Message("Time Interval pane is not visible on Screen")
    return null
  }
}

//This function is used to get the EndDateTime in TIC
function GetTICEndDateTime()
{
    if(Edtbx_EndDateTime.Exists)
  {
    Log.Message("Time Interval Pane is visible on screen") 
    return Edtbx_EndDateTime.wDate
  }
  else
  {
    Log.Message("Time Interval pane is not visible on Screen")
    return null
  }
}

//This function is used to set time for start date and time
function setStartDateTimeInTimeInterval()
{
  var currentDateTime = aqDateTime.Now()
  var updatedPQ10MinDateTime = aqDateTime.AddMinutes(currentDateTime, 30)
  if(StartDateTime.Visible)
  {
    DateTimePicker.set_Value(updatedPQ10MinDateTime)
    Log.Message("Set start date time for 30 min early")
    return true
  }
  else
  {
    Log.Message("Time Interval window is not visible") 
    return false
  }
  
}
//This function is used to set End date time
function setEndDateTimeInTimeInterval()
{
  var currentDateTimeEnd = aqDateTime.Now()
  if(StartDateTime.Visible)
  {
    Edtbx_EndDateTime.set_Value(currentDateTimeEnd)
    Log.Message("Set end date time to"+ currentDateTimeEnd)
    return true
  }
  else
  {
    Log.Message("Time Interval window is not visible") 
    return false
  }
}
