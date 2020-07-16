using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels;
using TeamBond.Syntosa.Validation.JsonToSyntosa.Views;

namespace TeamBond.Syntosa.Validation.JsonToSyntosa
{
    using TeamBond.Syntosa.Validation.JsonToSyntosa.Services;

    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            base.OnFrameworkInitializationCompleted();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var db = new Database();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }
        }
    }
}
