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
    /// The type unit prototype view model.
    /// </summary>
    public class TypeUnitPrototypeBuilderViewModel : ViewModelBase
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
        /// The application context.
        /// </summary>
        private readonly IUserContext userContext;

        /// <summary>
        /// The type unit name associated.
        /// </summary>
        private string typeUnitName;

        /// <summary>
        /// The type unit description.
        /// </summary>
        private string typeUnitDescription;

        /// <summary>
        /// A value indicating whether the type unit is active.
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
        /// The has parent.
        /// </summary>
        private bool hasParent;

        /// <summary>
        /// The selected type unit name.
        /// </summary>
        private string selectedTypeUnitName;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeUnitPrototypeBuilderViewModel"/> class.
        /// </summary>
        public TypeUnitPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userActivityService = TeamBondEngineContext.Current.Resolve<IUserActivityService>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();

            this.InsertTypeUnit = ReactiveCommand.Create(this.BuildTypeUnit);
        }

        /// <summary>
        /// Gets the insert type.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertTypeUnit { get; }

        /// <summary>
        /// Gets or sets the all module names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllModuleNamesAndUIds
        {
            get => this.GetAllModuleNamesAndUIds();
            set => this.GetAllModuleNamesAndUIds();
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
        /// Gets or sets all type function name and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeUnitNamesAndUIds
        {
            get => this.GetAllTypeUnitNamesAndUIds();
            set => this.GetAllTypeUnitNamesAndUIds();
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
        public string SelectedTypeUnitName
        {
            get => this.selectedTypeUnitName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeUnitName, value);
        }

        /// <summary>
        /// Gets or sets the type unit name.
        /// </summary>
        public string TypeUnitName
        {
            get => this.typeUnitName;
            set => this.RaiseAndSetIfChanged(ref this.typeUnitName, value);
        }

        /// <summary>
        /// Gets or sets the type unit description.
        /// </summary>
        public string TypeUnitDescription
        {
            get => this.typeUnitDescription;
            set => this.RaiseAndSetIfChanged(ref this.typeUnitDescription, value);
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
        /// The build type unit.
        /// </summary>
        private void BuildTypeUnit()
        {
            var failureMessage = new StringBuilder();
            var createdTypeUnit = new TypeUnit
            {
                Name = this.TypeUnitName,
                Description = this.TypeUnitDescription,
                IsActive = this.IsActive,
                IsBuiltIn = this.IsBuiltIn,
                ModifiedBy = this.userContext.CurrentUser.Email,
                ParentUId = Guid.Empty
            };

            if (string.IsNullOrWhiteSpace(this.SelectedModuleName))
            {
                failureMessage.AppendLine("Please select a module");
            }
            else
            {
                createdTypeUnit.ModuleUId = this.AllModuleNamesAndUIds[this.SelectedModuleName];
            }

            if (this.HasParent)
            {
                if (string.IsNullOrWhiteSpace(this.SelectedTypeUnitName))
                {
                    failureMessage.AppendLine("Please select a parent module or deselect the 'Has Parent' checkbox");
                }
                else
                {
                    createdTypeUnit.ParentUId = this.AllModuleNamesAndUIds[this.SelectedTypeUnitName];
                }
            }

            var typeUnitValidator = new TypeUnitValidator();
            ValidationResult validationResult = typeUnitValidator.Validate(createdTypeUnit);
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
            this.Errors = string.Empty;
            this.syntosaDal.CreateTypeUnit(createdTypeUnit);
            createdTypeUnit = this.syntosaDal.GetTypeUnitByAny(
                typeUnitName: this.TypeUnitName,
                typeUnitDesc: this.TypeUnitDescription,
                isActive: this.IsActive,
                isBuiltIn: this.IsBuiltIn).FirstOrDefault();
            //this.userActivityService.InsertActivity(
            //    this.userContext.CurrentUser,
            //    "Type Item Inserted",
            //    $"{this.userContext.CurrentUser.Email} has inserted the type unit named {createdTypeUnit.Name} with UId {createdTypeUnit.UId}");
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
        /// Gets all type unit names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All type unit names and UIds in the Syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllTypeUnitNamesAndUIds()
        {
            var typeUnits = this.syntosaDal.GetTypeUnitByAny();
            var typeUnitNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var typeUnit in typeUnits)
            {
                typeUnitNamesAndUIds.Add(typeUnit.Name, typeUnit.UId);
            }

            return typeUnitNamesAndUIds;
        }
    }
}