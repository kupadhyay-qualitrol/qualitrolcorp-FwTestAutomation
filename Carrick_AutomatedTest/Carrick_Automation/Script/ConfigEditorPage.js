/*
This page contains objects & method related to Config Editor.
Like the buttons/edit box Send to Device,Save to database, Close which are general.
*/
//USEUNIT CommonMethod

//Variables
var Btn_SendToDevice =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.frmBottom.btnSendToDevice
var Btn_Popup_Warning=Aliases.iQ_Plus.dlgWarning.btnYes
var Btn_SaveToDb =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.frmBottom.btnSaveToDb
var Btn_Close =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.frmBottom.btnClose
var Item_ConfigTree=Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel.configTree
//

//This method is used to click on Send to Device button
function ClickSendToDevice()
{
  if (Btn_SendToDevice.Exists)
  {
    Btn_SendToDevice.Click()
    Log.Message("Clicked on Send to Device button")
    do
    {
      aqUtils.Delay(1000)
    }
    while(!Btn_Popup_Warning.Exists)
    Btn_Popup_Warning.ClickButton()
    CommonMethod.CheckActivityLog("Configuration assigned successfully to device")
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
