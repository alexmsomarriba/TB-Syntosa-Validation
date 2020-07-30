namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System.Reactive.Linq;
    using System.Runtime.InteropServices.ComTypes;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.Content = this.TypeBuilder = new TypePrototypeBuilderViewModel();
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

        /// <summary>
        /// Gets the type builder.
        /// </summary>
        public TypePrototypeBuilderViewModel TypeBuilder { get; }
    }
}
