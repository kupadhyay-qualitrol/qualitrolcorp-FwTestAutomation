/*This file contains method for screenshot but right now not in use in code
 */
using System;
using OpenQA.Selenium;
using System.Drawing.Imaging;

namespace CashelFirmware.Utility
{
    class ScreenShotCapture
    {
        
        public static string Capture(IWebDriver webdriver, string screenShotName)
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)webdriver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "ErrorScreenshots\\" + screenShotName + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ImageFormat.Png);
            return localpath;
        }
    }
}
