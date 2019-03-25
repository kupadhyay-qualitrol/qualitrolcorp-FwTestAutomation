/*This file contains methods & Objects related to Device Overview*/

//Variables
var Grid_AnalogInputs =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucAnalogInputs.ugAnalogChannels
//

//This method is used to Get the Index of Channel Type
function GetChannelType(rowindex)
{
  if(Grid_AnalogInputs.Exists)
  {
    Log.Message("Analog Input Grid exists")
    var ChannelType= Grid_AnalogInputs.DataSource.List.Item(aqConvert.StrToInt(rowindex)).get_Unit().ToString().OleValue
    Log.Message("Channel Type for row "+rowindex+ " is "+ ChannelType)
    return ChannelType //returns V for Volts and A for Amps
  }
  else
  {
    Log.Message("Analog Input Grid is not visible.")
    return null
  }
}

function GetChannelName(rowindex)
{
  if(Grid_AnalogInputs.Exists)
  {
    Log.Message("Analog Input Grid exists")
    var ChannelName= Grid_AnalogInputs.DataSource.List.Item(aqConvert.StrToInt(rowindex)).Label.OleValue
    Log.Message("Channel Name for row "+rowindex+ " is "+ ChannelName)
    return ChannelName //returns V for Volts and A for Amps
  }
  else
  {
    Log.Message("Analog Input Grid is not visible.")
    return null
  }
}

function SetChannelName(rowindex,AnalogChannelName)
{
  if(Grid_AnalogInputs.Exists)
  {
    Log.Message("Analog Input Grid exists")
    Grid_AnalogInputs.DataSource.List.Item(aqConvert.StrToInt(rowindex)).set_Label(AnalogChannelName)
    Log.Message("Sets the Channel Name for row "+rowindex+ " to "+ AnalogChannelName)
    return true
  }
  else
  {
    Log.Message("Analog Input Grid is not visible.")
    return false
  }
}

function GetChannelCount()
{
  if(Grid_AnalogInputs.Exists)
  {
    Log.Message("Analog Input Grid exists")
    var RowsCount=Grid_AnalogInputs.DataSource.List.Count
    Log.Message("Total Number of rows are:- "+ RowsCount)
    return RowsCount
  }
  else
  {
    Log.Message("Analog Input Grid is not visible.")
    return null
  }
}