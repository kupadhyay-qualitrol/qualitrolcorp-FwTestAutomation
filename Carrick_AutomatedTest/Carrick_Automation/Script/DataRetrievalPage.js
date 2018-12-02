/*This file contains methods and objects related to Data Retrieval Page*/

//USEUNIT CommonMethod
//USEUNIT SessionLogPage

var DeviceManagementToolbar= CommonMethod.RibbonToolbar
//This method click on FR Manual Trigger under Device & Diagnostic Test in Data Retrieval pane
function ClickOnFRManualTrigger()
{
 if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   //Clear Session Log
   SessionLogPage.ClearLog()
   DeviceManagementToolbar.ClickItem("Device &Management")
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Device Diagnostic/&Test|FR &Manual Trigger")
   Log.Message("Clicked on FR Manual Trigger")
   return true
 }
 else
 {
   Log.Message("Unable to Click on FR Manual Trigger")
   return false
 }
}

//This method click on DFR Directory under Display Device Directory in Data Retrieval Pane
function ClickOnDFRDirectory()
{
 if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management") 
 {
   //Clear Session Log
   SessionLogPage.ClearLog()
   DeviceManagementToolbar.ClickItem("Device &Management")
   DeviceManagementToolbar.ClickItem("Device &Management|Data Retrieval|Displa&y Device Directory|&DFR Directory ")
   Log.Message("Clicked on DFR Directory")
   CommonMethod.CheckActivityLog("Directory list displayed successfully")
   return true
 }
 else
 {
   Log.Message("Unable to click on DFR Directory")
   return false
 }
}

var DFRDirectory=Aliases.iQ_Plus.SDPContainer
//This method is used to Get Latest DFR record number
function GetLatestRecordnumber()
{
  if (DFRDirectory.Exists)
  {
    Log.Message("DFR Directory window is visible")
    
  }
}