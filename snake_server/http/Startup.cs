using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace snake_server_http
{
    public class Startup
    {
        public Startup()
        {
        }

        public void Configure(IApplicationBuilder app) 
        {
            app.UseWebSockets();
            app.Run((context) => context.Response.WriteAsync("Started success"));
        }
    }
}
