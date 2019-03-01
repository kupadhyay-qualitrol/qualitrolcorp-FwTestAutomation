/*This file contains methods and objects related to Firmware Mfgindex page*/

function InitialiseWebdriver(deviceIP)
{
  var objBaseTestSuite = dotNET.CashelFirmware_TestSuite.BaseTestSuite.zctor(deviceIP)
  var ChromeDriverInstance = objBaseTestSuite.TestSetup(Project.ConfigPath+"ThirdParty\\bin\\DLL\\")
  return ChromeDriverInstance
}
