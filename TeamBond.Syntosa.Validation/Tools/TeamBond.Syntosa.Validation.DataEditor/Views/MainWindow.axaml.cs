using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeamBond.Syntosa.Validation.DataEditor.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
