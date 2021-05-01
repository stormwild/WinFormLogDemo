using Log4NetDemo.BusinessLayer;
using Log4NetDemo.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log4NetDemo
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new HostBuilder()
                .ConfigureServices((ctx, services) => 
                {
                    services.AddLogging(configure =>
                    {
                        configure.AddDebug();
                        configure.AddConsole();
                    })
                    .AddScoped<IMainService, MainService>()
                    .AddSingleton<IDbContext, DbContext>()
                    .AddScoped<Form>();

                })
                .ConfigureLogging(logBuilder =>
                {
                    logBuilder.AddLog4Net("log4net.config");
                });

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                try
                {
                    var services = serviceScope.ServiceProvider;

                    var form = services.GetRequiredService<Form>();
                    Application.Run(form);
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
    }
}
