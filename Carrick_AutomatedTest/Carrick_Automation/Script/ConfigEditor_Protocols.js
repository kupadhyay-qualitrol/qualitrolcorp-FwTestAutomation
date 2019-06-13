/*This files contains methods and objects related to the Protocols page.*/

//USEUNIT ConfigEditor_ConfigurationPage
//USEUNIT ConfigEditorPage

var Grid_Protocols = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucProtocols
var Btn_Add_Protocol = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucProtocols.llblAddProtocol
var Btn_Remove_Protocol = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucProtocols.llblRemoveProtocol
var Dlg_Select_Protocol = Aliases.iQ_Plus.SelectProtocol
var Drpdwn_Protocol_Name = Aliases.iQ_Plus.DropDownForm.ValueListDropDownUnsafe
var Btn_Ok_Select_Portocol = Aliases.iQ_Plus.SelectProtocol.btnSelect

//This function is used to click on Add protocol button
function ClickOnAddProtocol()
{
  if(ConfigEditorPage.ClickOnProtocols())
  {
    if(Grid_Protocols.Enabled)
    {
    Btn_Add_Protocol.Keys("[Enter]")
    Log.Message("Clicked on Add Protocol button")
    return true
    }
    else
    {
    Log.Message("Unable to click on Add Protocols button")
    return false
    }
  }
  else
  {
    Log.Message("Unable to click on protocls and not able to find the Protocols panel")
    return false
  }
}
//This function is used to click on Remove protocol button
function ClickOnRemoveProtocol()
{
  if(ConfigEditorPage.ClickOnProtocols())
  {
    if(Grid_Protocols.Enabled)
    {
    Btn_Remove_Protocol.Keys("[Enter]")
    Log.Message("Clicked on Remove Protocol button")
    return true
    }
    else
    {
    Log.Message("Unable to click on Remove Protocols button")
    return false
    }
  }
  else
  {
    Log.Message("Unable to click on protocls and not able to find the Protocols panel")
    return false
  }
}
//This method is used to select IEC61850 protocol in select protocol window
function SelectProtocol(Protocol_Name)
{
  if(Dlg_Select_Protocol.Exists)
  {
   Dlg_Select_Protocol.cmbProtocol.Text=Protocol_Name
   Btn_Ok_Select_Portocol.Click()
   Log.Message("Selected the Protocol")
   return true
  }
  else
  {
    Log.Message("Not able to select Protocol")
    return false
  }
}