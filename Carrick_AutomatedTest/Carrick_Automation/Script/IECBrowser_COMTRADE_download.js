//COMMON FUNCTIONS - START
//Hardcoded Device IP - 10.75.58.51
//Create Folder C://AIP-74_Testing

function Download_Files(NoOfFiles, CFGOnly, DATOnly, TestCaseName,startRow)
{
    //Check whether IECBrowser is already running or not?
    var iecBrowser_process=Sys.WaitProcess("Iec_Browser")
  
    if (iecBrowser_process.Exists)
    {
      //iecBrowser_process.Terminate() 
      //Log.Message("Terminated IECBrowser Application")
      Log.Message("IECBrowser is already running")
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
    FilesToppology = Aliases.Iec_Browser.mainForm.panel1.dataGrid1
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
    while (NoofChecks <= NoOfFiles)
    {
        if ((aqString.Find(FilesToppology.Item(row, 0).OleValue, "COMTRADE")) != -1)
        {
            if (usefilefilter && (aqString.Find(FilesToppology.Item(row, 0).OleValue, filefilter)) != -1)
            {
                FilesToppology.ClickCell(row, "GetFile")
                NoofChecks++;
            }
        }
        row++;
    }
    aqFileSystem.CreateFolder("C:\\AIP-74_Testing\\"+ TestCaseName + "\\")
    
    FilesToppology.ClickCellR(NoofChecks, "GetFile")
    FilesToppology.PopupMenu.Click("Get tagged Files...")
    Aliases.Iec_Browser.dlgBrowseForFolder.SHBrowseForFolderShellNameSpaceControl.TreeView.ClickItem("|Desktop|This PC|OS (C:)|AIP-74_Testing|" + TestCaseName)
    Aliases.Iec_Browser.dlgBrowseForFolder.btnOK.ClickButton()
    
    Log.Message(Aliases.Iec_Browser.mainForm.panel1.textBox_Output.Text)
    
    FilesToppology.ClickCellR(NoofChecks, "GetFile")
    FilesToppology.PopupMenu.Click("Refresh Table")
    return row;
}

function Download_10Chunks_Upto160_CFGOnly(TestCaseName)
{
    iChunkSize = 10;
    ifiles = 0;
    startRow = 1;
    while (ifiles < 30) {
        startRow = Download_Files(iChunkSize, true, false, TestCaseName + ifiles,startRow)
        ifiles = ifiles + iChunkSize;
    }
}

function Download_150Chunks_Upto600_CFGOnly(TestCaseName) {
    iChunkSize = 150;
    ifiles = 0;
    startRow = 1;
    while (ifiles < 600) {
        startRow = Download_Files(iChunkSize, true, false, TestCaseName + ifiles,startRow)
        ifiles = ifiles + iChunkSize;
    }
}

//COMMON FUNCTIONS - END

//ED1 - TEST CASES - START
//Ensure device is set-up with Ed1 protocol enabled

function BTC_587()
{
    Download_10Chunks_Upto160_CFGOnly("BTC_587");
}

function BTC_588()
{
    iChunkSize = 150;
    ifiles = 150;

    Download_Files(iChunkSize, true, false, "BTC_588" + ifiles,1)
}

function BTC_589()
{
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
}

//ED1 - TEST CASES - END

//ED2 - TEST CASES - START
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
}

//ED2 - TEST CASES - END