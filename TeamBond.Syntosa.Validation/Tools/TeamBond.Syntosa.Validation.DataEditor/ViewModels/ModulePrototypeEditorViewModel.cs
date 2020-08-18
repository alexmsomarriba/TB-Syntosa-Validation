namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive;
    using System.Text;

    using FluentValidation.Results;

    using global::Syntosa.Core.DataAccessLayer;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;
    using TeamBond.Services.Users;
    using TeamBond.Syntosa.Validation.DataEditor.Validators;

    /// <summary>
    /// The module prototype editor view model.
    /// </summary>
    public class ModulePrototypeEditorViewModel : ViewModelBase
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
        /// The name of the selected module to edit.
        /// </summary>
        private string selectedModuleName;

        /// <summary>
        /// The current name of the module to edit.
        /// </summary>
        private string currentName;

        /// <summary>
        /// The current description of the module to edit.
        /// </summary>
        private string currentDescription;

        /// <summary>
        /// The module description.
        /// </summary>
        private string newModuleDescription;

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
        /// A value indicating whether a module to edit has been selected.
        /// </summary>
        private bool hasSelected;

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
        private string newModuleName;

        /// <summary>
        /// The name of the selected parent module name.
        /// </summary>
        private string selectedModuleParentName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePrototypeEditorViewModel" /> class.
        /// </summary>
        public ModulePrototypeEditorViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userActivityService = TeamBondEngineContext.Current.Resolve<IUserActivityService>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();

            this.HasSelected = false;
            this.HasParent = false;

            this.UpdateModule = ReactiveCommand.Create(this.EditModule);
            this.Next = ReactiveCommand.Create(this.RetrieveInfo);
            this.Back = ReactiveCommand.Create(this.Return);
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
        /// Gets or sets the name of the selected module to edit.
        /// </summary>
        public string SelectedModuleName
        {
            get => this.selectedModuleName;
            set => this.RaiseAndSetIfChanged(ref this.selectedModuleName, value);
        }

        /// <summary>
        /// Gets or sets the current name of the module to edit.
        /// </summary>
        public string CurrentName
        {
            get => this.currentName;
            set => this.RaiseAndSetIfChanged(ref this.currentName, value);
        }

        /// <summary>
        /// Gets or sets the current description of the module to edit.
        /// </summary>
        public string CurrentDescription
        {
            get => this.currentDescription;
            set => this.RaiseAndSetIfChanged(ref this.currentDescription, value);
        }

        /// <summary>
        /// Gets or sets the all module names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllModuleNamesAndUIds
        {
            get => this.GetAllModuleNamesAndUIds();
            set => this.GetAllModuleNamesAndUIds();
        }

        /// <summary>
        /// Gets the create domain button interaction.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateModule { get; }

        /// <summary>
        /// Gets or sets the description of the module.
        /// </summary>
        public string NewModuleDescription
        {
            get => this.newModuleDescription;
            set => this.RaiseAndSetIfChanged(ref this.newModuleDescription, value);
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
        /// Gets or sets a value indicating whether a module to edit has been selected.
        /// </summary>
        public bool HasSelected
        {
            get => this.hasSelected;
            set => this.RaiseAndSetIfChanged(ref this.hasSelected, value);
        }

        /// <summary>
        /// Gets whether the Next button has been pressed.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Next { get; }

        /// <summary>
        /// Gets whether the Back button has been pressed.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Back { get; }

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
        public string NewModuleName
        {
            get => this.newModuleName;
            set => this.RaiseAndSetIfChanged(ref this.newModuleName, value);
        }

        /// <summary>
        /// Gets or sets the selected parent module name.
        /// </summary>
        public string SelectedModuleParentName
        {
            get => this.selectedModuleParentName;
            set => this.RaiseAndSetIfChanged(ref this.selectedModuleParentName, value);
        }

        /// <summary>
        /// Retrieves the info to display about the selected module.
        /// </summary>
        private void RetrieveInfo()
        {
            if (string.IsNullOrWhiteSpace(this.SelectedModuleName))
            {
                this.HasErrors = true;
                this.Errors = "Please select a module to edit";
                return;
            }

            var moduleToEdit = this.syntosaDal.GetModuleByAny(
                moduleName: this.SelectedModuleName,
                moduleUId: this.AllModuleNamesAndUIds[this.SelectedModuleName]).FirstOrDefault();

            this.HasSelected = true;

            this.CurrentName = $"The current name of this module is {moduleToEdit.Name}";
            this.CurrentDescription = $"The current description of this module is {moduleToEdit.Description}";
            this.IsActive = moduleToEdit.IsActive;
            this.IsBuiltIn = moduleToEdit.IsBuiltIn;
            if (moduleToEdit.ParentUId != Guid.Empty)
            {
                this.HasParent = true;
                this.SelectedModuleParentName = moduleToEdit.ParentName;
            }
        }

        /// <summary>
        /// The build module based off the given inputs.
        /// </summary>
        private void EditModule()
        {
            var failureMessage = new StringBuilder();
            bool hasChanged = false;
            var updatedModule = this.syntosaDal.GetModuleByAny(
                moduleName: this.SelectedModuleName,
                moduleUId: this.AllModuleNamesAndUIds[this.SelectedModuleName]).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(this.NewModuleName) && !updatedModule.Name.Equals(this.NewModuleName))
            {
                updatedModule.Name = this.NewModuleName;
                hasChanged = true;
            }

            if (!string.IsNullOrWhiteSpace(this.NewModuleDescription)
                && !updatedModule.Description.Equals(this.NewModuleDescription))
            {
                updatedModule.Description = this.NewModuleDescription;
                hasChanged = true;
            }

            if (updatedModule.IsActive != this.IsActive)
            {
                updatedModule.IsActive = this.IsActive;
                hasChanged = true;
            }

            if (updatedModule.IsBuiltIn != this.IsBuiltIn)
            {
                updatedModule.IsBuiltIn = this.IsBuiltIn;
                hasChanged = true;
            }

            if ((this.HasParent && !string.IsNullOrWhiteSpace(this.SelectedModuleParentName)
                                && updatedModule.ParentUId == Guid.Empty)
                || (!this.HasParent && updatedModule.ParentUId != Guid.Empty)
                || (this.HasParent && !updatedModule.ParentName.Equals(this.SelectedModuleParentName)))
            {
                if (this.HasParent && !string.IsNullOrWhiteSpace(this.SelectedModuleParentName)
                                   && updatedModule.ParentUId == Guid.Empty)
                {
                    updatedModule.ParentUId = this.AllModuleNamesAndUIds[this.SelectedModuleParentName];
                }

                if (!this.HasParent && updatedModule.ParentUId != Guid.Empty)
                {
                    updatedModule.ParentUId = Guid.Empty;
                }

                if (this.HasParent && !updatedModule.ParentName.Equals(this.SelectedModuleParentName))
                {
                    updatedModule.ParentUId = this.AllModuleNamesAndUIds[this.SelectedModuleParentName];
                }

                hasChanged = true;
            }

            var moduleValidator = new ModuleValidator();
            ValidationResult validationResult = moduleValidator.Validate(updatedModule);
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

            if (!hasChanged)
            {
                this.HasErrors = true;
                this.Errors = "Nothing has changed about the module";
                return;
            }

            updatedModule.ModifiedBy = this.userContext.CurrentUser.Email;
            this.syntosaDal.UpdateModule(updatedModule);
            this.userActivityService.InsertActivity(
                this.userContext.CurrentUser,
                "Module Updated",
                $"{this.userContext.CurrentUser.Email} has updated the module named {this.CurrentName} with UId {this.AllModuleNamesAndUIds[this.SelectedModuleName]}");
            this.errors = string.Empty;
            this.hasErrors = false;
        }

        /// <summary>
        /// Returns the view to the selection configuration.
        /// </summary>
        private void Return()
        {
            this.SelectedModuleName = string.Empty;
            this.HasSelected = false;
            this.HasParent = false;
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