//This file contains methods and objects variable related to IEC Browser

//USEUNIT AssertClass
//USEUNIT IECBrowserCommonMethod
//USEUNIT IECBrowserPage

//This method used for connecting the device and checking connection is proper with device or not
function ConnectDeviceToIECBrowser(DeviceIPAdd)
{
  //Step1. Click on connect button on tool bar
  AssertClass.IsTrue(IECBrowserPage.ConnectDevice(),"Clicked on connect button at toolbar of IECbrowser")
  
  //Step2. Set IP address in IP address text box
  AssertClass.IsTrue(IECBrowserPage.SetIPAddress(DeviceIPAdd),"IP address set as iedevice IP")
  
  //Step3. Click on connect button
  AssertClass.IsTrue(IECBrowserPage.ClickOnConnectButton(),"Clicked on connect button")
  
  //Step4. Test the connection
  IECBrowserPage.TestConnectionIECBrowser()
}
//This method will add protocol and download the cid files
function VerifyDeviceConnection()
{
  //Step1. Click on files in device tree view
  AssertClass.IsTrue(IECBrowserPage.SelectFilesInDeviceTree(), "Files selected under Device tree view")
  
  //Step2. Download the cid files
  AssertClass.IsTrue(IECBrowserPage.DownloadCIDFile(), "CID file downloaded")
  
  //Step3. Verify File downloaded successfully or not
  IECBrowserCommonMethod.CheckTraceLog("Copied with success.")
}