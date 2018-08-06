
using Microsoft.AspNetCore.Builder;
using snake_server.http;

namespace snake_server
{
    public class Startup
    {
        public Startup()
        {
        }

        public static void Configure(IApplicationBuilder app) 
        {
            app.UseWebSockets();
            app.Map("/room", HttpServer.MapWebsocket);
        }
    }
}
