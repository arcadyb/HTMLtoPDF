using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Threading;


namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            RunServer();
        }

        private static void RunServer()
        {
            var baseAddress = "http://localhost:5002/";
            var config = new HttpSelfHostConfiguration(baseAddress);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "post" }
            );


            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.CookieContainer = new CookieContainer();
                httpClientHandler.CookieContainer.Add(new Cookie("name", "value", "/", "google.de"));

                var client = new HttpClient(httpClientHandler) { BaseAddress = new Uri(/*"http://www.google.de/") }; */ baseAddress) };
                var request = new HttpRequestMessage(HttpMethod.Get, "api/hello");
                string receivedhtml = client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Client received: {0}",
                    client.SendAsync(request).Result.Content.ReadAsStringAsync().Result); // client.GetStringAsync("api/hello").Result);
            }
        }
    }

    public class HelloController : ApiController
    {
        public string Get()
        {
            return "Hello, world!";
        }
    }
}