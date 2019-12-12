/*This page contains method and objects related to State Seq*/

//USEUNIT AssertClass

//Variables
var Btn_Start_Continue =Aliases.OMSeq.wndAfx.xtpBarTop.toolbarTheRibbon.buttonStartContinue
var Tab_File =Aliases.OMSeq.wndAfx.xtpBarTop.toolbarTheRibbon.menubuttonFile
var Tab_Open =Aliases.OMSeq.wndAfx.menuitemOpenCtrlO
var Edtbx_FileName =Aliases.OMSeq.wndAfx.cbxFileName.ComboBox.Edit
var Btn_Open =Aliases.OMSeq.wndAfx.btnOpen
var Btn_SaveAs_No = Aliases.OMSeq.wndAfx.btnNo
//
//This method is used to launch State Sequencer Application
function LaunchStateSeq()
{
  if(TestedApps.OMSeq.Run())
  {
    Log.Message("Launched State Sequencer Application")
    if(Btn_Start_Continue.Exists)
    {
      Log.Message("State Sequencer launched successfully.")
      return true
    }
    else
    {
      Log.Message("Start button on State Sequencer doesn't exist")
      return false
    }
  }
}

//This function is used to Open the Test Data File
function OpenTestDataFile(OmicronFileWithPath)
{
  if(OmicronFileWithPath!=null)
  {
    if(Btn_Start_Continue.Exists)
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

//This method is used to click on Start in State Seq
function ClickOnStartContinue()
{
  if(Btn_Start_Continue.Exists)
  {
    Btn_Start_Continue.ClickButton()
    Log.Message("Clicked on Start Button on Quick CMC")
    return true
  }
  else
  {
    Log.Message("Button Start doesn't exists")
    return false
  }
}

//This function is used to close the State Seq Application
function CloseStateSeq()
{
  var stateSeq=Sys.WaitProcess("OMSeq")
  
  if (stateSeq.Exists)
  {
    stateSeq.Close()
    if(Btn_SaveAs_No.Exists)
    {
      Btn_SaveAs_No.ClickButton()
    }
    Log.Message("Terminated State Seq Application")
    return true
  }
  else
  {
    Log.Message("State Seq Application doesn't exists")  
    return false
  }
}

function RunSeqFile(FileName)
{
  AssertClass.IsTrue(LaunchStateSeq(),"Launching the State Seq application")
  AssertClass.IsTrue(OpenTestDataFile(FileName),"Open the TestData File")
  AssertClass.IsTrue(ClickOnStartContinue(),"Started Injection")
  AssertClass.IsTrue(CloseStateSeq(),"State seq file is closed")
}
