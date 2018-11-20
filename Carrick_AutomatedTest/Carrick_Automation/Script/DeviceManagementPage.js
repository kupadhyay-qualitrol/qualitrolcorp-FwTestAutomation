/*This file contains methods and objects variable related to device Management page
* on iQ+ UI. 
*/
//USEUNIT CommonMethod

var DeviceManagementToolbar= CommonMethod.RibbonToolbar

function ClickonRetrieveConfig()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved  Successfully")    
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}
