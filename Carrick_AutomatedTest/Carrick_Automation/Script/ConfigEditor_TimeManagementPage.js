/*This page contains methods and objects related to Time Management Page*/

//Variables
var drpdwn_TimeZone = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbxGroupBox2.cmbTimeZone
var Radiobtn_TimeMaster = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.rbtnTimeMaster
var Radiobtn_TimeSlave = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.rbtnTimeSlave
var Radiobtn_TimeSettingInternalClock = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.ugbxTimeMaster.rbtnITC
var Radiobtn_TimeSettingGPS = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.ugbxTimeMaster.rbtnGPS
var Radiobtn_TimeSettingExternal = Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.ugbxTimeMaster.rbtnExternal
var Radiobtn_PPSInput_None =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.ugbxTimeSlave.ugbxPhysCon.rbtnNone
var Edtbx_TimeMasterIPAdd1 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.ugbxTimeSlave.timeMasterIp.FieldControl0
var Edtbx_TimeMasterIPAdd2 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.ugbxTimeSlave.timeMasterIp.FieldControl1
var Edtbx_TimeMasterIPAdd3 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.ugbxTimeSlave.timeMasterIp.FieldControl2
var Edtbx_TimeMasterIPAdd4 =Aliases.iQ_Plus.Form.Confgiuration.ConfigEditor.pnlPaddingControl.gbxBorder.scContainer.SplitterPanel2.grpConfigPane.frmContainer.ucTimeManagement.ugbTimeSettings.ugbxTimeSlave.timeMasterIp.FieldControl3
//

//This method is used to set the device TimeZone
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

//This method is used to set the Time Settings for Master
function SetTimeSourceSettings_Master(SourceSetting)
{
  if (Radiobtn_TimeMaster.Exists)
  {
    Log.Message("Time Management Page Exists")
    switch (SourceSetting)
    {
      case "Internal Clock":
        Radiobtn_TimeSettingInternalClock.Click()
        Log.Message("Selected Internal Clock")
        return true
        break
    
      case "GPS Device":
        Radiobtn_TimeSettingGPS.Click()
        Log.Message("Selected GPS Clock")
        return true
        break
      
      case "External":
        Radiobtn_TimeSettingExternal.Click()
        Log.Message("Selected External Clock")
        return true
        break
      default:
        return false
        Log.Message("NoClock Selected")
        break
    }
  }
  else
  {
    Log.Message("Time Management Page doesn't exists")
    return false
  }
}

//This function is used to select the Time Master
function SetTimeMaster()
{
  if (Radiobtn_TimeMaster.Exists)
  {
    Log.Message("Time Management Page Exists")
    if(!Radiobtn_TimeMaster.Checked)
    {
      Radiobtn_TimeMaster.Click()
      Log.Message("Setting is Time Master now")
    }
    else
    {
      Log.Message("Setting is Time Master already")
    }
    return true
  }
  else
  {
    Log.Message("Time Management Page doesn't exists")
    return false
  }
}

//This function is used to select the Time Slave
function SetTimeSlave()
{
  if (Radiobtn_TimeSlave.Exists)
  {
    Log.Message("Time Management Page Exists")
    if(!Radiobtn_TimeSlave.Checked)
    {
      Radiobtn_TimeSlave.Click()
      Log.Message("Setting is Time Slave now")
    }
    else
    {
      Log.Message("Setting is Time Slave already")
    }
    return true
  }
  else
  {
    Log.Message("Time Management Page doesn't exists")
    return false
  }
}

//This method is used to set the MasterIPAddress for Time Sync
function SetTimeMasterIP_TimeSync(MasterIP)
{
  if(Radiobtn_TimeSlave.Exists)
  {
    Log.Message("Time Slave Exists") 
    var IPMaster0= aqString.Find(MasterIP,".")
    var IPMasterAdd0= aqString.SubString(MasterIP,0,IPMaster0)
    
    var IPMaster1=aqString.Find((aqString.SubString(MasterIP,IPMaster0+1,aqString.GetLength(MasterIP))),".")
    var IPMasterAdd1= aqString.SubString(aqString.SubString(MasterIP,IPMaster1+1,aqString.GetLength(MasterIP)),0,IPMaster1)
    
    var IPMaster2=aqString.Find((aqString.SubString(MasterIP,IPMaster1+1,aqString.GetLength(MasterIP))),".")
    var IPMasterAdd2= aqString.SubString(aqString.SubString(MasterIP,IPMaster0+IPMaster1+2,aqString.GetLength(MasterIP)),0,IPMaster2)
    
    var IPMaster3=aqString.Find((aqString.SubString(MasterIP,IPMaster2+1,aqString.GetLength(MasterIP))),".")
    var IPMasterAdd3= aqString.SubString(aqString.SubString(MasterIP,IPMaster0+IPMaster1+IPMaster2+3,aqString.GetLength(MasterIP)),0,IPMaster3) 
    
    Edtbx_TimeMasterIPAdd1.SetText(IPMasterAdd0)
    Edtbx_TimeMasterIPAdd2.SetText(IPMasterAdd1)
    Edtbx_TimeMasterIPAdd3.SetText(IPMasterAdd2)
    Edtbx_TimeMasterIPAdd4.SetText(IPMasterAdd3)
    return true
  }
  else
  {
    Log.Message("Time Slave doesn't Exists")
    return false
  }
}

//This method is used to Set PPS Input
function SetPPSInput(PPSInput)
{
  if(Radiobtn_TimeSlave.Exists)
  {
    Log.Message("Time Slave Exists") 
    switch (PPSInput)
    {
      case "None":
        Radiobtn_PPSInput_None.Click()
        return true
        break
      default :
        return false      
    }   
  }
  else
  {
    Log.Message("Time Slave doesn't Exists")
    return false
  }
}