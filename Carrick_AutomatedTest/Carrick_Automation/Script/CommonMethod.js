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