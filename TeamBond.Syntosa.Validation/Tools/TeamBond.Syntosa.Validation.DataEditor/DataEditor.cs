namespace TeamBond.Syntosa.Validation.DataEditor
{
    using System.IO;

    using Avalonia;
    using Avalonia.Logging.Serilog;
    using Avalonia.ReactiveUI;

    using Microsoft.Extensions.Configuration;

    using TeamBond.Application.Framework;

    /// <summary>
    /// Kick starts the Data Editor application.
    /// </summary>
    public class DataEditor : TeamBondEngineApplicationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditor" /> class.
        /// </summary>
        protected DataEditor()
        {
        }

        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            new DataEditor().BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        /// <summary>
        /// The Avalonia app builder.
        /// </summary>
        /// <returns>
        /// The <see cref="AppBuilder"/>.
        /// </returns>
        public AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>().UsePlatformDetect().LogToDebug().UseReactiveUI();
        }

        /// <inheritdoc />
        protected override void Configure(IConfigurationBuilder configBuilder)
        {
            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
        }
    }
}