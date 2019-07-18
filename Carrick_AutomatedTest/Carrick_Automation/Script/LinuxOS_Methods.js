/*This file contains methods to interact with Linux OS for performing device operation*/
//USEUNIT CommonMethod
//USEUNIT AssertClass

//Variables
var PasswordInfo
var SSHClient
//

//This method is used to make SSH connection with device
function ConnectToDevice(deviceIP,UserName,Password)
{
  PasswordInfo = new dotNET.Renci_SshNet.PasswordConnectionInfo.zctor(deviceIP,UserName,Password)                         
  SSHClient = new dotNET.Renci_SshNet.SshClient.zctor(PasswordInfo)
  SSHClient.Connect() //method returns void
  Log.Message("Created SSH Connection with device.")
  return true
}

//This method is used to Send Linux command to device and return output
function SendCommand(Command)
{
  var Output = SSHClient.RunCommand(Command)
  Log.Message("Output from Device is :- "+ Output.Result)
  return Output.Result
}

//This method is used to disconnect the SSH connection with Device
function DisconnectFromDevice()
{
  SSHClient.Disconnect()
  return true
}

function CleanDevice(deviceIP)
{
  var counter,deviceStatus
  do
  {
    deviceStatus=CommonMethod.GetDeviceStatusOnPing(deviceIP)
  }
  while (deviceStatus!="Success")
  ConnectToDevice(deviceIP,"root","qualcorpDec09")
  SendCommand("rm /home/config/dfr.CFG")
  SendCommand("rm /home/config/pqp.CFG")
  SendCommand("rm /home/config/event.CFG")
  SendCommand("rm /home/config/rms.CFG")
  SendCommand("rm /home/config/flr.CFG")
  SendCommand("rm /home/config/pmu.CFG")
  SendCommand("rm /home/config/soh.CFG")
  //This method will reboot IDM+ only not FL
  SendCommand("killall dispatch")
  DisconnectFromDevice() 
  
  counter =0
  do
  {
    deviceStatus=CommonMethod.GetDeviceStatusOnPing(deviceIP)
    if(deviceStatus=="Success")
    {
      counter=counter+1
      aqUtils.Delay(1000)
    }
  }
  while (deviceStatus=="Success" && counter<=20)
  if(counter==21)
  {
    Log.Error("Device has not gone for reboot after cleanup")
  }
  
  counter =0
  do
  {
    deviceStatus=CommonMethod.GetDeviceStatusOnPing(deviceIP)
    if(deviceStatus=="Success")
    {
      counter =counter+1
      aqUtils.Delay(1000)
    }
  }
  while (counter<=30)
}
