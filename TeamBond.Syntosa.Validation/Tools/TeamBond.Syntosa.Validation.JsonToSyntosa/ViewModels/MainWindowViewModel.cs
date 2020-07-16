using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels
{
    using System.Reactive.Linq;

    using ReactiveUI;

    using TeamBond.Syntosa.Validation.JsonToSyntosa.Models;
    using TeamBond.Syntosa.Validation.JsonToSyntosa.Services;

    /// <summary>
    /// The main window view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// The content.
        /// </summary>
        private ViewModelBase content;

        public MainWindowViewModel()
        {
            this.Content = this.JsonToConvert = new JsonConverterViewModel();
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public ViewModelBase Content
        {
            get => this.content;
            private set => this.RaiseAndSetIfChanged(ref this.content, value);
        }

        public JsonConverterViewModel JsonToConvert { get; }

        public void UpdateContent()
        {
            this.Content = new JsonConverterViewModel();
        }
    }
}
