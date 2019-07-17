/*This file contains methods and objects variable related to device Management page
* on iQ+ UI. 
*/
//USEUNIT CommonMethod
//USEUNIT SessionLogPage
//USEUNIT ConfigEditorPage

//Variables
var DeviceManagementToolbar= CommonMethod.RibbonToolbar
var Btn_PopUpConfigCheck_No=Aliases.iQ_Plus.CheckConfigVersion.btnNo
var Btn_DFRDirectory_DownloadDataNow = Aliases.iQ_Plus.SDPContainer.SDPCTRtsctrSDPToolsContainer.ToolStripContentPanel.DFRDirectory.DirectoryListView.DIRLSTVWbtnDownloadDataNow
//

//This method is to Click on Retrieve CConfiguration button
function ClickonRetrieveConfig()
{
  var deviceResponse= false
  if(DeviceManagementToolbar.wItems.Item("Device &Management").Text=="Device &Management")
  {
    //Clear Session log  
    SessionLogPage.ClearLog()  
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
    Log.Message("Clicked on Retrieve Configuration Page")
    do
    {
      if (CommonMethod.CheckActivityLog("Configuration retrieved successfully from device"))
      {
        break
      }
      if(CommonMethod.CheckActivityLog("Unable to establish connection with device"))
      {
       Log.Error("Device IP is wrong")
      }
      else
      {
        aqUtils.Delay(2000) //wait before retry 
        //Clear Session log  
        SessionLogPage.ClearLog()  
        DeviceManagementToolbar.ClickItem("Device &Management")
        DeviceManagementToolbar.ClickItem("Device &Management|Configuration|&Retrieve Configuration")
      }
    }
    while (!deviceResponse) 
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
    //Clear Session log  
    SessionLogPage.ClearLog()  
    DeviceManagementToolbar.ClickItem("Device &Management")
    DeviceManagementToolbar.ClickItem("Device &Management|Configuration|Modify Co&nfiguration")
    Log.Message("Clicked on Modify Configuration Page")
    if(Btn_PopUpConfigCheck_No.Exists)
    {
      Btn_PopUpConfigCheck_No.Click()
      Log.Message("Clicked on NO on Check Config version Popup")
    }
    return true
  }
  else
  {
    Log.Message("Unable to Click on Device Management Button")
    return false
  }
}