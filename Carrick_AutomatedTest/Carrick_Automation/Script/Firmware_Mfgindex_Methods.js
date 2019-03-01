/*This file contains methods related to Mfgindex Page of firmware*/

//This method is used to upload the calibration in the device using mfgindex page
function UploadCalibration(deviceIP,ChromeDriverInstance,CalibrationFilePath)
{
 if(deviceIP!=null && ChromeDriverInstance!=null && CalibrationFilePath!=null)
 {
  var objTestCode = dotNET.CashelFirmware_NunitTests.FirmwareCablingTest.zctor()
  var IsCalibrationUploaded = objTestCode.UploadCalbirationFile(deviceIP,ChromeDriverInstance, CalibrationFilePath)
  if(IsCalibrationUploaded)
  {
    Log.Message("Successfull uploaded Calibration file.Result:- "+IsCalibrationUploaded)  
    return true
  }
  else
  {
    Log.Message("Failed to load Calibration file.Result:- "+IsCalibrationUploaded)  
    return false
  }
 }
 else
 {
   Log.Message("Either of three are null")
   return false
 }
}