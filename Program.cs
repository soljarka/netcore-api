using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace netcore_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var portString = System.Environment.GetEnvironmentVariable("API_PORT");
            int port;
            if (!int.TryParse(portString, out port))
            {
                port = 5000;
            }

            var host = WebHost.CreateDefaultBuilder()
                .UseKestrel(options =>
                    options.Listen(IPAddress.Any, port, listenOptions =>
                    {
                        listenOptions.UseHttps("certificate.pfx", "mrgapass");
                    }
                    )
                )
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
