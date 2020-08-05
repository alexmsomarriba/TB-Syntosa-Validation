namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;
    using System.Text;

    using FluentValidation.Results;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Syntosa.Validation.DataEditor.Validators;

    /// <summary>
    /// The type function prototype builder view model.
    /// </summary>
    public class TypeFunctionPrototypeBuilderViewModel : ViewModelBase
    {
        /// <summary>
        /// The syntosa dal.
        /// </summary>
        private readonly SyntosaDal syntosaDal;

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
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFunctionPrototypeBuilderViewModel"/> class.
        /// </summary>
        public TypeFunctionPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.InsertTypeFunction = ReactiveCommand.Create(this.BuildTypeFunction);
        }

        /// <summary>
        /// Gets the insert type.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertTypeFunction { get; }

        /// <summary>
        /// Gets or sets the all module names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllModuleNamesAndUIds
        {
            get => this.GetAllModuleNamesAndUIds();
            set => value = this.GetAllModuleNamesAndUIds();
        }

        /// <summary>
        /// Gets the all type function names.
        /// </summary>
        public List<string> AllModuleNames
        {
            get
            {
                var moduleNames = new List<string>();
                foreach (var name in this.AllModuleNamesAndUIds.Keys)
                {
                    moduleNames.Add(name);
                }

                return moduleNames;
            }
        }

        /// <summary>
        /// Gets or sets the module auto collect u id.
        /// </summary>
        public string SelectedModuleName
        {
            get => this.selectedModuleName;
            set => this.RaiseAndSetIfChanged(ref this.selectedModuleName, value);
        }

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
            set => this.RaiseAndSetIfChanged(ref this.typeFunctionDescription, value);
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

        /// <summary>
        /// Gets or sets a value indicating whether has errors.
        /// </summary>
        public bool HasErrors
        {
            get => this.hasErrors;
            set => this.RaiseAndSetIfChanged(ref this.hasErrors, value);
        }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public string Errors
        {
            get => this.errors;
            set => this.RaiseAndSetIfChanged(ref this.errors, value);
        }

        /// <summary>
        /// The build type function.
        /// </summary>
        private void BuildTypeFunction()
        {
            var failureMessage = new StringBuilder();
            var createdTypeFunction = new TypeFunction
                                          {
                                              Name = this.TypeFunctionName,
                                              Description = this.TypeFunctionDescription,
                                              IsActive = this.IsActive,
                                              IsBuiltIn = this.IsBuiltIn
                                          };

            if (string.IsNullOrWhiteSpace(this.SelectedModuleName))
            {
                failureMessage.AppendLine("Please select a module");
            }
            else
            {
                createdTypeFunction.ModuleUId = this.AllModuleNamesAndUIds[this.SelectedModuleName];
            }

            var typeFunctionValidator = new TypeFunctionValidator();
            ValidationResult validationResult = typeFunctionValidator.Validate(createdTypeFunction);
            if (!validationResult.IsValid || failureMessage.Length != 0)
            {
                foreach (var failure in validationResult.Errors)
                {
                    failureMessage.AppendLine(
                        $"Property {failure.PropertyName} has failed validation with error {failure.ErrorMessage}");
                }

                this.HasErrors = true;
                this.Errors = failureMessage.ToString();
                return;
            }

            this.HasErrors = false;
            this.syntosaDal.CreateTypeFunction(createdTypeFunction);
        }

        /// <summary>
        /// Gets all modules names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All domain names and UIds in the Syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllModuleNamesAndUIds()
        {
            var modules = this.syntosaDal.GetModuleByAny();
            var moduleNamesUIds = new Dictionary<string, Guid>();
            foreach (var module in modules)
            {
                moduleNamesUIds.Add(module.Name, module.UId);
            }

            return moduleNamesUIds;
        }
    }
}