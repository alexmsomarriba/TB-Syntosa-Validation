namespace TeamBond.Syntosa.Validation.JsonToSyntosa
{
    using System;

    using Avalonia;
    using Avalonia.Logging.Serilog;
    using Avalonia.ReactiveUI;

    using Microsoft.Extensions.Configuration;

    using TeamBond.Application.Framework;
    using TeamBond.Core;

    public class DataEditor : TeamBondEngineApplicationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditor"/> class.
        /// </summary>
        protected DataEditor()
        {
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>().UsePlatformDetect().LogToDebug().UseReactiveUI();
        }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        /// <inheritdoc />
        protected override void Configure(IConfigurationBuilder configBuilder)
        {
            string applicationName = Environment.GetEnvironmentVariable("APP_NAME");

            if (string.IsNullOrWhiteSpace(applicationName))
            {
                var exception = new TeamBondException("Could not retrieve APP_NAME from environment variables");
            }
        }
    }
}