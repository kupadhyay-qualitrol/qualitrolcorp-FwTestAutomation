/* This script contains methods and objects related to RMS Validation Exe*/

var Edtbx_FilePath = Aliases.RMSDataValidation.RMSDataValidation.FilePathTextBox
var Edtbx_Voltage = Aliases.RMSDataValidation.RMSDataValidation.VoltageTextBox
var Edtbx_Current = Aliases.RMSDataValidation.RMSDataValidation.CurrentTextBox
var Btn_Start = Aliases.RMSDataValidation.RMSDataValidation.StartButton
var lbl_Status = Aliases.RMSDataValidation.RMSDataValidation.ValidationResultLabel
var Edtbx_VoltageTolerance= Aliases.RMSDataValidation.RMSDataValidation.VoltageToleranceTextBox
var Edtbx_CurrentTolerance= Aliases.RMSDataValidation.RMSDataValidation.CurrentToleranceTextBox
var EDTBX_TYPE = Aliases.RMSDataValidation.RMSDataValidation.Type
var LBL_MESSAGE = Aliases.RMSDataValidation.RMSDataValidation.ErrorMessage
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

//This fucntion is used to set validation type in RMSValidation exe
function setType(validationType)
{
  if(validationType!=null)
  {
    EDTBX_TYPE.SetText(validationType)
    Log.Message("Set the validation type to :-"+validationType)
    return true
  }
  else
  {
   Log.Message("validation type is null")
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

//This function is used to set Voltage Tolerance in RMSValidation exe
function SetVoltageTolerance(VoltageTolerance)
{
  if(VoltageTolerance!=null)
  {
    Edtbx_VoltageTolerance.SetText(VoltageTolerance)
    Log.Message("Set the Voltage Tolerance to :- "+VoltageTolerance)
    return true
  }
  else
  {
    Log.Message("Voltage Tolerance is null")
    return false
  }
}

//This function is used to set Current Tolerance in RMSValidation exe
function SetCurrentTolerance(CurrentTolerance)
{
  if(CurrentTolerance!=null)
  {
    Edtbx_CurrentTolerance.SetText(CurrentTolerance)
    Log.Message("Set the Current Tolerance to :- "+CurrentTolerance)
    return true
  }
  else
  {
    Log.Message("Current Tolerance is null")
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

function ValidateRMSData(RMSDataFileNameWithPath,RMSInjectedVolatge,RMSInjectedCurrent,VoltagTolerance,CurrentTolerance,validationType)
{
 if(RMSDataFileNameWithPath!=null&& RMSInjectedVolatge!=null && RMSInjectedCurrent!=null && VoltagTolerance!=null && CurrentTolerance!=null)
 {
   SetFilePath(RMSDataFileNameWithPath)
   SetRMSVoltage(RMSInjectedVolatge)
   SetRMSCurrent(RMSInjectedCurrent)
   SetVoltageTolerance(VoltagTolerance)
   SetCurrentTolerance(CurrentTolerance)
   setType(validationType)
   ClickStart()
   do
   {
    aqUtils.Delay(2000) 
   }
   while(!Btn_Start.Enabled)
   var status =lbl_Status.Text.OleValue
   var message = LBL_MESSAGE.Text.OleValue
   Log.Message("TC: Test to check PQ data validation is " +status+ " and message is " +message)
   CloseRMSValidationApplication()
   return status
 }
 else
 {
   Log.Message("Arguments is/are null")
   CloseRMSValidationApplication()
   return null
 }
}

function LaunchRMSValidationApplication()
{
  //Launch RMSValidation application.
  TestedApps.RMSDataValidation.Path= Project.ConfigPath+"\\ThirdParty\\"
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

function CloseRMSValidationApplication()
{
  var proc =Sys.WaitProcess("RMSDataValidation",20000)
  
  if(proc.Exists)
  {
    proc.Terminate()
    Log.Message("RMSValidation application terminated successfully")
  }
  else
  {
    Log.Message("RMSValidation application is not running")
  }
}