namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive;
    using System.Text;

    using FluentValidation.Results;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;
    using TeamBond.Services.Users;
    using TeamBond.Syntosa.Validation.DataEditor.Validators;

    /// <summary>
    /// The module prototype builder view model.
    /// </summary>
    public class ModulePrototypeBuilderViewModel : ViewModelBase
    {
        /// <summary>
        /// The syntosa dal.
        /// </summary>
        private readonly SyntosaDal syntosaDal;

        /// <summary>
        /// The user activity service.
        /// </summary>
        private readonly IUserActivityService userActivityService;

        /// <summary>
        /// The user context.
        /// </summary>
        private readonly IUserContext userContext;

        /// <summary>
        /// The module description.
        /// </summary>
        private string description;

        /// <summary>
        /// The errors with the module insertion.
        /// </summary>
        private string errors;

        /// <summary>
        /// A value indicating whether there were module insertion has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// A value indicating whether the module has a parent module.
        /// </summary>
        private bool hasParent;

        /// <summary>
        /// A value indicating if the module is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// A value indicating if the module is built in.
        /// </summary>
        private bool isBuiltIn;

        /// <summary>
        /// The module name.
        /// </summary>
        private string name;

        /// <summary>
        /// The name of the selected parent module name.
        /// </summary>
        private string selectedModuleName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePrototypeBuilderViewModel" /> class.
        /// </summary>
        public ModulePrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userActivityService = TeamBondEngineContext.Current.Resolve<IUserActivityService>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();

            this.CreateModule = ReactiveCommand.Create(this.BuildModule);
        }

        /// <summary>
        /// Gets the all type function names.
        /// </summary>
        public List<string> AllModuleNames
        {
            get
            {
                var moduleNames = new List<string>();
                foreach (var moduleName in this.AllModuleNamesAndUIds.Keys)
                {
                    moduleNames.Add(moduleName);
                }

                return moduleNames;
            }
        }

        /// <summary>
        /// Gets or sets the all module names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllModuleNamesAndUIds
        {
            get => this.GetAllModuleNamesAndUIds();
            set => value = this.GetAllModuleNamesAndUIds();
        }

        /// <summary>
        /// Gets the create domain button interaction.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CreateModule { get; }

        /// <summary>
        /// Gets or sets the description of the module.
        /// </summary>
        public string Description
        {
            get => this.description;
            set => this.RaiseAndSetIfChanged(ref this.description, value);
        }

        /// <summary>
        /// Gets or sets the errors with the module insert request.
        /// </summary>
        public string Errors
        {
            get => this.errors;
            set => this.RaiseAndSetIfChanged(ref this.errors, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the module insert has errors.
        /// </summary>
        public bool HasErrors
        {
            get => this.hasErrors;
            set => this.RaiseAndSetIfChanged(ref this.hasErrors, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the module has a parent module.
        /// </summary>
        public bool HasParent
        {
            get => this.hasParent;
            set => this.RaiseAndSetIfChanged(ref this.hasParent, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the module is active.
        /// </summary>
        public bool IsActive
        {
            get => this.isActive;
            set => this.RaiseAndSetIfChanged(ref this.isActive, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the module is built in.
        /// </summary>
        public bool IsBuiltIn
        {
            get => this.isBuiltIn;
            set => this.RaiseAndSetIfChanged(ref this.isBuiltIn, value);
        }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.RaiseAndSetIfChanged(ref this.name, value);
        }

        /// <summary>
        /// Gets or sets the selected parent module name.
        /// </summary>
        public string SelectedModuleName
        {
            get => this.selectedModuleName;
            set => this.RaiseAndSetIfChanged(ref this.selectedModuleName, value);
        }

        /// <summary>
        /// The build module based off the given inputs.
        /// </summary>
        private void BuildModule()
        {
            var failureMessage = new StringBuilder();
            var createdModule = new Module
                                    {
                                        Name = this.Name,
                                        Description = this.Description,
                                        IsActive = this.isActive,
                                        IsBuiltIn = this.IsBuiltIn,
                                        ParentUId = Guid.Empty,
                                        ModifiedBy = this.userContext.CurrentUser.Email
                                    };

            if (this.HasParent)
            {
                if (string.IsNullOrWhiteSpace(this.SelectedModuleName))
                {
                    failureMessage.AppendLine(
                        "Please select a parent module or deselect the 'Has parent domain' check box");
                }
                else
                {
                    createdModule.ParentUId = this.AllModuleNamesAndUIds[this.SelectedModuleName];
                }
            }

            var moduleValidator = new ModuleValidator();
            ValidationResult validationResult = moduleValidator.Validate(createdModule);
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

            this.syntosaDal.CreateModule(createdModule);
            createdModule = this.syntosaDal.GetModuleByAny(
                moduleName: this.Name,
                moduleDesc: this.Description,
                isActive: this.IsActive,
                isBuiltIn: this.isBuiltIn).FirstOrDefault();
            this.userActivityService.InsertActivity(
                this.userContext.CurrentUser,
                "Module Inserted",
                $"{this.userContext.CurrentUser.Email} has inserted the module named {createdModule.Name} with UId {createdModule.UId}");
            this.errors = string.Empty;
            this.hasErrors = false;
        }

        /// <summary>
        /// Gets all modules names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All module names and UIds in the Syntosa database.
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