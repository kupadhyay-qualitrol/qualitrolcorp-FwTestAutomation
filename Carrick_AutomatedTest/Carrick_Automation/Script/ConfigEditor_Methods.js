//This file contains methods and objects variable related to device Management page on iQ+ UI.

//USEUNIT DataRetrievalPage
//USEUNIT AssertClass
//USEUNIT CommonMethod
//USEUNIT ConfigEditor_ConfigurationPage
//USEUNIT DeviceManagementPage

function ExportConfigurationAsFile(File_Name)
{
   //Step0. Retrieve Configuration
   AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
    
   //Step1. Click on Export as a file button
   AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickExportAsFile(),"Clicked on export as a file button")
   
   //Step2. Set file name in Export configuration file popup
   AssertClass.IsTrue(ConfigEditor_ConfigurationPage.SetFileName(File_Name),"Set file name in File name text box")
   
   //Step3. Click on Save button on Export configuration file popup
   AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickOnSaveButtonExportAsFile(),"Clicked on Save button of Export configuration file popup")  
}
function ImportConfigurationAsFile(File_Name)
{
  //Step0. Retrieve Configuration
  AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
   
  //Step1. Click on Import as a file button
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickImportAsFile(),"Clicked on import as a file button")
  
  //Step2. Open configuration file and 
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.OpenConfiguratioAsFile(File_Name),"Select configuration file and click on open button")
}
function ExportConfigurationAsTemplate(Template_Name)
{
  //Step0. Retrieve Configuration
  AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
  
  //Step1. Click on Export as a Template button
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickExportAsTemplate(),"Clicked on export as a template button")
  
  //Step2. Set template name
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.SetTemplateName(Template_Name),"Set template name on template box")
}
function ImportConfigurationAsTemplate(Template_Name)
{
  //Step0. Retrieve Configuration
  AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
  
  //Step1. Click on Import as a Template button
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickImportAsTemplate(),"Clicked on Import as a template button")
  
  //Step2. Select the saved template from the template list
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.SelectTemplateFromTemplateList(Template_Name),"Selected template from the template list")
  
  //Step3. Click on Copy button for copying template
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickCopyTemplateButton(),"Clicked on Copy template button")
}
function ImportConfigFromOtherDevice(devicetype,deviceName)
{
  //Step0. Retrieve Configuration
  AssertClass.IsTrue(DeviceManagementPage.ClickonRetrieveConfig(),"Clicked on Retrieve Config")
  
  //Step1. Click on Import from other device button
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickImportFromOtherDevice(),"Clicked on Import from other device button")
  
  //Step2. Select the device from list of device listed in device list
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.SelectDeviceFromImportOtherDevice(devicetype,deviceName),"Selected device from the device list")
  
  //Step3. Click on Copy button for copying other device
  AssertClass.IsTrue(ConfigEditor_ConfigurationPage.ClickOnCopyButton(),"Clicked on Copy template button")
}