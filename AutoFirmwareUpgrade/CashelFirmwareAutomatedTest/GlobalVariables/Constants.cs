using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CashelFirmware.GlobalVariables
{
    public static class Constants
    {
        public static string glb_DeviceIP_18Channel = ConfigurationSettings.AppSettings.Get("DeviceIPAddress_18Channel");
        public static string glb_DeviceIP_9Channel = ConfigurationSettings.AppSettings.Get("DeviceIPAddress_9Channel");
        public static int glb_deviceType;
    }
}
