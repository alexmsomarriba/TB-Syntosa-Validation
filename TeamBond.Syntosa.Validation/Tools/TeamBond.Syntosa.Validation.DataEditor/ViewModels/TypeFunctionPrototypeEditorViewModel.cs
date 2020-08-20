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
    /// The type function prototype editor view model.
    /// </summary>
    public class TypeFunctionPrototypeEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// The syntosa dal.
        /// </summary>
        private readonly SyntosaDal syntosaDal;

        /// <summary>
        /// The application context.
        /// </summary>
        private readonly IUserContext userContext;

        /// <summary>
        /// The user activity service.
        /// </summary>
        private readonly IUserActivityService userActivityService;

        /// <summary>
        /// The current description of the edited type function.
        /// </summary>
        private string currentDescription;

        /// <summary>
        /// The current name of the edited type function.
        /// </summary>
        private string currentName;

        /// <summary>
        /// A value indicating whether the user has selected a type function to edit.
        /// </summary>
        private bool hasSelected;

        /// <summary>
        /// The name of the selected type function to edit.
        /// </summary>
        private string selectedTypeFunctionName;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

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
        /// The type function description.
        /// </summary>
        private string newTypeFunctionDescription;

        /// <summary>
        /// The type function name associated.
        /// </summary>
        private string newTypeFunctionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFunctionPrototypeEditorViewModel" /> class.
        /// </summary>
        public TypeFunctionPrototypeEditorViewModel()
        {
            this.HasSelected = false;

            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();
            this.userActivityService = TeamBondEngineContext.Current.Resolve<IUserActivityService>();

            this.InsertTypeFunction = ReactiveCommand.Create(this.UpdateTypeFunction);
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
                foreach (var name in this.AllModuleNamesAndUIds.Keys)
                {
                    moduleNames.Add(name);
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
            set => this.GetAllModuleNamesAndUIds();
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
        /// Gets the all type function names.
        /// </summary>
        public List<string> AllTypeFunctionNames
        {
            get
            {
                var typeFunctionNames = new List<string>();
                foreach (var name in this.AllTypeFunctionNamesAndUIds.Keys)
                {
                    typeFunctionNames.Add(name);
                }

                return typeFunctionNames;
            }
        }

        /// <summary>
        /// Gets back button input.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Back { get; }

        /// <summary>
        /// Gets or sets the all type function name and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeFunctionNamesAndUIds
        {
            get => this.GetAllTypeFunctionNamesAndUIds();
            set => this.GetAllTypeFunctionNamesAndUIds();
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
        /// Gets the insert type.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertTypeFunction { get; }

        /// <summary>
        /// Gets whether the Next button has been pressed.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Next { get; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive
        {
            get => this.isActive;
            set => this.RaiseAndSetIfChanged(ref this.isActive, value);
        }

        /// <summary>
        /// Gets or sets the type function u id.
        /// </summary>
        public string SelectedTypeFunctionName
        {
            get => this.selectedTypeFunctionName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeFunctionName, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether user has selected a type function to edit has selected.
        /// </summary>
        public bool HasSelected
        {
            get => this.hasSelected;
            set => this.RaiseAndSetIfChanged(ref this.hasSelected, value);
        }

        /// <summary>
        /// Gets or sets the current description of the selected type function.
        /// </summary>
        public string CurrentDescription
        {
            get => this.currentDescription;
            set => this.RaiseAndSetIfChanged(ref this.currentDescription, value);
        }

        /// <summary>
        /// Gets or sets the current name of the selected type function.
        /// </summary>
        public string CurrentName
        {
            get => this.currentName;
            set => this.RaiseAndSetIfChanged(ref this.currentName, value);
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
        /// Gets or sets the module auto collect u id.
        /// </summary>
        public string SelectedModuleName
        {
            get => this.selectedModuleName;
            set => this.RaiseAndSetIfChanged(ref this.selectedModuleName, value);
        }

        /// <summary>
        /// Gets or sets the type function description.
        /// </summary>
        public string NewTypeFunctionDescription
        {
            get => this.newTypeFunctionDescription;
            set => this.RaiseAndSetIfChanged(ref this.newTypeFunctionDescription, value);
        }

        /// <summary>
        /// Gets or sets the type function name.
        /// </summary>
        public string NewTypeFunctionName
        {
            get => this.newTypeFunctionName;
            set => this.RaiseAndSetIfChanged(ref this.newTypeFunctionName, value);
        }

        /// <summary>
        /// The retrieve info.
        /// </summary>
        private void RetrieveInfo()
        {
            if (string.IsNullOrWhiteSpace(this.SelectedTypeFunctionName))
            {
                this.HasErrors = true;
                this.Errors = "Please select a type function to edit";
                return;
            }

            this.HasErrors = false;
            this.Errors = string.Empty;
            this.HasSelected = true;
            var typeFunctionToEdit = this.syntosaDal.GetTypeFunctionByAny(
                    typeFunctionName: this.SelectedTypeFunctionName,
                    typeFunctionUId: this.AllTypeFunctionNamesAndUIds[this.SelectedTypeFunctionName])
                .FirstOrDefault();

            this.CurrentName = $"The current name of this type function is {typeFunctionToEdit.Name}";
            this.CurrentDescription = $"The current description of this type function is {typeFunctionToEdit.Description}";
            this.SelectedModuleName = this.AllModuleNamesAndUIds
                .FirstOrDefault(x => x.Value == typeFunctionToEdit.ModuleUId).Key;
            this.IsBuiltIn = typeFunctionToEdit.IsBuiltIn;
            this.IsActive = typeFunctionToEdit.IsActive;
        }

        /// <summary>
        /// The build type function.
        /// </summary>
        private void UpdateTypeFunction()
        {
            bool hasChanged = false;
            var failureMessage = new StringBuilder();
            TypeFunction updatedTypeFunction = this.syntosaDal.GetTypeFunctionByAny(
                typeFunctionName: this.SelectedTypeFunctionName,
                typeFunctionUId: this.AllTypeFunctionNamesAndUIds[this.SelectedTypeFunctionName]).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(this.NewTypeFunctionName))
            {
                updatedTypeFunction.Name = this.NewTypeFunctionName;
                hasChanged = true;
            }

            if (!string.IsNullOrWhiteSpace(this.NewTypeFunctionDescription))
            {
                updatedTypeFunction.Description = this.NewTypeFunctionDescription;
                hasChanged = true;
            }

            string currentModuleName = this.AllModuleNamesAndUIds
                .FirstOrDefault(x => x.Value == updatedTypeFunction.ModuleUId).Key;

            if (!currentModuleName.Equals(this.SelectedModuleName))
            {
                updatedTypeFunction.ModuleUId = this.AllModuleNamesAndUIds[this.SelectedModuleName];
                hasChanged = true;
            }

            if (updatedTypeFunction.IsBuiltIn != this.IsBuiltIn)
            {
                updatedTypeFunction.IsBuiltIn = this.IsBuiltIn;
                hasChanged = true;
            }

            if (updatedTypeFunction.IsActive != this.IsActive)
            {
                updatedTypeFunction.IsActive = this.IsActive;
                hasChanged = true;
            }

            var typeFunctionValidator = new TypeFunctionValidator();
            ValidationResult validationResult = typeFunctionValidator.Validate(updatedTypeFunction);
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
                this.Errors = "Nothing was changed about this type function";
                return;
            }

            if (!updatedTypeFunction.ModifiedBy.Equals(this.userContext.CurrentUser.Email))
            {
                updatedTypeFunction.ModifiedBy = this.userContext.CurrentUser.Email;
            }

            this.HasErrors = false;
            this.Errors = string.Empty;
            //this.userActivityService.InsertActivity(
            //    this.userContext.CurrentUser,
            //    "Updated Type Function",
            //    $"{this.userContext.CurrentUser.Email} has updated the type function named {this.CurrentName} with UId {this.AllTypeFunctionNamesAndUIds[this.SelectedTypeFunctionName]}.");
            this.syntosaDal.UpdateTypeFunction(updatedTypeFunction);
        }

        /// <summary>
        /// Resets the view to the type function select.
        /// </summary>
        private void Return()
        {
            this.HasSelected = false;
            this.SelectedTypeFunctionName = string.Empty;
            this.NewTypeFunctionName = string.Empty;
            this.NewTypeFunctionDescription = string.Empty;
        }

        /// <summary>
        /// Gets all modules names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All module names and UIds in the Syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllModuleNamesAndUIds()
        {
            var modules = this.syntosaDal.GetModuleByAny(isActive: true);
            var moduleNamesUIds = new Dictionary<string, Guid>();
            foreach (var module in modules)
            {
                moduleNamesUIds.Add(module.Name, module.UId);
            }

            return moduleNamesUIds;
        }

        /// <summary>
        /// Gets all type function names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All type function names and UIds in the Syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllTypeFunctionNamesAndUIds()
        {
            var typeFunctions = this.syntosaDal.GetTypeFunctionByAny();
            var typeFunctionNamesUIds = new Dictionary<string, Guid>();
            foreach (var typeFunction in typeFunctions)
            {
                typeFunctionNamesUIds.Add(typeFunction.Name, typeFunction.UId);
            }

            return typeFunctionNamesUIds;
        }
    }
}