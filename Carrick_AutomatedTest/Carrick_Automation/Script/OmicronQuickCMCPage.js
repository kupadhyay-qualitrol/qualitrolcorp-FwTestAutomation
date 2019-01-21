/*This file contains methods and objects related to Quick CMC Utility.
*/

//USEUNIT AssertClass

//Variables
var Btn_Start =Aliases.QuickCMC.wndAfx.xtpBarTop.toolbarTheRibbon.buttonStart
var Btn_Stop =Aliases.QuickCMC.wndAfx.xtpBarTop.toolbarTheRibbon.buttonStop
var Tab_Home =Aliases.QuickCMC.wndAfx.xtpBarTop.toolbarTheRibbon.Item.pagetabHome
var Tab_File= Aliases.QuickCMC.wndAfx.xtpBarTop.toolbarTheRibbon.menubuttonFile
var Tab_Open= Aliases.QuickCMC.wndAfx.menuitemOpenCtrlO
var Edtbx_FileName =Aliases.QuickCMC.wndAfx.cbxFileName.ComboBox.Edit
var Btn_Open =Aliases.QuickCMC.wndAfx.btnOpen
//

//This function is used to click on Start Button on Quick CMC page
function ClickStart()
{
  if(Btn_Start.Exists)
  {
    Btn_Start.ClickButton()
    Log.Message("Clicked on Start Button on Quick CMC")
    return true
  }
  else
  {
    Log.Message("Button Start doesn't exists")
    return false
  }
}

//This function is used to launch the Quick CMC Application
function LaunchQuickCMC()
{
  if(TestedApps.QuickCMC.Run())
  {
    Log.Message("Launched Quick CMC Application")
    if(Btn_Start.Exists)
    {
      Log.Message("Quick CMC launched successfully.")
      return true
    }
    else
    {
      Log.Message("Start button on Quick CMC doesn't exist")
      return false
    }
  }
  else
  {
    Log.Message("Quick CMC didn't launched successfully.")
    return false
  }
}

//This function is used to close the Quick CMC Application
function CloseQuickCMC()
{
  var QuickCMC=Sys.WaitProcess("QuickCMC")
  
  if (QuickCMC.Exists)
  {
    QuickCMC.Close() 
    Log.Message("Terminated Quick CMC Application")
    return true
  }
  else
  {
    Log.Message("Quick CMC Application doesn't exists")  
    return false
  }
}

//This function is used to Open the Test Data File
function OpenTestDataFile(OmicronFileWithPath)
{
  if(OmicronFileWithPath!=null)
  {
    if(Btn_Start.Exists)
    {
      Tab_File.Click()
      Tab_Open.Click()
      Edtbx_FileName.SetText(OmicronFileWithPath)
      Btn_Open.ClickButton()
      Log.Message("Opened the Omicron Test Data File")
      return true
    }
    else
    {
      Log.Message("Unable to Open the Omicron Test Data File")
      return false
    }
  }
  else
  {
    Log.Message("Omicron File Name with path is empty")  
    return false
  }
}


function InjectVoltCurrent(FileName)
{
  try
  {
    AssertClass.IsTrue(LaunchQuickCMC(),"Launching the Quick CMC application")
    AssertClass.IsTrue(OpenTestDataFile(FileName),"Open the TestData File")
    AssertClass.IsTrue(ClickStart(),"Started Injection")
  }
  catch(ex)
  {
    CloseQuickCMC()  
    Log.Message(ex.message)
    Log.Message("Failed to Inject VoltCurrent from Omicron")
  }
}