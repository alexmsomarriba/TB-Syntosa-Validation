namespace TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels
{
    using System;
    using System.Reactive;
    using System.Reactive.Linq;

    using ReactiveUI;

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

        /// <summary>
        /// Gets the json to convert.
        /// </summary>
        public JsonConverterViewModel JsonToConvert { get; }
    }
}
