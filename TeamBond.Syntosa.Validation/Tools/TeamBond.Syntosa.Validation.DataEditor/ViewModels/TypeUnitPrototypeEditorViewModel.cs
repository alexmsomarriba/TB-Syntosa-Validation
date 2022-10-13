using TeamBond.Services.Audit;

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
    /// The type unit prototype editor view model.
    /// </summary>
    public class TypeUnitPrototypeEditorViewModel : ViewModelBase
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
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// A value indicating whether the user has selected a type unit to edit.
        /// </summary>
        private bool hasSelected;

        /// <summary>
        /// A value indicating whether the delete button is visible to the current user.
        /// </summary>
        private bool isDeleteVisible;

        /// <summary>
        /// The current description of the type unit to edit.
        /// </summary>
        private string currentDescription;

        /// <summary>
        /// The current name of the type unit to edit.
        /// </summary>
        private string currentName;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// The has parent.
        /// </summary>
        private bool hasParent;

        /// <summary>
        /// A value indicating whether the type unit is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The is built in.
        /// </summary>
        private bool isBuiltIn;

        /// <summary>
        /// The type unit description.
        /// </summary>
        private string newTypeUnitDescription;

        /// <summary>
        /// The type unit name associated.
        /// </summary>
        private string newTypeUnitName;

        /// <summary>
        /// The selected module name.
        /// </summary>
        private string selectedModuleName;

        /// <summary>
        /// The name of the selected type unit to edit.
        /// </summary>
        private string selectedTypeUnitName;

        /// <summary>
        /// The selected type unit name.
        /// </summary>
        private string selectedTypeUnitParentName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeUnitPrototypeEditorViewModel" /> class.
        /// </summary>
        public TypeUnitPrototypeEditorViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userActivityService = TeamBondEngineContext.Current.Resolve<IUserActivityService>();
            //this.userService = TeamBondEngineContext.Current.Resolve<IUserService>();

            this.HasSelected = false;
            this.HasParent = false;
            this.IsDeleteVisible = false;

            this.Next = ReactiveCommand.Create(this.RetrieveInfo);
            this.UpdateTypeUnit = ReactiveCommand.Create(this.EditTypeUnit);
            this.Back = ReactiveCommand.Create(this.Return);
            this.DeleteTypeUnit = ReactiveCommand.Create(this.RemoveTypeUnit);
        }

        /// <summary>
        /// Gets the all type unit names.
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
        /// Gets or sets the name of the selected type unit to edit.
        /// </summary>
        public string SelectedTypeUnitName
        {
            get => this.selectedTypeUnitName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeUnitName, value);
        }

        /// <summary>
        /// Gets or sets the current name of the type unit to edit.
        /// </summary>
        public string CurrentName
        {
            get => this.currentName;
            set => this.RaiseAndSetIfChanged(ref this.currentName, value);
        }

        /// <summary>
        /// Gets or sets the current description of the type unit to edit.
        /// </summary>
        public string CurrentDescription
        {
            get => this.currentDescription;
            set => this.RaiseAndSetIfChanged(ref this.currentDescription, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user has selected a type unit to edit.
        /// </summary>
        public bool HasSelected
        {
            get => this.hasSelected;
            set => this.RaiseAndSetIfChanged(ref this.hasSelected, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the delete button is visible for the current user.
        /// </summary>
        public bool IsDeleteVisible
        {
            get => this.isDeleteVisible;
            set => this.RaiseAndSetIfChanged(ref this.isDeleteVisible, value);
        }

        /// <summary>
        /// Gets the all type function names.
        /// </summary>
        public List<string> AllTypeUnitNames
        {
            get
            {
                var typeUnitNames = new List<string>();
                foreach (var name in this.AllTypeUnitNamesAndUIds.Keys)
                {
                    typeUnitNames.Add(name);
                }

                return typeUnitNames;
            }
        }

        /// <summary>
        /// Gets or sets the all type function name and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeUnitNamesAndUIds
        {
            get => this.GetAllTypeUnitNamesAndUIds();
            set => this.GetAllTypeUnitNamesAndUIds();
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
        /// Gets or sets a value indicating whether has errors.
        /// </summary>
        public bool HasErrors
        {
            get => this.hasErrors;
            set => this.RaiseAndSetIfChanged(ref this.hasErrors, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has parent.
        /// </summary>
        public bool HasParent
        {
            get => this.hasParent;
            set => this.RaiseAndSetIfChanged(ref this.hasParent, value);
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
        /// Gets or sets the type unit description.
        /// </summary>
        public string NewTypeUnitDescription
        {
            get => this.newTypeUnitDescription;
            set => this.RaiseAndSetIfChanged(ref this.newTypeUnitDescription, value);
        }

        /// <summary>
        /// Gets or sets the type unit name.
        /// </summary>
        public string NewTypeUnitName
        {
            get => this.newTypeUnitName;
            set => this.RaiseAndSetIfChanged(ref this.newTypeUnitName, value);
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
        /// Gets or sets the type unit u id.
        /// </summary>
        public string SelectedTypeUnitParentName
        {
            get => this.selectedTypeUnitParentName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeUnitParentName, value);
        }

        /// <summary>
        /// Gets whether the insert type unit button has been pressed.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateTypeUnit { get; }

        /// <summary>
        /// Gets whether the next button has been pressed.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Next { get; }

        /// <summary>
        /// Gets whether the back button has been pressed.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Back { get; }

        /// <summary>
        /// Gets whether the delete type unit button has been pressed.
        /// </summary>
        public ReactiveCommand<Unit, Unit> DeleteTypeUnit { get; }

        /// <summary>
        /// Retrieves the info related to the selected type unit.
        /// </summary>
        private void RetrieveInfo()
        {
            if (string.IsNullOrWhiteSpace(this.SelectedTypeUnitName))
            {
                this.HasErrors = true;
                this.Errors = "Please select a type unit to edit";
                return;
            }

            var typeUnitToEdit = new global::Syntosa.Core.ObjectModel.CoreClasses.TypeUnit();//this.syntosaDal.GetTypeUnitByAny(
            //    typeUnitName: this.SelectedTypeUnitName,
            //    typeUnitUId: this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitName]).FirstOrDefault();
            //this.HasSelected = true;

            this.IsDeleteVisible = true;

            this.CurrentName = $"The current name of this type unit is {typeUnitToEdit.Name}";
            this.CurrentDescription = $"The current description of this type unit is {typeUnitToEdit.Description}";
            this.SelectedModuleName = typeUnitToEdit.ModuleName;
            this.IsBuiltIn = typeUnitToEdit.IsBuiltIn;
            this.IsActive = typeUnitToEdit.IsActive;
            if (typeUnitToEdit.ParentUId != Guid.Empty)
            {
                this.HasParent = true;
                this.SelectedTypeUnitParentName = typeUnitToEdit.ParentName;
            }
        }

        /// <summary>
        /// Updates the selected type unit.
        /// </summary>
        private void EditTypeUnit()
        {
            var failureMessage = new StringBuilder();
            bool hasChanged = false;
            var updatedTypeUnit = this.syntosaDal.GetTypeUnitByAny(
                typeUnitName: this.SelectedTypeUnitName,
                typeUnitUId: this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitName]).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(this.NewTypeUnitName) && !updatedTypeUnit.Name.Equals(this.NewTypeUnitName))
            {
                updatedTypeUnit.Name = this.NewTypeUnitName;
                hasChanged = true;
            }

            if (!string.IsNullOrWhiteSpace(this.NewTypeUnitDescription)
                && !updatedTypeUnit.Description.Equals(this.NewTypeUnitDescription))
            {
                updatedTypeUnit.Description = this.NewTypeUnitDescription;
                hasChanged = true;
            }

            if (!this.SelectedModuleName.Equals(updatedTypeUnit.ModuleName))
            {
                updatedTypeUnit.ModuleUId = this.AllModuleNamesAndUIds[this.SelectedModuleName];
                hasChanged = true;
            }

            if (updatedTypeUnit.IsBuiltIn != this.IsBuiltIn)
            {
                updatedTypeUnit.IsBuiltIn = this.IsBuiltIn;
                hasChanged = true;
            }

            if (updatedTypeUnit.IsActive != this.IsActive)
            {
                updatedTypeUnit.IsActive = this.IsActive;
                hasChanged = true;
            }

            if ((this.HasParent && !string.IsNullOrWhiteSpace(this.SelectedTypeUnitParentName)
                                && updatedTypeUnit.ParentUId == Guid.Empty)
                || (!this.HasParent && updatedTypeUnit.ParentUId != Guid.Empty)
                || (this.HasParent && !updatedTypeUnit.ParentName.Equals(this.SelectedTypeUnitParentName)))
            {
                if (this.HasParent && !string.IsNullOrWhiteSpace(this.SelectedTypeUnitParentName)
                                   && updatedTypeUnit.ParentUId == Guid.Empty)
                {
                    updatedTypeUnit.ParentUId = this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitParentName];
                }

                if (!this.HasParent && updatedTypeUnit.ParentUId != Guid.Empty)
                {
                    updatedTypeUnit.ParentUId = Guid.Empty;
                }

                if (this.HasParent && !updatedTypeUnit.ParentName.Equals(this.SelectedTypeUnitParentName))
                {
                    updatedTypeUnit.ParentUId = this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitParentName];
                }

                hasChanged = true;
            }

            var typeUnitValidator = new TypeUnitValidator();
            ValidationResult validationResult = typeUnitValidator.Validate(updatedTypeUnit);
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
                this.Errors = "Nothing about the type unit has changed";
                return;
            }

            updatedTypeUnit.ModifiedBy = "alex@teambond.io";
            this.syntosaDal.UpdateTypeUnit(updatedTypeUnit);
            //this.userActivityService.InsertActivity(
            //    this.userContext.CurrentUser,
            //    "Type Unit Updated",
            //    $"{this.userContext.CurrentUser.Email} has updated the type unit named {this.CurrentName} with UId {this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitName]}");
            this.HasErrors = false;
            this.Errors = string.Empty;
        }

        /// <summary>
        /// Removes the selected type unit from Syntosa.
        /// </summary>
        private void RemoveTypeUnit()
        {
            if (!this.HasErrors && string.IsNullOrWhiteSpace(this.Errors))
            {
                this.HasErrors = true;
                this.Errors = "To confirm deletion of this type unit press the 'Delete Type Unit' Button again.";
                return;
            }

            if (this.HasErrors && this.Errors.Equals(
                    "To confirm deletion of this type unit press the 'Delete Type Unit' Button again."))
            {
                this.syntosaDal.DeleteTypeUnit(this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitName]);
                this.HasErrors = false;
                this.Errors = string.Empty;
                this.Return();
            }
        }

        /// <summary>
        /// Return this view to its selection configuration.
        /// </summary>
        private void Return()
        {
            this.SelectedTypeUnitName = string.Empty;
            this.HasParent = false;
            this.HasSelected = false;
        }

        /// <summary>
        /// Gets all modules names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All module names and UIds in the Syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllModuleNamesAndUIds()
        {
            var modules = new List<global::Syntosa.Core.ObjectModel.CoreClasses.Module>(); //this.syntosaDal.GetModuleByAny(isActive: true);
            var moduleNamesUIds = new Dictionary<string, Guid>();
            foreach (var module in modules)
            {
                moduleNamesUIds.Add(module.Name, module.UId);
            }

            return moduleNamesUIds;
        }

        /// <summary>
        /// Gets all type unit names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All type unit names and UIds in the Syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllTypeUnitNamesAndUIds()
        {
            var typeUnits = new List<global::Syntosa.Core.ObjectModel.CoreClasses.TypeUnit>(); //this.syntosaDal.GetTypeUnitByAny();
            var typeUnitNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var typeUnit in typeUnits)
            {
                typeUnitNamesAndUIds.Add(typeUnit.Name, typeUnit.UId);
            }

            return typeUnitNamesAndUIds;
        }
    }
}