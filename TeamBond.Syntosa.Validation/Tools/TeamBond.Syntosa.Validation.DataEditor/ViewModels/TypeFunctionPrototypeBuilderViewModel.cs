namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System.Collections.Generic;

    using ReactiveUI;

    /// <summary>
    /// The type function prototype builder view model.
    /// </summary>
    public class TypeFunctionPrototypeBuilderViewModel : ViewModelBase
    {
        /// <summary>
        /// The type function name associated.
        /// </summary>
        private string typeFunctionName;

        /// <summary>
        /// The type function description.
        /// </summary>
        private string typeFunctionDescription;

        /// <summary>
        /// A value indicating whether the type function is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The is built in.
        /// </summary>
        private bool isBuiltIn;

        /// <summary>
        /// The selected module name.
        /// </summary>
        private string selectedModuleName;

        /// <summary>
        /// The list of all module names.
        /// </summary>
        private List<string> allModuleNames;

        /// <summary>
        /// Gets or sets the type function name.
        /// </summary>
        public string TypeFunctionName
        {
            get => this.typeFunctionName;
            set => this.RaiseAndSetIfChanged(ref this.typeFunctionName, value);
        }

        /// <summary>
        /// Gets or sets the type function description.
        /// </summary>
        public string TypeFunctionDescription
        {
            get => this.typeFunctionDescription;
            set => this.RaiseAndSetIfChanged(ref this.typeFunctionName, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive
        {
            get => this.isActive;
            set => this.RaiseAndSetIfChanged(ref this.isActive, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is built in.
        /// </summary>
        public bool IsBuiltIn
        {
            get => this.isBuiltIn;
            set => this.RaiseAndSetIfChanged(ref this.isBuiltIn, value);
        }
    }
}