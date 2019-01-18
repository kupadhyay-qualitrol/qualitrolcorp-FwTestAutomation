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

                            if (rmsValue >= (_injectedNominalVoltage + ((_injectedNominalVoltage * _voltageTolerance) / 100)) || rmsValue <= (_injectedNominalVoltage - ((_injectedNominalVoltage * _voltageTolerance) / 100)))
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

                            if (rmsValue >= (_injectedNominalCurrent + ((_injectedNominalCurrent * _currentTolerance))) || rmsValue <= (_injectedNominalCurrent - (_injectedNominalCurrent * _currentTolerance)))
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

        #endregion
    }
}
