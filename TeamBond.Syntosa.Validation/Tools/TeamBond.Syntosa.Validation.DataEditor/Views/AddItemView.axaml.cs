﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeamBond.Syntosa.Validation.DataEditor.Views
{
    public class AddItemView : UserControl
    {
        public AddItemView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
