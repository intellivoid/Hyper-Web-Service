using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HyperWS_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var Server = new Intellivoid.HyperWS.HttpServer
            {
                // Start a a random port on localhost only
                EndPoint = new IPEndPoint(IPAddress.Loopback, 0)
            };

            //Intellivoid.HyperWS.Logging.Enabled = true;
            //Intellivoid.HyperWS.Logging.VerboseLogging = true;

            // Run at a different port on localhost only
            //Server.EndPoint = new IPEndPoint(IPAddress.Loopback, 8080);

            // Run a different/random port listening for any request instead of just localhost
            //Server.EndPoint = new IPEndPoint(IPAddress.Any, 8080); // Port 8080
            //Server.EndPoint = new IPEndPoint(IPAddress.Any, 0); // Random Port

            // Set the Event Listener
            Server.RequestReceived += (s, e) => { RequestReceived(s, e); };

            // Start the server
            Server.Start();

            Process.Start($"http://{Server.EndPoint}");

            Console.WriteLine("Press Return to stop the server ...");

            Console.ReadLine();
            Server.Stop();
            Environment.Exit(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="content"></param>
        /// <param name="status_code"></param>
        public static void SendResponse(Intellivoid.HyperWS.HttpRequestEventArgs httpRequest, string content, int status_code)
        {
            httpRequest.Response.Headers.Add("X-Powered-By", "HyperWS");
            httpRequest.Response.StatusCode = status_code;
            using (var writer = new StreamWriter(httpRequest.Response.OutputStream))
            {
                writer.Write(content);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="filepath"></param>
        public static void SendFile(Intellivoid.HyperWS.HttpRequestEventArgs httpRequest, string filepath)
        {
            httpRequest.Response.Headers.Add("X-Powered-By", "HyperWS");
            httpRequest.Response.StatusCode = 200;
            using (var Stream = File.OpenRead(filepath))
            {
                Stream.CopyTo(httpRequest.Response.OutputStream);
            }
        }

        /// <summary>
        /// Raises when a request is received on the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="httpRequest"></param>
        public static void RequestReceived(object sender, Intellivoid.HyperWS.HttpRequestEventArgs httpRequest)
        {
            var RequestPath = httpRequest.Request.Path.Split('/');
            
            switch(RequestPath[1])
            {
                case "":
                    SendResponse(httpRequest, "<h1>HyperWS</h1> <p>Welcome to HyperWS Server</p>", 200);
                    break;

                case "favicon.ico":
                    SendFile(httpRequest, "favicon.ico");
                    break;

                default:
                    SendResponse(httpRequest, "<h1>404</h1> <p>Resource not found</p>", 404);
                    break;

            }
        }

    }
}
