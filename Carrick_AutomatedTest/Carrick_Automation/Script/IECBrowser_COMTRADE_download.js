//USEUNIT CommonMethod

//COMMON FUNCTIONS - START
//Hardcoded Device IP - 10.75.58.51
//Create Folder C://AIP-74_Testing

//Hardcoded Device IP - 10.75.58.51 for Ed1 & 10.75.58.50 for Ed2

//Create Folder C://AIP-74_Testing//TestCaseName

var DataSheetName = Project.ConfigPath +"TestData\\IECBrowser_COMTRADE_download.xlsx"
var Ed1DeviceIP = ""
var Ed2DeviceIP = ""
var FilesToppology

function Download_Files(NoOfFiles, CFGOnly, DATOnly, TestCaseName,startRow, deviceIP)
{
    //Check whether IECBrowser is already running or not?
    var iecBrowser_process=Sys.WaitProcess("Iec_Browser")
  
    if (iecBrowser_process.Exists)
    {
      //iecBrowser_process.Terminate() 
      //Log.Message("Terminated IECBrowser Application")
      Log.Message("IECBrowser is already running")
      Log.Message("IECBrowser is already running.Starting another new instance")
      //TestedApps.Iec_Browser.Run()
    }
    else
    {
      Log.Message("Launching IECBrowser as its not already running")
      TestedApps.Iec_Browser.Run()
    }
  
    var treetopology
    var FilesToppology
    Aliases.Iec_Browser.mainForm.toolBar1.ClickItem("Connect")
    Aliases.Iec_Browser.FormConnect.comboBoxIp.ComboBoxChildNativeWindow.SetText("10.75.58.51")
    iecBrowser_process=Sys.WaitProcess("Iec_Browser")
    }
  
    var treetopology
    Aliases.Iec_Browser.mainForm.toolBar1.ClickItem("Connect")
    Aliases.Iec_Browser.FormConnect.comboBoxIp.ComboBoxChildNativeWindow.SetText(deviceIP)
    aqUtils.Delay(2000)
    Aliases.Iec_Browser.FormConnect.buttonConnect.ClickButton()
    aqUtils.Delay(5000)
    treetopology = Aliases.Iec_Browser.mainForm.panel2.gridControl1

    for (i = treetopology.GridCellsRange.Top; i <= treetopology.GridCellsRange.Bottom; i++) {
        Log.Message(treetopology.Item(i, 1).Text.OleValue)
        if (treetopology.Item(i, 1).Text.OleValue == "Files") {
            treetopology.ClickCell(i, "Name")
            break
        }
    }
    Log.Message("check");
    if (Aliases.Iec_Browser.mainForm.checkBoxRefresh.CheckState.OleValue == "Checked") {
        Aliases.Iec_Browser.mainForm.checkBoxRefresh.ClickButton()
    }
<<<<<<< HEAD
    FilesToppology = Aliases.Iec_Browser.mainForm.panel1.dataGrid1
=======
>>>>>>> refs/remotes/origin/master
    NoofChecks = 1;
    row = startRow;
    if (CFGOnly)
    {
        filefilter = ".cfg"
        usefilefilter = true;
    }
    else if(DATOnly)
    {
        filefilter = ".dat"
        usefilefilter = true;
    }
    else
    {
        usefilefilter = false;
    }
<<<<<<< HEAD
    while (NoofChecks <= NoOfFiles)
    {
        if ((aqString.Find(FilesToppology.Item(row, 0).OleValue, "COMTRADE")) != -1)
        {
            if (usefilefilter && (aqString.Find(FilesToppology.Item(row, 0).OleValue, filefilter)) != -1)
=======
    FilesToppology = Aliases.Iec_Browser.mainForm.panel1.dataGrid1
    i = 1;
    while (NoofChecks <= NoOfFiles)
    {
        if (row >= FilesToppology.wRowCount) //If we have reached the end of rows, restart from 1.
        {
          row = 1
          Log.Message("row exceeded Available RowCount. So,downloading files checked so far and Resetting row to 1");
          DownloadSelectedFiles(TestCaseName,NoofChecks)
          TestCaseName = TestCaseName + "_Reset_" + i; //As there is chance that same files would be downloaded again, changing the Output Folder name here
          i++;
        }
        
        if ((aqString.Find(FilesToppology.Item(row, 0).OleValue, "COMTRADE")) != -1)
        { 
            if (usefilefilter)
            {
              if(aqString.Find(FilesToppology.Item(row, 0).OleValue, filefilter) != -1)
              {
                  FilesToppology.ClickCell(row, "GetFile")
                  NoofChecks++;
              }
            }
            else
>>>>>>> refs/remotes/origin/master
            {
                FilesToppology.ClickCell(row, "GetFile")
                NoofChecks++;
            }
        }
        row++;
    }
<<<<<<< HEAD
=======
    DownloadSelectedFiles(TestCaseName,NoofChecks)
    
    //iecBrowser_process.Terminate() 
    //Log.Message("Terminated IECBrowser Application")
    
    return row;
}

function DownloadSelectedFiles(TestCaseName,NoofChecks)
{
>>>>>>> refs/remotes/origin/master
    aqFileSystem.CreateFolder("C:\\AIP-74_Testing\\"+ TestCaseName + "\\")
    
    FilesToppology.ClickCellR(NoofChecks, "GetFile")
    FilesToppology.PopupMenu.Click("Get tagged Files...")
<<<<<<< HEAD
    Aliases.Iec_Browser.dlgBrowseForFolder.SHBrowseForFolderShellNameSpaceControl.TreeView.ClickItem("|Desktop|This PC|OS (C:)|AIP-74_Testing|" + TestCaseName)
=======
    Aliases.Iec_Browser.dlgBrowseForFolder.SHBrowseForFolderShellNameSpaceControl.TreeView.ClickItem("|Desktop|Computer|OS (C:)|AIP-74_Testing|" + TestCaseName)
>>>>>>> refs/remotes/origin/master
    Aliases.Iec_Browser.dlgBrowseForFolder.btnOK.ClickButton()
    
    Log.Message(Aliases.Iec_Browser.mainForm.panel1.textBox_Output.Text)
    
    FilesToppology.ClickCellR(NoofChecks, "GetFile")
    FilesToppology.PopupMenu.Click("Refresh Table")
<<<<<<< HEAD
    return row;
}

function Download_10Chunks_Upto160_CFGOnly(TestCaseName)
=======
}

function Download_10Chunks_Upto160_CFGOnly(TestCaseName, deviceIP)
>>>>>>> refs/remotes/origin/master
{
    iChunkSize = 10;
    ifiles = 0;
    startRow = 1;
<<<<<<< HEAD
    while (ifiles < 30) {
        startRow = Download_Files(iChunkSize, true, false, TestCaseName + ifiles,startRow)
=======
    while (ifiles < 160) {
        startRow = Download_Files(iChunkSize, true, false, TestCaseName + "_" + ifiles,startRow,deviceIP)
>>>>>>> refs/remotes/origin/master
        ifiles = ifiles + iChunkSize;
    }
}

<<<<<<< HEAD
function Download_150Chunks_Upto600_CFGOnly(TestCaseName) {
=======
function Download_150Chunks_Upto600_CFGOnly(TestCaseName, deviceIP) {
>>>>>>> refs/remotes/origin/master
    iChunkSize = 150;
    ifiles = 0;
    startRow = 1;
    while (ifiles < 600) {
<<<<<<< HEAD
        startRow = Download_Files(iChunkSize, true, false, TestCaseName + ifiles,startRow)
=======
        startRow = Download_Files(iChunkSize, true, false, TestCaseName + "_" + ifiles, startRow,deviceIP)
>>>>>>> refs/remotes/origin/master
        ifiles = ifiles + iChunkSize;
    }
}

//COMMON FUNCTIONS - END

//ED1 - TEST CASES - START
<<<<<<< HEAD
=======

>>>>>>> refs/remotes/origin/master
//Ensure device is set-up with Ed1 protocol enabled

function BTC_587()
{
<<<<<<< HEAD
    Download_10Chunks_Upto160_CFGOnly("BTC_587");
=======
    Log.Event("BTC_587 START")
    if(Ed1DeviceIP=="")
    {
      Ed1DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed1 Device IP")
    }
    Download_10Chunks_Upto160_CFGOnly("BTC_587", Ed1DeviceIP);
    Log.Event("BTC_587 END")
>>>>>>> refs/remotes/origin/master
}

function BTC_588()
{
<<<<<<< HEAD
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, true, false, "BTC_588" + ifiles,1)
=======
    Log.Event("BTC_588 START")
    if(Ed1DeviceIP=="")
    {
      Ed1DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed1 Device IP")
    }
    iChunkSize = 150;
    ifiles = 150;
    Download_Files(iChunkSize, true, false, "BTC_588_" + ifiles, 1, Ed1DeviceIP)
    Log.Event("BTC_588 END")
>>>>>>> refs/remotes/origin/master
}

function BTC_589()
{
<<<<<<< HEAD
    Download_150Chunks_Upto600_CFGOnly("BTC_589")
}

function BTC_590() {
    iChunkSize = 500;
    ifiles = 500;

    Download_Files(iChunkSize, true, false, "BTC_590" + ifiles,1)
}

function BTC_591() {
    iChunkSize = 500;
    ifiles = 500;

    Download_Files(iChunkSize, false, true, "BTC_591" + ifiles,1)
}

function BTC_597() {
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, false, false, "BTC_597" + ifiles,1)
=======
    Log.Event("BTC_589 START")
    if(Ed1DeviceIP=="")
    {
      Ed1DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed1 Device IP")
    }
    Download_150Chunks_Upto600_CFGOnly("BTC_589", Ed1DeviceIP)
    Log.Event("BTC_589 END")
}

function BTC_590() 
{
    Log.Event("BTC_590 START")
    if(Ed1DeviceIP=="")
    {
      Ed1DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed1 Device IP")
    }
    iChunkSize = 500;
    ifiles = 500;

    Download_Files(iChunkSize, true, false, "BTC_590_" + ifiles, 1, Ed1DeviceIP)
    Log.Event("BTC_590 END")
}

function BTC_591() 
{
    Log.Event("BTC_591 START")
    if(Ed1DeviceIP=="")
    {
      Ed1DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed1 Device IP")
    }
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, false, true, "BTC_591_" + ifiles, 1, Ed1DeviceIP)
    Log.Event("BTC_591 END")
}

function BTC_597() 
{
    Log.Event("BTC_597 START")
    if(Ed1DeviceIP=="")
    {
      Ed1DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed1 Device IP")
    }
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, false, false, "BTC_597_" + ifiles, 1, Ed1DeviceIP)
    Log.Event("BTC_597 END")
>>>>>>> refs/remotes/origin/master
}

//ED1 - TEST CASES - END

//ED2 - TEST CASES - START
<<<<<<< HEAD
//Ensure device is set-up with Ed2 protocol enabled

function BTC_592() {
    Download_10Chunks_Upto160_CFGOnly("BTC_592");
}

function BTC_593() {
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, true, false, "BTC_593" + ifiles,1)
}

function BTC_594() {
    Download_150Chunks_Upto600_CFGOnly("BTC_594")
}

function BTC_595() {
    iChunkSize = 500;
    ifiles = 500;

    Download_Files(iChunkSize, true, false, "BTC_595" + ifiles,1)
}

function BTC_596() {
    iChunkSize = 500;
    ifiles = 500;

    Download_Files(iChunkSize, false, true, "BTC_596" + ifiles,1)
}

function BTC_598() {
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, false, false, "BTC_598" + ifiles,1)
=======

//Ensure device is set-up with Ed2 protocol enabled

function BTC_592() 
{
    Log.Event("BTC_592 START")
    if(Ed2DeviceIP=="")
    {
      Ed2DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed2 Device IP")
    }
    Download_10Chunks_Upto160_CFGOnly("BTC_592", Ed2DeviceIP);
    Log.Event("BTC_592 END")
}

function BTC_593() 
{
    Log.Event("BTC_593 START")
    if(Ed2DeviceIP=="")
    {
      Ed2DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed2 Device IP")
    }
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, true, false, "BTC_593_" + ifiles, 1, Ed2DeviceIP)
    Log.Event("BTC_593 END")
}

function BTC_594() 
{
    Log.Event("BTC_594 START")
    if(Ed2DeviceIP=="")
    {
      Ed2DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed2 Device IP")
    }
    Download_150Chunks_Upto600_CFGOnly("BTC_594", Ed2DeviceIP)
    Log.Event("BTC_594 END")
}

function BTC_595() 
{
    Log.Event("BTC_595 START")
    if(Ed2DeviceIP=="")
    {
      Ed2DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed2 Device IP")
    }
    iChunkSize = 500;
    ifiles = 500;

    Download_Files(iChunkSize, true, false, "BTC_595_" + ifiles, 1, Ed2DeviceIP)
    Log.Event("BTC_595 END")
}

function BTC_596() 
{
    Log.Event("BTC_596 START")
    if(Ed2DeviceIP=="")
    {
      Ed2DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed2 Device IP")
    }
    deviceIP = "10.75.58.50"
    iChunkSize = 500;
    ifiles = 500;

    Download_Files(iChunkSize, false, true, "BTC_596_" + ifiles, 1, Ed2DeviceIP)
    Log.Event("BTC_596 END")
}

function BTC_598() 
{
    Log.Event("BTC_598 START")
    if(Ed2DeviceIP=="")
    {
      Ed2DeviceIP = CommonMethod.ReadDataFromExcel(DataSheetName,"Ed2 Device IP")
    }
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, false, false, "BTC_598_" + ifiles, 1, Ed2DeviceIP)
    Log.Event("BTC_598 END")
>>>>>>> refs/remotes/origin/master
}

//ED2 - TEST CASES - END