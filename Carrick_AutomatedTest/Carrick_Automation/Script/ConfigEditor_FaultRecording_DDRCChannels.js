/*This page contains methods and objectes related to DDRC channels Page*/

//Variables
var Drp_Dwn_Storage_Rate = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucDdrChannels.cmbSamplingRate
var Pane_DDRC_Channels = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane

function SetStorageRate(StorageRate)
{
  if(Pane_DDRC_Channels.Visible)
  {
    Drp_Dwn_Storage_Rate.Click;
    Log.Message("Clicked on Storage rate dorpdown")
    Drp_Dwn_Storage_Rate.set_Text(StorageRate);
    Log.Message("Storage rate has been set")
    return true
  }
  else
  {
    Log.Message("DDRC Channels pane is not displaying")
    return false
  }
}