/*This file contains method for screenshot but right now not in use in code
 */
using System;
using OpenQA.Selenium;
using System.Drawing.Imaging;

namespace CashelFirmware.Utility
{
    public static class ScreenShotCapture
    {        
        public static void Capture(IWebDriver webdriver, string screenShotName)
        {
            webdriver.Manage().Window.Size = new System.Drawing.Size(1024, 768);
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)webdriver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "ScreenShot\\" + screenShotName + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ImageFormat.Png);
        }
    }
}
