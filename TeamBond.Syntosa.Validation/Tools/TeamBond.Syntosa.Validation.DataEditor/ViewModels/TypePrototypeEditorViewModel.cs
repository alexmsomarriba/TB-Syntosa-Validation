﻿namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
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
    /// The type prototype editor view model.
    /// </summary>
    public class TypePrototypeEditorViewModel : ViewModelBase
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
        private string newTypeItemDescription;

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
        /// The name of the selected type item to edit.
        /// </summary>
        private string selectedTypeItemName;

        /// <summary>
        /// The current name of the type item to edit.
        /// </summary>
        private string currentName;

        /// <summary>
        /// The current description of the type item to edit.
        /// </summary>
        private string currentDescription;

        /// <summary>
        /// The parent type item name.
        /// </summary>
        private string selectedTypeItemParentName;

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
        private string newTypeItemName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypePrototypeEditorViewModel"/> class.
        /// </summary>
        public TypePrototypeEditorViewModel()
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
        /// Gets or sets the name of the selected type item to edit.
        /// </summary>
        public string SelectedTypeItemName
        {
            get => this.selectedTypeItemName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeItemName, value);
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
            set => value = this.GetAllTypeFunctionNamesAndUIds();
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
            set => value = this.GetAllTypeItemNamesAndUIds();
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
            set => value = this.GetAllTypeUnitNamesAndUIds();
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
        public string NewTypeItemDescription
        {
            get => this.newTypeItemDescription;
            set => this.RaiseAndSetIfChanged(ref this.newTypeItemDescription, value);
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
        /// Gets or sets the current name of the type item to edit.
        /// </summary>
        public string CurrentName
        {
            get => this.currentName;
            set => this.RaiseAndSetIfChanged(ref this.currentName, value);
        }

        /// <summary>
        /// Gets or sets the current description of the type item to edit.
        /// </summary>
        public string CurrentDescription
        {
            get => this.currentDescription;
            set => this.RaiseAndSetIfChanged(ref this.currentDescription, value);
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
        public string SelectedTypeItemParentName
        {
            get => this.selectedTypeItemParentName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeItemParentName, value);
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
        public string NewTypeItemName
        {
            get => this.newTypeItemName;
            set => this.RaiseAndSetIfChanged(ref this.newTypeItemName, value);
        }

        /// <summary>
        /// The retrieve info.
        /// </summary>
        private void RetrieveInfo()
        {
            var typeItemToEdit = this.syntosaDal.GetTypeItemByAny(
                                typeItemName: this.SelectedTypeItemName,
                                typeItemUId: this.AllTypeFunctionNamesAndUIds[this.SelectedTypeItemName])
                            .FirstOrDefault();

            this.CurrentName = typeItemToEdit.Name;
            this.CurrentDescription = typeItemToEdit.Description;
            this.SelectedTypeUnitName = typeItemToEdit.TypeUnitName;
            this.SelectedTypeFunctionName = typeItemToEdit.TypeFunctionName;
            this.IsAssignable = typeItemToEdit.IsAssignable;
            this.IsBuiltIn = typeItemToEdit.IsBuiltIn;
            this.IsNotifiable = typeItemToEdit.IsNotifiable;
            this.IsActive = typeItemToEdit.IsActive;
            this.IsAutoCollect = typeItemToEdit.IsAutoCollect;

            if (typeItemToEdit.ParentUId != Guid.Empty)
            {
                this.HasParent = true;
                this.SelectedTypeItemParentName = typeItemToEdit.ParentName;
            }

            if (typeItemToEdit.IsRelational)
            {
                this.SelectedDataStoreType = "Relational";
            }

            if (typeItemToEdit.IsKeyValue)
            {
                this.SelectedDataStoreType = "Key value";
            }

            if (typeItemToEdit.IsInMemory)
            {
                this.SelectedDataStoreType = "In memory";
            }

            if (typeItemToEdit.IsGraph)
            {
                this.SelectedDataStoreType = "Graph";
            }

            if (typeItemToEdit.IsDocument)
            {
                this.SelectedDataStoreType = "Document";
            }

            if (typeItemToEdit.IsLedger)
            {
                this.SelectedDataStoreType = "Ledger";
            }

            if (typeItemToEdit.IsTimeSeries)
            {
                this.SelectedDataStoreType = "Time series";
            }

            if (typeItemToEdit.IsSearch)
            {
                this.SelectedDataStoreType = "Search";
            }
        }

        /// <summary>
        /// The build type item.
        /// </summary>
        private void BuildTypeItem()
        {
            var failureMessages = new StringBuilder();
            bool hasChanged = false;
            TypeItem updatedTypeItem = this.syntosaDal.GetTypeItemByAny(
                typeItemName: this.SelectedTypeItemName,
                typeItemUId: this.AllTypeItemNamesAndUIds[this.SelectedTypeItemName]).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(this.NewTypeItemName))
            {
                updatedTypeItem.Name = this.NewTypeItemName;
                hasChanged = true;
            }

            if (!string.IsNullOrWhiteSpace(this.NewTypeItemDescription))
            {
                updatedTypeItem.Description = this.NewTypeItemDescription;
                hasChanged = true;
            }

            if (!this.SelectedTypeUnitName.Equals(updatedTypeItem.TypeUnitName))
            {
                updatedTypeItem.TypeUnitUId = this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitName];
                hasChanged = true;
            }

            if (!this.SelectedTypeFunctionName.Equals(updatedTypeItem.TypeFunctionName))
            {
                updatedTypeItem.TypeFunctionUId = this.AllTypeFunctionNamesAndUIds[this.SelectedTypeFunctionName];
                hasChanged = true;
            }

            string currentModuleName = this.AllModuleNamesAndUIds
                .FirstOrDefault(x => x.Value == updatedTypeItem.ModuleUIdAutoCollect).Key;

            if (!this.SelectedModuleAutoCollectName.Equals(currentModuleName))
            {
                updatedTypeItem.ModuleUIdAutoCollect = this.AllModuleNamesAndUIds[this.SelectedModuleAutoCollectName];
                hasChanged = true;
            }

            if (this.SelectedDataStoreType.Equals("Relational") && !updatedTypeItem.IsRelational)
            {
                updatedTypeItem.IsRelational = true;
                updatedTypeItem.IsKeyValue = false;
                updatedTypeItem.IsInMemory = false;
                updatedTypeItem.IsGraph = false;
                updatedTypeItem.IsDocument = false;
                updatedTypeItem.IsLedger = false;
                updatedTypeItem.IsTimeSeries = false;
                updatedTypeItem.IsSearch = false;
                hasChanged = true;
            }

            if (this.SelectedDataStoreType.Equals("Key value") && !updatedTypeItem.IsKeyValue)
            {
                updatedTypeItem.IsRelational = false;
                updatedTypeItem.IsKeyValue = true;
                updatedTypeItem.IsInMemory = false;
                updatedTypeItem.IsGraph = false;
                updatedTypeItem.IsDocument = false;
                updatedTypeItem.IsLedger = false;
                updatedTypeItem.IsTimeSeries = false;
                updatedTypeItem.IsSearch = false;
                hasChanged = true;
            }

            if (this.SelectedDataStoreType.Equals("In Memory") && !updatedTypeItem.IsInMemory)
            {
                updatedTypeItem.IsRelational = false;
                updatedTypeItem.IsKeyValue = false;
                updatedTypeItem.IsInMemory = true;
                updatedTypeItem.IsGraph = false;
                updatedTypeItem.IsDocument = false;
                updatedTypeItem.IsLedger = false;
                updatedTypeItem.IsTimeSeries = false;
                updatedTypeItem.IsSearch = false;
                hasChanged = true;
            }

            if (this.SelectedDataStoreType.Equals("Graph") && !updatedTypeItem.IsGraph)
            {
                updatedTypeItem.IsRelational = false;
                updatedTypeItem.IsKeyValue = false;
                updatedTypeItem.IsInMemory = false;
                updatedTypeItem.IsGraph = true;
                updatedTypeItem.IsDocument = false;
                updatedTypeItem.IsLedger = false;
                updatedTypeItem.IsTimeSeries = false;
                updatedTypeItem.IsSearch = false;
                hasChanged = true;
            }

            if (this.SelectedDataStoreType.Equals("Document") && !updatedTypeItem.IsDocument)
            {
                updatedTypeItem.IsRelational = false;
                updatedTypeItem.IsKeyValue = false;
                updatedTypeItem.IsInMemory = false;
                updatedTypeItem.IsGraph = false;
                updatedTypeItem.IsDocument = true;
                updatedTypeItem.IsLedger = false;
                updatedTypeItem.IsTimeSeries = false;
                updatedTypeItem.IsSearch = false;
                hasChanged = true;
            }

            if (this.SelectedDataStoreType.Equals("Ledger") && !updatedTypeItem.IsLedger)
            {
                updatedTypeItem.IsRelational = false;
                updatedTypeItem.IsKeyValue = false;
                updatedTypeItem.IsInMemory = false;
                updatedTypeItem.IsGraph = false;
                updatedTypeItem.IsDocument = false;
                updatedTypeItem.IsLedger = true;
                updatedTypeItem.IsTimeSeries = false;
                updatedTypeItem.IsSearch = false;
                hasChanged = true;
            }

            if (this.SelectedDataStoreType.Equals("Time series") && !updatedTypeItem.IsTimeSeries)
            {
                updatedTypeItem.IsRelational = false;
                updatedTypeItem.IsKeyValue = false;
                updatedTypeItem.IsInMemory = false;
                updatedTypeItem.IsGraph = false;
                updatedTypeItem.IsDocument = false;
                updatedTypeItem.IsLedger = false;
                updatedTypeItem.IsTimeSeries = true;
                updatedTypeItem.IsSearch = false;
                hasChanged = true;
            }

            if (this.SelectedTypeFunctionName.Equals("Search") && !updatedTypeItem.IsSearch)
            {
                updatedTypeItem.IsRelational = false;
                updatedTypeItem.IsKeyValue = false;
                updatedTypeItem.IsInMemory = false;
                updatedTypeItem.IsGraph = false;
                updatedTypeItem.IsDocument = false;
                updatedTypeItem.IsLedger = false;
                updatedTypeItem.IsTimeSeries = false;
                updatedTypeItem.IsSearch = true;
                hasChanged = true;
            }

            string currentParentName = this.AllTypeFunctionNamesAndUIds
                .FirstOrDefault(x => x.Value == updatedTypeItem.ParentUId).Key;

            if (this.HasParent && !this.SelectedTypeItemParentName.Equals(currentParentName))
            {
                updatedTypeItem.ParentUId = this.AllTypeItemNamesAndUIds[this.SelectedTypeItemParentName];
                hasChanged = true;
            }

            var typeItemValidator = new TypeItemValidator();
            ValidationResult validationResult = typeItemValidator.Validate(updatedTypeItem);
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

            if (!hasChanged)
            {
                this.HasErrors = true;
                this.Errors = "The type item has not changed";
                return;
            }

            updatedTypeItem.ModifiedBy = this.userContext.CurrentUser.Email;
            this.HasErrors = false;
            this.Errors = string.Empty;
            this.syntosaDal.UpdateTypeItem(updatedTypeItem);
            this.userActivityService.InsertActivity(this.userContext.CurrentUser, "Type Item Updated", $"{this.userContext.CurrentUser.Email} has updated the type item named {this.CurrentName} with UId {this.AllTypeFunctionNamesAndUIds[this.SelectedTypeItemName]}");
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
            foreach (var typeFunction in typeItems)
            {
                typeItemNamesUIds.Add(typeFunction.Name, typeFunction.UId);
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