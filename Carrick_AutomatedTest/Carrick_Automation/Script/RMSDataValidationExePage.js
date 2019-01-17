/* This script contains methods and objects related to RMS Validation Exe*/

var Edtbx_FilePath = Aliases.RMSDataValidation.Form1.FilePathTextBox
var Edtbx_Voltage = Aliases.RMSDataValidation.Form1.VoltageTextBox
var Edtbx_Current = Aliases.RMSDataValidation.Form1.CurrentTextBox
var Btn_Start = Aliases.RMSDataValidation.Form1.StartButton

//This function is used to set Filepath in RMSValidation exe
function SetFilePath(FilePathWithName)
{
  if(FilePathWithName!=null)
  {
    Edtbx_FilePath.SetText(FilePathWithName)
    Log.Message("Set the File Path to :- "+FilePathWithName)
    return true
  }
  else
  {
    Log.Message("FilePath is null")
    return false
  }
}

//This function is used to set Voltage in RMSValidation exe
function SetRMSVoltage(InjectedVoltage)
{
  if(InjectedVoltage!=null)
  {
    Edtbx_Voltage.SetText(InjectedVoltage)
    Log.Message("Set the Injected Voltage to :- "+InjectedVoltage)
    return true
  }
  else
  {
    Log.Message("Injected Voltage is null")
    return false
  }
}

//This function is used to set Current in RMSValidation exe
function SetRMSCurrent(InjectedCurrent)
{
  if(InjectedCurrent!=null)
  {
    Edtbx_Current.SetText(InjectedCurrent)
    Log.Message("Set the Injected Current to :- "+InjectedCurrent)
    return true
  }
  else
  {
    Log.Message("Injected Current is null")
    return false
  }
}

function ClickStart()
{
  if(Btn_Start.Enabled)
  {
    Btn_Start.ClickButton()
    Log.Message("Clicked on Start Button")
    return true
  }
  else
  {
    Log.Message("Start button on application is not enabled")
  
    return false
  }
}

function ValidateRMSData(RMSDataFileNameWithPath,RMSInjectedVolatge,RMSInjectedCurrent)
{
 if(RMSDataFileNameWithPath!=null&& RMSInjectedVolatge!=null && RMSInjectedCurrent!=null )
 {
   SetFilePath(RMSDataFileNameWithPath)
   SetRMSVoltage(RMSInjectedVolatge)
   SetRMSCurrent(RMSInjectedCurrent)
   ClickStart()
   do
   {
    aqUtils.Delay(2000) 
   }
   while(!Btn_Start.Enabled)
 }
 else
 {
   Log.Message("Arguments is/are null")
   return false
 }
}

function LaunchRMSValidationApplication()
{
  //Launch RMSValidation application.
  if(TestedApps.RMSDataValidation.Run())
  {
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Edtbx_FilePath.Exists)
    
    do
    {
      aqUtils.Delay(2000)
    }
    while (!Edtbx_FilePath.Enabled)
    
    Log.Message("Application launched successfully")
    return true
  }
  else
  {
    Log.Message("Unable to launch application")
    return false
  }
}