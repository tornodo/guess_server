using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using snake_server.websocket;

namespace snake_server.http
{
    public static class HttpServer
    {
        private static readonly int ErrorRetryCounts = 5;
        private static readonly ConcurrentBag<WebSocket> Sockets = new ConcurrentBag<WebSocket>();
        private static readonly CancellationToken token = new CancellationTokenSource().Token;
        
        public static void MapWebsocket(IApplicationBuilder app)
        {
            app.UseWebSockets(new WebSocketOptions {KeepAliveInterval = TimeSpan.FromSeconds(5)});
            app.Use(Acceptor);
        }
        
        private static async Task Acceptor(HttpContext http, Func<Task> n)
        {
            if (!http.WebSockets.IsWebSocketRequest)
            {
                return;
            }

            var socket = await http.WebSockets.AcceptWebSocketAsync();
            var buffer = new ArraySegment<byte>(new byte[4096]);
            using (var ms = new MemoryStream())
            {
                try
                {
                    WebSocketReceiveResult result;
                    do
                    {
                        result = await socket.ReceiveAsync(buffer, token);
                        if (result.MessageType != WebSocketMessageType.Binary)
                        {
                            return;
                        }

                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage);

                    ms.Seek(0, SeekOrigin.Begin);
                    var p = new Protocol();
                    p.MergeFrom(ms);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public static void Start() {
            new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
