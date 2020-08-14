namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using ReactiveUI;

    /// <summary>
    /// The prototype editor view model.
    /// </summary>
    public class PrototypeEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// The type prototype editor view model.
        /// </summary>
        private ViewModelBase typeEditorContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeEditorViewModel"/> class.
        /// </summary>
        public PrototypeEditorViewModel()
        {
            this.TypeEditorContent = this.TypeEditor = new TypePrototypeEditorViewModel();
        }

        /// <summary>
        /// Gets the type editor.
        /// </summary>
        public TypePrototypeEditorViewModel TypeEditor { get; }

        /// <summary>
        /// Gets or sets the type editor content.
        /// </summary>
        public ViewModelBase TypeEditorContent
        {
            get => this.typeEditorContent;
            set => this.RaiseAndSetIfChanged(ref this.typeEditorContent, value);
        }
    }
}