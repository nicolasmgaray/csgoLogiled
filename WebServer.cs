using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSGO_Logitech_Integration
{
    // All the web server logic is inside here 

    public delegate byte[] ProcessDataDelegate(string data);

    public class SimpleServer
    {
        private const int HandlerThread = 2;
        private readonly ProcessDataDelegate handler;
        private readonly System.Net.HttpListener listener;

        public SimpleServer(System.Net.HttpListener listener, string url, ProcessDataDelegate handler)
        {
            this.listener = listener;
            this.handler = handler;
            listener.Prefixes.Add(url);
        }

        public void Start()
        {
            if (listener.IsListening)
                return;

            listener.Start();

            for (int i = 0; i < HandlerThread; i++)
            {
                listener.GetContextAsync().ContinueWith(ProcessRequestHandler);
            }
        }

        public void Stop()
        {
            if (listener.IsListening)
                listener.Stop();
        }

        private void ProcessRequestHandler(Task<System.Net.HttpListenerContext> result)
        {
            var context = result.Result;

            if (!listener.IsListening)
                return;

            // Start new listener which replace this
            listener.GetContextAsync().ContinueWith(ProcessRequestHandler);

            // Read request
            string request = new StreamReader(context.Request.InputStream).ReadToEnd();

            // Prepare response
            var responseBytes = handler.Invoke(request);
            context.Response.ContentLength64 = responseBytes.Length;

            var output = context.Response.OutputStream;
            output.WriteAsync(responseBytes, 0, responseBytes.Length);
            output.Close();
        }
    }
}
