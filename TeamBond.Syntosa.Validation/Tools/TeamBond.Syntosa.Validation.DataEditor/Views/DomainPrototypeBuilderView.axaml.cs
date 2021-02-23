using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeamBond.Syntosa.Validation.DataEditor.Views
{
    public class DomainPrototypeBuilderView : UserControl
    {
        public DomainPrototypeBuilderView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
