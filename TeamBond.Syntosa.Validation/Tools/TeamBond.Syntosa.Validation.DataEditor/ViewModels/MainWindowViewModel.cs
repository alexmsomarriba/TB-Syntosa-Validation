namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using ReactiveUI;

    /// <summary>
    /// The main window view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// The content.
        /// </summary>
        private ViewModelBase typeCreatorContent;

        /// <summary>
        /// The type function creator content.
        /// </summary>
        private ViewModelBase typeFunctionCreatorContent;

        /// <summary>
        /// The prototype editor content.
        /// </summary>
        private ViewModelBase prototypeEditorContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.TypeCreatorContent = this.TypeBuilder = new TypePrototypeBuilderViewModel();
            this.TypeFunctionCreatorContent = this.TypeFunctionBuilder = new TypeFunctionPrototypeBuilderViewModel();
            this.PrototypeEditorContent = this.PrototypeEditor = new PrototypeEditorViewModel();
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public ViewModelBase TypeCreatorContent
        {
            get => this.typeCreatorContent;
            private set => this.RaiseAndSetIfChanged(ref this.typeCreatorContent, value);
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public ViewModelBase TypeFunctionCreatorContent
        {
            get => this.typeFunctionCreatorContent;
            private set => this.RaiseAndSetIfChanged(ref this.typeFunctionCreatorContent, value);
        }

        /// <summary>
        /// Gets the prototype editor content.
        /// </summary>
        public ViewModelBase PrototypeEditorContent
        {
            get => this.prototypeEditorContent;
            private set => this.RaiseAndSetIfChanged(ref this.prototypeEditorContent, value);
        }

        /// <summary>
        /// Gets the type function builder.
        /// </summary>
        public TypeFunctionPrototypeBuilderViewModel TypeFunctionBuilder { get; }

        /// <summary>
        /// Gets the type builder.
        /// </summary>
        public TypePrototypeBuilderViewModel TypeBuilder { get; }

        public PrototypeEditorViewModel PrototypeEditor { get; }
    }
}
