//USEUNIT CommonMethod
//USEUNIT SessionLogPage

/*This file contains methods and objects related to PDP Page*/
var PDPContainerWorkspace=Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace
var ultraGrid = Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList.ugBaseGrid;
var EventsList = Aliases.iQ_Plus.ShellForm.windowDockingArea3.dockableWindow1.PDPWorkspace.PDPContainer.PDPCTRtsctrPDPToolsContainer.ToolStripContentPanel.PDPContainerWorkspace.EventsList


//This function is used to verify the Downloaded record in PDP
function VerifyDownloadedRecord()
{
  var REC
  if (PDPContainerWorkspace.Exists)
  {
    Log.Message("PDP window is visible")
    //22 is the Item number for column Rec.#(DFR) in PDP view
    REC=EventsList.ugBaseGrid.ActiveRow.Cells.Item(22).get_Value()
    Log.Message("Rec # is "+REC)    
    return REC
  }
  else
  {
    Log.Message("PDP window is not visible")
    return null
  }
}

//This function is used to get the Record Duration
function GetRecordDuration()
{
  if (PDPContainerWorkspace.Exists)
  {
    Log.Message("PDP window is visible")
    var PDPcolumn
    var DurationColumnNo
    //Identify the column number for Record Duration
    for(PDPcolumn=0;PDPcolumn< ultraGrid.wColumnCount; PDPcolumn++)
    {
      if(ultraGrid.wColumn(PDPcolumn)=="Duration (hrs:min:sec.ms)")
      {
        DurationColumnNo = PDPcolumn
        break
      }
    }
    if(DurationColumnNo!=null)
    {
      Log.Message("Cloumn No. for Duration in PDP is :- "+DurationColumnNo)
    }
    else
    {
      Log.Message("Unable to find column no. for Duration in PDP pane.")
      return null
    }
    var Record_Duration=EventsList.ugBaseGrid.wValue(0,"Duration (hrs:min:sec.ms)")
    Log.Message("Record Duration is:- "+Record_Duration)    
    return Record_Duration
  }
  else
  {
    Log.Message("PDP window is not visible")
    return null
  }
}

//This method is used to get the Record Start Date & Time
function GetRecordStartDateTime()
{
  if (PDPContainerWorkspace.Exists)
  {
    Log.Message("PDP window is visible")
    var PDPcolumn
    var Record_StartDate_Time_ColumnNo=null
    
    //Identify the column number for Record StartTime
    for(PDPcolumn=0;PDPcolumn< ultraGrid.wColumnCount; PDPcolumn++)
    {
      if(ultraGrid.wColumn(PDPcolumn)=="Start Date & Time")
      {
        Record_StartDate_Time_ColumnNo = PDPcolumn
        break
      }
    }
    if(Record_StartDate_Time_ColumnNo!=null)
    {
      Log.Message("Cloumn No. for Start Date Time in PDP is :- "+Record_StartDate_Time_ColumnNo)
    }
    else
    {
      Log.Message("Unable to find column no. for Record_StartDate_Time in PDP pane.")
      return null
    }
    
    var Record_StartDateTime=EventsList.ugBaseGrid.wValue(0,"Start Date & Time")
    return CommonMethod.ConvertDateTimeIntoms(Record_StartDateTime.Day+"/"+Record_StartDateTime.Month+"/"+Record_StartDateTime.Year+" "+Record_StartDateTime.Hour+":"+Record_StartDateTime.Minute+":"+Record_StartDateTime.Second+"."+Record_StartDateTime.Millisecond)
    Log.Message("Record Start Date Time is:- "+aqConvert.DateTimeToStr(Record_StartDateTime))    
  }
  else
  {
    Log.Message("PDP window is not visible")
    return null
  }
}

//This method is used to get Record End Date & Time
function GetRecordEndDateTime()
{
  if (PDPContainerWorkspace.Exists)
  {
    Log.Message("PDP window is visible")
    var PDPcolumn
    var Record_EndDate_Time_ColumnNo=null
    
    //Identify the column number for Record EndDateTime
    for(PDPcolumn=0;PDPcolumn< ultraGrid.wColumnCount; PDPcolumn++)
    {
      if(ultraGrid.wColumn(PDPcolumn)=="End Date & Time")
      {
        Record_EndDate_Time_ColumnNo = PDPcolumn
        break
      }
    }
    if(Record_EndDate_Time_ColumnNo!=null)
    {
      Log.Message("Cloumn No. for End Date Time in PDP is :- "+Record_EndDate_Time_ColumnNo)
    }
    else
    {
      Log.Message("Unable to find column no. for Record_EndDate_Time in PDP pane.")
      return null
    }
    
    var Record_EndDate_Time=EventsList.ugBaseGrid.wValue(0, "End Date & Time")
    return CommonMethod.ConvertDateTimeIntoms(Record_EndDate_Time.Day+"/"+Record_EndDate_Time.Month+"/"+Record_EndDate_Time.Year+" "+Record_EndDate_Time.Hour+":"+Record_EndDate_Time.Minute+":"+Record_EndDate_Time.Second+"."+Record_EndDate_Time.Millisecond)
    Log.Message("Record End Date Time is:- "+aqConvert.DateTimeToStr(Record_EndDate_Time))    
  }
  else
  {
    Log.Message("PDP window is not visible")
    return null
  }
}

//This method is used to get Record GPS lock status
function GetTimeQualityStatus()
{
  if (PDPContainerWorkspace.Exists)
  {
    Log.Message("PDP window is visible")
    var PDPcolumn
    var Record_TimeQuality_ColumnNo=null
    
    //Identify the column number for Record TimeQuality
    for(PDPcolumn=0;PDPcolumn< ultraGrid.wColumnCount; PDPcolumn++)
    {
      if(ultraGrid.wColumn(PDPcolumn)=="Time Quality")
      {
        Record_TimeQuality_ColumnNo = PDPcolumn
        break
      }
    }
    if(Record_TimeQuality_ColumnNo!=null)
    {
      Log.Message("Cloumn No. for Time Quality in PDP is :- "+Record_TimeQuality_ColumnNo)
    }
    else
    {
      Log.Message("Unable to find column no. for Time Quality in PDP pane.")
      return null
    }
    
    var Record_TimeQuality=EventsList.ugBaseGrid.wValue(0,"Time Quality")
    Log.Message("Record Time Quality from Device status is:- "+Record_TimeQuality)    
    return Record_TimeQuality
  }
  else
  {
    Log.Message("PDP window is not visible")
    return null
  }
}

//This method is used to get Trigger Date & Time
function GetRecordTriggerDateTime()
{
  if (PDPContainerWorkspace.Exists)
  {
    Log.Message("PDP window is visible")
    var PDPcolumn
    var Record_TriggerDate_Time_ColumnNo=null
    
    //Identify the column number for Record EndDateTime
    for(PDPcolumn=0;PDPcolumn< ultraGrid.wColumnCount; PDPcolumn++)
    {
      if(ultraGrid.wColumn(PDPcolumn)=="Trigger Date & Time")
      {
        Record_TriggerDate_Time_ColumnNo = PDPcolumn
        break
      }
    }
    if(Record_TriggerDate_Time_ColumnNo!=null)
    {
      Log.Message("Cloumn No. for Trigger Date Time in PDP is :- "+Record_TriggerDate_Time_ColumnNo)
    }
    else
    {
      Log.Message("Unable to find column no. for Record_TriggerDate_Time in PDP pane.")
      return null
    }
    var Record_TriggerDate_Time=EventsList.ugBaseGrid.wValue(0, "Trigger Date & Time")
    return CommonMethod.ConvertDateTimeIntoms(Record_TriggerDate_Time.Day+"/"+Record_TriggerDate_Time.Month+"/"+Record_TriggerDate_Time.Year+" "+Record_TriggerDate_Time.Hour+":"+Record_TriggerDate_Time.Minute+":"+Record_TriggerDate_Time.Second+"."+Record_TriggerDate_Time.Millisecond)
    Log.Message("Record Trigger Date Time is:- "+Record_TriggerDate_Time+"_"+Record_TriggerDate_Timems)    
  }
  else
  {
    Log.Message("PDP window is not visible")
    return null
  }
}