/*This file contain methods related to the iq-plus 
*common functionality which can be accessed by other TestScripts and are generic. 
*/

//Global Variables
var RibbonToolbar=Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top
var Activitylog = Aliases.iQ_Plus.ShellForm.windowDockingArea1.dockableWindow3.ActivityLog.ActivityMonitor.ACTYLOGtxtLog
//
function AssertIsTrue(Expected,Actual,LogMessage)
{
 if(Expected=Actual)
 {
   Log.Message(LogMessage)
 }
 else
 {
   Log.Error("Results didn't match")
 }
}

//This method launches iq+ plus application from installed folder path.
function Launch_iQ_Plus()
{
  try 
  {
    //Launch iq+ plus application.
    if(TestedApps.iQ_Plus.Run())
    {
      Log.Message("Application launched successfully")
    }
  }
  catch(ex)
  {
    //Post the message to log file  
    Log.Error(ex.message)
  }
}

//This method closes iq+ plus application running instance.
function Close_iQ_Plus()
{
  try 
  {
    //Launch iq+ plus application.
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
function ReadDataFromExcel(FileName,DataHead)
{
    var Excel, s;
    Excel = Sys.OleObject("Excel.Application")
    Delay (3000)
    // Wait until Excel starts
    Excel.Visible = false
    Excel.Workbooks.Open(FileName)
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
    return Excel.ActiveSheet.Cells.Item((Project.TestItems.Current.Iteration+1),j).Value2
}

//Method to check progress from activity log
function CheckActivityLog(logmessage)
{
 do
 {
 aqUtils.Delay(2000)
 }
 while (aqString.FindLast(Activitylog.Text.OleValue,logmessage)==-1);
  
}

//Terminate iq+ Client
function Terminate_iQ_Plus()
{
  //Check whether the iq+ is running or not?
  var iQ_process=Sys.WaitProcess("iQ-Plus",30)
  
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
  var iQ_Server_process=Sys.WaitProcess("iQ-PlusServer",30)
  
  if (iQ_Server_process.Exists)
  {
    iQ_Server_process.Terminate() 
    Log.Message("Terminated iQ+ Server Application") 
  }  
}

//Install iq+
function Install_iq_plus()
{
  MapNetworkDrive("C:\\My_Documents\\Projects\\4.15_Sprint_1\\Des1")
  
}

//Map Networkdrive
function MapNetworkDrive(DriveName)
{
 // MapNetworkDrive(DriveName+"\\"+Version)
  var iQ_LatestBuild_File =LastModifiedFile(DriveName)
 // aqFile.Copy(DriveName+ "\\" + iQ_LatestBuild_File, Project.Path+"Builds\\",true) 
  WshShell.Run("C:\\My_Documents\\Projects\\4.15_Sprint_1\\Des1\\"+iQ_LatestBuild_File)
}

//LastModified File
function LastModifiedFile(FilePath)
{

  var FolderName = FilePath

  var FolderInfo = aqFileSystem.GetFolderInfo(FolderName)

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