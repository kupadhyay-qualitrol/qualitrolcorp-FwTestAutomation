/*
This page contains objects & method related to IEC browser.
*/
//USEUNIT CommonMethod
//USEUNIT IECBrowserCommonMethod
//USEUNIT AssertClass
//USEUNIT IECBrowser_Methods


var IECBrowser = Aliases.Iec_Browser
var Main_Menu = Aliases.Iec_Browser.mainForm
var Toolbar = Aliases.Iec_Browser.mainForm.toolBar1
var Dlg_Connect_Device = Aliases.Iec_Browser.FormConnect
var Edt_Bx_IP = Aliases.Iec_Browser.FormConnect.comboBoxIp
var Btn_Connect = Aliases.Iec_Browser.FormConnect.buttonConnect
var Device_Tree_view = Aliases.Iec_Browser.mainForm.panel2.gridControl1
var Data_view = Aliases.Iec_Browser.mainForm.panel1.dataGrid1
var Trace_Log = Aliases.Iec_Browser.mainForm.panel1.textBox_Output
var Dlg_BrowseFolder = Aliases.Iec_Browser.dlgBrowseForFolder.SHBrowseForFolderShellNameSpaceControl.TreeView
var Btn_ok_select_folder = Aliases.Iec_Browser.dlgBrowseForFolder.btnOK


//This function used for connecting device with IECBrowser page.
function ConnectDevice()
{
  AssertClass.IsTrue(IECBrowserCommonMethod.LaunchIECBrowser())
  if(IECBrowser.Enabled)
  {
    Toolbar.ClickItem("Connect")
    Log.Message("Connect button clicked")
    return true
  }
  else
  {
    Log.Message("Unable to Click on Connect Button")
    return false
  }
}
//This function used for seeting IP address in IP address text box of Connect to device form page.
function SetIPAddress(DeviceIPAdd)
{
  if(Dlg_Connect_Device.Enabled)
  {
    Dlg_Connect_Device.SetFocus()
    Edt_Bx_IP.SetText(DeviceIPAdd)
    Log.Message("IP address set for the device")
    return true
  }
  else
  {
    Log.Message("Unable to set IP address")
    return false
  }
}
//This function used for clicking on connect button of Connect to device dialouge popup
function ClickOnConnectButton()
{
  if(Dlg_Connect_Device.Enabled)
  {
    Btn_Connect.ClickButton()
    Log.Message("Connect button clicked")
    return true
  }
  else
  {
    Log.Message("Unable to Click on Connect Button")
    return false
  }
}
//This function used to select the files folder and download the CID file
function SelectFilesInDeviceTree()
{
  if(Device_Tree_view.Enabled)
  {
   Files_row = Device_Tree_view.FindRow(1,"Files") 
   Device_Tree_view.ClickCell(Files_row,1)
   Log.Message("Clicked on Files under Device tree grid1")
   Log.Picture(Device_Tree_view, "Snapshot for verififcation of connection with device")
   return true
  }
  else
  {
    return false
    Log.Message("Unable to click on Files under Device tree")
  }
}
//This function used to download the CID file
function DownloadCIDFile()
{
    var SysUserName = CommonMethod.GetSystemUsername()
    var CIDFilesPath ="C:\\Users\\"+SysUserName+"\\Desktop\\CIDFiles\\"
    aqFileSystem.CreateFolder(CIDFilesPath)
    
  if(Data_view.Exists)
  {
    Data_row = Data_view.FindRow("Name","*.cid")
    Data_view.ClickCell(Data_row,1)
    Log.Message("Selected CID file")
    Data_view.ClickCell(Data_row,"GetFile")
    Log.Message("Checked CID file")
    Data_view.ClickCellR(Data_row,"GetFile")
    Data_view.PopupMenu.Click("Get tagged Files...")
    Dlg_BrowseFolder.ClickItem("|Desktop|")
    Dlg_BrowseFolder.ClickItem("|Desktop|CIDFiles")
    Btn_ok_select_folder.ClickButton()
    Log.Message("Selected the folder and clicked on Ok button")
    return true
  }
  else
  {
    return false
    Log.Message("Not able to download the CID file")
  }
}
//This function used for Disconnecting device from IECBrowser page.
function DisconnectDevice()
{
  if(IECBrowser.Enabled)
  {
    Toolbar.ClickItem("Disconnect")
    IECBrowserCommonMethod.CheckTraceLog("Disconnecting from:")
    Log.Message("Disconnect button clicked")
    return true
  }
  else
  {
    Log.Message("Unable to Click on Disconnect Button")
    return false
  }
}
//This function is used to clear trace log
function CleareTraceLog()
{
  if(IECBrowser.Enabled)
  {
    Main_Menu.MainMenu.Click("Edit|Del Trace")
    Log.Message("Cleared Trace log")
    return true
  }
  else
  {
    Log.Message("Unable to Clear trace log")
    return false
  }  
}
