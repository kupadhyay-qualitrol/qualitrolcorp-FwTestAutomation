/*
 * $Author: L&T
 * Copyright © 2008 Qualitrol
 * All rights reserved.
 * This software is the confidential and proprietary information
 * of Qualitrol.  Copying or reproduction without prior written
 * approval is prohibited.
 */
using System;
using System.Collections.Generic;
using System.Text;
using Carrick.Framework.DTO;
using System.Data;
using Carrick.MessageBus.Framework;
using Carrick.Framework.Constants.MessageBus;
using System.Threading;
using Carrick.MessageBus.Framework.Services;
using Carrick.Server.Framework;
using System.IO;
using Carrick.Framework.Entity;
using System.Globalization;
using Carrick.Framework.Util;
using Carrick.Framework.Constants;
using Carrick.Framework.Constants.ImportConstants;
using Carrick.MessageBus.Framework.Exceptions;
using Carrick.Server.DataObjects;
using Carrick.Framework.Util.GDIWaveformViewer;
using System.Xml;
using System.Collections;
using Carrick.Framework.Constants.LookUp;

namespace Carrick.Server.BO.ImportBO
{
    //**************************************************************
    // Class Name  :  Comtrade Import
    // Purpose	   :  This class is responsible to import the comtrade file.Construct cdf and make db entry.
    //
    // Modification History:
    //  Ver #        Date      	Author/Modified By	    Remarks
    //--------------------------------------------------------------
    //   1.0        26-Feb-10  	 Ashoo Bhadoria         Initial  
    //   4.2.2      21-Jun-13  	 Srikanth H J           Import the triggered parameter info on comtrade import.  
    //   4.2.2      26-Jul-13    Ashwini R.T            Added by Ashwini to fix the bug #8144:iQ+ cannot import some COMTRADE ASCII 1999 from relay
    //   4.2.2      31-Jul-13    Deepak D               Added to fix the bug #8211:to show Last Action Status window 
    //   4.4        18-Apr-14    Ashwini R T            Added to support multiple comtrade import #8927
    //   4.6        07-Oct-14    Srikanth H J           Modified code to dispose the datatables and dataset once usage is completed (Bug #8580)
    //   4.7        23-Jan-15    Rajesh Kumar Patel     Added the code to implement DDR-T comtrade Import. OB #6285
    //************************************************************** 
    public partial class Import
    {
        #region Variables
        long lngCreatedDeviceId = -1;
        bool _boolIsFileMissing = false;
        bool _boolIsLicenced = true;
        bool _boolNoSamplesFailed = false; //Flag to specify if Import failed because of No Samples in COMTRADE file
        int NoOfComtradeFile = 0;
        // Added by Ashwini to support multiple comtrade import #8927
        char chrDelimiter = WaveformViewerConstants.COMTRADE_FILE_DELIMITER;
        string strCarriageReturn = WaveformViewerConstants.COMTRADE_FILE_CARRIAGERETURN;
        string strLineFeed = WaveformViewerConstants.COMTRADE_FILE_NEWLINEFEED;
        CultureInfo _cultureInfo = new CultureInfo("en-US");
        #endregion

        #region File Parsing & Publishing to PQBO

        /// <summary
        /// <summary>
        /// To check whether device mapping already exists
        /// </summary>
        /// <param name="cbkImportdata"></param>
        private void CheckComtradeDeviceMappingAlreadyExists(CallBack cbkImportdata)
        {
            // throw new NotImplementedException();
            ComtradeImportDTO objComtradeImportDto = (ComtradeImportDTO)cbkImportdata.PayLoad;
            if (null != objComtradeImportDto.lstDeviceInfo && objComtradeImportDto.lstDeviceInfo.Count > 0)
            {
                ReplyData replyData = PublishRequestMessage(cbkImportdata.PayLoad, Topics.DeviceManager.DeviceAdministration,
                                    MessageActions.DeviceManager.CheckComtradeDeviceAlreadyExists,
                                    ResponseTopics.DeviceManager.DeviceAdministration,
                                    ResponseMessageActions.DeviceManager.CheckComtradeDeviceAlreadyExists);

                if (replyData.PayLoad.GetType() == typeof(ComtradeImportDTO))
                {
                    objComtradeImportDto = (ComtradeImportDTO)replyData.PayLoad;
                    PublishDataMessage(cbkImportdata.RequestorTopic, ResponseMessageActions.Import.CheckMappingForComtrade, MessageType.Text, objComtradeImportDto);
                }
            }
            else
            {
                // Logs message
                MessageBusLogger.LogError("ComtradeImport::CheckComtradeDeviceMappingAlreadyExists", LogWriter.Level.INFO, "Devices are not presnt in Qis database.",
                    null, "", this.ToString());
                // Publishes error message
                PublishResponseMessage(cbkImportdata, "ACTYLOGMSG1465", ResponseMessageType.Error, true);
            }
        }

        /// <summary>
        /// Depth-first recursive delete, with handling for descendant 
        /// directories open in Windows Explorer.
        /// </summary>
        public static void DeleteDirectory(string path)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }
            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
            }
        }
        /// <summary>
        /// Method to store file in temp folder and created device if needed for scheduler
        /// Added to support multiple comtrade import #8927
        /// </summary>
        /// <param name="cbkImportdata"></param>
        private void ImportComtradeDataForMappedDevicesFromSchedular(CallBack cbkImportdata)
        {
            ImportComtradeFileDTO dtoComtradeImport = null;
            DataProcessingSchedulerDTO dtoDPS = null;
            string strComtradeFolderPath = string.Empty;
            String strPath = String.Empty;
            List<int> lstDeviceID = new List<int>();
            try
            {
                object[] objArrDTOS = (object[])cbkImportdata.PayLoad;
                dtoDPS = (DataProcessingSchedulerDTO)objArrDTOS[0];
                dtoComtradeImport = (ImportComtradeFileDTO)objArrDTOS[1];
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" + dtoComtradeImport.FolderName))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory
                        + "\\Temp\\" + dtoComtradeImport.FolderName);

                }
                if (false == dtoComtradeImport.IsLastFile && _boolIsLicenced == true)
                {
                    WriteComtradeFilesToTempFolder(dtoComtradeImport.FileData, dtoComtradeImport.FolderName,
                        dtoComtradeImport.FileName);
                }
                else if (true == dtoComtradeImport.IsLastFile && _boolIsLicenced == true)
                {
                    List<string> arrFiles = new List<string>();
                    List<FileInfo> lstFiles = new List<FileInfo>();
                    strPath = AppDomain.CurrentDomain.BaseDirectory
                         + "\\Temp\\" + dtoComtradeImport.FolderName;
                    strComtradeFolderPath = GetComtradeFolderDeatilsToMove(cbkImportdata);
                    DirectoryInfo dirPath = new DirectoryInfo(strPath);
                    string[] strArrValidExtensions = new string[] { "*.cfg", "*.dat", "*.inf" };
                    foreach (string strExtension in strArrValidExtensions)
                    {
                        lstFiles.AddRange(dirPath.GetFiles(strExtension, SearchOption.AllDirectories));
                    }
                    for (int intCount = 0; intCount < lstFiles.Count; intCount++)
                    {
                        arrFiles.Add(lstFiles[intCount].FullName);
                    }
                    ParseComtradecfgAndDatFilesForSchedular(dtoComtradeImport, lstDeviceID, objArrDTOS, arrFiles);
                }
                else if (_boolIsLicenced == false)
                {
                    PublishResponseMessage(cbkImportdata, string.Empty, ResponseMessageType.Information, false);
                }
                if (lstDeviceID.Count > 1)
                {
                    string strDeviceId = string.Empty;
                    for (int intdevicecount = 0; intdevicecount < lstDeviceID.Count; intdevicecount++)
                    {
                        strDeviceId = strDeviceId + lstDeviceID[intdevicecount] + ",";
                    }
                }
            }
            catch (Exception ex)
            {
                if (NoOfComtradeFile > 1)
                {
                    MessageBusLogger.LogError("ComtradeImport::ImportComtradeDataForMappedDevicesFromSchedular", LogWriter.Level.ERROR, ex.Message,
                                 ex.InnerException, "", ToString());
                    PublishResponseMessage(cbkImportdata, "ACTYLOGMSG1462",
                                        ResponseMessageType.Error, true);
                }
                else
                {
                    MessageBusLogger.LogError("ComtradeImport::ImportComtradeDataForMappedDevicesFromSchedular", LogWriter.Level.ERROR, ex.Message,
                                                    ex.InnerException, "", ToString());
                    PublishResponseMessage(cbkImportdata, "ACTYLOGMSG1551",
                                        ResponseMessageType.Error, true);
                }
            }
            finally
            {
                if (null != dtoComtradeImport && null != dtoComtradeImport.FolderName &&
                    true == dtoComtradeImport.IsLastFile)
                {
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" +
                        dtoComtradeImport.FolderName))
                    {
                        strComtradeFolderPath = GetComtradeFolderDeatilsToMove(cbkImportdata);
                        if (!string.IsNullOrEmpty(strComtradeFolderPath) && Directory.Exists(strComtradeFolderPath))
                        {
                            MoveComtradeFilesToRelativeFolderImported(strPath, strComtradeFolderPath, dtoComtradeImport.ActualFolderName);
                            string strFolderPath = AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" +
                            dtoComtradeImport.FolderName;
                            //Directory.Delete(strFolderPath, true);
                            DeleteDirectory(strFolderPath);
                        }
                    }
                }
                NoOfComtradeFile = 0;
            }
        }

        /// <summary>
        /// To parse the comtrade files from schedular #8927
        /// </summary>
        /// <param name="dtoComtradeImport"></param>
        /// <param name="lstDeviceID"></param>
        /// <param name="objArrDTOS"></param>
        /// <param name="arrFiles"></param>
        private void ParseComtradecfgAndDatFilesForSchedular(ImportComtradeFileDTO dtoComtradeImport, List<int> lstDeviceID, object[] objArrDTOS, List<string> arrFiles)
        {
            try
            {
                #region Added to implement OB #6285
                // Making a List of Cashel DeviceTypeID to differentiate DeviceTypes. OB #6285
                List<string> lstCashelDeviceTypeId = new List<string>();
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.Cashel);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.CashelModular);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.IDMplus);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.IDME);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.INFORMA9);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.IDMPLUS9);
                #endregion Added to implement OB #6285

                for (int intTotalFiles = 0; intTotalFiles < arrFiles.Count; intTotalFiles++)
                {
                    FileInfo fnFile = new FileInfo(arrFiles[intTotalFiles]);
                    if (fnFile.Extension.ToUpper().Equals(".CFG"))
                    {
                        NoOfComtradeFile++;
                        _boolIsFileMissing = false;
                        CarrickData objCarrickData = new CarrickData();
                        DFRDataType objDfrData = new DFRDataType();
                        CSSDataTypeDataDescriptor objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                        CSSDataType objCssDataType = new CSSDataType();
                        DFRDataTypeDataDescriptor objDfrDataDescriptor = new DFRDataTypeDataDescriptor();
                        objCarrickData.CreatedBy = "IQ+";
                        objCarrickData.CreatedOn = Convert.ToString(DateTime.Now);
                        CarrickDataDeviceDescriptors objDevicedescriptors = new CarrickDataDeviceDescriptors();
                        CSSDataType objDdrtDataType = new CSSDataType();
                        string strCfgFileContent = ReadFile(arrFiles[intTotalFiles]);
                        string strTempString, strDeviceTypeID = string.Empty;

                        string[] strData = strCfgFileContent.Split(strLineFeed.ToCharArray());
                        strTempString = strData[0].Replace(strCarriageReturn, "").Trim();
                        string[] strFirstLine = strTempString.Split(chrDelimiter);
                        string strComtradeSubstationName = (strTempString.Split(chrDelimiter))[0].Trim();
                        string strComtradeDeviceName = (strTempString.Split(chrDelimiter))[1].Trim();
                        int intDeviceIndex = dtoComtradeImport.ComTradeDeviceDetails.FindIndex(p => p.ComtradeDeviceName == strComtradeDeviceName
                         && p.ComtradeSubstationName == strComtradeSubstationName && p.ComtradeDeviceId > 0);

                        if (intDeviceIndex != null && intDeviceIndex >= 0)
                        {
                            int intComtradeYear = 0;
                            int intAnalogChannelCount = 0;
                            int intDigitalChannelCount = 0;
                            int intTotalChannelCount = 0;
                            bool blnIsDDR = false;
                            if (strTempString.Split(chrDelimiter).Length == 3)
                            {
                                int.TryParse((strTempString.Split(chrDelimiter))[2].Trim(), out intComtradeYear);
                            }
                            #region Default COMTRADE standard revision 1991
                            // If not specified, COMTRADE standard revision 1991 is applied by default,                          
                            if (intComtradeYear > 1999)
                            {
                                intComtradeYear = 1999;
                            }
                            else if (intComtradeYear == 0)
                            {
                                intComtradeYear = 1991;
                            }
                            #endregion Default COMTRADE standard revision 1991

                            #region COMTRADE 1997/1999 standard

                            if (intComtradeYear == WaveformViewerConstants.COMTRADE_VERSION_SUPPORTED ||
                                intComtradeYear == 1997)
                            {
                                GetComtradeDetailsForSchedular(dtoComtradeImport, arrFiles, intDeviceIndex, intTotalFiles, ref objDfrData, ref objDfrDataDescriptor, ref objDevicedescriptors, ref strTempString, strData, intComtradeYear, ref intAnalogChannelCount, ref intDigitalChannelCount, ref intTotalChannelCount);

                                ChannelsType objChannelType;
                                ChannelsTypeAnalogChannel[] arrAnalogChannelInfo;
                                InitialiseChannelTypeFillAnalogChannelDataForSchedular(arrFiles, intTotalFiles, strTempString, strData, intAnalogChannelCount, out objChannelType, out arrAnalogChannelInfo, true);

                                //Initialise the ChannelsTypeDigitalChannel.
                                ChannelsTypeDigitalChannel[] arrDigitalChannelInfo = new ChannelsTypeDigitalChannel[intDigitalChannelCount];
                                int intDigitalChannelIndex = 0;
                                for (int intLoop = intAnalogChannelCount; intLoop < intTotalChannelCount; intLoop++)
                                {
                                    strTempString = strData[intLoop + 2].Replace(strCarriageReturn, "").Trim();
                                    //Fill digital channel details for all the digital channels present.
                                    arrDigitalChannelInfo[intDigitalChannelIndex] = new ChannelsTypeDigitalChannel();
                                    arrDigitalChannelInfo[intDigitalChannelIndex].ChannelNumber = intLoop - intAnalogChannelCount + 1;
                                    arrDigitalChannelInfo[intDigitalChannelIndex].ChannelName = (strTempString.Split(chrDelimiter))[1].Trim();
                                    arrDigitalChannelInfo[intDigitalChannelIndex].NormalState = strTempString.Split(chrDelimiter)[4].Trim();
                                    arrDigitalChannelInfo[intDigitalChannelIndex].Min = 0;
                                    arrDigitalChannelInfo[intDigitalChannelIndex].Max = 1;
                                    intDigitalChannelIndex++;
                                }

                                objChannelType.DigitalChannel = arrDigitalChannelInfo;
                                objChannelType.DigitalChannelCount = intDigitalChannelCount.ToString();
                                objCarrickData.DeviceDescriptors = objDevicedescriptors;

                                if (objDfrData.SampleRateInHz[0] < 500)
                                {
                                    blnIsDDR = true;
                                    //For DDR sampling rate will be less than 500 Hz
                                    //If inter-sample interval is less than 500Hz, record is slow-scan.
                                    //Selected to be just a little higher than half-cycle data rate at nominal power-line frequency (60Hz gives 120Hz)
                                    objCssDataType = FillCssCdfEntity(strData, intTotalChannelCount, arrFiles[intTotalFiles], intComtradeYear == 1991, (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));
                                    objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                                    objCSSDataDescriptor.ChannelDescriptions = objChannelType;
                                    objCssDataType.DataDescriptor = objCSSDataDescriptor;
                                    objCarrickData.Item = objCssDataType;
                                    objCssDataType.IsValidRecord = true;
                                    objCssDataType.BinaryDataCount = 0;

                                    if (objCssDataType.TriggerTimeSeconds == objCssDataType.StartTimeSeconds)
                                    {
                                        objCarrickData.RecordType = CarrickDataType.DDRC;
                                    }
                                    else
                                    {
                                        objCarrickData.RecordType = CarrickDataType.DDRT;
                                    }
                                }
                                else
                                {
                                    //Fill channel descriptions.
                                    objDfrDataDescriptor.ChannelDescriptions = objChannelType;
                                    //Fill data description.
                                    objDfrData.DataDescriptor = objDfrDataDescriptor;

                                    objCarrickData.Item = objDfrData;
                                    objCarrickData.RecordType = CarrickDataType.DFR;
                                }
                                string strDataFilePath = arrFiles[intTotalFiles].Remove(arrFiles[intTotalFiles].LastIndexOf('.')) + ".dat";
                                if (File.Exists(strDataFilePath))
                                {
                                    //Added code to fix bug #8211
                                    if (null != objDfrData.TotalNumberOfSamples && objDfrData.TotalNumberOfSamples.Length > 0)
                                    {
                                        string textOrBinary = strData[intTotalChannelCount + 7];

                                        // Some wide chars are not supported by the zip package, cause the resulted DFR cannot be opened.
                                        //-- Replaced by JX Peng on 25 Sep. 2013 fixing bug #5977.
                                        string strTempDataFilePath = FilterDfrBinaryDataFileName(arrFiles[intTotalFiles]) + "Temp.dat";

                                        byte[] bytArrBinaryData =
                                            ParseDataFile(strDataFilePath, textOrBinary, int.Parse(objChannelType.AnalogChannelCount),
                                            int.Parse(objChannelType.DigitalChannelCount), objDfrData.TotalNumberOfSamples[0],
                                            arrAnalogChannelInfo, arrDigitalChannelInfo, _cultureInfo, strTempDataFilePath, arrFiles[intTotalFiles].Remove(arrFiles[intTotalFiles].LastIndexOf('.')) + ".dat");
                                        if (null != bytArrBinaryData)
                                        {
                                            objCarrickData.BinaryData = bytArrBinaryData;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    _boolIsFileMissing = true;
                                }
                                if (false == _boolIsFileMissing)
                                {
                                    #region Commented Code
                                    //((ImportComtradeFileDTO)objArrDTOS[1]).DeviceDetails[0].Deviceid = objDevicedescriptors.DeviceDescriptor[0].DeviceID;
                                    //((ImportComtradeFileDTO)objArrDTOS[1]).FileName = fnFile.Name;

                                    //((ImportComtradeFileDTO)objArrDTOS[1]).DeviceDetails[0].Deviceid = objDevicedescriptors.DeviceDescriptor[0].DeviceID;
                                    //((ImportComtradeFileDTO)objArrDTOS[1]).FileName = fnFile.Name;
                                    //PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.UpdateDPSHistoryForComtradeImport,
                                    //    MessageType.Binary, objArrDTOS); 
                                    #endregion Commented Code
                                    object[] objArrParams = new object[3];
                                    objArrParams[0] = objArrDTOS[0];
                                    objArrParams[1] = objDevicedescriptors.DeviceDescriptor[0].DeviceID;
                                    objArrParams[2] = fnFile.Name;
                                    PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.UpdateDPSHistoryForComtradeImport,
                                        MessageType.Binary, objArrParams);
                                    EntityList<CarrickData> entylstCarrickData = new EntityList<CarrickData>();
                                    //entylstCarrickData.Add(objCarrickData);
                                    if (blnIsDDR)
                                    {
                                        objCssDataType.SegmentStartTimeSeconds = (uint)objCssDataType.StartTimeSecondsLocal;
                                        objCssDataType.SegmentEndTimeinSeconds = (uint)objCssDataType.EndTimeSecondsLocal;
                                        objCssDataType.BinaryDataCount = 0;
                                        objCssDataType.LastRecord = true;

                                        if (objCarrickData.RecordType == CarrickDataType.DDRC)
                                        {
                                            objCssDataType.RecordStatus = EventType.SSS.GetHashCode();

                                            // Filling CSS entity.
                                            entylstCarrickData.Add(objCarrickData);

                                            PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportCSS,
                                              MessageType.Binary, entylstCarrickData);
                                        }
                                        else
                                        {
                                            objCssDataType.RecordStatus = EventType.TSS.GetHashCode();

                                            // Fetching DeviceTypeID from DB. OB #6285
                                            strDeviceTypeID = DataBaseManagerDataObject.GetDeviceTypeID(
                                                entylstCarrickData[0].DeviceDescriptors.DeviceDescriptor[0].DeviceID);

                                            // Checking if Cashel Device is available for COMTRADE Import. OB #6285
                                            if (lstCashelDeviceTypeId.Contains(strDeviceTypeID))
                                            {
                                                objDdrtDataType = FillDdrtCdfEntity(strData, intTotalChannelCount,
                                                arrFiles[intTotalFiles], intComtradeYear == 1991,
                                                (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));

                                                // As 'CSSDataTypeDataDescriptor' class is same for both DDR-C and DDR-T
                                                // hence using same class here
                                                objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                                                objCSSDataDescriptor.ChannelDescriptions = objChannelType;
                                                objDdrtDataType.DataDescriptor = objCSSDataDescriptor;
                                                objCarrickData.Item = objDdrtDataType;
                                                objDdrtDataType.IsValidRecord = true;
                                                objDdrtDataType.BinaryDataCount = 0;

                                                objDdrtDataType.SegmentStartTimeSeconds = (uint)objDdrtDataType.StartTimeSecondsLocal;
                                                objDdrtDataType.SegmentEndTimeinSeconds = (uint)objDdrtDataType.EndTimeSecondsLocal;
                                                objDdrtDataType.BinaryDataCount = 0;
                                                objDdrtDataType.LastRecord = true;

                                                // Filling DDR-T entity.
                                                entylstCarrickData.Add(objCarrickData);

                                                //Changed MessageAction to redirect to DDR-T record insertion. OB #6285
                                                PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportDDRT,
                                                 MessageType.Binary, entylstCarrickData);
                                            }
                                            else
                                            {
                                                // Filling CSS entity.
                                                entylstCarrickData.Add(objCarrickData);

                                                PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportCSS,
                                                 MessageType.Binary, entylstCarrickData);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Filling DFR entity.
                                        entylstCarrickData.Add(objCarrickData);

                                        PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportDFRForScheduler,
                                            MessageType.Binary, entylstCarrickData);
                                    }
                                    if (!lstDeviceID.Contains(objDevicedescriptors.DeviceDescriptor[0].DeviceID))
                                        lstDeviceID.Add(objDevicedescriptors.DeviceDescriptor[0].DeviceID);
                                }
                                else
                                {
                                    //UpdateComtradeImportStatistics("ACTYLOGMSG1467", "", "", "", 0, 0, cbkImportdata);
                                }
                            }
                            #endregion
                            #region COMTRADE 1991 standard
                            else if (intComtradeYear == 1991)
                            {
                                GetComtradeDetailsForSchedular(dtoComtradeImport, arrFiles, intDeviceIndex, intTotalFiles, ref objDfrData, ref objDfrDataDescriptor, ref objDevicedescriptors, ref strTempString, strData, intComtradeYear, ref intAnalogChannelCount, ref intDigitalChannelCount, ref intTotalChannelCount);

                                ChannelsType objChannelType;
                                ChannelsTypeAnalogChannel[] arrAnalogChannelInfo;
                                InitialiseChannelTypeFillAnalogChannelDataForSchedular(arrFiles, intTotalFiles, strTempString, strData, intAnalogChannelCount, out objChannelType, out arrAnalogChannelInfo, false);

                                //Initialise the ChannelsTypeDigitalChannel.
                                ChannelsTypeDigitalChannel[] arrDigitalChannelInfo = new ChannelsTypeDigitalChannel[intDigitalChannelCount];
                                int intDigitalChannelIndex = 0;
                                for (int intLoop = intAnalogChannelCount; intLoop < intTotalChannelCount; intLoop++)
                                {
                                    strTempString = strData[intLoop + 2].Replace(strCarriageReturn, "").Trim();
                                    //Fill digital channel details for all the digital channels present.
                                    arrDigitalChannelInfo[intDigitalChannelIndex] = new ChannelsTypeDigitalChannel();
                                    arrDigitalChannelInfo[intDigitalChannelIndex].ChannelNumber = intLoop;
                                    arrDigitalChannelInfo[intDigitalChannelIndex].ChannelName = (strTempString.Split(chrDelimiter))[1].Trim();
                                    //if (strTempString.Split(chrDelimiter).Length == 5)
                                    //{
                                    //    arrDigitalChannelInfo[intDigitalChannelIndex].NormalState = strTempString.Split(chrDelimiter)[4].Trim();
                                    //}
                                    if ((strTempString.Split(chrDelimiter)).Length > 3)
                                    {
                                        arrDigitalChannelInfo[intDigitalChannelIndex].NormalState = strTempString.Split(chrDelimiter)[4].Trim();
                                    }
                                    else
                                    {
                                        arrDigitalChannelInfo[intDigitalChannelIndex].NormalState = strTempString.Split(chrDelimiter)[2].Trim();
                                    }

                                    arrDigitalChannelInfo[intDigitalChannelIndex].Min = 0;
                                    arrDigitalChannelInfo[intDigitalChannelIndex].Max = 1;
                                    intDigitalChannelIndex++;
                                }

                                objChannelType.DigitalChannel = arrDigitalChannelInfo;
                                objChannelType.DigitalChannelCount = intDigitalChannelCount.ToString();

                                objCarrickData.DeviceDescriptors = objDevicedescriptors;

                                if (objDfrData.SampleRateInHz[0] < 500)
                                {

                                    blnIsDDR = true;
                                    //For DDR sampling rate will be less than 500 Hz
                                    //If inter-sample interval is less than 500Hz, record is slow-scan.
                                    //Selected to be just a little higher than half-cycle data rate at nominal power-line frequency (60Hz gives 120Hz)
                                    objCssDataType = FillCssCdfEntity(strData, intTotalChannelCount, arrFiles[intTotalFiles], intComtradeYear == 1991, (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));
                                    objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                                    objCSSDataDescriptor.ChannelDescriptions = objChannelType;
                                    objCssDataType.DataDescriptor = objCSSDataDescriptor;
                                    objCarrickData.Item = objCssDataType;
                                    objCssDataType.IsValidRecord = true;
                                    objCssDataType.BinaryDataCount = 0;

                                    if (objCssDataType.TriggerTimeSeconds == objCssDataType.StartTimeSeconds)
                                    {
                                        objCarrickData.RecordType = CarrickDataType.DDRC;
                                    }
                                    else
                                    {
                                        objCarrickData.RecordType = CarrickDataType.DDRT;
                                    }
                                }
                                else
                                {
                                    //Fill channel descriptions.
                                    objDfrDataDescriptor.ChannelDescriptions = objChannelType;
                                    //Fill data description.
                                    objDfrData.DataDescriptor = objDfrDataDescriptor;

                                    objCarrickData.Item = objDfrData;
                                    objCarrickData.RecordType = CarrickDataType.DFR;
                                }
                                string strDataFilePath = arrFiles[intTotalFiles].Remove(arrFiles[intTotalFiles].LastIndexOf('.')) + ".dat";
                                if (File.Exists(strDataFilePath))
                                {
                                    if (null != objDfrData.TotalNumberOfSamples && objDfrData.TotalNumberOfSamples.Length > 0)
                                    {
                                        string textOrBinary = strData[intTotalChannelCount + 7];
                                        // Some wide chars are not supported by the zip package, cause the resulted DFR cannot be opened.
                                        //-- Replaced by JX Peng on 25 Sep. 2013 fixing bug #5977.
                                        string strTempDataFilePath = FilterDfrBinaryDataFileName(arrFiles[intTotalFiles]) + "Temp.dat";

                                        byte[] bytArrBinaryData =
                                            ParseDataFile(strDataFilePath, textOrBinary, int.Parse(objChannelType.AnalogChannelCount),
                                            int.Parse(objChannelType.DigitalChannelCount), objDfrData.TotalNumberOfSamples[0],
                                            arrAnalogChannelInfo, arrDigitalChannelInfo, _cultureInfo, strTempDataFilePath, arrFiles[intTotalFiles].Remove(arrFiles[intTotalFiles].LastIndexOf('.')) + ".dat");
                                        if (null != bytArrBinaryData)
                                        {
                                            objCarrickData.BinaryData = bytArrBinaryData;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    _boolIsFileMissing = true;
                                }
                                if (false == _boolIsFileMissing)
                                {
                                    #region Commented code
                                    //((ImportComtradeFileDTO)objArrDTOS[1]).DeviceDetails[0].Deviceid = objDevicedescriptors.DeviceDescriptor[0].DeviceID;
                                    //((ImportComtradeFileDTO)objArrDTOS[1]).FileName = fnFile.Name;
                                    //PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.UpdateDPSHistoryForComtradeImport,
                                    //    MessageType.Binary, objArrDTOS); 
                                    #endregion Commented code
                                    object[] objArrParams = new object[2];
                                    objArrParams[0] = objArrDTOS[0];
                                    objArrParams[0] = objDevicedescriptors.DeviceDescriptor[0].DeviceID;
                                    objArrParams[1] = fnFile.Name;
                                    PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.UpdateDPSHistoryForComtradeImport,
                                        MessageType.Binary, objArrParams);

                                    EntityList<CarrickData> entylstCarrickData = new EntityList<CarrickData>();
                                    //entylstCarrickData.Add(objCarrickData);
                                    if (blnIsDDR)
                                    {
                                        objCssDataType.SegmentStartTimeSeconds = (uint)objCssDataType.StartTimeSecondsLocal;
                                        objCssDataType.SegmentEndTimeinSeconds = (uint)objCssDataType.EndTimeSecondsLocal;
                                        objCssDataType.BinaryDataCount = 0;
                                        objCssDataType.LastRecord = true;

                                        if (objCarrickData.RecordType == CarrickDataType.DDRC)
                                        {
                                            objCssDataType.RecordStatus = EventType.SSS.GetHashCode();

                                            // Filling CSS entity.
                                            entylstCarrickData.Add(objCarrickData);

                                            PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportCSS,
                                              MessageType.Binary, entylstCarrickData);
                                        }
                                        else
                                        {
                                            objCssDataType.RecordStatus = EventType.TSS.GetHashCode();

                                            // Fetching DeviceTypeID from DB. OB #6285
                                            strDeviceTypeID = DataBaseManagerDataObject.GetDeviceTypeID(
                                                entylstCarrickData[0].DeviceDescriptors.DeviceDescriptor[0].DeviceID);

                                            // Checking if Cashel Device is available for COMTRADE Import. OB #6285
                                            if (lstCashelDeviceTypeId.Contains(strDeviceTypeID))
                                            {
                                                objDdrtDataType = FillDdrtCdfEntity(strData, intTotalChannelCount,
                                                arrFiles[intTotalFiles], intComtradeYear == 1991,
                                                (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));

                                                // As 'CSSDataTypeDataDescriptor' class is same for both DDR-C and DDR-T
                                                // hence using same class here
                                                objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                                                objCSSDataDescriptor.ChannelDescriptions = objChannelType;
                                                objDdrtDataType.DataDescriptor = objCSSDataDescriptor;
                                                objCarrickData.Item = objDdrtDataType;
                                                objDdrtDataType.IsValidRecord = true;
                                                objDdrtDataType.BinaryDataCount = 0;

                                                objDdrtDataType.SegmentStartTimeSeconds = (uint)objDdrtDataType.StartTimeSecondsLocal;
                                                objDdrtDataType.SegmentEndTimeinSeconds = (uint)objDdrtDataType.EndTimeSecondsLocal;
                                                objDdrtDataType.BinaryDataCount = 0;
                                                objDdrtDataType.LastRecord = true;

                                                // Filling DDR-T entity.
                                                entylstCarrickData.Add(objCarrickData);

                                                //Changed MessageAction to redirect to DDR-T record insertion. OB #6285
                                                PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportDDRT,
                                                 MessageType.Binary, entylstCarrickData);
                                            }
                                            else
                                            {
                                                // Filling CSS entity.
                                                entylstCarrickData.Add(objCarrickData);

                                                PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportCSS,
                                                 MessageType.Binary, entylstCarrickData);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Filling DFR entity.
                                        entylstCarrickData.Add(objCarrickData);

                                        PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportDFRForScheduler,
                                            MessageType.Binary, entylstCarrickData);
                                    }

                                    if (!lstDeviceID.Contains(objDevicedescriptors.DeviceDescriptor[0].DeviceID))
                                        lstDeviceID.Add(objDevicedescriptors.DeviceDescriptor[0].DeviceID);
                                }
                                else
                                {
                                    //UpdateComtradeImportStatistics("ACTYLOGMSG1467", "", "", "", 0, 0, cbkImportdata);
                                }

                                if (File.Exists(arrFiles[intTotalFiles]))
                                {
                                    File.Move(arrFiles[intTotalFiles], Path.ChangeExtension(arrFiles[intTotalFiles], ".scfg"));
                                }
                                string strInfFilePath = arrFiles[intTotalFiles].Remove(arrFiles[intTotalFiles].LastIndexOf('.')) + ".inf";
                                if (File.Exists(strInfFilePath))
                                {
                                    File.Move(strInfFilePath, Path.ChangeExtension(arrFiles[intTotalFiles], ".sinf"));
                                }
                                string strComDataFilePath = arrFiles[intTotalFiles].Remove(arrFiles[intTotalFiles].LastIndexOf('.')) + ".dat";
                                if (File.Exists(strComDataFilePath))
                                {
                                    File.Move(strComDataFilePath, Path.ChangeExtension(arrFiles[intTotalFiles], ".sdat"));
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::ParseComtradecfgAndDatFilesForSchedular", LogWriter.Level.ERROR, ex.Message,
                                                   ex.InnerException, "", ToString());
            }
        }

        private void InitialiseChannelTypeFillAnalogChannelDataForSchedular(List<string> arrFiles, int intTotalFiles, string strTempString,
            string[] strData, int intAnalogChannelCount, out ChannelsType objChannelType, out ChannelsTypeAnalogChannel[] arrAnalogChannelInfo, bool Is1997)
        {
            //Create instance of Channelstype.
            objChannelType = new ChannelsType();
            //Initialise the ChannelsTypeAnalogChannel.
            arrAnalogChannelInfo = new ChannelsTypeAnalogChannel[intAnalogChannelCount];

            for (int intChannelLoop = 0; intChannelLoop < intAnalogChannelCount; intChannelLoop++)
            {
                //Fill analog channel details for all the analog channels present.
                arrAnalogChannelInfo[intChannelLoop] = new ChannelsTypeAnalogChannel();
                strTempString = strData[intChannelLoop + 2].Replace(strCarriageReturn, "").Trim();
                arrAnalogChannelInfo[intChannelLoop].ChannelName = (strTempString.Split(chrDelimiter))[1].Trim();
                if (Is1997)
                {
                    arrAnalogChannelInfo[intChannelLoop].ChannelNumber = intChannelLoop + 1;
                }
                else
                {
                    arrAnalogChannelInfo[intChannelLoop].ChannelNumber = intChannelLoop;
                }
                arrAnalogChannelInfo[intChannelLoop].ChannelUnit = (strTempString.Split(chrDelimiter))[4].Trim();
                arrAnalogChannelInfo[intChannelLoop].ChannelMultiplier =
                     float.Parse((strTempString.Split(chrDelimiter))[5].Trim(), _cultureInfo);
                arrAnalogChannelInfo[intChannelLoop].ChannelOffsetAdder =
                     float.Parse((strTempString.Split(chrDelimiter))[6].Trim(), _cultureInfo);
                arrAnalogChannelInfo[intChannelLoop].MaxValue =
                    (float.Parse((strTempString.Split(chrDelimiter))[9].Trim(), _cultureInfo)
                    * arrAnalogChannelInfo[intChannelLoop].ChannelMultiplier) + arrAnalogChannelInfo[intChannelLoop].ChannelOffsetAdder;
                arrAnalogChannelInfo[intChannelLoop].MinValue =
                    (float.Parse((strTempString.Split(chrDelimiter))[8].Trim(), _cultureInfo)
                    * arrAnalogChannelInfo[intChannelLoop].ChannelMultiplier) + arrAnalogChannelInfo[intChannelLoop].ChannelOffsetAdder;
                //arrAnalogChannelInfo[intLoop].NominalValue = entyViewerCDF.AnalogInfo[intLoop].DataMin;
                string strInfFilePath = arrFiles[intTotalFiles].Remove(arrFiles[intTotalFiles].LastIndexOf('.')) + ".inf";
                if (File.Exists(strInfFilePath))
                {
                    Carrick.Framework.Util.InIParser infFile = new Carrick.Framework.Util.InIParser(strInfFilePath);
                    if (!string.IsNullOrEmpty(infFile.GetSetting("[" +
                    WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                       , WaveformViewerConstants.AnalogChannelFSD)))
                    {
                        arrAnalogChannelInfo[intChannelLoop].FSD =
                            float.Parse(infFile.GetSetting("[" +
                        WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                           , WaveformViewerConstants.AnalogChannelFSD));
                    }
                    else
                    {
                        arrAnalogChannelInfo[intChannelLoop].FSD = 0f;
                    }
                    arrAnalogChannelInfo[intChannelLoop].PhaseLabel = infFile.GetSetting("[" +
                    WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                       , WaveformViewerConstants.AnalogChannelPhaseLabel);

                    ChannelsTypeAnalogChannelTxRatio ctaTxRatio = new ChannelsTypeAnalogChannelTxRatio();
                    ctaTxRatio.Secondary = 0F;
                    ctaTxRatio.Primary = 0F;
                    arrAnalogChannelInfo[intChannelLoop].TxRatio = ctaTxRatio;
                    arrAnalogChannelInfo[intChannelLoop].FeederName = infFile.GetSetting("[" +
                    WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                       , WaveformViewerConstants.AnalogChannelFeederName);

                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.
                    PublicRecordInformation, WaveformViewerConstants.Lower32ChannelStatus)))
                    {
                        objChannelType.Lower32DigitalChannelStatus
                            = (uint)Convert.ToDouble(infFile.GetSetting(WaveformViewerConstants.
                        PublicRecordInformation, WaveformViewerConstants.Lower32ChannelStatus));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.
                        PublicRecordInformation, WaveformViewerConstants.Upper32ChannelStatus)))
                    {
                        objChannelType.Upper32DigitalChannelStatus
                            = (uint)Convert.ToDouble(infFile.GetSetting(WaveformViewerConstants.
                        PublicRecordInformation, WaveformViewerConstants.Upper32ChannelStatus));
                    }
                }
                else
                {
                    //_boolIsFileMissing = true;
                }
            }
            objChannelType.AnalogChannel = arrAnalogChannelInfo;
            objChannelType.AnalogChannelCount = intAnalogChannelCount.ToString();

        }

        private void GetComtradeDetailsForSchedular(ImportComtradeFileDTO dtoComtradeImport, List<string> arrFiles, int intDeviceIndex, int intTotalFiles, ref DFRDataType objDfrData, ref DFRDataTypeDataDescriptor objDfrDataDescriptor, ref CarrickDataDeviceDescriptors objDevicedescriptors, ref string strTempString, string[] strData, int intComtradeYear, ref int intAnalogChannelCount, ref int intDigitalChannelCount, ref int intTotalChannelCount)
        {
            string[] strArrFileName = arrFiles[intTotalFiles].Split('\\');

            //Parse TotalNumber of channels.
            strTempString = strData[1].Replace(strCarriageReturn, "").Trim();
            intTotalChannelCount = int.Parse((strTempString.Split(chrDelimiter))[0].Trim(), _cultureInfo);

            intAnalogChannelCount = int.Parse((strTempString.Split(chrDelimiter))[1].
            Remove((strTempString.Split(chrDelimiter))[1].Length - 1).Trim(), _cultureInfo);

            intDigitalChannelCount =
                int.Parse((strTempString.Split(chrDelimiter))[2].Remove
                ((strTempString.Split(chrDelimiter))[2].Length - 1).Trim(), _cultureInfo);

            intTotalChannelCount = intAnalogChannelCount + intDigitalChannelCount;

            if (dtoComtradeImport.DeviceCreationType == 2)
            {
                objDevicedescriptors =
                    FillEntityForExistingComtradeDevice(intTotalChannelCount, strCarriageReturn,
                    _cultureInfo, (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]), strData);
            }


            objDfrData = new DFRDataType();
            //Create instance of DFRDataTypeDataDescriptor.
            objDfrDataDescriptor = new DFRDataTypeDataDescriptor();
            objDfrData = FillDfrCdfEntity(strData, intTotalChannelCount,
                 arrFiles[intTotalFiles], intComtradeYear == 1991, (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));
        }

        /// <summary>
        /// Method to store file in temp folder and import data from cfg, .dat files
        /// </summary>
        /// <param name="cbkImportdata"></param>
        private void ImportComtradeDataForAllDevicesFromCFG(CallBack cbkImportdata)
        {
            ImportComtradeFileDTO dtoComtradeImport = null;
            string strComtradeFolderPath = string.Empty;
            String strPath = String.Empty;
            try
            {
                dtoComtradeImport = (ImportComtradeFileDTO)cbkImportdata.PayLoad;

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" + dtoComtradeImport.FolderName))
                {
                    UpdateComtradeImportStatistics("ACTYLOGMSG1463", "", "", "", 0, 0, cbkImportdata);
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory
                        + "\\Temp\\" + dtoComtradeImport.FolderName);
                    UpdateComtradeImportStatistics("ACTYLOGMSG1468", "", "", "", 0, 0, cbkImportdata);

                }
                if (false == dtoComtradeImport.IsLastFile && _boolIsLicenced == true)
                {
                    WriteComtradeFilesToTempFolder(dtoComtradeImport.FileData, dtoComtradeImport.FolderName,
                    dtoComtradeImport.FileName);
                }
                else if (true == dtoComtradeImport.IsLastFile && _boolIsLicenced == true)
                {

                    Thread.Sleep(5000);
                    UpdateComtradeImportStatistics("ACTYLOGMSG1469", "", "", "", 0, 0, cbkImportdata);
                    List<string> lstFiles = new List<string>();
                    List<FileInfo> arrFiles = new List<FileInfo>();
                    strPath = AppDomain.CurrentDomain.BaseDirectory
                         + "\\Temp\\" + dtoComtradeImport.FolderName;

                    strComtradeFolderPath = GetComtradeFolderDeatilsToMove(cbkImportdata);

                    DirectoryInfo dirPath = new DirectoryInfo(strPath);
                    string[] strArrValidExtensions = new string[] { "*.cfg", "*.dat", "*.inf" };
                    foreach (string strExtension in strArrValidExtensions)
                    {
                        arrFiles.AddRange(dirPath.GetFiles(strExtension, SearchOption.AllDirectories));
                    }
                    for (int intCount = 0; intCount < arrFiles.Count; intCount++)
                    {
                        lstFiles.Add(arrFiles[intCount].FullName);
                    }
                    ParseComtradecfgAndDatFiles(cbkImportdata, dtoComtradeImport, lstFiles);

                    // If no run-time exception is thrown but COMTRADE import is aborted for some failed Validation (Example: No Samples Information), the import failed message has to be posted to Client Session Log.
                    if (_boolNoSamplesFailed)
                    { 
                        UpdateComtradeImportStatistics("ACTYLOGMSG14692", "", "", "", 1, 0, cbkImportdata);
                        _boolNoSamplesFailed = false;
                    }
                    else
                    {
                        UpdateComtradeImportStatistics("ACTYLOGMSG1461", "", "", "", 1, 0, cbkImportdata);
                    }
                }
                else if (_boolIsLicenced == false)
                {
                    PublishResponseMessage(cbkImportdata, string.Empty, ResponseMessageType.Information, false);
                }
            }
            catch (Exception ex)
            {
                if (NoOfComtradeFile > 1)
                {
                    MessageBusLogger.LogError("ComtradeImport::CreateDeviceAndStoreFiles", LogWriter.Level.ERROR, ex.Message,
                                 ex.InnerException, "", ToString());
                    //Some COMTRADE files were imported successfully and some COMTRADE files import failed.
                    PublishResponseMessage(cbkImportdata, "ACTYLOGMSG1462",
                                        ResponseMessageType.Error, true);
                }
                else
                {
                    MessageBusLogger.LogError("ComtradeImport::CreateDeviceAndStoreFiles", LogWriter.Level.ERROR, ex.Message,
                                                    ex.InnerException, "", ToString());
                    //COMTRADE file import failed, Please try again.
                    PublishResponseMessage(cbkImportdata, "ACTYLOGMSG1551",
                                        ResponseMessageType.Error, true);
                }
            }
            finally
            {
                if (null != dtoComtradeImport && null != dtoComtradeImport.FolderName &&
                    true == dtoComtradeImport.IsLastFile)
                {

                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" +
                        dtoComtradeImport.FolderName))
                    {

                        MoveComtradeFilesToRelativeFolderImported(strPath, strComtradeFolderPath, dtoComtradeImport.ActualFolderName);
                        string strFolderPath = AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" +
                        dtoComtradeImport.FolderName;
                        //Directory.Delete(strFolderPath, true);
                        DeleteDirectory(strFolderPath);
                    }
                }
                NoOfComtradeFile = 0;
            }


        }


        /// <summary>
        /// Moves the Files to the relative folder in the same hirarchy for #8927
        /// </summary>
        /// <param name="strSourceComtradeFolderPath"></param>
        /// <param name="strDstnComtradeFolderPath"></param>
        /// <param name="strActualComtradefolder"></param>
        private void MoveComtradeFilesToRelativeFolderImported(string strSourceComtradeFolderPath, string strDstnComtradeFolderPath, string strActualComtradefolder)
        {
            if (!string.IsNullOrEmpty(strDstnComtradeFolderPath))
            {
                String strImportdFolder = Path.Combine(strDstnComtradeFolderPath, "Imported");
                String strFailedFolder = Path.Combine(strDstnComtradeFolderPath, "Failed");
                DirectoryInfo Importsource = new DirectoryInfo(strSourceComtradeFolderPath);
                List<FileInfo> lstFiles = new List<FileInfo>();
                string[] strArrSucessfullExtensions = new string[] { "*.scfg", "*.sdat", "*.sinf" };
                string[] strArrFailureExtensions = new string[] { "*.cfg", "*.dat", "*.inf" };
                DirectoryInfo dirPath = new DirectoryInfo(strSourceComtradeFolderPath);
                foreach (string strExtension in strArrSucessfullExtensions)
                {
                    lstFiles.AddRange(dirPath.GetFiles(strExtension, SearchOption.AllDirectories));
                }
                if (lstFiles.Count > 0)
                {
                    if (!Directory.Exists(strImportdFolder + "\\" + strActualComtradefolder))
                    {
                        Directory.CreateDirectory(strImportdFolder + "\\" + strActualComtradefolder);

                    }
                    lstFiles.Clear();
                    DirectoryInfo Importeddestination = new DirectoryInfo(strImportdFolder + "\\" + strActualComtradefolder);
                    //DirectoryInfo Importeddestination = new DirectoryInfo(strImportdFolder);
                    CopyAllSucessfullComtradeRecords(Importsource, Importeddestination);
                }

                foreach (string strExtension in strArrFailureExtensions)
                {
                    lstFiles.AddRange(dirPath.GetFiles(strExtension, SearchOption.AllDirectories));
                }
                if (lstFiles.Count > 0)
                {
                    if (!Directory.Exists(strFailedFolder + "\\" + strActualComtradefolder))
                    {
                        Directory.CreateDirectory(strFailedFolder + "\\" + strActualComtradefolder);
                    }
                    DirectoryInfo Faileddestination = new DirectoryInfo(strFailedFolder + "\\" + strActualComtradefolder);
                    //DirectoryInfo Faileddestination = new DirectoryInfo(strFailedFolder);   
                    CopyAllFailureComtradeRecords(Importsource, Faileddestination);
                }

            }


        }


        /// <summary>
        /// Copies all sucessfully imported files
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void CopyAllSucessfullComtradeRecords(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            FileInfo[] cfgfiles = source.GetFiles("*.scfg");
            // Copy each file into it’s new directory.
            foreach (FileInfo fi in cfgfiles)
            {
                //Console.WriteLine(@”Copying {0}\{1}”, target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name.Remove(fi.Name.LastIndexOf('.')) + ".cfg"), true);
            }

            FileInfo[] datfiles = source.GetFiles("*.sdat");
            // Copy each file into it’s new directory.
            foreach (FileInfo fi in datfiles)
            {
                //Console.WriteLine(@”Copying {0}\{1}”, target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name.Remove(fi.Name.LastIndexOf('.')) + ".dat"), true);
            }

            FileInfo[] inffiles = source.GetFiles("*.sinf");
            // Copy each file into it’s new directory.
            foreach (FileInfo fi in inffiles)
            {
                //Console.WriteLine(@”Copying {0}\{1}”, target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name.Remove(fi.Name.LastIndexOf('.')) + ".inf"), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAllSucessfullComtradeRecords(diSourceSubDir, nextTargetSubDir);
            }
        }
        
        /// <summary>
        /// Copies the files which are failed to import
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void CopyAllFailureComtradeRecords(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            FileInfo[] cfgfiles = source.GetFiles("*.cfg");
            // Copy each file into it’s new directory.
            foreach (FileInfo fi in cfgfiles)
            {
                //Console.WriteLine(@”Copying {0}\{1}”, target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            FileInfo[] datfiles = source.GetFiles("*.dat");
            // Copy each file into it’s new directory.
            foreach (FileInfo fi in datfiles)
            {
                //Console.WriteLine(@”Copying {0}\{1}”, target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            FileInfo[] inffiles = source.GetFiles("*.inf");
            // Copy each file into it’s new directory.
            foreach (FileInfo fi in inffiles)
            {
                //Console.WriteLine(@”Copying {0}\{1}”, target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAllFailureComtradeRecords(diSourceSubDir, nextTargetSubDir);
            }
        }
        
        private string GetComtradeFolderDeatilsToMove(CallBack cbkImportdata)
        {

            string strComtradeSettings = string.Empty;
            string strComtradeFolderPath = string.Empty;
            ReplyData replyData = PublishRequestMessage(cbkImportdata.PayLoad, Topics.FR.DFRDataRequest,
                                  MessageActions.FR.GetComtradeSettings,
                                  ResponseTopics.FR.DFRDataRequest,
                                  ResponseMessageActions.FR.GetComtradeSettings);

            if (replyData.PayLoad.GetType() == typeof(string))
            {
                strComtradeSettings = (string)replyData.PayLoad;

            }
            if (!string.IsNullOrEmpty(strComtradeSettings))
            {

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(strComtradeSettings);
                XmlNode xmlNode;
                xmlNode = xDoc.SelectSingleNode("AutoCOMTRADE/COMTRADEImportFolderPath");
                if (xmlNode != null)
                {
                    strComtradeFolderPath = xmlNode.InnerText;
                }
            }
            else
            {
                strComtradeFolderPath = ApplicationConfiguration.RelativeDataPath + "Comtrade_Import";
            }
            if (!Directory.Exists(strComtradeFolderPath))
            {
                Directory.CreateDirectory(strComtradeFolderPath);

            }
            if (Directory.Exists(strComtradeFolderPath))
            {
                String strImportdFolder = Path.Combine(strComtradeFolderPath, "Imported");
                if (!Directory.Exists(strImportdFolder))
                {
                    Directory.CreateDirectory(strImportdFolder);
                }
                String strFailedFolder = Path.Combine(strComtradeFolderPath, "Failed");
                if (!Directory.Exists(strFailedFolder))
                {
                    Directory.CreateDirectory(strFailedFolder);
                }

            }
            return strComtradeFolderPath;
        }
        
        /// <summary>
        /// To parse comtrade file for manual import #8927
        /// </summary>
        /// <param name="cbkImportdata"></param>
        /// <param name="dtoComtradeImport"></param>
        /// <param name="lstFiles"></param>
        private void ParseComtradecfgAndDatFiles(CallBack cbkImportdata, ImportComtradeFileDTO dtoComtradeImport, List<string> lstFiles)
        {
            try
            {
                #region Added to implement OB #6285
                // Making a List of Cashel DeviceTypeID to differentiate DeviceTypes. OB #6285
                List<string> lstCashelDeviceTypeId = new List<string>();
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.Cashel);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.CashelModular);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.IDMplus);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.IDME);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.INFORMA9);
                lstCashelDeviceTypeId.Add(LookUpConstants.DeviceType.IDMPLUS9); 
                #endregion Added to implement OB #6285
                
                for (int intTotalFiles = 0; intTotalFiles < lstFiles.Count; intTotalFiles++)
                {
                    FileInfo fnFile = new FileInfo(lstFiles[intTotalFiles]);
                    if (fnFile.Extension.ToUpper().Equals(".CFG"))
                    {
                        NoOfComtradeFile++;
                        _boolIsFileMissing = false;
                        CarrickData objCarrickData = new CarrickData();
                        DFRDataType objDfrData = new DFRDataType();
                        CSSDataTypeDataDescriptor objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                        CSSDataType objCssDataType = new CSSDataType();
                        DFRDataTypeDataDescriptor objDfrDataDescriptor = new DFRDataTypeDataDescriptor();
                        objCarrickData.CreatedBy = "IQ+";
                        objCarrickData.CreatedOn = Convert.ToString(DateTime.Now);
                        CarrickDataDeviceDescriptors objDevicedescriptors = new CarrickDataDeviceDescriptors();
                        CSSDataType objDdrtDataType = new CSSDataType();
                        string strCfgFileContent = ReadFile(lstFiles[intTotalFiles]);
                        string strTempString, strDeviceTypeID = string.Empty;

                        string[] strData = strCfgFileContent.Split(strLineFeed.ToCharArray());
                        strTempString = strData[0].Replace(strCarriageReturn, "").Trim();
                        string[] strFirstLine = strTempString.Split(chrDelimiter);
                        string strComtradeSubstationName = (strTempString.Split(chrDelimiter))[0].Trim();
                        string strComtradeDeviceName = (strTempString.Split(chrDelimiter))[1].Trim();
                        int intDeviceIndex = dtoComtradeImport.ComTradeDeviceDetails.FindIndex(p => p.ComtradeDeviceName == strComtradeDeviceName
                            && p.ComtradeSubstationName == strComtradeSubstationName && p.ComtradeDeviceId > 0);

                        if (intDeviceIndex != null && intDeviceIndex >= 0)
                        {
                            int intComtradeYear = 0;
                            int intAnalogChannelCount = 0;
                            int intDigitalChannelCount = 0;
                            int intTotalChannelCount = 0;
                            bool blnIsDDR = false;
                            if (strTempString.Split(chrDelimiter).Length == 3)
                            {
                                int.TryParse((strTempString.Split(chrDelimiter))[2].Trim(), out intComtradeYear);
                            }

                            #region Default COMTRADE standard revision 1991
                            // If not specified, COMTRADE standard revision 1991 is applied by default,                          
                            if (intComtradeYear > 1999)
                            {
                                intComtradeYear = 1999;
                            }
                            else if (intComtradeYear == 0)
                            {
                                intComtradeYear = 1991;
                            }
                            #endregion Default COMTRADE standard revision 1991

                            #region COMTRADE 1997 standard

                            //Validates COMTRADE Version
                            if (intComtradeYear == WaveformViewerConstants.COMTRADE_VERSION_SUPPORTED ||
                                intComtradeYear == 1997)
                            {
                                GetComtradeDetails(cbkImportdata, dtoComtradeImport, lstFiles, intTotalFiles, ref objDfrData, ref objDevicedescriptors, ref strTempString,
                                strData, intDeviceIndex, intComtradeYear, ref intAnalogChannelCount, ref intDigitalChannelCount, ref intTotalChannelCount);

                                objDfrDataDescriptor = new DFRDataTypeDataDescriptor();
                                //For DFR sampling rate will be more than 500 Hz
                                ChannelsType objChannelType;
                                ChannelsTypeAnalogChannel[] arrAnalogChannelInfo;
                                //Initialise the ChannelsType and fill AnalogChannel.
                                InitialiseChannelTypeFillAnalogChannelData(lstFiles, intTotalFiles, strTempString, strData, intAnalogChannelCount,
                                    out objChannelType, out arrAnalogChannelInfo, true);

                                //Initialise the ChannelsTypeDigitalChannel.
                                ChannelsTypeDigitalChannel[] arrDigitalChannelInfo = new ChannelsTypeDigitalChannel[intDigitalChannelCount];
                                int intDigitalChannelIndex = 0;
                                for (int intLoop = intAnalogChannelCount; intLoop < intTotalChannelCount; intLoop++)
                                {
                                    strTempString = strData[intLoop + 2].Replace(strCarriageReturn, "").Trim();
                                    //Fill digital channel details for all the digital channels present.
                                    arrDigitalChannelInfo[intDigitalChannelIndex] = new ChannelsTypeDigitalChannel();
                                    arrDigitalChannelInfo[intDigitalChannelIndex].ChannelNumber = intLoop - intAnalogChannelCount + 1;
                                    arrDigitalChannelInfo[intDigitalChannelIndex].ChannelName = (strTempString.Split(chrDelimiter))[1].Trim();
                                    arrDigitalChannelInfo[intDigitalChannelIndex].NormalState = strTempString.Split(chrDelimiter)[4].Trim();
                                    arrDigitalChannelInfo[intDigitalChannelIndex].Min = 0;
                                    arrDigitalChannelInfo[intDigitalChannelIndex].Max = 1;
                                    intDigitalChannelIndex++;
                                }

                                objChannelType.DigitalChannel = arrDigitalChannelInfo;
                                objChannelType.DigitalChannelCount = intDigitalChannelCount.ToString();
                                objCarrickData.DeviceDescriptors = objDevicedescriptors;
                                if (null != objDfrData.SampleRateInHz && objDfrData.SampleRateInHz.Length > 0) //Check if the imported file contains Sample Information. Else throw Error msg into ServerLog file.
                                { 
                                    if (objDfrData.SampleRateInHz[0] < 500)
                                    {
                                        blnIsDDR = true;
                                        //For DDR sampling rate will be less than 500 Hz
                                        //If inter-sample interval is less than 500Hz, record is slow-scan.
                                        //Selected to be just a little higher than half-cycle data rate at nominal power-line frequency (60Hz gives 120Hz)
                                        objCssDataType = FillCssCdfEntity(strData, intTotalChannelCount, lstFiles[intTotalFiles], intComtradeYear == 1991, (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));
                                        objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                                        objCSSDataDescriptor.ChannelDescriptions = objChannelType;
                                        objCssDataType.DataDescriptor = objCSSDataDescriptor;
                                        objCarrickData.Item = objCssDataType;
                                        objCssDataType.IsValidRecord = true;
                                        objCssDataType.BinaryDataCount = 0;

                                        if (objCssDataType.TriggerTimeSeconds == objCssDataType.StartTimeSeconds)
                                        {
                                            objCarrickData.RecordType = CarrickDataType.DDRC;
                                        }
                                        else
                                        {
                                            objCarrickData.RecordType = CarrickDataType.DDRT;
                                        }
                                    }
                                    else
                                    {
                                        //Fill channel descriptions.
                                        objDfrDataDescriptor.ChannelDescriptions = objChannelType;
                                        //Fill data description.
                                        objDfrData.DataDescriptor = objDfrDataDescriptor;

                                        objCarrickData.Item = objDfrData;
                                        objCarrickData.RecordType = CarrickDataType.DFR;
                                    }
                                }
                                else
                                {
                                    //Post error Message to Server Error Log
                                    Exception ex = new Exception("The Comtrade file does not contain proper Samples information");
                                    MessageBusLogger.LogError("ComtradeImport::ParseComtradecfgAndDatFiles", LogWriter.Level.ERROR, ex.Message,ex, "", ToString());
                                    //Publish Message to Client about the File Import status
                                    UpdateComtradeImportStatistics("ACTYLOGMSG14691", fnFile.Name, "", "", 0, 0, cbkImportdata);
                                    //Set the Flag to True
                                    _boolNoSamplesFailed = true;
                                    continue;
                                }
                                #region Data File
                                string strDataFilePath = lstFiles[intTotalFiles].Remove(lstFiles[intTotalFiles].LastIndexOf('.')) + ".dat";
                                if (File.Exists(strDataFilePath))
                                {
                                    if (null != objDfrData.TotalNumberOfSamples && objDfrData.TotalNumberOfSamples.Length > 0)
                                    {
                                        string textOrBinary = strData[intTotalChannelCount + 7];
                                        string strTempDataFilePath = FilterDfrBinaryDataFileName(lstFiles[intTotalFiles]) + "Temp.dat";

                                        byte[] bytArrBinaryData =
                                            ParseDataFile(strDataFilePath, textOrBinary, int.Parse(objChannelType.AnalogChannelCount),
                                            int.Parse(objChannelType.DigitalChannelCount), objDfrData.TotalNumberOfSamples[0],
                                            arrAnalogChannelInfo, arrDigitalChannelInfo, _cultureInfo, strTempDataFilePath, lstFiles[intTotalFiles].Remove(lstFiles[intTotalFiles].LastIndexOf('.')) + ".dat");
                                        if (null != bytArrBinaryData)
                                        {
                                            objCarrickData.BinaryData = bytArrBinaryData;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    _boolIsFileMissing = true;
                                }
                                #endregion Data File
                                if (false == _boolIsFileMissing)
                                {
                                    EntityList<CarrickData> entylstCarrickData = new EntityList<CarrickData>();
                                    //entylstCarrickData.Add(objCarrickData);
                                    UpdateComtradeImportStatistics("ACTYLOGMSG1466", "", "", "", 0, 0, cbkImportdata);
                                    if (blnIsDDR)
                                    {
                                        objCssDataType.SegmentStartTimeSeconds = (uint)objCssDataType.StartTimeSecondsLocal;
                                        objCssDataType.SegmentEndTimeinSeconds = (uint)objCssDataType.EndTimeSecondsLocal;
                                        objCssDataType.BinaryDataCount = 0;
                                        objCssDataType.LastRecord = true;

                                        if (objCarrickData.RecordType == CarrickDataType.DDRC)
                                        {
                                            objCssDataType.RecordStatus = EventType.SSS.GetHashCode();

                                            // Filling CSS entity.
                                            entylstCarrickData.Add(objCarrickData);

                                            PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportCSS,
                                              MessageType.Binary, entylstCarrickData);
                                        }
                                        else
                                        {
                                            objCssDataType.RecordStatus = EventType.TSS.GetHashCode();

                                            // Fetching DeviceTypeID from DB. OB #6285
                                            strDeviceTypeID = DataBaseManagerDataObject.GetDeviceTypeID(
                                                objCarrickData.DeviceDescriptors.DeviceDescriptor[0].DeviceID);

                                            // Checking if Cashel Device is available for COMTRADE Import. OB #6285
                                            if (lstCashelDeviceTypeId.Contains(strDeviceTypeID))
                                            {
                                                objDdrtDataType = FillDdrtCdfEntity(strData, intTotalChannelCount, 
                                                    lstFiles[intTotalFiles], intComtradeYear == 1991, 
                                                    (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));

                                                // As 'CSSDataTypeDataDescriptor' class is same for both DDR-C and DDR-T
                                                // hence using same class here
                                                objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                                                objCSSDataDescriptor.ChannelDescriptions = objChannelType;
                                                objDdrtDataType.DataDescriptor = objCSSDataDescriptor;
                                                objCarrickData.Item = objDdrtDataType;
                                                objDdrtDataType.IsValidRecord = true;
                                                objDdrtDataType.BinaryDataCount = 0;

                                                objDdrtDataType.SegmentStartTimeSeconds = (uint)objDdrtDataType.StartTimeSecondsLocal;
                                                objDdrtDataType.SegmentEndTimeinSeconds = (uint)objDdrtDataType.EndTimeSecondsLocal;
                                                objDdrtDataType.BinaryDataCount = 0;
                                                objDdrtDataType.LastRecord = true;

                                                // Filling DDR-T entity.
                                                entylstCarrickData.Add(objCarrickData);

                                                //Changed MessageAction to redirect to DDR-T record insertion. OB #6285
                                                PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportDDRT,
                                                 MessageType.Binary, entylstCarrickData);
                                            }
                                            else
                                            {
                                                // Filling CSS entity.
                                                entylstCarrickData.Add(objCarrickData);

                                                PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportCSS,
                                                 MessageType.Binary, entylstCarrickData);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Filling DFR entity.
                                        entylstCarrickData.Add(objCarrickData);

                                        PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportDFR,
                                            MessageType.Binary, entylstCarrickData);
                                    }
                                }
                                else
                                {
                                    UpdateComtradeImportStatistics("ACTYLOGMSG1467", "", "", "", 0, 0, cbkImportdata);
                                }
                                //}

                            }
                            #endregion

                            #region COMTRADE 1991 standard
                            else if (intComtradeYear == 1991)
                            {
                                GetComtradeDetails(cbkImportdata, dtoComtradeImport, lstFiles, intTotalFiles, ref objDfrData, ref objDevicedescriptors, ref strTempString, strData, intDeviceIndex, intComtradeYear, ref intAnalogChannelCount, ref intDigitalChannelCount, ref intTotalChannelCount);
                                objDfrDataDescriptor = new DFRDataTypeDataDescriptor();

                                //For DFR sampling rate will be more than 500 Hz
                                ChannelsType objChannelType;
                                ChannelsTypeAnalogChannel[] arrAnalogChannelInfo;
                                InitialiseChannelTypeFillAnalogChannelData(lstFiles, intTotalFiles, strTempString, strData, intAnalogChannelCount,
                                    out objChannelType, out arrAnalogChannelInfo, false);

                                //Initialise the ChannelsTypeDigitalChannel.
                                ChannelsTypeDigitalChannel[] arrDigitalChannelInfo = new ChannelsTypeDigitalChannel[intDigitalChannelCount];
                                int intDigitalChannelIndex = 0;
                                for (int intLoop = intAnalogChannelCount; intLoop < intTotalChannelCount; intLoop++)
                                {
                                    strTempString = strData[intLoop + 2].Replace(strCarriageReturn, "").Trim();
                                    arrDigitalChannelInfo[intDigitalChannelIndex] = ParseDigitalChannelString(strTempString);
                                    intDigitalChannelIndex++;
                                }

                                objChannelType.DigitalChannel = arrDigitalChannelInfo;
                                objChannelType.DigitalChannelCount = intDigitalChannelCount.ToString();
                                objCarrickData.DeviceDescriptors = objDevicedescriptors;
                                if (objDfrData.SampleRateInHz[0] < 500)
                                {
                                    blnIsDDR = true;
                                    //For DDR sampling rate will be less than 500 Hz
                                    //If inter-sample interval is less than 500Hz, record is slow-scan.
                                    //Selected to be just a little higher than half-cycle data rate at nominal power-line frequency (60Hz gives 120Hz)
                                    objCssDataType = FillCssCdfEntity(strData, intTotalChannelCount, lstFiles[intTotalFiles], intComtradeYear == 1991, (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));
                                    objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                                    objCSSDataDescriptor.ChannelDescriptions = objChannelType;
                                    objCssDataType.DataDescriptor = objCSSDataDescriptor;
                                    objCarrickData.Item = objCssDataType;
                                    objCssDataType.IsValidRecord = true;
                                    objCssDataType.BinaryDataCount = 0;

                                    if (objCssDataType.TriggerTimeSeconds == objCssDataType.StartTimeSeconds)
                                    {
                                        objCarrickData.RecordType = CarrickDataType.DDRC;
                                    }
                                    else
                                    {
                                        objCarrickData.RecordType = CarrickDataType.DDRT;
                                    }
                                }
                                else
                                {
                                    //Fill channel descriptions.
                                    objDfrDataDescriptor.ChannelDescriptions = objChannelType;
                                    //Fill data description.
                                    objDfrData.DataDescriptor = objDfrDataDescriptor;
                                    objCarrickData.Item = objDfrData;
                                    objCarrickData.RecordType = CarrickDataType.DFR;
                                }
                                string strDataFilePath = lstFiles[intTotalFiles].Remove(lstFiles[intTotalFiles].LastIndexOf('.')) + ".dat";
                                if (File.Exists(strDataFilePath))
                                {
                                    //Added by Ashwini to fix the bug #8144:iQ+ cannot import some COMTRADE ASCII 1999 from relay
                                    if (null != objDfrData.TotalNumberOfSamples && objDfrData.TotalNumberOfSamples.Length > 0)
                                    {
                                        string textOrBinary = strData[intTotalChannelCount + 7];
                                        string strTempDataFilePath = FilterDfrBinaryDataFileName(lstFiles[intTotalFiles]) + "Temp.dat";
                                        byte[] bytArrBinaryData =
                                            ParseDataFile(strDataFilePath, textOrBinary, int.Parse(objChannelType.AnalogChannelCount),
                                            int.Parse(objChannelType.DigitalChannelCount), objDfrData.TotalNumberOfSamples[0],
                                            arrAnalogChannelInfo, arrDigitalChannelInfo, _cultureInfo, strTempDataFilePath, lstFiles[intTotalFiles].Remove(lstFiles[intTotalFiles].LastIndexOf('.')) + ".dat");
                                        if (null != bytArrBinaryData)
                                        {
                                            objCarrickData.BinaryData = bytArrBinaryData;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    _boolIsFileMissing = true;
                                }
                                if (false == _boolIsFileMissing)
                                {
                                    EntityList<CarrickData> entylstCarrickData = new EntityList<CarrickData>();
                                    //entylstCarrickData.Add(objCarrickData);
                                    UpdateComtradeImportStatistics("ACTYLOGMSG1466", "", "", "", 0, 0, cbkImportdata);
                                    if (blnIsDDR)
                                    {
                                        objCssDataType.SegmentStartTimeSeconds = (uint)objCssDataType.StartTimeSecondsLocal;
                                        objCssDataType.SegmentEndTimeinSeconds = (uint)objCssDataType.EndTimeSecondsLocal;
                                        objCssDataType.BinaryDataCount = 0;
                                        objCssDataType.LastRecord = true;

                                        if (objCarrickData.RecordType == CarrickDataType.DDRC)
                                        {
                                            objCssDataType.RecordStatus = EventType.SSS.GetHashCode();

                                            // Filling CSS entity.
                                            entylstCarrickData.Add(objCarrickData);

                                            PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportCSS,
                                              MessageType.Binary, entylstCarrickData);
                                        }
                                        else
                                        {
                                            objCssDataType.RecordStatus = EventType.TSS.GetHashCode();

                                            // Fetching DeviceTypeID from DB. OB #6285
                                            strDeviceTypeID = DataBaseManagerDataObject.GetDeviceTypeID(
                                                entylstCarrickData[0].DeviceDescriptors.DeviceDescriptor[0].DeviceID);

                                            // Checking if Cashel Device is available for COMTRADE Import. OB #6285
                                            if (lstCashelDeviceTypeId.Contains(strDeviceTypeID))
                                            {
                                                objDdrtDataType = FillDdrtCdfEntity(strData, intTotalChannelCount,
                                                    lstFiles[intTotalFiles], intComtradeYear == 1991,
                                                    (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));

                                                // As 'CSSDataTypeDataDescriptor' class is same for both DDR-C and DDR-T
                                                // hence using same class here. OB #6285
                                                objCSSDataDescriptor = new CSSDataTypeDataDescriptor();
                                                objCSSDataDescriptor.ChannelDescriptions = objChannelType;
                                                objDdrtDataType.DataDescriptor = objCSSDataDescriptor;
                                                objCarrickData.Item = objDdrtDataType;
                                                objDdrtDataType.IsValidRecord = true;
                                                objDdrtDataType.BinaryDataCount = 0;

                                                objDdrtDataType.SegmentStartTimeSeconds = (uint)objDdrtDataType.StartTimeSecondsLocal;
                                                objDdrtDataType.SegmentEndTimeinSeconds = (uint)objDdrtDataType.EndTimeSecondsLocal;
                                                objDdrtDataType.BinaryDataCount = 0;
                                                objDdrtDataType.LastRecord = true;

                                                // Filling DDR-T entity.
                                                entylstCarrickData.Add(objCarrickData);

                                                //Changed MessageAction to redirect to DDR-T record insertion. OB #6285
                                                PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportDDRT,
                                                 MessageType.Binary, entylstCarrickData);
                                            }
                                            else
                                            {
                                                // Filling CSS entity.
                                                entylstCarrickData.Add(objCarrickData);

                                                PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportCSS,
                                                 MessageType.Binary, entylstCarrickData);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Filling DFR entity.
                                        entylstCarrickData.Add(objCarrickData);

                                        PublishDataMessage(Topics.FR.DFRDataRequest, MessageActions.FR.ImportDFR,
                                            MessageType.Binary, entylstCarrickData);
                                    }
                                }
                                else
                                {
                                    UpdateComtradeImportStatistics("ACTYLOGMSG1467", "", "", "", 0, 0, cbkImportdata);
                                }

                            #endregion
                            }
                            if (File.Exists(lstFiles[intTotalFiles]))
                            {
                                File.Move(lstFiles[intTotalFiles], Path.ChangeExtension(lstFiles[intTotalFiles], ".scfg"));
                            }
                            string strInfFilePath = lstFiles[intTotalFiles].Remove(lstFiles[intTotalFiles].LastIndexOf('.')) + ".inf";
                            if (File.Exists(strInfFilePath))
                            {
                                File.Move(strInfFilePath, Path.ChangeExtension(lstFiles[intTotalFiles], ".sinf"));
                            }
                            string strComDataFilePath = lstFiles[intTotalFiles].Remove(lstFiles[intTotalFiles].LastIndexOf('.')) + ".dat";
                            if (File.Exists(strComDataFilePath))
                            {
                                File.Move(strComDataFilePath, Path.ChangeExtension(lstFiles[intTotalFiles], ".sdat"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::ParseComtradecfgAndDatFiles", LogWriter.Level.ERROR, ex.Message,
                                                   ex.InnerException, "", ToString());
            }
        }

        private ChannelsTypeDigitalChannel ParseDigitalChannelString(string strDefinition)
        {
            try
            {
                // Split the string and extract the component parts we need
                string[] split = strDefinition.Split(WaveformViewerConstants.COMTRADE_FILE_DELIMITER);
                int channelNumber = int.Parse(split[0]);
                string name = split[1];
                string normalState = split.Length > 3 ? split[3] : split[2]; // Previous code used split[4] but that caused array exception so I changed it to split[3]

                if (normalState != "0" && normalState != "1")
                    throw new Exception(string.Format("Unexpected normalState value: ", normalState));

                // Generate the channel object based on the component parts
                ChannelsTypeDigitalChannel chan = new ChannelsTypeDigitalChannel()
                {
                    ChannelNumber = channelNumber,
                    ChannelName = name,
                    NormalState = normalState,
                    Min = 0,
                    Max = 1
                };
                return chan;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Failed to parse string {0}. Exception {1}:{2}", strDefinition, ex, ex.StackTrace);
                MessageBusLogger.LogError("ParseDigitalChannelString", LogWriter.Level.ERROR, msg, ex, "", this.ToString());
                throw ex;
            }
        }

        private void InitialiseChannelTypeFillAnalogChannelData(List<string> lstFiles, int intTotalFiles, string strTempString, string[] strData, int intAnalogChannelCount,
            out ChannelsType objChannelType, out ChannelsTypeAnalogChannel[] arrAnalogChannelInfo, bool Is1997)
        {
            //Create instance of Channelstype.
            objChannelType = new ChannelsType();
            //Initialise the ChannelsTypeAnalogChannel.
            arrAnalogChannelInfo = new ChannelsTypeAnalogChannel[intAnalogChannelCount];

            for (int intChannelLoop = 0; intChannelLoop < intAnalogChannelCount; intChannelLoop++)
            {
                //Fill analog channel details for all the analog channels present.
                arrAnalogChannelInfo[intChannelLoop] = new ChannelsTypeAnalogChannel();
                strTempString = strData[intChannelLoop + 2].Replace(strCarriageReturn, "").Trim();
                arrAnalogChannelInfo[intChannelLoop].ChannelName = (strTempString.Split(chrDelimiter))[1].Trim();
                if (Is1997)
                {
                    arrAnalogChannelInfo[intChannelLoop].ChannelNumber = intChannelLoop + 1;
                }
                else
                {
                    arrAnalogChannelInfo[intChannelLoop].ChannelNumber = intChannelLoop;
                }
                arrAnalogChannelInfo[intChannelLoop].ChannelUnit = (strTempString.Split(chrDelimiter))[4].Trim();
                arrAnalogChannelInfo[intChannelLoop].ChannelMultiplier =
                     float.Parse((strTempString.Split(chrDelimiter))[5].Trim(), _cultureInfo);
                arrAnalogChannelInfo[intChannelLoop].ChannelOffsetAdder =
                     float.Parse((strTempString.Split(chrDelimiter))[6].Trim(), _cultureInfo);
                arrAnalogChannelInfo[intChannelLoop].MaxValue =
                    (float.Parse((strTempString.Split(chrDelimiter))[9].Trim(), _cultureInfo)
                    * arrAnalogChannelInfo[intChannelLoop].ChannelMultiplier) + arrAnalogChannelInfo[intChannelLoop].ChannelOffsetAdder;
                arrAnalogChannelInfo[intChannelLoop].MinValue =
                    (float.Parse((strTempString.Split(chrDelimiter))[8].Trim(), _cultureInfo)
                    * arrAnalogChannelInfo[intChannelLoop].ChannelMultiplier) + arrAnalogChannelInfo[intChannelLoop].ChannelOffsetAdder;
                //arrAnalogChannelInfo[intLoop].NominalValue = entyViewerCDF.AnalogInfo[intLoop].DataMin;
                string strInfFilePath = lstFiles[intTotalFiles].Remove(lstFiles[intTotalFiles].LastIndexOf('.')) + ".inf";
                if (File.Exists(strInfFilePath))
                {
                    Carrick.Framework.Util.InIParser infFile = new Carrick.Framework.Util.InIParser(strInfFilePath);
                    if (!string.IsNullOrEmpty(infFile.GetSetting("[" +
                    WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                       , WaveformViewerConstants.AnalogChannelFSD)))
                    {
                        arrAnalogChannelInfo[intChannelLoop].FSD =
                            float.Parse(infFile.GetSetting("[" +
                        WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                           , WaveformViewerConstants.AnalogChannelFSD));
                    }
                    else
                    {
                        arrAnalogChannelInfo[intChannelLoop].FSD = 0f;
                    }
                    arrAnalogChannelInfo[intChannelLoop].PhaseLabel = infFile.GetSetting("[" +
                    WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                       , WaveformViewerConstants.AnalogChannelPhaseLabel);

                    ChannelsTypeAnalogChannelTxRatio ctaTxRatio = new ChannelsTypeAnalogChannelTxRatio();
                    if (!string.IsNullOrEmpty(infFile.GetSetting("[" +
                    WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                       , WaveformViewerConstants.AnalogChannelPrimary)))
                    {
                        ctaTxRatio.Primary = float.Parse(infFile.GetSetting("[" +
                        WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                           , WaveformViewerConstants.AnalogChannelPrimary));
                    }
                    else
                    {
                        ctaTxRatio.Primary = 0f;
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting("[" +
                    WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                       , WaveformViewerConstants.AnalogChannelSecondary)))
                    {
                        ctaTxRatio.Secondary = float.Parse(infFile.GetSetting("[" +
                        WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                           , WaveformViewerConstants.AnalogChannelSecondary));
                    }
                    else
                    {
                        ctaTxRatio.Primary = 0f;
                    }
                    arrAnalogChannelInfo[intChannelLoop].TxRatio = ctaTxRatio;
                    arrAnalogChannelInfo[intChannelLoop].FeederName = infFile.GetSetting("[" +
                    WaveformViewerConstants.PublicAnalogChannel + (intChannelLoop + 1).ToString() + "]"
                       , WaveformViewerConstants.AnalogChannelFeederName);

                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.
                    PublicRecordInformation, WaveformViewerConstants.Lower32ChannelStatus)))
                    {
                        objChannelType.Lower32DigitalChannelStatus
                            = (uint)Convert.ToDouble(infFile.GetSetting(WaveformViewerConstants.
                        PublicRecordInformation, WaveformViewerConstants.Lower32ChannelStatus));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.
                        PublicRecordInformation, WaveformViewerConstants.Upper32ChannelStatus)))
                    {
                        objChannelType.Upper32DigitalChannelStatus
                            = (uint)Convert.ToDouble(infFile.GetSetting(WaveformViewerConstants.
                        PublicRecordInformation, WaveformViewerConstants.Upper32ChannelStatus));
                    }
                }
                else
                {
                    //_boolIsFileMissing = true;
                }
            }
            objChannelType.AnalogChannel = arrAnalogChannelInfo;
            objChannelType.AnalogChannelCount = intAnalogChannelCount.ToString();

        }

        private void GetComtradeDetails(CallBack cbkImportdata, ImportComtradeFileDTO dtoComtradeImport, List<string> lstFiles,
            int intTotalFiles, ref DFRDataType objDfrData, ref CarrickDataDeviceDescriptors objDevicedescriptors, ref string strTempString
           , string[] strData, int intDeviceIndex, int intComtradeYear, ref int intAnalogChannelCount, ref int intDigitalChannelCount, ref int intTotalChannelCount)
        {
            string[] strArrFileName = lstFiles[intTotalFiles].Split('\\');
            if (strArrFileName.Length > 0)
            {
                UpdateComtradeImportStatistics("ACTYLOGMSG1464",
                     strArrFileName[strArrFileName.Length - 1], "", "", lstFiles.Count, intTotalFiles, cbkImportdata);
            }
            //Parse TotalNumber of channels.
            strTempString = strData[1].Replace(strCarriageReturn, "").Trim();
            intTotalChannelCount = int.Parse((strTempString.Split(chrDelimiter))[0].Trim(), _cultureInfo);

            intAnalogChannelCount = int.Parse((strTempString.Split(chrDelimiter))[1].
            Remove((strTempString.Split(chrDelimiter))[1].Length - 1).Trim(), _cultureInfo);

            intDigitalChannelCount =
                int.Parse((strTempString.Split(chrDelimiter))[2].Remove
                ((strTempString.Split(chrDelimiter))[2].Length - 1).Trim(), _cultureInfo);

            intTotalChannelCount = intAnalogChannelCount + intDigitalChannelCount;
            if (dtoComtradeImport.DeviceCreationType == 2)
            {
                objDevicedescriptors =
                    FillEntityForExistingComtradeDevice(intTotalChannelCount, strCarriageReturn,
                    _cultureInfo, (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]), strData);
            }

            objDfrData = new DFRDataType();
            //Create instance of DFRDataTypeDataDescriptor.

            objDfrData = FillDfrCdfEntity(strData, intTotalChannelCount,
                lstFiles[intTotalFiles], intComtradeYear == 1991, (ComtradeDeviceInfo)(dtoComtradeImport.ComTradeDeviceDetails[intDeviceIndex]));
            //objDfrDataDescriptor = new DFRDataTypeDataDescriptor();
            //objDfrData = FillDfrCdfEntity(strData, intTotalChannelCount,
            //    _cultureInfo, strCarriageReturn, chrDelimiter, lstFiles[intTotalFiles].ToString(), true);

        }


        /// <summary>
        /// This method filter out chars from a file path such that only letters, digits, underline and dot present.
        /// While all other chars are replaced using underlines.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static private string FilterDfrBinaryDataFileName(string path)
        {
            // Some wide chars are not supported by the zip package, cause the resulted DFR cannot be opened.
            // -- added by JX Peng on 25 Sep. 2013 fixing bug #5977.
            FileInfo tempFI = new FileInfo(path);
            string strTempDataFile = tempFI.Name.Remove(tempFI.Name.LastIndexOf('.'));
            for (int offset = 0; offset < strTempDataFile.Length; offset++)
            {
                char ch = strTempDataFile[offset];
                if ((ch < 'A' || ch > 'Z') && (ch < 'a' || ch > 'z') &&
                    (ch < '0' || ch > '9') && ch != '_' && ch != '.')
                {
                    strTempDataFile = strTempDataFile.Replace(ch, '_');
                }
            }
            return Path.Combine(tempFI.DirectoryName, strTempDataFile);
        }

        /// <summary>
        /// Method to fill entity for device created with comtrade file.
        /// </summary>
        /// <param name="intTotalNoOfChannels"></param>
        /// <param name="strCarriageReturn"></param>
        /// <param name="_cultureInfo"></param>
        /// <param name="strData"></param>
        /// <param name="chrDelimiter"></param>
        /// <returns></returns>
        private CarrickDataDeviceDescriptors FillEntityForDeviceCreatedFromFile(int intTotalNoOfChannels,
            string strCarriageReturn, CultureInfo _cultureInfo, string[] strData, char chrDelimiter)
        {
            string strTempString = string.Empty;
            try
            {
                //Create instance of CarrickDataDeviceDescriptors.
                CarrickDataDeviceDescriptors objDevicedescriptors = new CarrickDataDeviceDescriptors();
                //Create instance of CarrickDataDeviceDescriptor.
                CarrickDataDeviceDescriptor objDeviceDescriptor = new CarrickDataDeviceDescriptor();
                //Since only one header is present for the IDm record initialize the DeviceDescriptor array to one.
                objDevicedescriptors.DeviceDescriptor = new CarrickDataDeviceDescriptor[1];

                objDeviceDescriptor.DeviceType = CarrickDataDeviceType.COMTRADE;
                strTempString = strData[0].Replace(strCarriageReturn, "").Trim();

                objDeviceDescriptor.DeviceName = (strTempString.Split(chrDelimiter))[1].Trim();
                objDeviceDescriptor.StationName = (strTempString.Split(chrDelimiter))[0].Trim();

                #region "If not device name is specified, use station name as the device name"
                // Added by JX Peng on 25 Sep. 2013, bug #5977
                if (string.IsNullOrEmpty(objDeviceDescriptor.DeviceName))
                {
                    objDeviceDescriptor.DeviceName = objDeviceDescriptor.StationName.Trim();
                }
                #endregion "If not device name is specified, use station name as the device name"

                objDeviceDescriptor.NetworkID = "0";
                DeviceDTO dtoDevice = new DeviceDTO();
                dtoDevice.DeviceName = (strTempString.Split(chrDelimiter))[1].Trim();
                dtoDevice.SubStationName = (strTempString.Split(chrDelimiter))[0].Trim();
                //Added by Ashwini to fix the bug #8144:iQ+ cannot import some COMTRADE ASCII 1999 from relay
                if (string.IsNullOrEmpty(dtoDevice.DeviceName))
                {
                    if (!string.IsNullOrEmpty(dtoDevice.SubStationName))
                    {
                        dtoDevice.DeviceName = dtoDevice.SubStationName;
                    }
                }
                if (string.IsNullOrEmpty(dtoDevice.SubStationName))
                {
                    if (!string.IsNullOrEmpty(dtoDevice.DeviceName))
                    {
                        dtoDevice.SubStationName = dtoDevice.DeviceName;
                    }
                }
                dtoDevice.DeviceType = DeviceType.COMTRADE;
                //dtoDevice.SLNo = "123456";
                List<DeviceDTO> lstDeviceDTO = new List<DeviceDTO>();
                lstDeviceDTO.Add(dtoDevice);
                long lngDeviceId = -1;
                ReplyData replyData = PublishRequestMessage(lstDeviceDTO[0],
                          Topics.DeviceManager.DeviceAdministration,
                          MessageActions.DeviceManager.CreateComtradeDevices,
                           ResponseTopics.DeviceManager.DeviceAdministration + "_"
                           + Thread.CurrentThread.ManagedThreadId.ToString()
                           + DateTime.Now.Ticks.ToString(),
                          ResponseMessageActions.DeviceManager.CreateComtradeDevices);
                if (replyData.PayLoad.GetType() == typeof(ResponseMessage))
                {
                    lngDeviceId = Convert.ToInt64(((ResponseMessage)replyData.PayLoad).Response);
                }
                objDeviceDescriptor.DeviceID = int.Parse(lngDeviceId.ToString());
                //Get Line Frequency
                strTempString = strData[intTotalNoOfChannels + 2].Replace(strCarriageReturn, "").Trim();
                objDeviceDescriptor.LineFrequency = float.Parse(strTempString, _cultureInfo).ToString();

                objDeviceDescriptor.TimeLocked = CarrickDataDeviceDescriptorsDeviceDescriptorTimeLocked.GPSLOCK;
                //FillDfrCdfEntity the device descriptor of objDevicedescriptors.
                objDevicedescriptors.DeviceDescriptor[0] = objDeviceDescriptor;
                // At present only one device data in XML.
                objDevicedescriptors.count = "1";
                return objDevicedescriptors;
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::FillEntityForDeviceCreatedFromFile", LogWriter.Level.ERROR, ex.Message,
                             ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
            }
        }
        /// <summary>
        /// Method to fill entity for already existing devices.
        /// </summary>
        /// <param name="intTotalNoOfChannels"></param>
        /// <param name="strCarriageReturn"></param>
        /// <param name="_cultureInfo"></param>
        /// <param name="dtoDevice"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        private CarrickDataDeviceDescriptors FillEntityForExistingDevice(int intTotalNoOfChannels, string strCarriageReturn,
            CultureInfo _cultureInfo, DeviceDTO dtoDevice, string[] strData)
        {
            try
            {
                //Create instance of CarrickDataDeviceDescriptors.
                CarrickDataDeviceDescriptors objDevicedescriptors = new CarrickDataDeviceDescriptors();
                //Create instance of CarrickDataDeviceDescriptor.
                CarrickDataDeviceDescriptor objDeviceDescriptor = new CarrickDataDeviceDescriptor();
                //Since only one header is present for the IDm record initialize the DeviceDescriptor array to one.
                objDevicedescriptors.DeviceDescriptor = new CarrickDataDeviceDescriptor[1];

                //objDeviceDescriptor.DeviceType = "IDM";
                objDeviceDescriptor.DeviceName = dtoDevice.DeviceName;
                objDeviceDescriptor.StationName = dtoDevice.SubStationName;
                objDeviceDescriptor.NetworkID = "0";
                objDeviceDescriptor.DeviceID = dtoDevice.Deviceid;

                //Get Line Frequency
                string strTempString = strData[intTotalNoOfChannels + 2].Replace(strCarriageReturn, "").Trim();
                objDeviceDescriptor.LineFrequency = float.Parse(strTempString, _cultureInfo).ToString();

                objDeviceDescriptor.TimeLocked = CarrickDataDeviceDescriptorsDeviceDescriptorTimeLocked.GPSLOCK;
                //FillDfrCdfEntity the device descriptor of objDevicedescriptors.
                objDevicedescriptors.DeviceDescriptor[0] = objDeviceDescriptor;
                // At present only one device data in XML.
                objDevicedescriptors.count = "1";
                return objDevicedescriptors;
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::FillEntityForExistingDevice", LogWriter.Level.ERROR, ex.Message,
                             ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
            }
        }

        /// <summary>
        /// Method to fill entity for already existing comtrade devices.
        /// Added to support multiple comtrade import #8927
        /// </summary>
        /// <param name="intTotalNoOfChannels"></param>
        /// <param name="strCarriageReturn"></param>
        /// <param name="_cultureInfo"></param>
        /// <param name="dtoDevice"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        private CarrickDataDeviceDescriptors FillEntityForExistingComtradeDevice(int intTotalNoOfChannels, string strCarriageReturn,
            CultureInfo _cultureInfo, ComtradeDeviceInfo dtoDevice, string[] strData)
        {
            try
            {
                //Create instance of CarrickDataDeviceDescriptors.
                CarrickDataDeviceDescriptors objDevicedescriptors = new CarrickDataDeviceDescriptors();
                //Create instance of CarrickDataDeviceDescriptor.
                CarrickDataDeviceDescriptor objDeviceDescriptor = new CarrickDataDeviceDescriptor();
                //Since only one header is present for the IDm record initialize the DeviceDescriptor array to one.
                objDevicedescriptors.DeviceDescriptor = new CarrickDataDeviceDescriptor[1];

                //objDeviceDescriptor.DeviceType = "IDM";
                objDeviceDescriptor.DeviceName = dtoDevice.DeviceName;
                objDeviceDescriptor.StationName = dtoDevice.SubstationName;
                objDeviceDescriptor.NetworkID = "0";
                objDeviceDescriptor.DeviceID = dtoDevice.ComtradeDeviceId;

                //Get Line Frequency
                string strTempString = strData[intTotalNoOfChannels + 2].Replace(strCarriageReturn, "").Trim();
                objDeviceDescriptor.LineFrequency = float.Parse(strTempString, _cultureInfo).ToString();

                objDeviceDescriptor.TimeLocked = CarrickDataDeviceDescriptorsDeviceDescriptorTimeLocked.GPSLOCK;
                //FillDfrCdfEntity the device descriptor of objDevicedescriptors.
                objDevicedescriptors.DeviceDescriptor[0] = objDeviceDescriptor;
                // At present only one device data in XML.
                objDevicedescriptors.count = "1";
                return objDevicedescriptors;
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::FillEntityForExistingComtradeDevice", LogWriter.Level.ERROR, ex.Message,
                             ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
            }
        }
        /// <summary>
        /// Method to fill entity for device created by user.
        /// </summary>
        /// <param name="intTotalNoOfChannels"></param>
        /// <param name="strCarriageReturn"></param>
        /// <param name="_cultureInfo"></param>
        /// <param name="dtoDevice"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        private CarrickDataDeviceDescriptors FillEntityForCreatedDevice(int intTotalNoOfChannels,
            string strCarriageReturn, CultureInfo _cultureInfo, DeviceDTO dtoDevice, string[] strData)
        {
            try
            {
                //Create instance of CarrickDataDeviceDescriptors.
                CarrickDataDeviceDescriptors objDevicedescriptors = new CarrickDataDeviceDescriptors();
                //Create instance of CarrickDataDeviceDescriptor.
                CarrickDataDeviceDescriptor objDeviceDescriptor = new CarrickDataDeviceDescriptor();
                //Since only one header is present for the IDm record initialize the DeviceDescriptor array to one.
                objDevicedescriptors.DeviceDescriptor = new CarrickDataDeviceDescriptor[1];

                if (dtoDevice.DeviceType == "D0001")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.QWavePMDA;
                }
                else if (dtoDevice.DeviceType == "D1002")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.InformaPMDA;
                }
                else if (dtoDevice.DeviceType == "D2007")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.Ben5000;
                }
                else if (dtoDevice.DeviceType == "D2008")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.Ben6000;
                }
                else if (dtoDevice.DeviceType == "D4000")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.DFRII;
                }
                else if (dtoDevice.DeviceType == "D5000")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.DFR1200;
                }
                else if (dtoDevice.DeviceType == "D3001")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.IDMV2;
                }
                else if (dtoDevice.DeviceType == "D3000")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.IDM;
                }
                else if (dtoDevice.DeviceType == "D6000")
                {
                    objDeviceDescriptor.DeviceType = CarrickDataDeviceType.IMS;
                }
                objDeviceDescriptor.DeviceName = dtoDevice.DeviceName;
                objDeviceDescriptor.StationName = dtoDevice.SubStationName;
                objDeviceDescriptor.NetworkID = "0";
                objDeviceDescriptor.DeviceID = int.Parse(lngCreatedDeviceId.ToString());
                //Get Line Frequency
                string strTempString = strData[intTotalNoOfChannels + 2].Replace(strCarriageReturn, "").Trim();
                objDeviceDescriptor.LineFrequency = float.Parse(strTempString, _cultureInfo).ToString();

                objDeviceDescriptor.TimeLocked = CarrickDataDeviceDescriptorsDeviceDescriptorTimeLocked.GPSLOCK;
                //FillDfrCdfEntity the device descriptor of objDevicedescriptors.
                objDevicedescriptors.DeviceDescriptor[0] = objDeviceDescriptor;
                // At present only one device data in XML.
                objDevicedescriptors.count = "1";
                return objDevicedescriptors;
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::FillEntityForCreatedDevice", LogWriter.Level.ERROR, ex.Message,
                             ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
            }
        }
        /// <summary>
        /// Method to parse data file.
        /// </summary>
        /// <param name="strDataFilePath"></param>
        /// <param name="intAnalogChannelCount"></param>
        /// <param name="intDigitalChannelCount"></param>
        /// <param name="intSamplesCount"></param>
        /// <param name="arrAnalogChannelInfo"></param>
        /// <param name="arrDigitalChannelInfo"></param>
        /// <param name="_cultureInfo"></param>
        /// <param name="strTempDataFilePath"></param>
        /// <returns></returns>
        private byte[] ParseDataFile(string strDataFilePath, string format, int intAnalogChannelCount,
            int intDigitalChannelCount, int intSamplesCount, ChannelsTypeAnalogChannel[] arrAnalogChannelInfo,
            ChannelsTypeDigitalChannel[] arrDigitalChannelInfo, CultureInfo _cultureInfo,
            string strTempDataFilePath, string strdatFilepath)
        {
            FileStream fileStream = null;
            BinaryWriter binaryWriter = null;
            float[][] fltAnalogChannelValues;
            byte[][] bytDigitalChannelValues;
            float[] fltDigitalChannelMin;
            float[] fltDigitalChannelMax;
            byte[] bytArrReadFile = null;

            #region "Add for extension to binary comtrade" --- Bug #5977, 2013-09-24 by JX Peng
            // IEEE C37.111-1999
            Stream streamData = null;
            BinaryReader binaryReader = null;
            format = format.Replace(WaveformViewerConstants.COMTRADE_FILE_NEWLINEFEED, "");
            format = format.Replace(WaveformViewerConstants.COMTRADE_FILE_CARRIAGERETURN, "").Trim();
            bool binaryData = format.Equals("BINARY", StringComparison.CurrentCultureIgnoreCase);
            long nDataRecordsCount = 0;
            #endregion "Add for extension to binary comtrade" --- Bug #5977, 2013-09-24 by JX Peng
            try
            {

                if (System.IO.File.Exists(strDataFilePath))
                {
                    char chrDelimiter = WaveformViewerConstants.COMTRADE_FILE_DELIMITER;
                    string strCarriageReturn = WaveformViewerConstants.COMTRADE_FILE_CARRIAGERETURN;
                    string strLineFeed = WaveformViewerConstants.COMTRADE_FILE_NEWLINEFEED;

                    //string strDatFileContents;
                    string strTempString;
                    string[] strChannelData;
                    //int intAnalogChannelCount = _dsDataSource.HeaderInfo.AnalogChannelCount;
                    //int intDigitalChannelCount = _dsDataSource.HeaderInfo.DigitalChannelCount;
                    string[] strData = null;
                    //int intSamplesCount;
                    //_dsDataSource.HeaderInfo.Carrick.Framework.Util.TimeZoneInfo = TimeZoneHandler.FromId(-1);

                    #region "Add for extension to binary comtrade" --- Bug #5977, 2013-09-24 by JX Peng
                    if (!binaryData)
                    {
                        //strData = System.IO.File.ReadAllLines(strDataFilePath);
                        strData = System.IO.File.ReadAllLines(strDataFilePath);
                        nDataRecordsCount = strData.Length;
                    }
                    else
                    {
                        streamData = File.OpenRead(strDataFilePath);
                        binaryReader = new BinaryReader(streamData);
                        int statusWords = intDigitalChannelCount / 16;
                        if (intDigitalChannelCount % 16 != 0) statusWords++;
                        nDataRecordsCount = streamData.Length / (4 * 2 + 2 * (intAnalogChannelCount + statusWords));
                    }
                    #endregion "Add for extension to binary comtrade" --- Bug #5977, 2013-09-24 by JX Peng

                    if (nDataRecordsCount > 0)
                    {
                        //Update the number of samples in the record,by considering the number of samples in the .dat file.
                        // _wfvDataSource.TotalNumberOfSamples = strData.Length - 1; // TODO why is it added here???
                        //Get the total number fo samples.
                        //intSamplesCount = _dsDataSource.TotalNumberOfSamples;

                        fltAnalogChannelValues = new float[intAnalogChannelCount][];
                        for (int intAnalogChannelIndex = 0; intAnalogChannelIndex < intAnalogChannelCount; intAnalogChannelIndex++)
                        {
                            fltAnalogChannelValues[intAnalogChannelIndex] = new float[intSamplesCount];
                        }
                        bytDigitalChannelValues = new byte[intDigitalChannelCount][];
                        for (int intDigitalChannelIndex = 0; intDigitalChannelIndex < intDigitalChannelCount; intDigitalChannelIndex++)
                        {
                            bytDigitalChannelValues[intDigitalChannelIndex] = new byte[intSamplesCount];
                        }

                        fltDigitalChannelMax = new float[intDigitalChannelCount];
                        fltDigitalChannelMin = new float[intDigitalChannelCount];

                        //Get All Sample values for All Channels
                        #region "Add for extension to binary comtrade" --- Bug #5977, 2013-09-24 by JX Peng
                        Int64 fieldRecordId;
                        Int64 fieldTime;
                        Int64[] fields = new Int64[intAnalogChannelCount + intDigitalChannelCount];
                        #endregion "Add for extension to binary comtrade"

                        for (int intSamplesIndex = 0; intSamplesIndex < intSamplesCount; intSamplesIndex++)
                        {
                            //strTempString = strData[intSamplesIndex].Replace(strCarriageReturn, "").Trim();
                            //strChannelData = strTempString.Split(chrDelimiter);
                            ////Get Sample Value for Analog Channel
                            //int intChannelIndex = 0;
                            //for (intChannelIndex = 0; intChannelIndex < intAnalogChannelCount; intChannelIndex++)
                            //{
                            //    fltAnalogChannelValues[intChannelIndex][intSamplesIndex] =
                            //    (arrAnalogChannelInfo[intChannelIndex].ChannelMultiplier *
                            //    ((int.Parse(strChannelData[2 + intChannelIndex].Trim(), _cultureInfo)) >= 99999 ? float.NaN :
                            //    (int.Parse(strChannelData[2 + intChannelIndex].Trim(), _cultureInfo)))
                            //    + arrAnalogChannelInfo[intChannelIndex].ChannelOffsetAdder);
                            //}

                            ////Get Sample Value for Digital Channel
                            //for (; intChannelIndex < intAnalogChannelCount + intDigitalChannelCount; intChannelIndex++)
                            //{
                            //    bytDigitalChannelValues[intChannelIndex - intAnalogChannelCount][intSamplesIndex] =
                            //        byte.Parse(strChannelData[2 + intChannelIndex].Trim(), _cultureInfo);

                            //    if (bytDigitalChannelValues[intChannelIndex - intAnalogChannelCount][intSamplesIndex] < fltDigitalChannelMin[intChannelIndex - intAnalogChannelCount])
                            //    {
                            //        fltDigitalChannelMin[intChannelIndex - intAnalogChannelCount] = bytDigitalChannelValues[intChannelIndex - intAnalogChannelCount][intSamplesIndex];
                            //    }
                            //    else if (bytDigitalChannelValues[intChannelIndex - intAnalogChannelCount][intSamplesIndex] > fltDigitalChannelMax[intChannelIndex - intAnalogChannelCount])
                            //    {
                            //        fltDigitalChannelMax[intChannelIndex - intAnalogChannelCount] = bytDigitalChannelValues[intChannelIndex - intAnalogChannelCount][intSamplesIndex];
                            //    }
                            //}

                            #region "Rewritten for extension to binary comtrade"
                            if (!binaryData)
                            {
                                // For text data file, parse all fileds of a record/line
                                strTempString = strData[intSamplesIndex].Replace(strCarriageReturn, "").Trim();
                                strChannelData = strTempString.Split(chrDelimiter);
                                fieldRecordId = Int64.Parse(strChannelData[0].Trim(), _cultureInfo);
                                fieldTime = Int64.Parse(strChannelData[1].Trim(), _cultureInfo);
                                for (int f = 0; f < fields.Length; f++)
                                {
                                    fields[f] = Int64.Parse(strChannelData[2 + f].Trim(), _cultureInfo);
                                }
                            }
                            else
                            {
                                // For binary data file, read all fields of a record. 
                                // Comtrade records are of fixed length, all values are 16-bit signed integers 
                                // except for record number and microsecond offset which are 32-bit unsigned integers.
                                fieldRecordId = binaryReader.ReadInt32();
                                fieldTime = binaryReader.ReadInt32();
                                for (int f = 0; f < intAnalogChannelCount; f++)
                                {
                                    fields[f] = binaryReader.ReadInt16();
                                }
                                int dChInd = 0;
                                ushort word = 0;
                                for (int f = intAnalogChannelCount; f < fields.Length; f++)
                                {
                                    //Status channel sample data are stored in groups of two bytes for each 16 status channels, 
                                    //with the LSB of a word assigned to the smallest input channel number belonging to that group. 
                                    if (dChInd % 16 == 0) word = binaryReader.ReadUInt16();
                                    fields[f] = ((word & 1) == 0 ? 0 : 1);
                                    word >>= 1;
                                    dChInd++;
                                }
                            }
                            for (int f = 0; f < intAnalogChannelCount; f++)
                            {
                                // COMTRADE standards specifies 0x8000 reserved to mark missing data. The original 99999 is not applying.
                                if (fields[f] == 0x8000)
                                {
                                    fltAnalogChannelValues[f][intSamplesIndex] = float.NaN;
                                }
                                else
                                {
                                    fltAnalogChannelValues[f][intSamplesIndex] =
                                        (arrAnalogChannelInfo[f].ChannelMultiplier * fields[f] + arrAnalogChannelInfo[f].ChannelOffsetAdder);
                                }
                            }
                            for (int f = intAnalogChannelCount; f < fields.Length; f++)
                            {
                                bytDigitalChannelValues[f - intAnalogChannelCount][intSamplesIndex] = (byte)fields[f];
                                //Get minimum and maximum values for digital channels
                                if (f == intAnalogChannelCount || fields[f] < fltDigitalChannelMin[f - intAnalogChannelCount])
                                {
                                    fltDigitalChannelMin[f - intAnalogChannelCount] = fields[f];
                                }
                                if (f == intAnalogChannelCount || fields[f] > fltDigitalChannelMax[f - intAnalogChannelCount])
                                {
                                    fltDigitalChannelMax[f - intAnalogChannelCount] = fields[f];
                                }
                            }
                            #endregion "Add for extension to binary comtrade"
                        }

                        for (int intChannelIndex = 0; intChannelIndex < intDigitalChannelCount; intChannelIndex++)
                        {
                            arrDigitalChannelInfo[intChannelIndex].Min = fltDigitalChannelMin[intChannelIndex];
                            arrDigitalChannelInfo[intChannelIndex].Max = fltDigitalChannelMax[intChannelIndex];

                            arrDigitalChannelInfo[intChannelIndex].ActiveChannel =
                                (arrDigitalChannelInfo[intChannelIndex].Min == arrDigitalChannelInfo[intChannelIndex].Max) ? false : true;
                        }

                        fileStream = new FileStream(strTempDataFilePath, FileMode.Append, FileAccess.Write);
                        binaryWriter = new BinaryWriter(fileStream);

                        for (int intAnalogChannelIndex = 0; intAnalogChannelIndex < intAnalogChannelCount; intAnalogChannelIndex++)
                        {
                            for (int intSampleIndex = 0; intSampleIndex < intSamplesCount; intSampleIndex++)
                            {
                                binaryWriter.Write(fltAnalogChannelValues[intAnalogChannelIndex][intSampleIndex]);
                            }
                        }
                        for (int intDigitalChannelIndex = 0; intDigitalChannelIndex < intDigitalChannelCount; intDigitalChannelIndex++)
                        {
                            byte bytResultant = 0;
                            for (int intSampleIndex = 0; intSampleIndex < intSamplesCount; intSampleIndex++)
                            {
                                bytResultant = (byte)(bytResultant | (bytDigitalChannelValues[intDigitalChannelIndex][intSampleIndex] << (intSampleIndex % 8)));

                                if (intSampleIndex != 0 && (intSampleIndex == intSamplesCount - 1 || intSampleIndex % 8 == 7))
                                {
                                    if (intSampleIndex == intSamplesCount - 1)
                                    {
                                        bytResultant = (byte)(bytResultant << (8 - (intSamplesCount % 8)));
                                    }
                                    binaryWriter.Write(bytResultant);
                                    bytResultant = 0;
                                }
                            }
                        }
                        binaryWriter.Close();
                        fileStream.Close();
                        //fileStream.Dispose();
                        List<string> lststrFileList = new List<string>();
                        lststrFileList.Add(strTempDataFilePath);
                        string strZipLocation = strTempDataFilePath.Remove(strTempDataFilePath.LastIndexOf('.'))
                            + "Temp.zip";
                        //Thread.Sleep(5000);
                        Compress.Zip(strZipLocation, lststrFileList);
                        //Thread.Sleep(2000);
                        FileStream fs = new FileStream(strZipLocation, FileMode.Open, FileAccess.Read);
                        bytArrReadFile = new byte[fs.Length];
                        fs.Read(bytArrReadFile, 0, (int)fs.Length);
                        fs.Close();

                        File.Delete(strTempDataFilePath);
                        File.Delete(strZipLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::ParseDataFile", LogWriter.Level.ERROR, ex.Message,
                             ex.InnerException, "", ToString());
                fileStream = null;
                binaryWriter = null;
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
                if (streamData != null) streamData.Close();
                if (binaryReader != null) binaryReader.Close();
                //File.Move(strFilePath, Path.ChangeExtension(strdatFilepath, ".datf"));
            }
            finally
            {
                if (streamData != null) streamData.Close();
                if (binaryReader != null) binaryReader.Close();
                File.Move(strdatFilepath, Path.ChangeExtension(strdatFilepath, ".sdat"));
            }
            return bytArrReadFile;
        }


        /// <summary>
        /// Method to fill dfr cdf entity
        ///  Added to support multiple comtrade import #8927
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="intTotalChannel"></param>
        /// <param name="strFilePath"></param>
        /// <param name="Is1991or1997"></param>
        /// <returns></returns>
        private DFRDataType FillDfrCdfEntity(string[] strData, int intTotalChannel,
            string strFilePath, bool Is1991or1997, ComtradeDeviceInfo objSelectedDev)
        {
            ApplyServerCulture();
            //Create instance of DFRCDFEntity
            DFRDataType objDfrData = new DFRDataType();
            string strTempString = string.Empty;
            //DataSource dsSource = new DataSource();
            CustomDateTime StartTime = new CustomDateTime();
            CustomDateTime TriggerTime = new CustomDateTime();

            double dblSamplingRate = 0;
            double[] aDblSamplingFrequenceInHz = null;
            string strInfFilePath = string.Empty;

            try
            {
                //string strCfgFileName = strFilePath.Substring(strFilePath.LastIndexOf("\\") + 1);
                //string[] temp = strCfgFileName.Split(',');
                string strTimeZone = string.Empty;
                //if (temp.Length > 2)
                //{
                //    strTimeZone = temp[2];
                //}
                if (!objSelectedDev.IsComtradeFormatUTC)
                {
                    strTimeZone = objSelectedDev.ComtradeTimeCode;
                }
                string[] strTimeZoneValue = strTimeZone.Split('h');
                int hour = 0, min = 0;
                if (strTimeZoneValue.Length > 0)
                {
                    int.TryParse(strTimeZoneValue[0].Replace("-", ""), out hour);
                }
                if (strTimeZoneValue.Length > 1)
                {
                    int.TryParse(strTimeZoneValue[1], out min);
                }
                TimeSpan timeDifference = new TimeSpan(hour, min, 0);

                uint timeZoneSeconds = uint.Parse(timeDifference.TotalSeconds.ToString());

                strTempString = strData[intTotalChannel + 6].Replace(strCarriageReturn, "").Trim();
                DateTime dtTriggerTime = GetDateTimeFromString(strTempString, _cultureInfo, Is1991or1997);
                uint uintTriggerTimeMicroSeconds = uint.Parse((strTempString.Split('.')[1]), _cultureInfo);
                TriggerTime = new CustomDateTime(dtTriggerTime, uintTriggerTimeMicroSeconds);

                strTempString = strData[intTotalChannel + 5].Replace(strCarriageReturn, "").Trim();
                DateTime dtStartTime = GetDateTimeFromString(strTempString, _cultureInfo, Is1991or1997);
                uint uintStartTimeMicroSeconds = uint.Parse((strTempString.Split('.')[1]), _cultureInfo);
                StartTime = new CustomDateTime(dtStartTime, uintStartTimeMicroSeconds);

                string strTime = string.Empty;
                if (StartTime.ToEPOCH().ToString().Split('.').Length > 0)
                {
                    strTime = StartTime.ToEPOCH().ToString().Split('.')[0];
                }

                objDfrData.StartTimeSecondsLocal = uint.Parse(strTime);
                //if (strTimeZoneValue[0].Contains("+"))
                //{
                //    objDfrData.StartTimeSeconds = uint.Parse(strTime) + timeZoneSeconds;
                //}
                //else if (strTimeZoneValue[0].Contains("-"))
                //{
                //    objDfrData.StartTimeSeconds = uint.Parse(strTime) - timeZoneSeconds;
                //}
                //else
                {
                    objDfrData.StartTimeSeconds = uint.Parse(strTime);
                }


                if (StartTime.ToString().Split('.').Length > 1)
                {
                    if (StartTime.ToString().Split('.').Length == 2)
                    {
                        strTime = StartTime.ToString().Split('.')[1];
                    }
                    else if (StartTime.ToString().Split('.').Length == 3)
                    {
                        strTime = StartTime.ToString().Split('.')[1] + StartTime.ToString().Split('.')[2];
                    }
                }
                objDfrData.StartTimeMicroSeconds = uint.Parse(strTime);

                if (TriggerTime.ToEPOCH().ToString().Split('.').Length > 0)
                {
                    strTime = TriggerTime.ToEPOCH().ToString().Split('.')[0];
                }
                objDfrData.TriggerTimeSecondsLocal = uint.Parse(strTime);

                //if (strTimeZoneValue[0].Contains("+"))
                //{
                //    objDfrData.TriggerTimeSeconds = uint.Parse(strTime) + timeZoneSeconds;
                //}
                //else if (strTimeZoneValue[0].Contains("-"))
                //{
                //    objDfrData.TriggerTimeSeconds = uint.Parse(strTime) - timeZoneSeconds;
                //}
                //else
                {
                    objDfrData.TriggerTimeSeconds = uint.Parse(strTime);
                }


                if (!objSelectedDev.IsComtradeFormatUTC)
                {

                    if (strTimeZoneValue[0].Contains("+"))
                    {
                        objDfrData.StartTimeSeconds = objDfrData.StartTimeSeconds + timeZoneSeconds;
                        objDfrData.EndTimeinSeconds = objDfrData.EndTimeinSeconds + timeZoneSeconds;
                        objDfrData.TriggerTimeSeconds = objDfrData.TriggerTimeSeconds + timeZoneSeconds;

                    }
                    else if (strTimeZoneValue[0].Contains("-"))
                    {
                        objDfrData.StartTimeSeconds = objDfrData.StartTimeSeconds - timeZoneSeconds;
                        objDfrData.EndTimeinSeconds = objDfrData.EndTimeinSeconds - timeZoneSeconds;
                        objDfrData.TriggerTimeSeconds = objDfrData.TriggerTimeSeconds - timeZoneSeconds;
                    }

                }
                else
                {
                    //Date Time is already in UTC


                }
                if (TriggerTime.ToString().Split('.').Length > 1)
                {
                    if (TriggerTime.ToString().Split('.').Length == 2)
                    {
                        strTime = TriggerTime.ToString().Split('.')[1];
                    }
                    else if (TriggerTime.ToString().Split('.').Length == 3)
                    {
                        strTime = TriggerTime.ToString().Split('.')[1] +
                            TriggerTime.ToString().Split('.')[2];
                    }
                }
                objDfrData.TriggerTimeMicroSeconds = uint.Parse(strTime);

                //Get Sampling Rate,Total number of samples
                strTempString = strData[intTotalChannel + 3].Replace(strCarriageReturn, "").Trim();
                dblSamplingRate = int.Parse(strTempString, _cultureInfo);

                objDfrData.SamplingRate = dblSamplingRate;
                objDfrData.TotalNumberOfSamples = new int[(int)dblSamplingRate];
                objDfrData.SampleRateInHz = new double[(int)dblSamplingRate];

                int intTotalNumberOfSamples = 0;
                List<int> lstDataPoints = new List<int>();
                aDblSamplingFrequenceInHz = new double[(int)dblSamplingRate];
                #region Sampling Rate and No of samples for that sampling rate
                for (int intSampleRateIndex = 0; intSampleRateIndex <
                    dblSamplingRate; intSampleRateIndex++)
                {
                    strTempString = strData[intTotalChannel +
                        intSampleRateIndex + 4].Replace(strCarriageReturn, "").Trim();
                    aDblSamplingFrequenceInHz[intSampleRateIndex] = (double.Parse
                        ((strTempString.Split(chrDelimiter))[0].Trim()));

                    int intSamples = int.Parse((strTempString.Split(chrDelimiter))[1].Trim());
                    lstDataPoints.Add(intSamples - intTotalNumberOfSamples);
                    intTotalNumberOfSamples = intSamples;

                    objDfrData.SampleRateInHz[intSampleRateIndex] = aDblSamplingFrequenceInHz
                        [aDblSamplingFrequenceInHz.Length - 1];
                    objDfrData.TotalNumberOfSamples[intSampleRateIndex] = lstDataPoints[lstDataPoints.Count - 1];
                }
                #endregion

                if (dblSamplingRate != 0)
                {
                    objDfrData.Duration = intTotalNumberOfSamples / (float)aDblSamplingFrequenceInHz[0];
                }
                else
                {
                    objDfrData.Duration = 0;
                }
                objDfrData.EndTimeinSeconds = (double)objDfrData.StartTimeSeconds + objDfrData.Duration; ;
                objDfrData.EndTimeinSecondsLocal = (double)objDfrData.StartTimeSecondsLocal + objDfrData.Duration;

                strTempString = strData[0].Replace(strCarriageReturn, "").Trim();
                string[] strComtradeYear = strTempString.Split(chrDelimiter);
                if (strComtradeYear.Length > 2)
                {
                    objDfrData.ComtradeYear = ((strTempString.Split(chrDelimiter))[2].Trim());
                }
                else
                {
                    objDfrData.ComtradeYear = "1991";
                }
                objDfrData.CauseOfTrigger = 256;//Un Known if inf file is not present

                strInfFilePath = strFilePath.Remove(strFilePath.LastIndexOf('.')) + ".inf";
                if (File.Exists(strInfFilePath))
                {
                    Carrick.Framework.Util.InIParser infFile = new Carrick.Framework.Util.InIParser(strInfFilePath);
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                             , WaveformViewerConstants.CauseOfTrigger)))
                    {
                        objDfrData.CauseOfTrigger = Enum.Parse(typeof(Carrick.Framework.Constants.CauseOfTrigger),
                            infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation, WaveformViewerConstants.CauseOfTrigger)).GetHashCode();
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PublicRecordInformation
                            , WaveformViewerConstants.DecimationFactor)))
                    {
                        objDfrData.DecimationFactor = Convert.ToInt32(infFile.GetSetting(WaveformViewerConstants.PublicRecordInformation
                                , WaveformViewerConstants.DecimationFactor));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.Priority)))
                    {
                        objDfrData.Priority = Convert.ToInt32(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.Priority));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.LastRecord)))
                    {
                        objDfrData.LastRecord = Convert.ToBoolean(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.LastRecord));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.RecordStatus)))
                    {
                        objDfrData.RecordStatus = Convert.ToInt32(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.RecordStatus));
                    }
                    if (!string.IsNullOrEmpty(objDfrData.TriggerTimeSeconds.ToString()))
                    {
                        objDfrData.RecordNumber = (long)objDfrData.TriggerTimeSeconds;//Convert.ToInt32(infFile.GetSetting(WaveformViewerConstants.PublicRecordInformation
                        //, WaveformViewerConstants.RecordNumber));
                    }
                    //Code to add the triggered parameter info details yo inf file.(#4500)
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.COTText)))
                    {
                        objDfrData.CalculationLabel = infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.COTText);
                    }
                }
                else
                {
                    //_boolIsFileMissing = true;
                    if (!string.IsNullOrEmpty(objDfrData.TriggerTimeSeconds.ToString()))
                    {
                        objDfrData.RecordNumber = (long)objDfrData.TriggerTimeSeconds;
                    }
                }

                if (File.Exists(strFilePath))
                {
                    //return the filled DFRCDFEntity object.
                    File.Move(strFilePath, Path.ChangeExtension(strInfFilePath, ".scfg"));
                }
                if (File.Exists(strInfFilePath))
                {
                    File.Move(strInfFilePath, Path.ChangeExtension(strInfFilePath, ".sinf"));
                }
            }
            catch (Exception ex)
            {
                //File.Move(strFilePath, Path.ChangeExtension(strInfFilePath, ".cfgf"));
                //File.Move(strInfFilePath, Path.ChangeExtension(strInfFilePath, ".inff"));
                MessageBusLogger.LogError("ComtradeImport::FillDfrCdfEntity", LogWriter.Level.ERROR, ex.Message,
                              ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);

            }
            return objDfrData;
        }

        #region Filling entity fir DDR-T. OB #6285
        /// <summary>
        /// Method to fill cdf entity for DDR-T type. OB #6285
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="intTotalChannel"></param>
        /// <param name="strFilePath"></param>
        /// <param name="Is1991or1997"></param>
        /// <returns>DDRTDataType</returns>
        private CSSDataType FillDdrtCdfEntity(string[] strData, int intTotalChannel, string strFilePath, bool Is1991or1997, ComtradeDeviceInfo objSelectedDev)
        {
            ApplyServerCulture();
            //Create instance of DFRCDFEntity
            CSSDataType objDdrtCdf = new CSSDataType();
            string strTempString = string.Empty;
            CustomDateTime StartTime = new CustomDateTime();
            CustomDateTime TriggerTime = new CustomDateTime();

            double dblSamplingRate = 0;
            double[] aDblSamplingFrequenceInHz = null;
            string strInfFilePath = string.Empty;

            try
            {
                string strTimeZone = string.Empty;
                if (!objSelectedDev.IsComtradeFormatUTC)
                {
                    strTimeZone = objSelectedDev.ComtradeTimeCode;
                }
                string[] strTimeZoneValue = strTimeZone.Split('h');
                int hour = 0, min = 0;
                if (strTimeZoneValue.Length > 0)
                {
                    int.TryParse(strTimeZoneValue[0].Replace("-", ""), out hour);
                }
                if (strTimeZoneValue.Length > 1)
                {
                    int.TryParse(strTimeZoneValue[1], out min);
                }
                TimeSpan timeDifference = new TimeSpan(hour, min, 0);
                uint timeZoneSeconds = uint.Parse(timeDifference.TotalSeconds.ToString());


                strTempString = strData[intTotalChannel + 6].Replace(strCarriageReturn, "").Trim();
                DateTime dtTriggerTime = GetDateTimeFromString(strTempString, _cultureInfo, Is1991or1997);
                uint uintTriggerTimeMicroSeconds = uint.Parse((strTempString.Split('.')[1]), _cultureInfo);
                TriggerTime = new CustomDateTime(dtTriggerTime, uintTriggerTimeMicroSeconds);

                strTempString = strData[intTotalChannel + 5].Replace(strCarriageReturn, "").Trim();
                DateTime dtStartTime = GetDateTimeFromString(strTempString, _cultureInfo, Is1991or1997);
                uint uintStartTimeMicroSeconds = uint.Parse((strTempString.Split('.')[1]), _cultureInfo);
                StartTime = new CustomDateTime(dtStartTime, uintStartTimeMicroSeconds);

                string strTime = string.Empty;
                if (StartTime.ToEPOCH().ToString().Split('.').Length > 0)
                {
                    strTime = StartTime.ToEPOCH().ToString().Split('.')[0];
                }

                objDdrtCdf.StartTimeSecondsLocal = uint.Parse(strTime);
                objDdrtCdf.StartTimeSeconds = uint.Parse(strTime);

                if (StartTime.ToString().Split('.').Length > 1)
                {
                    if (StartTime.ToString().Split('.').Length == 2)
                    {
                        strTime = StartTime.ToString().Split('.')[1];
                    }
                    else if (StartTime.ToString().Split('.').Length == 3)
                    {
                        strTime = StartTime.ToString().Split('.')[1] + StartTime.ToString().Split('.')[2];
                    }
                }
                objDdrtCdf.StartTimeMicroSeconds = uint.Parse(strTime);

                if (TriggerTime.ToEPOCH().ToString().Split('.').Length > 0)
                {
                    strTime = TriggerTime.ToEPOCH().ToString().Split('.')[0];
                }

                objDdrtCdf.TriggerTimeSecondsLocal = uint.Parse(strTime);
                objDdrtCdf.TriggerTimeSeconds = uint.Parse(strTime);

                if (!objSelectedDev.IsComtradeFormatUTC)
                {

                    if (strTimeZoneValue[0].Contains("+"))
                    {
                        objDdrtCdf.StartTimeSeconds = objDdrtCdf.StartTimeSeconds + timeZoneSeconds;
                        objDdrtCdf.EndTimeinSeconds = objDdrtCdf.EndTimeinSeconds + timeZoneSeconds;
                        objDdrtCdf.TriggerTimeSeconds = objDdrtCdf.TriggerTimeSeconds + timeZoneSeconds;

                    }
                    else if (strTimeZoneValue[0].Contains("-"))
                    {
                        objDdrtCdf.StartTimeSeconds = objDdrtCdf.StartTimeSeconds - timeZoneSeconds;
                        objDdrtCdf.EndTimeinSeconds = objDdrtCdf.EndTimeinSeconds - timeZoneSeconds;
                        objDdrtCdf.TriggerTimeSeconds = objDdrtCdf.TriggerTimeSeconds - timeZoneSeconds;
                    }
                }
                else
                {
                    //Date Time is already in UTC
                }

                if (TriggerTime.ToString().Split('.').Length > 1)
                {
                    if (TriggerTime.ToString().Split('.').Length == 2)
                    {
                        strTime = TriggerTime.ToString().Split('.')[1];
                    }
                    else if (TriggerTime.ToString().Split('.').Length == 3)
                    {
                        strTime = TriggerTime.ToString().Split('.')[1] +
                            TriggerTime.ToString().Split('.')[2];
                    }
                }

                objDdrtCdf.TriggerTimeMicroSeconds = uint.Parse(strTime);

                //Get Sampling Rate,Total number of samples
                strTempString = strData[intTotalChannel + 3].Replace(strCarriageReturn, "").Trim();
                dblSamplingRate = int.Parse(strTempString, _cultureInfo);

                objDdrtCdf.SamplingRate = dblSamplingRate;
                objDdrtCdf.TotalNumberOfSamples = (int)dblSamplingRate;
                objDdrtCdf.SampleRateInHz = (int)dblSamplingRate;

                int intTotalNumberOfSamples = 0;
                List<int> lstDataPoints = new List<int>();
                aDblSamplingFrequenceInHz = new double[(int)dblSamplingRate];
                #region Sampling Rate and No of samples for that sampling rate
                for (int intSampleRateIndex = 0; intSampleRateIndex <
                    dblSamplingRate; intSampleRateIndex++)
                {
                    strTempString = strData[intTotalChannel +
                        intSampleRateIndex + 4].Replace(strCarriageReturn, "").Trim();
                    aDblSamplingFrequenceInHz[intSampleRateIndex] = (double.Parse
                        ((strTempString.Split(chrDelimiter))[0].Trim()));

                    int intSamples = int.Parse((strTempString.Split(chrDelimiter))[1].Trim());
                    lstDataPoints.Add(intSamples - intTotalNumberOfSamples);
                    intTotalNumberOfSamples = intSamples;

                    objDdrtCdf.SampleRateInHz = aDblSamplingFrequenceInHz[aDblSamplingFrequenceInHz.Length - 1];
                    objDdrtCdf.TotalNumberOfSamples = lstDataPoints[lstDataPoints.Count - 1];
                }
                #endregion Sampling Rate and No of samples for that sampling rate

                uint dblDuration = 0;
                if (dblSamplingRate != 0)
                {
                    dblDuration = (uint)intTotalNumberOfSamples / (uint)aDblSamplingFrequenceInHz[0];
                }
                else
                {
                    dblDuration = 0;
                }

                objDdrtCdf.EndTimeinSeconds = objDdrtCdf.StartTimeSeconds + dblDuration;
                objDdrtCdf.EndTimeSecondsLocal = (double)objDdrtCdf.StartTimeSecondsLocal + dblDuration;

                strTempString = strData[0].Replace(strCarriageReturn, "").Trim();
                string[] strComtradeYear = strTempString.Split(chrDelimiter);
                if (strComtradeYear.Length > 2)
                {
                    objDdrtCdf.ComtradeYear = ((strTempString.Split(chrDelimiter))[2].Trim());
                }
                else
                {
                    objDdrtCdf.ComtradeYear = "1991";
                }
                objDdrtCdf.CauseOfTrigger = 256; //Unknown if inf file is not present

                strInfFilePath = strFilePath.Remove(strFilePath.LastIndexOf('.')) + ".sinf";
                if (File.Exists(strInfFilePath))
                {
                    File.Move(strInfFilePath, Path.ChangeExtension(strInfFilePath, ".inf"));
                    strInfFilePath = strFilePath.Remove(strFilePath.LastIndexOf('.')) + ".inf";
                }
                if (File.Exists(strInfFilePath))
                {
                    Carrick.Framework.Util.InIParser infFile = new Carrick.Framework.Util.InIParser(strInfFilePath);
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                             , WaveformViewerConstants.CauseOfTrigger)))
                    {
                        objDdrtCdf.CauseOfTrigger = Enum.Parse(typeof(Carrick.Framework.Constants.CauseOfTrigger),
                            infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation, WaveformViewerConstants.CauseOfTrigger)).GetHashCode();
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PublicRecordInformation
                            , WaveformViewerConstants.DecimationFactor)))
                    {
                        objDdrtCdf.DecimationFactor = Convert.ToInt32(infFile.GetSetting(WaveformViewerConstants.PublicRecordInformation
                                , WaveformViewerConstants.DecimationFactor));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.LastRecord)))
                    {
                        objDdrtCdf.LastRecord = Convert.ToBoolean(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.LastRecord));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.RecordStatus)))
                    {
                        objDdrtCdf.RecordStatus = Convert.ToInt32(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.RecordStatus));
                    }
                    if (!string.IsNullOrEmpty(objDdrtCdf.TriggerTimeSeconds.ToString()))
                    {
                        objDdrtCdf.RecordNumber = (long)objDdrtCdf.TriggerTimeSeconds;
                    }
                    //Code to add the triggered parameter info details to inf file.
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.COTText)))
                    {
                        objDdrtCdf.CalculationLabel = infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.COTText);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(objDdrtCdf.TriggerTimeSeconds.ToString()))
                    {
                        objDdrtCdf.RecordNumber = (long)objDdrtCdf.TriggerTimeSeconds;
                    }
                }
                if (File.Exists(strFilePath))
                {
                    File.Move(strFilePath, Path.ChangeExtension(strInfFilePath, ".scfg"));
                }
                if (File.Exists(strInfFilePath))
                {
                    File.Move(strInfFilePath, Path.ChangeExtension(strInfFilePath, ".sinf"));
                }
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::FillDdrtCdfEntity", LogWriter.Level.ERROR, ex.Message,
                              ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);

            }
            return objDdrtCdf;
        }
        #endregion Filling entity fir DDR-T. OB #6285

        /// <summary>
        /// Method to fill entity for DDR type
        /// Added to support multiple comtrade import #8927
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="intTotalChannel"></param>
        /// <param name="strFilePath"></param>
        /// <param name="Is1991or1997"></param>
        /// <returns></returns>
        private CSSDataType FillCssCdfEntity(string[] strData, int intTotalChannel, string strFilePath, bool Is1991or1997, ComtradeDeviceInfo objSelectedDev)
        {
            ApplyServerCulture();
            //Create instance of DFRCDFEntity
            CSSDataType objCSSCdf = new CSSDataType();
            string strTempString = string.Empty;
            //DataSource dsSource = new DataSource();
            CustomDateTime StartTime = new CustomDateTime();
            CustomDateTime TriggerTime = new CustomDateTime();

            double dblSamplingRate = 0;
            double[] aDblSamplingFrequenceInHz = null;
            string strInfFilePath = string.Empty;

            try
            {
                //string strCfgFileName = strFilePath.Substring(strFilePath.LastIndexOf("\\") + 1);
                //string[] temp = strCfgFileName.Split(',');
                //string strTimeZone = string.Empty;
                //if (temp.Length > 2)
                //{
                //    strTimeZone = temp[2];
                //}

                string strTimeZone = string.Empty;
                if (!objSelectedDev.IsComtradeFormatUTC)
                {
                    strTimeZone = objSelectedDev.ComtradeTimeCode;
                }
                string[] strTimeZoneValue = strTimeZone.Split('h');
                int hour = 0, min = 0;
                if (strTimeZoneValue.Length > 0)
                {
                    int.TryParse(strTimeZoneValue[0].Replace("-", ""), out hour);
                }
                if (strTimeZoneValue.Length > 1)
                {
                    int.TryParse(strTimeZoneValue[1], out min);
                }
                TimeSpan timeDifference = new TimeSpan(hour, min, 0);
                uint timeZoneSeconds = uint.Parse(timeDifference.TotalSeconds.ToString());


                strTempString = strData[intTotalChannel + 6].Replace(strCarriageReturn, "").Trim();
                DateTime dtTriggerTime = GetDateTimeFromString(strTempString, _cultureInfo, Is1991or1997);
                uint uintTriggerTimeMicroSeconds = uint.Parse((strTempString.Split('.')[1]), _cultureInfo);
                TriggerTime = new CustomDateTime(dtTriggerTime, uintTriggerTimeMicroSeconds);

                strTempString = strData[intTotalChannel + 5].Replace(strCarriageReturn, "").Trim();
                DateTime dtStartTime = GetDateTimeFromString(strTempString, _cultureInfo, Is1991or1997);
                uint uintStartTimeMicroSeconds = uint.Parse((strTempString.Split('.')[1]), _cultureInfo);
                StartTime = new CustomDateTime(dtStartTime, uintStartTimeMicroSeconds);

                string strTime = string.Empty;
                if (StartTime.ToEPOCH().ToString().Split('.').Length > 0)
                {
                    strTime = StartTime.ToEPOCH().ToString().Split('.')[0];
                }

                objCSSCdf.StartTimeSecondsLocal = uint.Parse(strTime);
                objCSSCdf.StartTimeSeconds = uint.Parse(strTime);


                if (StartTime.ToString().Split('.').Length > 1)
                {
                    if (StartTime.ToString().Split('.').Length == 2)
                    {
                        strTime = StartTime.ToString().Split('.')[1];
                    }
                    else if (StartTime.ToString().Split('.').Length == 3)
                    {
                        strTime = StartTime.ToString().Split('.')[1] + StartTime.ToString().Split('.')[2];
                    }
                }
                objCSSCdf.StartTimeMicroSeconds = uint.Parse(strTime);

                if (TriggerTime.ToEPOCH().ToString().Split('.').Length > 0)
                {
                    strTime = TriggerTime.ToEPOCH().ToString().Split('.')[0];
                }
                //objCSSCdf.TriggerTimeSecondsLocal = uint.Parse(strTime);
                objCSSCdf.TriggerTimeSeconds = uint.Parse(strTime);


                if (TriggerTime.ToString().Split('.').Length > 1)
                {
                    if (TriggerTime.ToString().Split('.').Length == 2)
                    {
                        strTime = TriggerTime.ToString().Split('.')[1];
                    }
                    else if (TriggerTime.ToString().Split('.').Length == 3)
                    {
                        strTime = TriggerTime.ToString().Split('.')[1] +
                            TriggerTime.ToString().Split('.')[2];
                    }
                }

                if (!objSelectedDev.IsComtradeFormatUTC)
                {

                    if (strTimeZoneValue[0].Contains("+"))
                    {
                        objCSSCdf.StartTimeSeconds = objCSSCdf.StartTimeSeconds + timeZoneSeconds;
                        objCSSCdf.EndTimeinSeconds = objCSSCdf.EndTimeinSeconds + timeZoneSeconds;
                        objCSSCdf.TriggerTimeSeconds = objCSSCdf.TriggerTimeSeconds + timeZoneSeconds;

                    }
                    else if (strTimeZoneValue[0].Contains("-"))
                    {
                        objCSSCdf.StartTimeSeconds = objCSSCdf.StartTimeSeconds - timeZoneSeconds;
                        objCSSCdf.EndTimeinSeconds = objCSSCdf.EndTimeinSeconds - timeZoneSeconds;
                        objCSSCdf.TriggerTimeSeconds = objCSSCdf.TriggerTimeSeconds - timeZoneSeconds;
                    }

                }
                else
                {
                    //Date Time is already in UTC


                }
                objCSSCdf.TriggerTimeMicroSeconds = uint.Parse(strTime);

                //Get Sampling Rate,Total number of samples
                strTempString = strData[intTotalChannel + 3].Replace(strCarriageReturn, "").Trim();
                dblSamplingRate = int.Parse(strTempString, _cultureInfo);

                objCSSCdf.SamplingRate = dblSamplingRate;
                //objCSSCdf.TotalNumberOfSamples = new int[(int)dblSamplingRate];
                //objCSSCdf.SampleRateInHz = new double[(int)dblSamplingRate];

                int intTotalNumberOfSamples = 0;
                List<int> lstDataPoints = new List<int>();
                aDblSamplingFrequenceInHz = new double[(int)dblSamplingRate];
                #region Sampling Rate and No of samples for that sampling rate
                for (int intSampleRateIndex = 0; intSampleRateIndex <
                    dblSamplingRate; intSampleRateIndex++)
                {
                    strTempString = strData[intTotalChannel +
                        intSampleRateIndex + 4].Replace(strCarriageReturn, "").Trim();
                    aDblSamplingFrequenceInHz[intSampleRateIndex] = (double.Parse
                        ((strTempString.Split(chrDelimiter))[0].Trim()));

                    int intSamples = int.Parse((strTempString.Split(chrDelimiter))[1].Trim());
                    lstDataPoints.Add(intSamples - intTotalNumberOfSamples);
                    intTotalNumberOfSamples = intSamples;

                    //objCSSCdf.SampleRateInHz[intSampleRateIndex] = aDblSamplingFrequenceInHz
                    //    [aDblSamplingFrequenceInHz.Length - 1];
                    //objCSSCdf.TotalNumberOfSamples[intSampleRateIndex] = lstDataPoints[lstDataPoints.Count - 1];

                    objCSSCdf.SampleRateInHz = aDblSamplingFrequenceInHz[aDblSamplingFrequenceInHz.Length - 1];
                    objCSSCdf.TotalNumberOfSamples = lstDataPoints[lstDataPoints.Count - 1];
                }
                #endregion

                uint dblDuration = 0;
                if (dblSamplingRate != 0)
                {
                    dblDuration = (uint)intTotalNumberOfSamples / (uint)aDblSamplingFrequenceInHz[0];
                }
                else
                {
                    dblDuration = 0;
                }

                objCSSCdf.EndTimeinSeconds = objCSSCdf.StartTimeSeconds + dblDuration;
                objCSSCdf.EndTimeSecondsLocal = (double)objCSSCdf.StartTimeSecondsLocal + dblDuration;

                strTempString = strData[0].Replace(strCarriageReturn, "").Trim();
                string[] strComtradeYear = strTempString.Split(chrDelimiter);
                if (strComtradeYear.Length > 2)
                {
                    objCSSCdf.ComtradeYear = ((strTempString.Split(chrDelimiter))[2].Trim());
                }
                else
                {
                    objCSSCdf.ComtradeYear = "1991";
                }
                objCSSCdf.CauseOfTrigger = 256;//Un Known if inf file is not present

                strInfFilePath = strFilePath.Remove(strFilePath.LastIndexOf('.')) + ".sinf";
                if (File.Exists(strInfFilePath))
                {
                    File.Move(strInfFilePath, Path.ChangeExtension(strInfFilePath, ".inf"));
                    strInfFilePath = strFilePath.Remove(strFilePath.LastIndexOf('.')) + ".inf";
                }
                if (File.Exists(strInfFilePath))
                {
                    Carrick.Framework.Util.InIParser infFile = new Carrick.Framework.Util.InIParser(strInfFilePath);
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                             , WaveformViewerConstants.CauseOfTrigger)))
                    {
                        objCSSCdf.CauseOfTrigger = Enum.Parse(typeof(Carrick.Framework.Constants.CauseOfTrigger),
                            infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation, WaveformViewerConstants.CauseOfTrigger)).GetHashCode();
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PublicRecordInformation
                            , WaveformViewerConstants.DecimationFactor)))
                    {
                        objCSSCdf.DecimationFactor = Convert.ToInt32(infFile.GetSetting(WaveformViewerConstants.PublicRecordInformation
                                , WaveformViewerConstants.DecimationFactor));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.LastRecord)))
                    {
                        objCSSCdf.LastRecord = Convert.ToBoolean(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.LastRecord));
                    }
                    if (!string.IsNullOrEmpty(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                            , WaveformViewerConstants.RecordStatus)))
                    {
                        objCSSCdf.RecordStatus = Convert.ToInt32(infFile.GetSetting(WaveformViewerConstants.PrivateRecordInformation
                                , WaveformViewerConstants.RecordStatus));
                    }
                }
                if (File.Exists(strFilePath))
                {
                    File.Move(strFilePath, Path.ChangeExtension(strInfFilePath, ".scfg"));
                }
                if (File.Exists(strInfFilePath))
                {
                    File.Move(strInfFilePath, Path.ChangeExtension(strInfFilePath, ".sinf"));
                }

            }
            catch (Exception ex)
            {
                //File.Move(strFilePath, Path.ChangeExtension(strInfFilePath, ".cfgf"));
                //File.Move(strInfFilePath, Path.ChangeExtension(strInfFilePath, ".inff"));
                MessageBusLogger.LogError("ComtradeImport::FillCssCdfEntity", LogWriter.Level.ERROR, ex.Message,
                              ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);

            }
            return objCSSCdf;
        }

        /// <summary>
        /// Gets the date time from string.
        /// </summary>
        /// <param name="strTempDateTime">The STR temp date time.</param>
        /// <param name="_cultureInfo">The _culture info.</param>
        /// <param name="Is1991">if set to <c>true</c> [is1991].</param>
        /// <returns></returns>
        private DateTime GetDateTimeFromString(string strTempDateTime, CultureInfo _cultureInfo, bool Is1991)
        {
            DateTime dtConvertedDateTime = new DateTime();
            IFormatProvider culture;
            try
            {
                dtConvertedDateTime = new DateTime();
                string[] strSubString = strTempDateTime.Split('.');
                //Convert String to DateTime
                culture = new CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false);
                if (Is1991)
                {
                    string[] strFormats = new string[] { WaveformViewerConstants.OLE_DATE_FORMATE1991, WaveformViewerConstants.OLE_DATE_FORMATE1991_Ben };
                    dtConvertedDateTime = (DateTime.ParseExact(
                 strSubString[0], strFormats, _cultureInfo, DateTimeStyles.NoCurrentDateDefault));
                }
                else
                {
                    string[] strFormats = new string[] { WaveformViewerConstants.OLE_DATE_FORMATE, WaveformViewerConstants.OLE_DATE_FORMATE_Ben };
                    dtConvertedDateTime = (DateTime.ParseExact(
                    strSubString[0], strFormats, _cultureInfo, DateTimeStyles.NoCurrentDateDefault));
                }
            }

            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::GetDateTimeFromString", LogWriter.Level.ERROR, ex.Message,
                             ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);

            }
            return dtConvertedDateTime;
        }
        /// <summary>
        /// Method to read file.
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        private string ReadFile(string strFilePath)
        {
            string strdata = string.Empty;
            FileStream fsFileStream = null;
            StreamReader srFleStreamReader = null;
            try
            {
                if (System.IO.File.Exists(strFilePath))
                {
                    fsFileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
                    srFleStreamReader = new StreamReader(fsFileStream);
                    strdata = srFleStreamReader.ReadToEnd();
                    srFleStreamReader.Close();
                    fsFileStream.Close();
                }

            }
            catch (Exception ex)
            {
                if (fsFileStream != null)
                {
                    fsFileStream.Close();
                }
                if (srFleStreamReader != null)
                {
                    srFleStreamReader.Close();
                }
                //File.Move(strFilePath, Path.ChangeExtension(strFilePath, ".cfgf"));
                MessageBusLogger.LogError("ComtradeImport::ReadFile", LogWriter.Level.ERROR, ex.Message,
                               ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
            }
            return strdata;
        }
        /// <summary>
        /// Method to write comtrade files to temp folder.
        /// </summary>
        /// <param name="bytArrFileData"></param>
        /// <param name="strFolderName"></param>
        /// <param name="strFileName"></param>
        private void WriteComtradeFilesToTempFolder(byte[] bytArrFileData, string strFolderName,
            string strFileName)
        {
            FileStream fsStream = null;
            try
            {
                string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" +
                    strFolderName + "\\" + strFileName;
                fsStream = new FileStream(strFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                fsStream.Write(bytArrFileData, 0, bytArrFileData.Length);
                fsStream.Close();
                ZipUtil.UnZipFiles(strFilePath, Path.GetDirectoryName(strFilePath));
                File.Delete(strFilePath);

            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::WriteComtradeFilesToTempFolder", LogWriter.Level.ERROR, ex.Message,
                               ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
            }
            finally
            {
                fsStream.Close();
            }
        }
        #endregion

        #region Create Device & Update Statistics
        /// <summary>
        /// Method to create comtrade devices
        /// </summary>
        /// <param name="lstDeviceDTO"></param>
        /// <param name="cbkImportdata"></param>
        private void CreateComtradeDevices(List<DeviceDTO> lstDeviceDTO, CallBack cbkImportdata)
        {
            _boolIsLicenced = true;
            DataTable dtDeviceTimezone = new DataTable();

            try
            {

                dtDeviceTimezone.Columns.Add("DEVICEID", typeof(long));
                dtDeviceTimezone.Columns.Add("DEVICENAME", typeof(string));
                dtDeviceTimezone.Columns.Add("SUBSTATIONNAME", typeof(string));
                dtDeviceTimezone.Columns.Add("TIMEZONEID", typeof(string));
                dtDeviceTimezone.Columns.Add("DAYLIGHTSAVING", typeof(bool));
                for (int intDeviceCount = 0; intDeviceCount < lstDeviceDTO.Count; intDeviceCount++)
                {

                    ReplyData replyData = PublishRequestMessage(lstDeviceDTO[intDeviceCount],
                           Topics.DeviceManager.DeviceAdministration,
                           MessageActions.DeviceManager.CreateComtradeDevices,
                            ResponseTopics.DeviceManager.DeviceAdministration + "_"
                            + Thread.CurrentThread.ManagedThreadId.ToString()
                            + DateTime.Now.Ticks.ToString(),
                           ResponseMessageActions.DeviceManager.CreateComtradeDevices);
                    if (replyData.PayLoad.GetType() == typeof(ResponseMessage))
                    {
                        //Get the Response from call back object
                        ResponseMessage rsmsgResponseMessage = (ResponseMessage)replyData.PayLoad;
                        ResponseMessage rmgResponseMessagePub = new ResponseMessage();
                        //Is device created successfully
                        if (rsmsgResponseMessage.Result == "DMMsg1002"
                            || rsmsgResponseMessage.Result == "DMMsg1000")
                        {
                            if (rsmsgResponseMessage.Result == "DMMsg1002")
                            {
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1156";
                            }
                            else
                            {
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1094";
                            }
                            long lngDeviceId = Convert.ToInt64(((ResponseMessage)replyData.PayLoad).Response);
                            lngCreatedDeviceId = lngDeviceId;
                            GetDeviceDetails(lngDeviceId,
                                ref dtDeviceTimezone, lstDeviceDTO[intDeviceCount]);
                            rmgResponseMessagePub.Response = lstDeviceDTO[intDeviceCount].DeviceName;

                            PublishResponseMessage(cbkImportdata, rmgResponseMessagePub.Result,
                                 ResponseMessageType.Information, false);
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1036")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "ACTYLOGMSG1352";
                            PublishResponseMessage(cbkImportdata, rmgResponseMessagePub.Result,
                                ResponseMessageType.Information, false);
                            //PublishDataMessage(cbkImportdata, rmgResponseMessagePub, "CreateComtradeDevices");
                            _boolIsLicenced = false;
                            break;
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1037")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "ACTYLOGMSG1351";
                            PublishResponseMessage(cbkImportdata, rmgResponseMessagePub.Result,
                                ResponseMessageType.Information, false);
                            //PublishDataMessage(cbkImportdata, rmgResponseMessagePub, "CreateComtradeDevices");
                            _boolIsLicenced = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::CreateComtradeDevices", LogWriter.Level.ERROR, ex.Message,
                               ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
            }
            finally
            {
                if (null != dtDeviceTimezone)
                    dtDeviceTimezone.Dispose();
                dtDeviceTimezone = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstDeviceDTO"></param>
        /// <param name="cbkImportdata"></param>
        private void CreateComtradeDevicesForScheduler(List<DeviceDTO> lstDeviceDTO, CallBack cbkImportdata)
        {
            _boolIsLicenced = true;
            DataTable dtDeviceTimezone = new DataTable();

            try
            {
                dtDeviceTimezone.Columns.Add("DEVICEID", typeof(long));
                dtDeviceTimezone.Columns.Add("DEVICENAME", typeof(string));
                dtDeviceTimezone.Columns.Add("SUBSTATIONNAME", typeof(string));
                dtDeviceTimezone.Columns.Add("TIMEZONEID", typeof(string));
                dtDeviceTimezone.Columns.Add("DAYLIGHTSAVING", typeof(bool));
                for (int intDeviceCount = 0; intDeviceCount < lstDeviceDTO.Count; intDeviceCount++)
                {

                    ReplyData replyData = PublishRequestMessage(lstDeviceDTO[intDeviceCount],
                           Topics.DeviceManager.DeviceAdministration,
                           MessageActions.DeviceManager.CreateComtradeDevicesForScheduler,
                            ResponseTopics.DeviceManager.DeviceAdministration + "_"
                            + Thread.CurrentThread.ManagedThreadId.ToString()
                            + DateTime.Now.Ticks.ToString(),
                           ResponseMessageActions.DeviceManager.CreateComtradeDevicesForScheduler);
                    if (replyData.PayLoad.GetType() == typeof(ResponseMessage))
                    {
                        //Get the Response from call back object
                        ResponseMessage rsmsgResponseMessage = (ResponseMessage)replyData.PayLoad;
                        ResponseMessage rmgResponseMessagePub = new ResponseMessage();
                        //Is device created successfully
                        if (rsmsgResponseMessage.Result == "DMMsg1002"
                            || rsmsgResponseMessage.Result == "DMMsg1000")
                        {
                            if (rsmsgResponseMessage.Result == "DMMsg1002")
                            {
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1156";
                            }
                            else
                            {
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1094";
                            }
                            long lngDeviceId = Convert.ToInt64(((ResponseMessage)replyData.PayLoad).Response);
                            lngCreatedDeviceId = lngDeviceId;
                            GetDeviceDetails(lngDeviceId,
                                ref dtDeviceTimezone, lstDeviceDTO[intDeviceCount]);
                            rmgResponseMessagePub.Response = lstDeviceDTO[intDeviceCount].DeviceName;

                            PublishResponseMessage(cbkImportdata, rmgResponseMessagePub.Result,
                                 ResponseMessageType.Information, false);
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1036")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "ACTYLOGMSG1352";
                            PublishResponseMessage(cbkImportdata, rmgResponseMessagePub.Result,
                                ResponseMessageType.Information, false);
                            PublishDataMessage(cbkImportdata, rmgResponseMessagePub, "CreateComtradeDevices");
                            _boolIsLicenced = false;
                            break;
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1037")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "ACTYLOGMSG1351";
                            PublishResponseMessage(cbkImportdata, rmgResponseMessagePub.Result,
                                ResponseMessageType.Information, false);
                            PublishDataMessage(cbkImportdata, rmgResponseMessagePub, "CreateComtradeDevices");
                            _boolIsLicenced = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBusLogger.LogError("ComtradeImport::CreateComtradeDevices", LogWriter.Level.ERROR, ex.Message,
                               ex.InnerException, "", ToString());
                throw new ServerBOException(ex.Message, "ACTYLOGMSG1462", ex);
            }
            finally
            {
                if (null != dtDeviceTimezone)
                    dtDeviceTimezone.Dispose();
                dtDeviceTimezone = null;
            }
        }

        /// <summary>
        ///  Method to create fresh comtrade devices from comtrade file.
        ///  Added for #8927 implementation
        /// </summary>
        /// <param name="cbkImportdata"></param>
        private void CreateUnmappedComtradeDevicesFromFile(CallBack cbkImportdata)
        {
            bool blnIsDeviceExists = false, blnIsDeviceLimitExceed = false;

            try
            {

                List<ComtradeDeviceInfo> lstDeviceDTO = (List<ComtradeDeviceInfo>)cbkImportdata.PayLoad;
                List<int> lstDeviceIds = new List<int>();
                //List<ComtradeDeviceInfo> lstNewDeviceDTO = new List<ComtradeDeviceInfo>();
                for (int intDeviceCount = 0; intDeviceCount < lstDeviceDTO.Count; intDeviceCount++)
                {

                    ReplyData replyData = PublishRequestMessage(lstDeviceDTO[intDeviceCount],
                           Topics.DeviceManager.DeviceAdministration,
                           MessageActions.DeviceManager.CreateComtradeDevices,
                            ResponseTopics.DeviceManager.DeviceAdministration + "_"
                            + Thread.CurrentThread.ManagedThreadId.ToString()
                            + DateTime.Now.Ticks.ToString(),
                           ResponseMessageActions.DeviceManager.CreateComtradeDevices);
                    if (replyData.PayLoad.GetType() == typeof(ResponseMessage))
                    {
                        //Get the Response from call back object
                        ResponseMessage rsmsgResponseMessage = (ResponseMessage)replyData.PayLoad;
                        ResponseMessage rmgResponseMessagePub = new ResponseMessage();

                        //Is device created successfully
                        if (rsmsgResponseMessage.Result == "DMMsg1002"
                            || rsmsgResponseMessage.Result == "DMMsg1000")
                        {
                            if (rsmsgResponseMessage.Result == "DMMsg1002")
                            {
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1156";
                                //blnDeviceCreated = false;
                                blnIsDeviceExists = true;

                            }
                            else
                            {

                                int lngDeviceId = Convert.ToInt32(((ResponseMessage)replyData.PayLoad).Response);
                                lstDeviceIds.Add(lngDeviceId);
                                lstDeviceDTO[intDeviceCount].ComtradeDeviceId = lngDeviceId;
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1094";
                            }
                            rmgResponseMessagePub.Response = lstDeviceDTO[intDeviceCount].DeviceName;
                            PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                          MessageType.Text, rmgResponseMessagePub);
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1036")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "UMMsg1036";
                            PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                          MessageType.Text, rmgResponseMessagePub);
                            blnIsDeviceLimitExceed = true;
                            break;
                        }
                    }

                    Thread.Sleep(20);
                    UpdateStatistics(ImportConstants.IMPORTDEVICES, lstDeviceDTO[intDeviceCount].DeviceName, intDeviceCount + 1,
                        lstDeviceDTO.Count, cbkImportdata);
                    Thread.Sleep(20);

                }
                if (lstDeviceIds.Count > 0)
                {

                    PublishDataMessage(cbkImportdata.RequestorTopic, ResponseMessageActions.Import.CreateComtradeDevicesfromFile,
                                                     MessageType.Text, (List<ComtradeDeviceInfo>)lstDeviceDTO);
                }
                else
                {
                    if (blnIsDeviceExists)
                    {
                        ResponseMessage rmgResponseMessage = new ResponseMessage();
                        rmgResponseMessage.Result = "ACTYLOGMSG1349";
                        PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                      MessageType.Text, rmgResponseMessage);
                        PublishDataMessage(cbkImportdata, (List<ComtradeDeviceInfo>)lstDeviceDTO, "CreateReplayPlusDevices");
                    }
                    else
                    {
                        ResponseMessage rmgResponseMessage = new ResponseMessage();
                        if (blnIsDeviceLimitExceed)
                        {
                            rmgResponseMessage.Result = "ACTYLOGMSG1352";
                        }
                        else
                        {
                            rmgResponseMessage.Result = "ACTYLOGMSG1351";
                        }
                        PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                      MessageType.Text, rmgResponseMessage);
                        PublishDataMessage(cbkImportdata, rmgResponseMessage, "CreateReplayPlusDevices");
                    }
                }
            }
            catch (Exception expt)
            {
                MessageBusLogger.LogEvent("Import::CreateUnmappedComtradeDevicesFromFile: Error while processing imported"
                                          + "dat files result.", LogWriter.Level.ERROR);
                MessageBusLogger.LogError("Import::CreateUnmappedComtradeDevicesFromFile",
                                          LogWriter.Level.ERROR, expt.Message, expt.InnerException,
                                          "IMPMsg1002", ToString());
                PublishResponseMessage(cbkImportdata, "ACTYLOGMSG1462",
                                                ResponseMessageType.Error, true);
            }
        }
        /// <summary>
        /// Method to create comtrade devices from comtrade file.
        /// </summary>
        /// <param name="cbkImportdata"></param>
        private void CreateComtradeDevicesFromFile(CallBack cbkImportdata)
        {
            _boolIsLicenced = true;
            DataTable dtDeviceTimezone = new DataTable();
            DataTable dtTimeZoneId = null;
            DataSet dsDevicetimezone = null;

            try
            {
                bool blnIsDeviceExists = false;
                bool blnIsDeviceLimitExceed = false;

                dtDeviceTimezone.Columns.Add("DEVICEID", typeof(long));
                dtDeviceTimezone.Columns.Add("DEVICENAME", typeof(string));
                dtDeviceTimezone.Columns.Add("SUBSTATIONNAME", typeof(string));
                dtDeviceTimezone.Columns.Add("TIMEZONEID", typeof(string));
                dtDeviceTimezone.Columns.Add("DAYLIGHTSAVING", typeof(bool));

                List<DeviceDTO> lstDeviceDTO = (List<DeviceDTO>)cbkImportdata.PayLoad;
                List<DeviceDTO> lstNewDeviceDTO = new List<DeviceDTO>();
                for (int intDeviceCount = 0; intDeviceCount < lstDeviceDTO.Count; intDeviceCount++)
                {

                    ReplyData replyData = PublishRequestMessage(lstDeviceDTO[intDeviceCount],
                           Topics.DeviceManager.DeviceAdministration,
                           MessageActions.DeviceManager.CreateComtradeDevices,
                            ResponseTopics.DeviceManager.DeviceAdministration + "_"
                            + Thread.CurrentThread.ManagedThreadId.ToString()
                            + DateTime.Now.Ticks.ToString(),
                           ResponseMessageActions.DeviceManager.CreateComtradeDevices);
                    if (replyData.PayLoad.GetType() == typeof(ResponseMessage))
                    {
                        //Get the Response from call back object
                        ResponseMessage rsmsgResponseMessage = (ResponseMessage)replyData.PayLoad;
                        ResponseMessage rmgResponseMessagePub = new ResponseMessage();
                        //Is device created successfully
                        if (rsmsgResponseMessage.Result == "DMMsg1002"
                            || rsmsgResponseMessage.Result == "DMMsg1000")
                        {
                            if (rsmsgResponseMessage.Result == "DMMsg1002")
                            {
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1156";
                                //blnDeviceCreated = false;
                                blnIsDeviceExists = true;

                            }
                            else
                            {
                                long lngDeviceId = Convert.ToInt64(((ResponseMessage)replyData.PayLoad).Response);
                                GetDeviceDetails(lngDeviceId,
                                    ref dtDeviceTimezone, lstDeviceDTO[intDeviceCount]);
                                //lstNewDeviceDTO.Add(lstDeviceDTO[intDeviceCount]);
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1094";
                            }
                            rmgResponseMessagePub.Response = lstDeviceDTO[intDeviceCount].DeviceName;
                            PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                          MessageType.Text, rmgResponseMessagePub);
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1036")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "UMMsg1036";
                            PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                          MessageType.Text, rmgResponseMessagePub);
                            blnIsDeviceLimitExceed = true;
                            break;
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1037")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "UMMsg1037";

                            string strDeviceType = string.Empty;
                            switch (lstDeviceDTO[intDeviceCount].DeviceType)
                            {
                                case DeviceType.DFR1200:
                                    strDeviceType = "DFR1200";
                                    break;
                                case DeviceType.DFRII:
                                    strDeviceType = "DFRII";
                                    break;
                                case DeviceType.IDM:
                                    strDeviceType = "IDM";
                                    break;
                                case DeviceType.IMS:
                                    strDeviceType = "IMS";
                                    break;
                                case DeviceType.DSFLMK1_2:
                                    strDeviceType = "DSFLMK1_2";
                                    break;
                                case DeviceType.DSFLMK3:
                                    strDeviceType = "DSFLMK3";
                                    break;
                                case DeviceType.TWSMK3_4:
                                    strDeviceType = "TWSMK3_4";
                                    break;
                                case DeviceType.TWSMK5_6:
                                    strDeviceType = "TWSMK5_6";
                                    break;
                            }
                            rmgResponseMessagePub.Response = strDeviceType;
                            PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                          MessageType.Text, rmgResponseMessagePub);
                        }
                    }
                    Thread.Sleep(20);
                    UpdateStatistics(ImportConstants.IMPORTDEVICES, lstDeviceDTO[intDeviceCount].DeviceName, intDeviceCount + 1,
                        lstDeviceDTO.Count, cbkImportdata);
                    Thread.Sleep(20);
                }
                if (dtDeviceTimezone.Rows.Count > 0)
                {
                    dsDevicetimezone = new DataSet();
                    dtDeviceTimezone.TableName = "DeviceTimeZone";
                    dsDevicetimezone.Tables.Add(dtDeviceTimezone.Copy());
                    dtTimeZoneId = new DataTable();
                    string strquery = "select ZoneID from TimeZone";
                    Thread.Sleep(1000);
                    ReplyData replyData = PublishRequestMessage(strquery, Topics.DeviceManager.DeviceAdministration,
                                    MessageActions.DeviceManager.GetDataForImport,
                                    ResponseTopics.DeviceManager.DeviceAdministration
                                    + DateTime.Now.Ticks.ToString(),
                                    ResponseMessageActions.DeviceManager.CreateComtradeDevices);
                    dtTimeZoneId = (DataTable)replyData.PayLoad;
                    dtTimeZoneId.TableName = "TimeZoneID";
                    //dtTimeZoneId = objQis.GetTimeZoneIDs();
                    dsDevicetimezone.Tables.Add(dtTimeZoneId.Copy());

                    PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.GetTimeZoneInfo,
                                                     MessageType.Text, dsDevicetimezone.Copy());
                }
                else
                {
                    if (blnIsDeviceExists)
                    {
                        ResponseMessage rmgResponseMessage = new ResponseMessage();
                        rmgResponseMessage.Result = "ACTYLOGMSG1349";
                        PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                      MessageType.Text, rmgResponseMessage);
                        PublishDataMessage(cbkImportdata, dtDeviceTimezone.Copy(), "CreateReplayPlusDevices");
                    }
                    else
                    {
                        ResponseMessage rmgResponseMessage = new ResponseMessage();
                        if (blnIsDeviceLimitExceed)
                        {
                            rmgResponseMessage.Result = "ACTYLOGMSG1352";
                        }
                        else
                        {
                            rmgResponseMessage.Result = "ACTYLOGMSG1351";
                        }
                        PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                      MessageType.Text, rmgResponseMessage);
                        PublishDataMessage(cbkImportdata, rmgResponseMessage, "CreateReplayPlusDevices");
                    }
                }
            }
            catch (Exception expt)
            {
                MessageBusLogger.LogEvent("Import::CreateReplayPlusDevices: Error while processing imported"
                                          + "dat files result.", LogWriter.Level.ERROR);
                MessageBusLogger.LogError("Import::CreateReplayPlusDevices",
                                          LogWriter.Level.ERROR, expt.Message, expt.InnerException,
                                          "IMPMsg1002", ToString());
                PublishResponseMessage(cbkImportdata, "ACTYLOGMSG1462",
                                                ResponseMessageType.Error, true);
            }
            finally
            {
                if (null != dtDeviceTimezone)
                    dtDeviceTimezone.Dispose();
                dtDeviceTimezone = null;

                if (null != dtTimeZoneId)
                    dtTimeZoneId.Dispose();
                dtTimeZoneId = null;

                if (null != dsDevicetimezone)
                    dsDevicetimezone.Dispose();
                dsDevicetimezone = null;
            }
        }
        /// <summary>
        /// Method to create comtrade devices from comtrade file for scheduler
        /// </summary>
        /// <param name="cbkImportdata"></param>
        private void CreateComtradeDevicesFromFileForScheduler(CallBack cbkImportdata)
        {
            _boolIsLicenced = true;
            DataTable dtDeviceTimezone = new DataTable();
            DataTable dtTimeZoneId = null;

            try
            {
                bool blnIsDeviceExists = false;
                bool blnIsDeviceLimitExceed = false;

                dtDeviceTimezone.Columns.Add("DEVICEID", typeof(long));
                dtDeviceTimezone.Columns.Add("DEVICENAME", typeof(string));
                dtDeviceTimezone.Columns.Add("SUBSTATIONNAME", typeof(string));
                dtDeviceTimezone.Columns.Add("TIMEZONEID", typeof(string));
                dtDeviceTimezone.Columns.Add("DAYLIGHTSAVING", typeof(bool));
                Dictionary<object, object> dctComtradeImport = (Dictionary<object, object>)cbkImportdata.PayLoad;
                List<DeviceDTO> lstDeviceDTO = null;
                DataProcessingSchedulerDTO dtoDataProcessingSchedulerDTO = null;
                if (null != dctComtradeImport && dctComtradeImport.Count > 0)
                {
                    foreach (KeyValuePair<object, object> kvPair in dctComtradeImport)
                    {
                        dtoDataProcessingSchedulerDTO = (DataProcessingSchedulerDTO)kvPair.Key;
                        lstDeviceDTO = (List<DeviceDTO>)kvPair.Value;
                        break;
                    }
                }
                List<DeviceDTO> lstNewDeviceDTO = new List<DeviceDTO>();
                if (null == lstDeviceDTO || null == dtoDataProcessingSchedulerDTO)
                    return;
                for (int intDeviceCount = 0; intDeviceCount < lstDeviceDTO.Count; intDeviceCount++)
                {

                    ReplyData replyData = PublishRequestMessage(lstDeviceDTO[intDeviceCount],
                           Topics.DeviceManager.DeviceAdministration,
                           MessageActions.DeviceManager.CreateComtradeDevicesForScheduler,
                            ResponseTopics.DeviceManager.DeviceAdministration + "_"
                            + Thread.CurrentThread.ManagedThreadId.ToString()
                            + DateTime.Now.Ticks.ToString(),
                           ResponseMessageActions.DeviceManager.CreateComtradeDevicesForScheduler);
                    if (replyData.PayLoad.GetType() == typeof(ResponseMessage))
                    {
                        //Get the Response from call back object
                        ResponseMessage rsmsgResponseMessage = (ResponseMessage)replyData.PayLoad;
                        ResponseMessage rmgResponseMessagePub = new ResponseMessage();
                        //Is device created successfully
                        if (rsmsgResponseMessage.Result == "DMMsg1002"
                            || rsmsgResponseMessage.Result == "DMMsg1000")
                        {
                            if (rsmsgResponseMessage.Result == "DMMsg1002")
                            {
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1156";
                                //blnDeviceCreated = false;
                                blnIsDeviceExists = true;

                            }
                            else
                            {
                                long lngDeviceId = Convert.ToInt64(((ResponseMessage)replyData.PayLoad).Response);
                                GetDeviceDetails(lngDeviceId,
                                    ref dtDeviceTimezone, lstDeviceDTO[intDeviceCount]);
                                //DataBaseManagerDataObject.UpdateDPScheduledActionsHistoryOfComtradeImport(dtoDataProcessingSchedulerDTO.JobId,
                                //    lngDeviceId, dtoDataProcessingSchedulerDTO.DataTypes, DateTime.Now, DateTime.Now, (int)JobExecutionStatus.Success, 3);

                                //lstNewDeviceDTO.Add(lstDeviceDTO[intDeviceCount]);
                                rmgResponseMessagePub.Result = "ACTYLOGMSG1094";
                            }
                            if (dctComtradeImport.Count > 0)
                            {
                                long lngDeviceId = Convert.ToInt64(((ResponseMessage)replyData.PayLoad).Response);
                                DataBaseManagerDataObject.UpdateDPSchedulerStatus(dtoDataProcessingSchedulerDTO.JobId, lngDeviceId);
                            }
                            rmgResponseMessagePub.Response = lstDeviceDTO[intDeviceCount].DeviceName;
                            PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                          MessageType.Text, rmgResponseMessagePub);
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1036")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "UMMsg1036";
                            PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                          MessageType.Text, rmgResponseMessagePub);
                            blnIsDeviceLimitExceed = true;
                            break;
                        }
                        else if (rsmsgResponseMessage.Result == "UMMsg1037")
                        {
                            rmgResponseMessagePub = new ResponseMessage();
                            rmgResponseMessagePub.Result = "UMMsg1037";

                            string strDeviceType = string.Empty;
                            switch (lstDeviceDTO[intDeviceCount].DeviceType)
                            {
                                case DeviceType.DFR1200:
                                    strDeviceType = "DFR1200";
                                    break;
                                case DeviceType.DFRII:
                                    strDeviceType = "DFRII";
                                    break;
                                case DeviceType.IDM:
                                    strDeviceType = "IDM";
                                    break;
                                case DeviceType.IMS:
                                    strDeviceType = "IMS";
                                    break;
                                case DeviceType.DSFLMK1_2:
                                    strDeviceType = "DSFLMK1_2";
                                    break;
                                case DeviceType.DSFLMK3:
                                    strDeviceType = "DSFLMK3";
                                    break;
                                case DeviceType.TWSMK3_4:
                                    strDeviceType = "TWSMK3_4";
                                    break;
                                case DeviceType.TWSMK5_6:
                                    strDeviceType = "TWSMK5_6";
                                    break;
                            }
                            rmgResponseMessagePub.Response = strDeviceType;
                            PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                          MessageType.Text, rmgResponseMessagePub);
                        }
                    }
                    Thread.Sleep(20);
                    //UpdateStatistics(ImportConstants.IMPORTDEVICES, lstDeviceDTO[intDeviceCount].DeviceName, intDeviceCount + 1,
                    //    lstDeviceDTO.Count, cbkImportdata);
                    //Thread.Sleep(20);
                }
                if (dtDeviceTimezone.Rows.Count > 0)
                {
                    DataSet dsDevicetimezone = new DataSet();
                    dtDeviceTimezone.TableName = "DeviceTimeZone";
                    dsDevicetimezone.Tables.Add(dtDeviceTimezone.Copy());
                    dtTimeZoneId = new DataTable();
                    string strquery = "select ZoneID from TimeZone";
                    Thread.Sleep(1000);
                    ReplyData replyData = PublishRequestMessage(strquery, Topics.DeviceManager.DeviceAdministration,
                                    MessageActions.DeviceManager.GetDataForImport,
                                    ResponseTopics.DeviceManager.DeviceAdministration
                                    + DateTime.Now.Ticks.ToString(),
                                    ResponseMessageActions.DeviceManager.CreateComtradeDevicesForScheduler);
                    dtTimeZoneId = (DataTable)replyData.PayLoad;
                    dtTimeZoneId.TableName = "TimeZoneID";
                    //dtTimeZoneId = objQis.GetTimeZoneIDs();
                    dsDevicetimezone.Tables.Add(dtTimeZoneId.Copy());

                    PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.GetTimeZoneInfo,
                                                     MessageType.Text, dsDevicetimezone);
                }
                else
                {
                    if (blnIsDeviceExists)
                    {
                        ResponseMessage rmgResponseMessage = new ResponseMessage();
                        rmgResponseMessage.Result = "ACTYLOGMSG1349";
                        PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                      MessageType.Text, rmgResponseMessage);
                        PublishDataMessage(cbkImportdata, dtDeviceTimezone.Copy(), "CreateReplayPlusDevices");
                    }
                    else
                    {
                        ResponseMessage rmgResponseMessage = new ResponseMessage();
                        if (blnIsDeviceLimitExceed)
                        {
                            rmgResponseMessage.Result = "ACTYLOGMSG1352";
                        }
                        else
                        {
                            rmgResponseMessage.Result = "ACTYLOGMSG1351";
                        }
                        PublishDataMessage(cbkImportdata.RequestorTopic, MessageActions.ServerActivity,
                                      MessageType.Text, rmgResponseMessage);
                        PublishDataMessage(cbkImportdata, rmgResponseMessage, "CreateReplayPlusDevices");
                    }
                }
            }
            catch (Exception expt)
            {
                MessageBusLogger.LogEvent("Import::CreateReplayPlusDevices: Error while processing imported"
                                          + "dat files result.", LogWriter.Level.ERROR);
                MessageBusLogger.LogError("Import::CreateReplayPlusDevices",
                                          LogWriter.Level.ERROR, expt.Message, expt.InnerException,
                                          "IMPMsg1002", ToString());
                PublishResponseMessage(cbkImportdata, "ACTYLOGMSG1462",
                                                ResponseMessageType.Error, true);
            }
            finally
            {
                if (null != dtDeviceTimezone)
                    dtDeviceTimezone.Dispose();
                dtDeviceTimezone = null;

                if (null != dtTimeZoneId)
                    dtTimeZoneId.Dispose();
                dtTimeZoneId = null;
            }
        }

        /// <summary>
        /// Method to update comtrade statistics.
        /// </summary>
        /// <param name="strDeviceName"></param>
        /// <param name="strDeviceConst"></param>
        /// <param name="strEventName"></param>
        /// <param name="strEventConst"></param>
        /// <param name="intTotalNo"></param>
        /// <param name="intcount"></param>
        /// <param name="cbkdata"></param>
        private void UpdateComtradeImportStatistics(string strDeviceName, string strDeviceConst,
            string strEventName, string strEventConst, int intTotalNo, int intcount, CallBack cbkdata)
        {
            try
            {
                StatisticsDTO statDTO = new StatisticsDTO();
                statDTO.EventName = strEventName;
                statDTO.DeviceName = strDeviceName;
                statDTO.DeviceConstantMessages = strDeviceConst;
                statDTO.PresentCount = intcount;
                statDTO.EventConstantMessages = strEventConst;
                statDTO.TotalCount = intTotalNo;
                object objTemp = statDTO;
                intPercentageCount++;
                //publish the progress bar status. 
                PublishDataMessage(cbkdata.RequestorTopic, MessageActions.Import.ImportComtradeFiles,
                    MessageType.Binary, objTemp);
                intPercentageCount = 0;

                Thread.Sleep(20);
            }
            catch (MBException mbex)
            {
                //Log the error to the file
                MessageBusLogger.LogError("UpdateComtradeImportStatistics", LogWriter.Level.ERROR,
                                            mbex.InnerException.Message, mbex.InnerException, mbex.Message, ToString());
                PublishResponseMessage(cbkdata, "QWAEx1014", ResponseMessageType.Information, true);

            }
        }
        #endregion
    }
}
