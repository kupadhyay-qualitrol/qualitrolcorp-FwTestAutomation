/*This file contains methods and objects related to Data Retrieval Page*/

//USEUNIT CommonMethod
//USEUNIT SessionLogPage
var DeviceManagementToolbar=CommonMethod.RibbonToolbar
var RibbonToolbar=Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top
var Activitylog = Aliases.iQ_Plus.ShellForm.windowDockingArea1.dockableWindow3.ActivityLog.ActivityMonitor.ACTYLOGtxtLog


//This function is used to get the CurrentDateTime for the Device
function ClickOnDeviceStatusView()
{
 if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   //Clear Session Log
   SessionLogPage.ClearLog()
   DeviceManagementToolbar.ClickItem("Device &Management")
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test")
   aqObject.CheckProperty(Aliases.iQ_Plus.DropDownForm.PopupMenuControlTrusted, "Enabled", cmpEqual, true)
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|D&evice Status")
   Log.Message("Clicked on Device Status Option")
   if(ClickOnDeviceStatusView())
   {
     Log.Message("Clicked on Device Status button")
     CommonMethod.CheckActivityLog("Device information displayed successfully")  
     return true
   }
   else
   {
     Log.Message("Device Status button is not available")   
     return false
   }
 }
 }
 