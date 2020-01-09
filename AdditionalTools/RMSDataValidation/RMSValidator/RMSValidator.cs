using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace RMSValidator
{
    public class RMSValidator
    {
        #region Variables

        string _fileLocation;
        double _injectedNominalVoltage;
        double _injectedNominalCurrent;
        readonly double _voltageTolerance = 0.15;
        readonly double _currentTolerance = 0.01;
        readonly int _sampleDataForRMSCalc = 1024;
        DataTable _analogData = new DataTable();

        const string RMS = "RMS";
        const string AVERAGRE = "AVG";        
        const string NOT_A_NUMBER = "NAN";
        const string VOLTAGE = "VOLTAGE";
        const string CURRENT = "CURRENT";
        const string MILLIAMPERE = "MA";
        const string MILLIVOLT = "MV";
        const string NOT_A_NUMBER_ERROR_MESSAGE = "Failed because of NaN";
        const char DELIMITER = ',';

        #endregion

        #region Constructor

        public RMSValidator(string fileToValidate, double InjectedNominalVoltage, double InjectedNominalCurrent, double VoltageTolerance, double CurrentTolerance)
        {
            Initilaize(fileToValidate, InjectedNominalVoltage, InjectedNominalCurrent);
            this._voltageTolerance = VoltageTolerance;
            this._currentTolerance = CurrentTolerance;
        }

        public RMSValidator(string fileToValidate, double InjectedNominalVoltage, double InjectedNominalCurrent)
        {
            Initilaize(fileToValidate, InjectedNominalVoltage, InjectedNominalCurrent);
        }

        #endregion

        #region Private Methods

        private void Initilaize(string fileToValidate, double InjectedNominalVoltage, double InjectedNominalCurrent)
        {
            _fileLocation = fileToValidate;
            _injectedNominalVoltage = InjectedNominalVoltage;
            _injectedNominalCurrent = InjectedNominalCurrent;

        }

        private float CalculateRMS(int rowCounter, int columnsCounter)
        {
            var result = _analogData.AsEnumerable().Where((row, index) => index >= rowCounter - _sampleDataForRMSCalc && index < rowCounter);

            var temp = from row in result
                       select row.Field<float>("Calculated" + columnsCounter);

            float rms = (float)Math.Sqrt(temp.Cast<float>().Average());

            _analogData.Rows[rowCounter]["RMS" + columnsCounter] = rms;

            return rms;
        }

        #endregion

        #region Public Method

        public bool Validate()
        {
            bool isValidated = true;

            try
            {
                _analogData.Clear();

                List<string> listRows = new List<string>();

                using (var reader = new StreamReader(File.OpenRead(_fileLocation)))
                {
                    while (!reader.EndOfStream)
                    {
                        listRows.Add(reader.ReadLine());
                    }                    
                }

                //Create DataColumns
                string[] channelType = listRows[5].Split(',');
                string[] channelNames = listRows[6].Split(',');

                List<int> usedIndexs = new List<int>();
                Dictionary<string, string> ChannelTypes = new Dictionary<string, string>();

                int channelNumber = 1;

                for (int counter = 0; counter < channelType.Count(); counter++)
                {
                    if (channelNames[counter] != "Sample" && channelNames[counter] != "TimeStamps" && !string.IsNullOrEmpty(channelNames[counter]) && !string.IsNullOrEmpty(channelType[counter]))
                    {
                        usedIndexs.Add(counter);
                        _analogData.Columns.Add("Channel" + channelNumber, typeof(float));
                        _analogData.Columns.Add("Calculated" + channelNumber, typeof(float));
                        _analogData.Columns.Add("RMS" + channelNumber, typeof(float));

                        ChannelTypes.Add("Channel" + channelNumber, channelType[counter]);
                        channelNumber++;
                    }
                }

                for (int rowCounter = 7; rowCounter < listRows.Count; rowCounter++)
                {
                    string[] data = listRows[rowCounter].Split(',');
                    DataRow dr = _analogData.NewRow();
                    channelNumber = 1;
                    foreach (var index in usedIndexs)
                    {
                        float value = float.Parse(data[index]);
                        dr["Channel" + channelNumber] = value;
                        dr["Calculated" + channelNumber] = value * value;
                        channelNumber++;
                    }
                    _analogData.Rows.Add(dr);
                }

                for (int columnsCounter = 1; columnsCounter <= usedIndexs.Count; columnsCounter++)
                {
                    string value = ChannelTypes["Channel" + columnsCounter];

                    if (value == "V")
                    {
                        for (int rowCounter = _sampleDataForRMSCalc; rowCounter < _analogData.Rows.Count; rowCounter++)
                        {
                            float rmsValue = CalculateRMS(rowCounter, columnsCounter);

                            //if (rmsValue >= (_injectedNominalVoltage + ((_injectedNominalVoltage * _voltageTolerance) / 100)) || rmsValue <= (_injectedNominalVoltage - ((_injectedNominalVoltage * _voltageTolerance) / 100)))

                            if (rmsValue >= (_injectedNominalVoltage + _voltageTolerance) || rmsValue <= (_injectedNominalVoltage - _voltageTolerance))
                            {
                                throw new ValidationFailedException();
                            }
                        }
                    }
                    if (value == "A")
                    {
                        for (int rowCounter = _sampleDataForRMSCalc; rowCounter < _analogData.Rows.Count; rowCounter++)
                        {
                            float rmsValue = CalculateRMS(rowCounter, columnsCounter);

                            //if (rmsValue >= (_injectedNominalCurrent + ((_injectedNominalCurrent * _currentTolerance))) || rmsValue <= (_injectedNominalCurrent - (_injectedNominalCurrent * _currentTolerance)))
                            if (rmsValue >= (_injectedNominalCurrent + _currentTolerance) || rmsValue <= (_injectedNominalCurrent - _currentTolerance))
                            {
                                throw new ValidationFailedException();
                            }
                        }
                    }
                }
            }
            catch (ValidationFailedException)
            {
                isValidated = false;
            }

            return isValidated;
        }

        public bool PQValidate(out string errorMessage)
        {
            bool isValidated = true;
            errorMessage = string.Empty;

            double voltageHighRange = _injectedNominalVoltage + _voltageTolerance;
            double voltageLowRange = _injectedNominalVoltage - _voltageTolerance;
            double currentHighRange = _injectedNominalCurrent + _currentTolerance;
            double currentLowRange = _injectedNominalCurrent - _currentTolerance;

            try
            {
                List<string> listRows = new List<string>();

                using (var reader = new StreamReader(File.OpenRead(_fileLocation)))
                {
                    while (!reader.EndOfStream)
                    {
                        listRows.Add(reader.ReadLine());
                    }
                    reader.Close();
                }

                int totalRecordCount = listRows.Count;

                //Create DataColumns
                string[] channelType = listRows[5].Split(DELIMITER);
                string[] channelNames = listRows[6].Split(DELIMITER);

                int multiplier = 1;

                for (int rowcounter = 7; rowcounter < totalRecordCount; rowcounter++)
                {
                    string[] rowValues = listRows[rowcounter].Split(DELIMITER);
                    double rowValue;

                    for (short counter = 1; counter < channelType.Length; counter++)
                    {
                        multiplier = (channelType[counter].ToUpper().Contains(MILLIAMPERE) || channelType[counter].ToUpper().Contains(MILLIVOLT)) ? 1000 : 1;

                        //Check if channel name contains the RMS and AVG
                        if (channelNames[counter].ToUpper().Contains(RMS) && channelNames[counter].ToUpper().Contains(AVERAGRE))
                        {
                            if (rowValues[counter].ToUpper() != NOT_A_NUMBER)
                            {
                                rowValue = double.Parse(rowValues[counter]);

                                if (channelNames[counter].ToUpper().Contains(VOLTAGE))
                                {
                                    //put logic here for voltage validation
                                    if (rowValue >= voltageHighRange * multiplier || rowValue <= voltageLowRange * multiplier)
                                    {
                                        throw new ValidationFailedException(FAIL_MESSAGE);
                                    }

                                }
                                else if (channelNames[counter].ToUpper().Contains(CURRENT))
                                {
                                    //put logic here for current validation
                                    if (rowValue >= currentHighRange * multiplier || rowValue <= currentLowRange * multiplier)
                                    {
                                        throw new ValidationFailedException(FAIL_MESSAGE);
                                    }

                                }
                                else //For Standalone channels
                                {
                                    if ((rowValue >= voltageHighRange * multiplier || rowValue <= voltageLowRange * multiplier) && (rowValue >= currentHighRange * multiplier || rowValue <= currentLowRange * multiplier))
                                    {
                                        throw new ValidationFailedException(FAIL_MESSAGE);
                                    }
                                }
                            }
                            else
                            {
                                throw new ValidationFailedException(NOT_A_NUMBER_ERROR_MESSAGE);
                            }                            
                        }
                    }
                }
            }
            catch (ValidationFailedException ex)
            {
                isValidated = false;
                errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                isValidated = false;
                errorMessage = ex.Message;
            }

            return isValidated;
        }
        #endregion
    }
}
