/*This page contains the Objects and methods related to iQ+ Device Topology
*/
var DeviceTopologyTree =Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow5.DeviceTopology.DeviceTopology.UserControlBase_Fill_Panel.TPGYutscTopologies.ultraTabSharedControlsPage1.panelTree.TPGYutvTopologyTree

function IsDeviceExist(devicetype,deviceName)
{
  //IsDeviceExist=false
  if(Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top.Enabled)
  {
    Log.Message("iQ+ UI is open and enabled")
    if(DeviceTopologyTree.wItems.Item("All Devices").Items.Item(devicetype).Items.Count!=0)
    {
      for(Itemcnt=0;Itemcnt<=DeviceTopologyTree.wItems.Item("All Devices").Items.Item(devicetype).Items.Count-1;Itemcnt++)
      {
         if(deviceName==DeviceTopologyTree.wItems.Item("All Devices").Items.Item(devicetype).Items.Item(Itemcnt).Text)
         {
           return true
           break
         }
      }
    }
    else
    {
      Log.Message("Device of the type IDM+18 doesn't exist")
      return false
    }
  }
  else
  {
    Log.Error("iQ+ UI is not up/enabled")
  }
}

function ClickonDevice(devicetype,deviceName)
{
  //ClickonDevice=false
  if(IsDeviceExist(devicetype,deviceName))
  {
    DeviceTopologyTree.ClickItem("All Devices|"+devicetype+"|"+ deviceName)
    Log.Message("Clicked on the device with name :- "+ deviceName)
    if(DeviceTopologyTree.wItems.Item("All Devices").Items.Item(devicetype).Items.Item(deviceName).Selected)
    {
      Log.Message("Device is selected")
      return true
    }
    else
    {
      Log.Message("Device is not selected")
      return false
    }
  }
  else
  {
    Log.Message("Device is not present in the tree,so unable to click")
  }
}

function ClickonAllDevices()
{
  if(DeviceTopologyTree.Enabled)
  {
    DeviceTopologyTree.ClickItem("All Devices")
    return true
  }
  else
  {
    return false
  }
}