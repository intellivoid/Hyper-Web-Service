using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace HyperWS_Example
{
    internal static class Program
    {
        private static void Main()
        {
            var server = new Intellivoid.HyperWS.HttpServer
            {
                // Start a a random port on localhost only
                EndPoint = new IPEndPoint(IPAddress.Loopback, 0)
            };

            //Intellivoid.HyperWS.Logging.Enabled = true;
            //Intellivoid.HyperWS.Logging.VerboseLogging = true;

            // Run at a different port on localhost only
            //server.EndPoint = new IPEndPoint(IPAddress.Loopback, 8080);

            // Run a different/random port listening for any request instead of just localhost
            //server.EndPoint = new IPEndPoint(IPAddress.Any, 8080); // Port 8080
            //server.EndPoint = new IPEndPoint(IPAddress.Any, 0); // Random Port

            // Set the Event Listener
            server.RequestReceived += RequestReceived;

            // Start the server
            server.Start();

            Process.Start($"http://{server.EndPoint}");

            Console.WriteLine(ProgramResources.StopServerMessage);

            Console.ReadLine();
            server.Stop();
            Environment.Exit(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="content"></param>
        /// <param name="statusCode"></param>
        private static void SendResponse(Intellivoid.HyperWS.HttpRequestEventArgs httpRequest, string content, int statusCode)
        {
            httpRequest.Response.Headers.Add("X-Powered-By", "HyperWS");
            httpRequest.Response.StatusCode = statusCode;
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
        private static void SendFile(Intellivoid.HyperWS.HttpRequestEventArgs httpRequest, string filepath)
        {
            httpRequest.Response.Headers.Add("X-Powered-By", "HyperWS");
            httpRequest.Response.StatusCode = 200;
            using (var stream = File.OpenRead(filepath))
            {
                stream.CopyTo(httpRequest.Response.OutputStream);
            }
        }

        /// <summary>
        /// Raises when a request is received on the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="httpRequest"></param>
        private static void RequestReceived(object sender, Intellivoid.HyperWS.HttpRequestEventArgs httpRequest)
        {
            var requestPath = httpRequest.Request.Path.Split('/');
            
            switch(requestPath[1])
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
