namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using ReactiveUI;

    /// <summary>
    /// The prototype editor view model.
    /// </summary>
    public class PrototypeEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// The module editor view model.
        /// </summary>
        private ViewModelBase moduleEditorContent;

        /// <summary>
        /// The type prototype editor view model.
        /// </summary>
        private ViewModelBase typeEditorContent;

        /// <summary>
        /// The type function editor view model.
        /// </summary>
        private ViewModelBase typeFunctionEditorContent;

        /// <summary>
        /// The type unit editor content.
        /// </summary>
        private ViewModelBase typeUnitEditorContent;

        /// <summary>
        /// The element editor content.
        /// </summary>
        private ViewModelBase elementEditorContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeEditorViewModel"/> class.
        /// </summary>
        public PrototypeEditorViewModel()
        {
            this.ModuleEditorContent = this.ModuleEditor = new ModulePrototypeEditorViewModel();
            this.TypeUnitEditorContent = this.TypeUnitEditor = new TypeUnitPrototypeEditorViewModel();
            this.TypeFunctionEditorContent = this.TypeFunctionEditor = new TypeFunctionPrototypeEditorViewModel();
            this.TypeEditorContent = this.TypeEditor = new TypePrototypeEditorViewModel();
            this.ElementEditorContent = this.ElementEditor = new ElementPrototypeEditorViewModel();
        }

        /// <summary>
        /// Gets the module editor.
        /// </summary>
        public ModulePrototypeEditorViewModel ModuleEditor { get; }

        /// <summary>
        /// Gets or sets the module editor content.
        /// </summary>
        public ViewModelBase ModuleEditorContent
        {
            get => this.moduleEditorContent;
            set => this.RaiseAndSetIfChanged(ref this.moduleEditorContent, value);
        }

        /// <summary>
        /// Gets the type editor.
        /// </summary>
        public TypePrototypeEditorViewModel TypeEditor { get; }

        /// <summary>
        /// Gets or sets the element editor content.
        /// </summary>
        public ViewModelBase ElementEditorContent
        {
            get => this.elementEditorContent;
            set => this.RaiseAndSetIfChanged(ref this.elementEditorContent, value);
        }

        /// <summary>
        /// Gets the element editor.
        /// </summary>
        public ElementPrototypeEditorViewModel ElementEditor { get; }

        /// <summary>
        /// Gets or sets the type editor content.
        /// </summary>
        public ViewModelBase TypeEditorContent
        {
            get => this.typeEditorContent;
            set => this.RaiseAndSetIfChanged(ref this.typeEditorContent, value);
        }

        /// <summary>
        /// Gets the type function editor.
        /// </summary>
        public TypeFunctionPrototypeEditorViewModel TypeFunctionEditor { get; }

        /// <summary>
        /// Gets or sets the type function editor content.
        /// </summary>
        public ViewModelBase TypeFunctionEditorContent
        {
            get => this.typeFunctionEditorContent;
            set => this.RaiseAndSetIfChanged(ref this.typeFunctionEditorContent, value);
        }

        /// <summary>
        /// Gets the type unit editor.
        /// </summary>
        public TypeUnitPrototypeEditorViewModel TypeUnitEditor { get; }

        /// <summary>
        /// Gets or sets the type unit editor content.
        /// </summary>
        public ViewModelBase TypeUnitEditorContent
        {
            get => this.typeUnitEditorContent;
            set => this.RaiseAndSetIfChanged(ref this.typeUnitEditorContent, value);
        }
    }
}