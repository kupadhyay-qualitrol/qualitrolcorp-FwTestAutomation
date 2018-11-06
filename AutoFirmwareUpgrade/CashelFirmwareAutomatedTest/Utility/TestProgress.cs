using System;
using System.IO;

namespace CashelFirmware.Utility
{
    public class TestProgressStatus
    {
        /// <summary>
        /// This is constructor to create a text file in the project directory.
        /// </summary>
        public TestProgressStatus()
        {
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestProgress.txt")))
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestProgress.txt"), string.Empty);
            }
            else
            {
                File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestProgress.txt"));
            }
        }

        public bool TestCase_Status(string TestCaseName, string TestCaseStatus)
        {
            try
            {
                File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestProgress.txt"), TestCaseName + "-----" + TestCaseStatus + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestProgress.txt"), ex.Message.ToString() + "----" + ex.StackTrace.ToString() + Environment.NewLine);
                return false;
            }
        }
    }
}