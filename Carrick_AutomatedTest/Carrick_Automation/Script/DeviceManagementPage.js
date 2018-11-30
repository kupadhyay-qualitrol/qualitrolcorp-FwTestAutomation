/*This file contains methods and objects variable related to device Management page
* on iQ+ UI. 
*/
//USEUNIT CommonMethod

var DeviceManagementToolbar= CommonMethod.RibbonToolbar
//This method is to Click on Retrieve CConfiguration button
function ClickonRetrieveConfig()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved successfully from device")    
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}

//This method is to Click on Modify CConfiguration button
function ClickonModifyConfig()
{
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|Modify Co&nfiguration")
    Log.Message("Clicked on Modify Configuration Page")
    CommonMethod.CheckActivityLog("Configuration retrieved successfully from device")    
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}
