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
    /// The type proto type builder view model.
    /// </summary>
    public class TypePrototypeBuilderViewModel : ViewModelBase
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
        /// The description of the type.
        /// </summary>
        private string description;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// A value indicating whether this type has a parent.
        /// </summary>
        private bool hasParent;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// A value indicating whether the type is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// A value indicating whether the type is assignable.
        /// </summary>
        private bool isAssignable;

        /// <summary>
        /// A value indicating whether the type  auto collect.
        /// </summary>
        private bool isAutoCollect;

        /// <summary>
        /// A value indicating whether the type is built in.
        /// </summary>
        private bool isBuiltIn;

        /// <summary>
        /// A value indicating whether the type is notifiable.
        /// </summary>
        private bool isNotifiable;

        /// <summary>
        /// The module auto collect name.
        /// </summary>
        private string moduleAutoCollectName;

        /// <summary>
        /// The selected data store type.
        /// </summary>
        private string selectedDataStoreType;

        /// <summary>
        /// The type function name.
        /// </summary>
        private string selectedTypeFunctionName;

        /// <summary>
        /// The parent type item name.
        /// </summary>
        private string selectedTypeItemName;

        /// <summary>
        /// The type unit name.
        /// </summary>
        private string selectedTypeUnitName;

        /// <summary>
        /// The sort order.
        /// </summary>
        private string sortOrder;

        /// <summary>
        /// The type name.
        /// </summary>
        private string typeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypePrototypeBuilderViewModel"/> class.
        /// </summary>
        public TypePrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userActivityService = TeamBondEngineContext.Current.Resolve<IUserActivityService>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();

            this.InsertType = ReactiveCommand.Create(this.BuildTypeItem);
        }

        /// <summary>
        /// Gets or sets the current user identifier.
        /// </summary>
        public string CurrentUserIdentifier { get; set; }

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
        /// Gets or sets the all type function name and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeFunctionNamesAndUIds
        {
            get => this.GetAllTypeFunctionNamesAndUIds();
            set => this.GetAllTypeFunctionNamesAndUIds();
        }

        /// <summary>
        /// Gets the all type function names.
        /// </summary>
        public List<string> AllTypeItemNames
        {
            get
            {
                var typeFunctionNames = new List<string>();
                foreach (var name in this.AllTypeItemNamesAndUIds.Keys)
                {
                    typeFunctionNames.Add(name);
                }

                return typeFunctionNames;
            }
        }

        /// <summary>
        /// Gets or sets the all type function name and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeItemNamesAndUIds
        {
            get => this.GetAllTypeItemNamesAndUIds();
            set => this.GetAllTypeItemNamesAndUIds();
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
        /// Gets or sets all the type function name and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeUnitNamesAndUIds
        {
            get => this.GetAllTypeUnitNamesAndUIds();
            set => this.GetAllTypeUnitNamesAndUIds();
        }

        /// <summary>
        /// The database types.
        /// </summary>
        public List<string> DatabaseTypes =>
            new List<string>
                {
                    "Relational",
                    "Key value",
                    "In memory",
                    "Graph",
                    "Document",
                    "Ledger",
                    "Time series",
                    "Search"
                };

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get => this.description;
            set => this.RaiseAndSetIfChanged(ref this.description, value);
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
        /// Gets or sets a value indicating whether this type has a parent.
        /// </summary>
        public bool HasParent
        {
            get => this.hasParent;
            set => this.RaiseAndSetIfChanged(ref this.hasParent, value);
        }

        /// <summary>
        /// Gets the insert type.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive
        {
            get => this.isActive;
            set => this.RaiseAndSetIfChanged(ref this.isActive, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is assignable.
        /// </summary>
        public bool IsAssignable
        {
            get => this.isAssignable;
            set => this.RaiseAndSetIfChanged(ref this.isAssignable, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is auto collect.
        /// </summary>
        public bool IsAutoCollect
        {
            get => this.isAutoCollect;
            set => this.RaiseAndSetIfChanged(ref this.isAutoCollect, value);
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
        /// Gets or sets a value indicating whether is notifiable.
        /// </summary>
        public bool IsNotifiable
        {
            get => this.isNotifiable;
            set => this.RaiseAndSetIfChanged(ref this.isNotifiable, value);
        }

        /// <summary>
        /// The is user admin.
        /// </summary>
        public bool IsUserAdmin => true;

        /// <summary>
        /// Gets or sets the selected type.
        /// </summary>
        public string SelectedDataStoreType
        {
            get => this.selectedDataStoreType;
            set => this.RaiseAndSetIfChanged(ref this.selectedDataStoreType, value);
        }

        /// <summary>
        /// Gets or sets the module auto collect u id.
        /// </summary>
        public string SelectedModuleAutoCollectName
        {
            get => this.moduleAutoCollectName;
            set => this.RaiseAndSetIfChanged(ref this.moduleAutoCollectName, value);
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
        /// Gets or sets the type unit u id.
        /// </summary>
        public string SelectedTypeItemName
        {
            get => this.selectedTypeItemName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeItemName, value);
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
        /// Gets or sets the sort order.
        /// </summary>
        public string SortOrder
        {
            get => this.sortOrder;
            set => this.RaiseAndSetIfChanged(ref this.sortOrder, value);
        }

        /// <summary>
        /// Gets or sets the type name.
        /// </summary>
        public string TypeName
        {
            get => this.typeName;
            set => this.RaiseAndSetIfChanged(ref this.typeName, value);
        }

        /// <summary>
        /// The build type item.
        /// </summary>
        private void BuildTypeItem()
        {
            var failureMessages = new StringBuilder();
            var createdTypeItem = new TypeItem
                                      {
                                          IsActive = this.IsActive,
                                          IsBuiltIn = this.IsBuiltIn,
                                          IsAutoCollect = this.IsAutoCollect,
                                          IsAssignable = this.IsAssignable,
                                          IsNotifiable = this.IsNotifiable,
                                          SortOrder = Convert.ToInt32(this.SortOrder),
                                          Name = this.TypeName,
                                          Description = this.Description,
                                          ModuleUIdAutoCollect = this.AllModuleNamesAndUIds.Values.FirstOrDefault(),
                                          ParentUId = Guid.Empty,
                                          ModifiedBy = this.userContext.CurrentUser.Email,
                                          IsRelational = false,
                                          IsKeyValue = false,
                                          IsInMemory = false,
                                          IsGraph = false,
                                          IsDocument = false,
                                          IsLedger = false,
                                          IsTimeSeries = false,
                                          IsSearch = false
                                      };

            if (string.IsNullOrWhiteSpace(this.SelectedTypeFunctionName))
            {
                failureMessages.AppendLine("Please select a type function");
            }
            else
            { 
                createdTypeItem.TypeFunctionUId = this.AllTypeFunctionNamesAndUIds[this.SelectedTypeFunctionName];
            }

            if (string.IsNullOrWhiteSpace(this.SelectedTypeUnitName))
            {
                failureMessages.AppendLine("Please select a type unit");
            }
            else
            {
                createdTypeItem.TypeUnitUId = this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitName];
            }

            if (this.IsAutoCollect)
            {
                if (string.IsNullOrWhiteSpace(this.SelectedModuleAutoCollectName))
                {
                    failureMessages.AppendLine("Please select a module to auto collect from");
                }
                else
                {
                    createdTypeItem.ModuleUIdAutoCollect =
                        this.AllModuleNamesAndUIds[this.SelectedModuleAutoCollectName];
                }
            }

            if (this.HasParent)
            {
                if (string.IsNullOrWhiteSpace(this.SelectedTypeItemName))
                {
                    failureMessages.AppendLine("Please select a parent type");
                }
                else
                {
                    createdTypeItem.ParentUId = this.AllTypeItemNamesAndUIds[this.SelectedTypeItemName];
                }
            }

            switch (this.SelectedDataStoreType)
            {
                case "Relational":
                    createdTypeItem.IsRelational = true;
                    break;
                case "Key value":
                    createdTypeItem.IsKeyValue = true;
                    break;
                case "In memory":
                    createdTypeItem.IsInMemory = true;
                    break;
                case "Graph":
                    createdTypeItem.IsGraph = true;
                    break;
                case "Document":
                    createdTypeItem.IsDocument = true;
                    break;
                case "Ledger":
                    createdTypeItem.IsLedger = true;
                    break;
                case "Time series":
                    createdTypeItem.IsTimeSeries = true;
                    break;
                case "Search":
                    createdTypeItem.IsSearch = true;
                    break;
            }

            var typeItemValidator = new TypeItemValidator();
            ValidationResult validationResult = typeItemValidator.Validate(createdTypeItem);
            if (!validationResult.IsValid || failureMessages.Length != 0)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    failureMessages.AppendLine(
                        $"Property ({failure.PropertyName}) failed validation with error ({failure.ErrorMessage}).");
                }

                this.HasErrors = true;
                this.Errors = failureMessages.ToString();
                return;
            }

            this.HasErrors = false;
            this.Errors = string.Empty;
            this.syntosaDal.CreateTypeItem(createdTypeItem);
            createdTypeItem = this.syntosaDal.GetTypeItemByAny(
                typeItemName: this.TypeName,
                typeItemDesc: this.Description,
                isActive: this.IsActive,
                isBuiltIn: this.IsBuiltIn,
                isAssignable: this.IsAssignable).FirstOrDefault();

            // this.userActivityService.InsertActivity(
            //    this.userContext.CurrentUser,
            //    "Type Item Inserted",
            //    $"{this.userContext.CurrentUser.Email} has inserted the type item named {createdTypeItem.Name} with UId {createdTypeItem.UId}");
        }

        /// <summary>
        /// Gets all domains names and UIds in the Syntosa database.
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

        /// <summary>
        /// Gets all type function names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All type function names and UIds in the Syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllTypeItemNamesAndUIds()
        {
            var typeItems = this.syntosaDal.GetTypeItemByAny(isAssignable: true);
            var typeItemNamesUIds = new Dictionary<string, Guid>();
            foreach (var typeItem in typeItems)
            {
                if (string.Equals(typeItem.TypeFunctionName, "EdgeLabels"))
                {
                    typeItem.Name = typeItem.Name + "(Edge Label)";
                }

                typeItemNamesUIds.Add(typeItem.Name, typeItem.UId);
            }

            return typeItemNamesUIds;
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