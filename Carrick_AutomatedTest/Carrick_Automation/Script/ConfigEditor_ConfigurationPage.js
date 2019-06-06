/*This file contains methods and objects variable related to device Management page
* on iQ+ UI. 
*/

//Variables//
var EdtBx_File_Name = Aliases.iQ_Plus.dlg.DUIViewWndClassName.Explorer_Pane.FloatNotifySink.ComboBox.Edit
var Configuration_Editor_box = Aliases.iQ_Plus.Form
var Dialog_Confirm_Save_As = Aliases.iQ_Plus.dlgConfirmSaveAs
var Btn_Yes_Confirm_Save = Aliases.iQ_Plus.dlgConfirmSaveAs.Confirm_Save_As.CtrlNotifySink.btnYes
var Btn_Save = Aliases.iQ_Plus.dlg.btnSave
var Dlg_Export_Configuration = Aliases.iQ_Plus.dlg
var Dlg_Import_Configuration = Aliases.iQ_Plus.dlgSelectACashelMConfigurationFile
var Dlg_Save_As_Template = Aliases.iQ_Plus.Form2
var Btn_Ok = Aliases.iQ_Plus.Form2.Save_As_Template.SaveAsTemplate.btnOk
var EdtBx_Template_Name = Aliases.iQ_Plus.Form2.Save_As_Template.SaveAsTemplate.txtUsername
var Dlg_Template = Aliases.iQ_Plus.Form3
var List_Template = Aliases.iQ_Plus.Form3.Import_Template.ImportTemplate.gbDefaultConfigurations.lstDefaultTemplates
var Btn_Copy_Template = Aliases.iQ_Plus.Form3.Import_Template.ImportTemplate.btnImport
var Import_Device_topology = Aliases.iQ_Plus.Form4.ImportOtherDevice.ImportDeviceTemplate.pnlTopContainer.gbxImportConfig.TopologyWorkspace.DeviceTopology.UserControlBase_Fill_Panel.TPGYutscTopologies.ultraTabSharedControlsPage1.panelTree.TPGYutvTopologyTree
var Btn_Copy_Other_Device = Aliases.iQ_Plus.Form4.ImportOtherDevice.ImportDeviceTemplate.pnlContainer.btnImport
var Dlg_Other_Device_popup = Aliases.iQ_Plus.Form4

// This method is to click on Export as a file button
function ClickExportAsFile()
{
    if(Configuration_Editor_box.Enabled)
    {
    Configuration_Editor_box.SetFocus()
    Configuration_Editor_box.StripMainMenu.Click("Configuration|Export|As a File")
    Log.Message("Export as a file button clicked")
    return true
    }
    else
    {
    Log.Message("Unable to Click on Export As a File Button")
    return false
    }
}
// This method is to set file name 
function SetFileName(File_Name)
{
    if(Dlg_Export_Configuration.Enabled)
    {
    Dlg_Export_Configuration.SetFocus()
    EdtBx_File_Name.SetText(aqString.Trim(File_Name))
    Log.Message("Configuration file set to file name")
    return true
    }
    else
    {
    Log.Message("Not able to set file name in Export configuration window")
    return false
    }
}
//This method is used for clicking on Save button of Export pop up
function ClickOnSaveButtonExportAsFile()
{
  if(Dlg_Export_Configuration.Enabled)  
  {
  Btn_Save.Click()
  if(Dialog_Confirm_Save_As.Exists)
  {
    Btn_Yes_Confirm_Save.Click()
    Log.Message("New configuration is replaced with the previous configuration")
  }
  Log.Message("Save button clicked on Export configuration window")
  return true
  }
  else
  {
  Log.Message("Unable to Click on Save Button of Export configuration window")
  return false
  }
}
// This method is to import as a file button
function ClickImportAsFile()
{
  if(Configuration_Editor_box.Enabled)
    {
    Configuration_Editor_box.SetFocus()
    Configuration_Editor_box.StripMainMenu.Click("Configuration")
    Configuration_Editor_box.StripMainMenu.Click("Configuration|Import|From File")
    Log.Message("Import from file button clicked")
    return true
    }
    else
    {
      Log.Message("Unable to click on Import As A File button")
    }
    return false
}
// This method is to Open configuration as a file
function OpenConfiguratioAsFile(File_Name)
{
  if(Dlg_Import_Configuration.Enabled)
  {
  Dlg_Import_Configuration.SetFocus()
  Dlg_Import_Configuration.OpenFile(File_Name, "CFG files (*.cfg)")
  Log.Message("Clicked on Open button of Import Configuration")
  return true
  }
  else
  {
  Log.Message("Not able to click on Open button of Select a ChashelIM configuration file")
  return false
  }
}
//This method is used for the Clicking on Export as a template button
function ClickExportAsTemplate()
{
  if(Configuration_Editor_box.Enabled)
  {
    Configuration_Editor_box.SetFocus()
    Configuration_Editor_box.StripMainMenu.Click("Configuration|Export|As a Template")
    Log.Message("Export as a template button clicked")
    return true
  }
  else
  {
    Log.Message("Unable to click on Export as a Template button")
  }
}
//This method is used for setting the Template name
function SetTemplateName(Template_Name)
{
  if(Dlg_Save_As_Template.Enabled)
  {
    Dlg_Save_As_Template.SetFocus()
    EdtBx_Template_Name.SetText(aqString.Trim(Template_Name))
    Btn_Ok.ClickButton()
    aqUtils.Delay(3000)
    Configuration_Editor_box.SetFocus()
    return true
  }
  else
  {
    Log.Message("Unable to Set Template Name")
    return false
  }
}
//This method is used for Clicking on Import as a template button
function ClickImportAsTemplate()
{
  if(Configuration_Editor_box.Enabled)
  {
  Configuration_Editor_box.SetFocus()
  Configuration_Editor_box.StripMainMenu.Click("Configuration|Import|From Template")
  Log.Message("Click on Import As A Template")
  return true
  }
  else
  {
  Log.Message("Not able to click on Import As A Template")
  return false
  }
}
//This method is used for selecting Template from the template list
function SelectTemplateFromTemplateList(Template_Name)
{
  if(Dlg_Template.Enabled)
  {
    List_Template.ClickItem(Template_Name)
    return true
  }  
 else
 {
    Log.Message("Not able to Select the template")
    return false
  }
}
//This method is used for clicking on Copy button of Import 4template pop up.
function ClickCopyTemplateButton()
{
  if(Dlg_Template.Enabled)
  {
    Btn_Copy_Template.ClickButton()
    aqUtils.Delay(2000);
    return true
  }  
 else
 {
    Log.Message("Not able to Select the template")
    return false
  }
}  
//This method is used for clicking on Import from other device button  
function ClickImportFromOtherDevice()
{
  if(Configuration_Editor_box.Enabled)
  {
    Configuration_Editor_box.SetFocus()
    Configuration_Editor_box.StripMainMenu.Click("Configuration") 
    Configuration_Editor_box.StripMainMenu.Click("Configuration|Import|From Another Device")
    return true
   }
   else
   {
    Log.Message("Unable to Click on Import from another device Button")
    return false
   }
}
//This method is used for click on Copy button of Import other device pop up of Import configuration from other device
function ClickOnCopyButton()
{
  if(Import_Device_topology.Enabled)
  {
    Btn_Copy_Other_Device.ClickButton()
    aqUtils.Delay(3000);
    return true
  }
  else
  {
    Log.Message("Click on Copy button on Other device pop up")
    return false
  }
}
//This method used for selecting device from the device topology of Import other device pop up of Import configuration from other device devicetype,deviceName "+devicetype+" "+ deviceName
function SelectDeviceFromImportOtherDevice(devicetype,deviceName)
{
  if(Dlg_Other_Device_popup.Enabled)
  {
    Import_Device_topology.ClickItem("All Devices|"+devicetype+"|"+ deviceName)
    Log.Message("Device has been selected for Import the configuration from other device")
    return true
  }
  else
  {
    Log.Message("Not able to select Device from the topology")
    return false
  }
}
