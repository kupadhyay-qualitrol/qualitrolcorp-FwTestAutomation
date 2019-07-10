//This file contains methods and objects variable related to IEC Browser

//USEUNIT AssertClass
//USEUNIT IECBrowserCommonMethod
//USEUNIT IECBrowserPage

//This method used for connecting the device and checking connection is proper with device or not
function ConnectToIECBrowser(deviceIPAdd)
{
  //Step1. Click on connect button on tool bar
  AssertClass.IsTrue(IECBrowserPage.ConnectDevice(),"Clicked on connect button at toolbar of IECbrowser")
  
  //Step2. Set IP address in IP address text box
  AssertClass.IsTrue(IECBrowserPage.SetIPAddress(deviceIPAdd),"IP address set as iedevice IP")
  
  //Step3. Click on connect button
  AssertClass.IsTrue(IECBrowserPage.ClickOnConnectButton(),"Clicked on connect button")
  
   aqUtils.Delay(3*Referesh_time)
}
function ConnectDeviceToIECBrowser(deviceIPAdd)
{
  ConnectToIECBrowser(deviceIPAdd)
  
  if(IECBrowserCommonMethod.CheckTraceLog("Done"))
    {
     Log.Message("Connection successful") 
     aqUtils.Delay(2*Referesh_time)  //Delay for getting page refereshed
    }
	else if(IECBrowserCommonMethod.CheckTraceLog("Connect failed!"))
	{
	 var device_connection = false
	 var protocol_enable_counter = 0
	
	do
  {
    protocol_enable_counter++
	
	 ConnectToIECBrowser(deviceIPAdd)
	 
	if(IECBrowserCommonMethod.CheckTraceLog("Done"))
	{
	  Log.Message("Connection successful") 
     device_connection = true   
     aqUtils.Delay(2*Referesh_time)  //Delay for getting page refereshed
	}    
    else
	{
	 IECBrowserPage.CleareTraceLog()
     Log.Message("Retry connection")
	}	
  }
  while(!device_connection && protocol_enable_counter<=10)
	}
}

//function ConnectDeviceToIECBrowser(deviceIPAdd)
//{
//  //Step1. Click on connect button on tool bar
//  AssertClass.IsTrue(IECBrowserPage.ConnectDevice(),"Clicked on connect button at toolbar of IECbrowser")
//  
//  //Step2. Set IP address in IP address text box
//  AssertClass.IsTrue(IECBrowserPage.SetIPAddress(deviceIPAdd),"IP address set as iedevice IP")
//  
//  //Step3. Click on connect button
//  AssertClass.IsTrue(IECBrowserPage.ClickOnConnectButton(),"Clicked on connect button")
//  
//  //Step4. Test the connection
//  var device_connection = false
//  var protocol_enable_counter = 0
//  aqUtils.Delay(3*Referesh_time)
//  do
//  {
//    protocol_enable_counter++
//    if(IECBrowserCommonMethod.CheckTraceLog("Done"))
//    {
//     Log.Message("Connection successful") 
//     device_connection = true   
//     aqUtils.Delay(2*Referesh_time)  //Delay for getting page refereshed
//    }
//    else if(IECBrowserCommonMethod.CheckTraceLog("Connect failed!"))
//    {
//     IECBrowserPage.CleareTraceLog()
//     Log.Message("Retry connection")
//     IECBrowser_Methods.ConnectDeviceToIECBrowser(deviceIPAdd)
//    }  
//  }
//  while(!device_connection && protocol_enable_counter<=10)
//}
//This method will add protocol and download the cid files
function VerifyDeviceConnection()
{
  //Step1. Click on files in device tree view
  AssertClass.IsTrue(IECBrowserPage.SelectFilesInDeviceTree(), "Files selected under Device tree view")
  
  //Step2. Download the cid files
  AssertClass.IsTrue(IECBrowserPage.DownloadCIDFile(), "CID file downloaded")
  
  //Step3. Verify File downloaded successfully or not
  AssertClass.IsTrue(IECBrowserCommonMethod.CheckTraceLog("Copied with success."))
}