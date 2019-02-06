/*This file contains methods to interact with Linux OS for performing device operation*/

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