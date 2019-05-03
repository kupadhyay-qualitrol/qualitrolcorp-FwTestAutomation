/*This file contain methods related to the iq-plus 
*common functionality which can be accessed by other TestScripts and are generic. 
*/

//Global Variables
var RibbonToolbar=Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top
var Activitylog = Aliases.iQ_Plus.ShellForm.windowDockingArea1.dockableWindow3.ActivityLog.ActivityMonitor.ACTYLOGtxtLog
var Edtbx_Username = Aliases.iQ_Plus.UserLogin.USRLOGINtxtUserName

//This method launches iq+ plus application from installed folder path.
function Launch_iQ_Plus()
{
  try 
  {
    //Before launching iQ+ .Terminate if any instance of iQ+ is running.
    Terminate_iQ_Plus()
    //Before launching iQ+ server.Terminate if any instance of iQ+ server is running.
    Terminate_iQ_Plus_Server()
  
    //Launch iq+ plus application.
    if(TestedApps.iQ_Plus.Run())
    {
      do
      {
        aqUtils.Delay(2000)
      }
      while (!Edtbx_Username.Exists)
    
      do
      {
        aqUtils.Delay(2000)
      }
      while (!Edtbx_Username.Enabled)
    
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

//This method closes iq+ plus application running instance.
function Close_iQ_Plus()
{
  try 
  {
    //Close iq+ plus application.
    if(TestedApps.iQ_Plus.Terminate())
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

//DataDriver to fetch data from excel sheet
function ReadDataFromExcel(FileName,DataHead,SheetName=null,Iteration=null)
{
    var Excel, s;
    var returnValue
    Excel = Sys.OleObject("Excel.Application")
    // Wait until Excel starts
    Excel.Visible = false
    Excel.Workbooks.Open(FileName)
    if(SheetName!=null)
    {
      var WorkSheetCount = Excel.ActiveWorkbook.Worksheets.Count
      for(let IterateSheets=0;IterateSheets<WorkSheetCount;IterateSheets++)
      {
        if(SheetName== Excel.ActiveWorkbook.Worksheets.Item(IterateSheets+1).Name)
        {
          Excel.ActiveWorkbook.Worksheets.Item(IterateSheets+1).Select(true)        
          break
        }
      }
    }
    ColumnCnt= Excel.ActiveCell.CurrentRegion.Columns.Count
    var j=-1
    for (let i = 1;i<=ColumnCnt; i++)
    {
      if(Excel.ActiveSheet.Cells.Item(1,i).Value2==DataHead)
      {
        j=i
        break
      }
    }
    if(Iteration==null)
    {
      returnValue = Excel.ActiveSheet.Cells.Item((Project.TestItems.Current.Iteration+1),j).Value2
    }
    else
    {
      returnValue = Excel.ActiveSheet.Cells.Item((Iteration+2),j).Value2
    }
    
    Excel.ActiveWorkbook.Close()
    Excel =null
    return returnValue;
}

//Method to check progress from activity log
function CheckActivityLog(logmessage)
{
 do
 {
 aqUtils.Delay(2000)
 }
 while (aqString.FindLast(Activitylog.Text,logmessage)==-1)  
}

//Terminate iQ+ Client
function Terminate_iQ_Plus()
{
  //Check whether the iq+ is running or not?
  var iQ_process=Sys.WaitProcess("iQ-Plus")
  
  if (iQ_process.Exists)
  {
    iQ_process.Terminate() 
    Log.Message("Terminated iQ+ Client Application")
  }  
}

//Terminate iQ+ Server
function Terminate_iQ_Plus_Server()
{
  //Check whether the iq+ Server is running or not?
  var iQ_Server_process=Sys.WaitProcess("iQ-PlusServer")
  
  if (iQ_Server_process.Exists)
  {
    iQ_Server_process.Terminate() 
    Log.Message("Terminated iQ+ Server Application") 
  }  
}

//LaunchApplication from Wshell
function LaunchApplication(PathWithFileName)
{
  WshShell.Run(PathWithFileName)
}

//LastModified File
function LastModifiedFile(FilePath)
{
  //TODO This method to be optimised using javascript query as per review comment 
  var FolderName = FilePath

  var FolderInfo = aqFileSystem.GetFolderInfo(FolderName)
  
  if(FolderInfo.Files!=null)
  {
    var Num = FolderInfo.Files.Count

    Log.Message("The folder contains " + Num + " files.")
  
    var FileDateModified = []
    var ExecutableFile=[]
    var temp
    var tempExecutableFile
  
    for (var i=0; i < Num; i++)
    {
        var FileDate = FolderInfo.Files.Item(i).DateLastModified
        var strFileDate = aqConvert.DateTimeToStr(FolderInfo.Files.Item(i).DateLastModified)

        if(i==0)
        {
          temp= FileDate
          tempExecutableFile=FolderInfo.Files.Item(i).Name          
        }
        else
        {            
          if(aqDateTime.Compare(temp,FileDate)==-1)
          {
            temp= FileDate
            tempExecutableFile=FolderInfo.Files.Item(i).Name 
          }
        }
    }
    Log.Message("Latest Build in the folder is:- "+ tempExecutableFile)
    return tempExecutableFile
  }
  else
  {
    Log.Message("Folder is Empty")  
    return null
  }
}

function CreateDirectory(DirectoryName)
{
 if (aqFileSystem.Exists(DirectoryName))
 {
  Log.Message("Directory Exists with name:- "+DirectoryName)
  return DirectoryName
 }
 else
 {
   Log.Message("Directory doesn't Exists with name:- "+DirectoryName)
   Log.Message("Creating Directory with name:- "+DirectoryName)
   var tempresult =aqFileSystem.CreateFolder(DirectoryName)
   if(tempresult==0)
   {
    Log.Message("Folder Created successfully")
    return DirectoryName
   }
   else
   {
    Log.Message("Unable to create the folder.Error code is:- "+tempresult+" & error message is :- "+aqUtils.SysErrorMessage(tempresult)) 
    return null
   }
 }
}

/*This method can be used to add any node to the xml file.
function AddSectiontoXML()
{
  var FileSection=Storages.XML("")  
  var Section= FileSection.GetSubSection("iQ_PlusFilePath")
  Section.SetOption("BuildServerPath","\\\\qbeleng11\\Builds\\4.15\\")
  FileSection.SaveAs(Project.ConfigPath+"Config.xml")
  
}*/
//This method is used to read data from the xml file

function ReadXml(NodeName,Variable,filenamewithpath)
{
  if(NodeName!=null && Variable!=null)
  {
    var FileSection=Storages.XML(filenamewithpath)
    Section= FileSection.GetSubSection(NodeName)
    return aqConvert.VarToStr(Section.GetOption(Variable,0))
  }
  else
  {
    Log.Message("NodeName/Variable argument is null.")
    return null
  }
}

//This method is used to convert time from hh:mm:ss.xxx  to milliseconds
function ConvertTimeIntoms(Timeinhhmmssms)
{
  var DateTimeFrmt
  if(Timeinhhmmssms!=null)
  {
    var Time= Timeinhhmmssms.OleValue.split(".")    
    if(Time.length > 1)
    {
      var Hours= aqDateTime.GetHours(Time[0])
      var Minutes= aqDateTime.GetMinutes(Time[0])
      var Seconds= aqDateTime.GetSeconds(Time[0])
      var Milliseconds= Time[1]
   
      TimeInms = (Hours*60*60*1000)+(Minutes*60*1000)+(Seconds*1000)+ aqConvert.StrToInt64(Milliseconds)
      return TimeInms   
    }
    else
    {
      Log.Message("Input Time is not in correct format")
      return null
    }
  }
  else
  {
    Log.Message("Input Time is null")
    return null
  }
}

//This method is used to convert DateTime from to milliseconds with reference to 01-01-2000 00:00:00.000
function ConvertDateTimeIntomsFrom2000(DateTimeinddmmyyyyhhmmssms)
{
  var DateTimeFrmt
  if(DateTimeinddmmyyyyhhmmssms!=null)
  {
    var DateTime= DateTimeinddmmyyyyhhmmssms.split(".")
    if(DateTime.length > 1)
    {
      var TimeinmsFrom2000 = aqDateTime.TimeInterval(DateTime[0],"01/01/2000 00:00:00")
    
      var Day = aqDateTime.GetDay(TimeinmsFrom2000)    
      var Hours= aqDateTime.GetHours(TimeinmsFrom2000)
      var Minutes= aqDateTime.GetMinutes(TimeinmsFrom2000)
      var Seconds= aqDateTime.GetSeconds(TimeinmsFrom2000)
      var Milliseconds= DateTime[1]
    
      TimeInms = (Day*24*60*60*1000)+(Hours*60*60*1000)+(Minutes*60*1000)+(Seconds*1000)+ aqConvert.StrToInt64(Milliseconds)
      return TimeInms 
    }
    else
    {
      Log.Message("Input DateTime is not in correct format")
      return null
    }  
  }
  else
  {
    Log.Message("Input DateTime is null")
    return null
  }
}

//This function is used to get the Username of the System
function GetSystemUsername()
{
  var NetworkObject = Sys.OleObject("WScript.Network")
  
  if (NetworkObject!=null)
  {
    Log.Message("System Username is :-"+ NetworkObject.UserName)  
    return NetworkObject.UserName  
  }
  else
  {
    Log.Message("Unable to get Network Object")
    return null
  }
}

//This function is used to kill the process if it exists
function KillProcess(Process)
{ 
 if(Process!=null)
 {
   while(Sys.WaitProcess(Process,5000).Exists)
   {
     Sys.WaitProcess(Process,5000).Terminate()
     aqUtils.Delay(1000)
   }
   return true
 }
 else
 {
   Log.Message("Process input is empty")
   return false
 }
}

//This function is used to check if application is running/not
function IsExist(AppProcess)
{
  if(AppProcess!=null)
  {
    var Application=Sys.WaitProcess(AppProcess)
    if (Application.Exists)
    {
      Log.Message("Application is running")
      return true   
    }
    else
    {
      Log.Message("Application is not running") 
      return false
    }
  }
  else
  {
    Log.Message("Input is null")
    return false
  }
}

//This function is used to set the PC timezone
function SetPCTimeZone(PCTimeZone)
{
  var objShell = Sys.OleObject("Wscript.Shell")
  objShell.Exec("tzutil.exe /s " +aqString.Quote(PCTimeZone))
}

function GetDeviceStatusOnPing(deviceIPAddress)
{
  var IPAdd = dotNET.System_Net.IPAddress.Parse(deviceIPAddress)
  var Pinger = new dotNET.System_Net_NetworkInformation.Ping.zctor()
  var PingReply=Pinger.Send(IPAdd)
  return PingReply.Status.OleValue
}

//This function is used to get the installed version of iQ+
function GetiQPlusInstallInfo()
{
  var versionInfo =null
  var command ="wmic product get Name,Version"
  var oShell = getActiveXObject("WScript.Shell") // Or oShell = WshShell
  //var oExec = oShell.Exec("powershell -command Get-Process");
  var oExec = oShell.Exec("powershell -command "+command)
  oExec.StdIn.Close(); // Close standard input before reading output

  // Get PowerShell output
  var strOutput = oExec.StdOut.ReadAll()
  // Trim leading and trailing empty lines
  strOutput = aqString.Trim(strOutput, aqString.stAll)

  // Post PowerShell output to the test log line by line
  aqString.ListSeparator = "\r\n";
  for (var indexProgram = 0; indexProgram < aqString.GetListLength(strOutput); indexProgram++)
  {
    if(aqString.Find(aqString.GetListItem(strOutput,indexProgram),"Qualitrol")==-1)
    {
    
    }
    else
    {
      versionInfo = aqString.GetListItem(strOutput, indexProgram) 
      Log.Message(versionInfo)
      break
    }    
  }
  return versionInfo
}