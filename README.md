# Windows Forms Log4Net Demo

Sample .NET 5 (Core) Windows Forms application with GenericHostBuilder and Log4Net.

## Log4Net

Configuration

```xml
<log4net>
  <root>
    <level value="ALL" />
    <appender-ref ref="Console" />
    <appender-ref ref="RollingFile" />
  </root>
  <appender name="Console" type="log4net.Appender.ConsoleAppender">
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date %level %logger - %message%newline" />
    </layout>
  </appender>
  <appender name="RollingFile" type="log4net.Appender.RollingFileAppender">
    <file value="application.log" />
    <appendToFile value="true" />
    <rollingStyle value="Size" />
    <maxSizeRollBackups value="2" />
    <maximumFileSize value="2MB" />
    <staticLogFileName value="true" />
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date [%thread] %5level %logger.%method [%line] - MSG: %message %exception %newline" />
    </layout>
  </appender>
</log4net>
```

Sample Output of `application.log`

```
2021-05-01 16:37:16,767 [1]  INFO Log4NetDemo.Form.ShowMessageButton_Click [32] - MSG: Button clicked  
2021-05-01 16:37:16,817 [1]  INFO Log4NetDemo.BusinessLayer.MainService.DoTask [20] - MSG: DoTask Started  
2021-05-01 16:37:16,821 [1]  INFO Log4NetDemo.Persistence.DbContext.Add [17] - MSG: Adding Entity  
2021-05-01 16:37:18,510 [1]  INFO Log4NetDemo.Form.ShowMessageButton_Click [38] - MSG: DoTask done  
```

## References

- [Using Log4Net for Logging in Windows Form Application](https://www.thecodebuzz.com/log4net-logging-in-windows-form-desktop-application/)
- [Using HostBuilder, ServiceProvider and Dependency Injection with Windows Forms on .NET Core 3](https://marcominerva.wordpress.com/2020/03/09/using-hostbuilder-serviceprovider-and-dependency-injection-with-windows-forms-on-net-core-3/)

    ```csharp
    namespace WindowsFormsNetCore
    {
        internal static class Program
        {
            /// <summary>
            ///  The main entry point for the application.
            /// </summary>
            [STAThread]
            private static void Main()
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var host = Host.CreateDefaultBuilder()
                                .ConfigureAppConfiguration((context, builder) =>
                                {
                                    // Add other configuration files...
                                    builder.AddJsonFile("appsettings.local.json", optional: true);
                                })
                                .ConfigureServices((context, services) =>
                                {
                                    ConfigureServices(context.Configuration, services);
                                })
                                .ConfigureLogging(logging =>
                                {
                                    // Add other loggers...
                                })
                                .Build();

                var services = host.Services;
                var mainForm = services.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }

            private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
            {
                services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
                services.AddScoped<ISampleService, SampleService>();

                services.AddSingleton<MainForm>();
                services.AddTransient<SecondForm>();
            }
        }
    }
    ```

Background Tasks in .NET (Core)

- [Background tasks with hosted services in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio)

Generic Host Builder

- [Generic Host Builder in ASP .NET Core 3.1](https://wakeupandcode.com/generic-host-builder-in-asp-net-core-3-1/) 
- [.NET Generic Host in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-5.0)
- [.NET Generic Host](https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host)

Others

- [Implementing Simple Log4Net Logger in ASP.NET Core](https://referbruv.com/blog/posts/implemenenting-simple-log4net-logger-in-aspnet-core)
- [How To Use Log4Net In ASP.NET Core Application](https://www.c-sharpcorner.com/blogs/how-to-use-log4net-in-asp-net-core-application)
- [Logging in .NET Core](https://visualstudiomagazine.com/articles/2019/03/22/logging-in-net-core.aspx)

    ```csharp
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(log =>
            {
               if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") 
                                   == Microsoft.Extensions.Hosting.Environments.Development)
               {
                  log.ClearProviders();
                  log.AddDebug();
               }
            });
    ```

- [Fundamentals of Logging in .NET Core](https://www.tutorialsteacher.com/core/fundamentals-of-logging-in-dotnet-core)

    ```csharp
    namespace DotnetCoreConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                ILoggerFactory loggerFactory = new LoggerFactory(
                                new[] { new ConsoleLoggerProvider((_, __) => true, true) }
                            );
                //or
                //ILoggerFactory loggerFactory = new LoggerFactory().AddConsole();
            
                ILogger logger = loggerFactory.CreateLogger<Program>();
                logger.LogInformation("This is log message.");
            }
        }
    }
    ```



