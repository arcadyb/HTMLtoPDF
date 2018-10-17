using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;
using System.Configuration;

namespace STRenderWebService
{
    static class Program
    {
  
        public static string GetIPAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading app settings");
            }
            return "";
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool isLocalHost = bool.Parse(ReadSetting("IsLocalHost"));
                int port = int.Parse(ReadSetting("port"));

                string baseAddress = string.Format("http://{0}:{1}/", GetIPAddress(), port);
                if (isLocalHost)
                {
                    baseAddress = string.Format("http://{0}:{1}/", "localhost", port);
                }
                var config = new HttpSelfHostConfiguration(baseAddress);
                config.MaxReceivedMessageSize = 2147483647;
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                HttpSelfHostServer server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();
                //RunServer();
                Application.Run(new Form1());
                server.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     }
}

