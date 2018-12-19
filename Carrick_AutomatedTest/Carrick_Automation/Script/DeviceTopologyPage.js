/*This page contains the Objects and methods related to iQ+ Device Topology
*/

//Variables
var DeviceTopologyTree =Aliases.iQ_Plus.ShellForm.windowDockingArea2.dockableWindow5.DeviceTopology.DeviceTopology.UserControlBase_Fill_Panel.TPGYutscTopologies.ultraTabSharedControlsPage1.panelTree.TPGYutvTopologyTree
//

//This method is used to check whether device exist or not?
function IsDeviceExist(devicetype,deviceName)
{
  if(Aliases.iQ_Plus.ShellForm.zShellForm_Toolbars_Dock_Area_Top.Enabled)
  {
    Log.Message("iQ+ UI is open and enabled")
    //Check if the device type exists in the tree/not.
    for(let devicetypecnt=0;devicetypecnt<DeviceTopologyTree.wItems.Item("All Devices").Items.Count;devicetypecnt++ )
    {
      if(DeviceTopologyTree.wItems.Item("All Devices").Items.Item(devicetypecnt).Text==devicetype)
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
        Log.Message("Device Type is not as per expected.Trying to check more...")
      }
    }
  }
  else
  {
    Log.Error("iQ+ is not launched correctly.")
  }
}

//This method is used to click on Device
function ClickonDevice(devicetype,deviceName)
{
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

//This method is used to click on All Device in Topology
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