/*This file contains methods related to Tabindex Page of firmware*/

function ValidateCabling(ChromeDriverInstance,TestLog,DeviceIP,DataSetFolderPath,Cabling,FwVersion)
{
 if(DeviceIP!=null && ChromeDriverInstance!=null && DataSetFolderPath!=null && DataSetFolderPath!=null && Cabling!=null)
 {
  var objTestCode = dotNET.CashelFirmware_NunitTests.FirmwareCablingTest.zctor()
  var Validate = objTestCode.ValidateCabling(ChromeDriverInstance,DeviceIP,TestLog,Cabling,DataSetFolderPath,FwVersion)
  if(Validate)
  {
    Log.Message("Validated Cabling on Firmware.Result:- "+Validate)  
    return true
  }
  else
  {
    Log.Message("Failed to Validate Cabling on Firmware.Result:- "+Validate)  
    return false
  }
 }
 else
 {
   Log.Message("Either of input parameter are null")
   return false
 }
}