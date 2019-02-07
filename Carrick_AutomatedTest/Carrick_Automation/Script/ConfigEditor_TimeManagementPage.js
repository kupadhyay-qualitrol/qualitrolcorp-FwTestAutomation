/*This page contains methods and objects related to Time Management Page*/

var drpdwn_TimeZone = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbxGroupBox2.cmbTimeZone
function SetTimeZone(TimeZone)
{
  if(drpdwn_TimeZone.Exists)
  {
    Log.Message("Time Management Page Exists")
    drpdwn_TimeZone.Text = TimeZone 
    if(drpdwn_TimeZone.Text==TimeZone)
    {
      Log.Message("Set the time zone:- "+TimeZone)
      return true
    }
    else
    {
      Log.Message("Unable to Set the Time Zone:-"+TimeZone)
      return false
    }
  }
  else
  {
    Log.Message("Time Management Page doesn't exists")
    return false
  }
}