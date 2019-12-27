/*
This page contains objects & method related to Config Editor.
Like the buttons/edit box Send to Device,Save to database, Close which are general.
*/
//USEUNIT CommonMethod
//USEUNIT ConfigEditor_FinishPage

//Variables
var Btn_SendToDevice =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.frmBottom.btnSendToDevice
var Btn_Popup_Warning=Aliases.iQ_Plus.dlgWarning.btnYes
var Btn_SaveToDb =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.frmBottom.btnSaveToDb
var Btn_Close =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.frmBottom.btnClose
var Item_ConfigTree=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel.configTree
var drpdwn_MenuView = Aliases.iQ_Plus.ToolStripDropDownMenu
//

//This method is used to click on Send to Device button
function ClickSendToDevice()
{
  if (Btn_SendToDevice.Exists)
  {
    Btn_SendToDevice.Click()
    Log.Message("Clicked on Send to Device button")
    if(!ConfigEditor_FinishPage.IsErrorFound())  //Added this code because of issue in iq+
    {
      Btn_SendToDevice.Click()
      Log.Message("Clicked on Send to Device button")
    }
    if(Btn_Popup_Warning.Exists)
    {
      Btn_Popup_Warning.ClickButton()
      CommonMethod.CheckActivityLog("Configuration assigned successfully to device")
    }    
    return true
  }
  else
  {
    Log.Message("Send to Device button doesn't exist/displayed")
    return false
  }
}

//This method is used to click on Save to Database button
function ClickSaveToDb()
{
  if (Btn_SaveToDb.Exists)
  {
    Btn_SaveToDb.Click()
    CommonMethod.CheckActivityLog("Configuration saved successfully for device")
    Log.Message("Clicked on Save to Database button.")
    return true
  }
  else
  {
    Log.Message("Send to Device button doesn't exist/displayed.")
    return false
  }
}

//This method is used to click on Close button
function ClickOnClose()
{
  if (Btn_Close.Exists)
  {
    Btn_Close.Click()
    Log.Message("Clicked on Close button")
    if(Btn_Popup_Warning.Exists)
    {
      Btn_Popup_Warning.ClickButton()
    }
    return true
  }
  else
  {
    Log.Message("Close button doesn't exist/displayed")
    return false
  }
}

//This method is used to Click on Fault Recording
function ClickOnFaultRecording()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Fault Recording")
    Log.Message("Clicked on Fault Recording")
    return true
  }
  else
  {
    Log.Message("Unable to click on Fault Recording")
    return false
  }
}

//This method is used to Click on Finish Page
function ClickOnFinish()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Finish")
    Log.Message("Clicked on Finish")
    return true
  }
  else
  {
    Log.Message("Unable to click on Finish")
    return false
  }
}

//This method is used to Click on Time Management
function ClickOnTimeManagement()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Time Management")
    Log.Message("Clicked on Time Management")
    return true
  }
  else
  {
    Log.Message("Unable to click on Time Management")
    return false
  }
}

//This method is used to click on Advanced option under View
function ClickonAdvance()
{
  if(Item_ConfigTree.Exists)
  {
    Aliases.iQ_Plus.Form.StripMainMenu.Click("View")
    if(aqObject.CheckProperty(drpdwn_MenuView,"Enabled",cmpEqual,true))
    {
      Aliases.iQ_Plus.Form.StripMainMenu.Click("View|Advanced")
      Log.Message("Clicked on Advance under View")
      return true
    }
    else
    {
      Log.Message("Unable to click on Advance under View")    
      return false
    }
  }
  else
  {
    Log.Message("Unable to find Config Main Menu")
    return false
  }
}

//This method is used to click on Network Services in Config Editor
function ClickonNetworkServices()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Communications|Network Services")
    Log.Message("Clicked on Network Services")
    return true
  }
  else
  {
    Log.Message("Unable to find Config Editor")
    return false
  }
}

//This method is used to click on Circuits in Config Editor
function ClickOnCircuits()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Circuits")
    Log.Message("Clicked on Circuits")
    return true
  }
  else
  {
    Log.Message("Unable to find Config Editor")
    return false
  }
}

//This method is used to click on Analog Inputs in Config Editor
function ClickOnAnalogInputs()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Device Overview|Analog Inputs")
    Log.Message("Clicked on analog Inputs")
    return true
  }
  else
  {
    Log.Message("Unable to find Config Editor")
    return false
  }
}

//This method is used to click on FR Sensor in Config Editor
function ClickOnFRSensor()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Fault Recording|FR Sensors")
    Log.Message("Clicked on FR Sensors")
    return true
  }
  else
  {
    Log.Message("Unable to find Config Editor")
    return false
  }
}
//This method is used to click on Protocols in Config Editor
function ClickOnProtocols()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Communications|Protocols")
    Log.Message("Clicked on Protocols")
    return true
  }
  else
  {
    Log.Message("Unable to find Config Editor")
    return false
  }
}
//This method is used to click on DDRC Channels in Config Editor
function ClickOnDDRCChannels()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Fault Recording|DDR Channels")
    Log.Message("Clicked on DDRC Channels")
    return true
  }
  else
  {
    Log.Message("Unable to find Config Editor")
    return false
  }
}




//This method is used to click on PQ free interval in Config Editor
function clickPqFreeInterval()
{
  if(Item_ConfigTree.Exists)
  {
    Item_ConfigTree.ClickItem("Power Quality|Continous Recording|Free Interval")
    Log.Message("Clicked on PQ Free Interval Page")
    return true
  }
  else
  {
    Log.Message("Unable to find Config Editor")
    return false
  }
}