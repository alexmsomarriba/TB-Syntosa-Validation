namespace TeamBond.Syntosa.Validation.DataEditor
{
    using System.IO;

    using Avalonia;
    using Avalonia.Logging.Serilog;
    using Avalonia.ReactiveUI;

    using Microsoft.Extensions.Configuration;

    using TeamBond.Application.Framework;

    public class DataEditor : TeamBondEngineApplicationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditor" /> class.
        /// </summary>
        protected DataEditor()
        {
        }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            new DataEditor().BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
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