namespace TeamBond.Syntosa.Validation.JsonToSyntosa
{
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Markup.Xaml;

    using TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels;
    using TeamBond.Syntosa.Validation.JsonToSyntosa.Views;

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

                desktop.MainWindow = new MainWindow
                                         {
                                             DataContext = new MainWindowViewModel(),
                                         };
            }
        }
    }
}
