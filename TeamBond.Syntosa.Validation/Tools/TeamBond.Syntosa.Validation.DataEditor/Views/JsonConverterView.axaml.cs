using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeamBond.Syntosa.Validation.DataEditor.Views
{
    public class JsonConverterView : UserControl
    {
        public JsonConverterView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
