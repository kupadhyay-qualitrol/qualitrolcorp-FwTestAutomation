/*This file contains methods and objects related to Firmware Mfgindex page*/
var objBaseTestSuite
function InitialiseWebdriver(deviceIP)
{
  CreateObject(deviceIP)
  var ChromeDriverInstance = objBaseTestSuite.TestSetup()
  return ChromeDriverInstance
}

function CreateObject(deviceIP)
{
  objBaseTestSuite = dotNET.CashelFirmware_TestSuite.BaseTestSuite.zctor(deviceIP)
  return true
}

function TearDown()
{
  objBaseTestSuite.TearDown()
  Log.Message("Test Case Ends")
  return true
}

function EndReport()
{
  objBaseTestSuite.EndReprot()
  Log.Message("Report has been ended")
  return true
}

function StartReport()
{  
  objBaseTestSuite.StartReport()
  Log.Message("Report for WebPage has been Started")
  return true
}

function StartTestCaseReport(TestCaseName)
{
  objBaseTestSuite.InfovarStartTest = dotNET.CashelFirmware_Reporting.ReportGeneration.extent.StartTest(TestCaseName,undefined)
  return true
}
