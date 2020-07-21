using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeamBond.Syntosa.Validation.JsonToSyntosa.Views
{
    public class ElementPrototypeBuilderView : UserControl
    {
        public ElementPrototypeBuilderView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
