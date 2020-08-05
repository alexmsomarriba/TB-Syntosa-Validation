namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using ReactiveUI;

    using TeamBond.Core.Engine;

    /// <summary>
    /// The prototype editor view model.
    /// </summary>
    public class PrototypeEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// The syntosa dal.
        /// </summary>
        private readonly SyntosaDal syntosaDal;

        /// <summary>
        /// The all data object names.
        /// </summary>
        private List<string> allDataObjectNames;

        /// <summary>
        /// The can be active.
        /// </summary>
        private bool canBeActive;

        /// <summary>
        /// The can be assigned.
        /// </summary>
        private bool canBeAssigned;

        /// <summary>
        /// The can be auto collect.
        /// </summary>
        private bool canBeAutoCollect;

        /// <summary>
        /// The can be built in.
        /// </summary>
        private bool canBeBuiltIn;

        /// <summary>
        /// The can be notified.
        /// </summary>
        private bool canBeNotified;

        /// <summary>
        /// The current description.
        /// </summary>
        private string currentDescription;

        /// <summary>
        /// The current name.
        /// </summary>
        private string currentName;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// The has description.
        /// </summary>
        private bool hasDescription;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// A value indicating whether the type  auto collect.
        /// </summary>
        private bool hasModule;

        /// <summary>
        /// The has name.
        /// </summary>
        private bool hasName;

        /// <summary>
        /// The has type function.
        /// </summary>
        private bool hasTypeFunction;

        /// <summary>
        /// A value indicating whether this type has a parent.
        /// </summary>
        private bool hasTypeItem;

        /// <summary>
        /// The has type unit.
        /// </summary>
        private bool hasTypeUnit;

        /// <summary>
        /// A value indicating whether the type is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// A value indicating whether the type is assignable.
        /// </summary>
        private bool isAssignable;

        /// <summary>
        /// A value indicating whether the type is built in.
        /// </summary>
        private bool isBuiltIn;

        /// <summary>
        /// A value indicating whether the type is notifiable.
        /// </summary>
        private bool isNotifiable;

        /// <summary>
        /// The description of the type.
        /// </summary>
        private string newDescription;

        /// <summary>
        /// The type name.
        /// </summary>
        private string newName;

        /// <summary>
        /// The selected data object name.
        /// </summary>
        private string selectedDataObjectName;

        /// <summary>
        /// The selected data object type.
        /// </summary>
        private string selectedDataObjectType;

        /// <summary>
        /// The selected data store type.
        /// </summary>
        private string selectedDataStoreType;

        /// <summary>
        /// The module auto collect name.
        /// </summary>
        private string selectedModuleName;

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
        /// Initializes a new instance of the <see cref="PrototypeEditorViewModel" /> class.
        /// </summary>
        public PrototypeEditorViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
        }

        /// <summary>
        /// Gets or sets the all data object names.
        /// </summary>
        public List<string> AllDataObjectNames
        {
            get => this.allDataObjectNames;
            set => this.RaiseAndSetIfChanged(ref this.allDataObjectNames, value);
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
        /// Gets or sets a value indicating whether can be active.
        /// </summary>
        public bool CanBeActive
        {
            get => this.canBeActive;
            set => this.RaiseAndSetIfChanged(ref this.canBeActive, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether can be assigned.
        /// </summary>
        public bool CanBeAssigned
        {
            get => this.canBeAssigned;
            set => this.RaiseAndSetIfChanged(ref this.canBeAssigned, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether can be auto collect.
        /// </summary>
        public bool CanBeAutoCollect
        {
            get => this.canBeAutoCollect;
            set => this.RaiseAndSetIfChanged(ref this.canBeAutoCollect, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether can be built in.
        /// </summary>
        public bool CanBeBuiltIn
        {
            get => this.canBeBuiltIn;
            set => this.RaiseAndSetIfChanged(ref this.canBeBuiltIn, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether can be notified.
        /// </summary>
        public bool CanBeNotified
        {
            get => this.canBeNotified;
            set => this.RaiseAndSetIfChanged(ref this.canBeNotified, value);
        }

        /// <summary>
        /// Gets or sets the current description.
        /// </summary>
        public string CurrentDescription
        {
            get => this.currentDescription;
            set => this.RaiseAndSetIfChanged(ref this.currentDescription, value);
        }

        /// <summary>
        /// Gets or sets the current name.
        /// </summary>
        public string CurrentName
        {
            get => this.currentName;
            set => this.RaiseAndSetIfChanged(ref this.currentName, value);
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
        /// The data object types.
        /// </summary>
        public List<string> DataObjectTypes => new List<string> { "Module", "Type unit", "Type function", "Type item" };

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public string Errors
        {
            get => this.errors;
            set => this.RaiseAndSetIfChanged(ref this.errors, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has description.
        /// </summary>
        public bool HasDescription
        {
            get => this.hasDescription;
            set => this.RaiseAndSetIfChanged(ref this.hasDescription, value);
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
        /// Gets or sets a value indicating whether has module.
        /// </summary>
        public bool HasModule
        {
            get => this.hasModule;
            set => this.RaiseAndSetIfChanged(ref this.hasModule, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has name.
        /// </summary>
        public bool HasName
        {
            get => this.hasName;
            set => this.RaiseAndSetIfChanged(ref this.hasName, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has type function.
        /// </summary>
        public bool HasTypeFunction
        {
            get => this.hasTypeFunction;
            set => this.RaiseAndSetIfChanged(ref this.hasTypeFunction, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has type item.
        /// </summary>
        public bool HasTypeItem
        {
            get => this.hasTypeItem;
            set => this.RaiseAndSetIfChanged(ref this.hasTypeItem, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has type unit.
        /// </summary>
        public bool HasTypeUnit
        {
            get => this.hasTypeUnit;
            set => this.RaiseAndSetIfChanged(ref this.hasTypeUnit, value);
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
        /// Gets or sets a value indicating whether is assignable.
        /// </summary>
        public bool IsAssignable
        {
            get => this.isAssignable;
            set => this.RaiseAndSetIfChanged(ref this.isAssignable, value);
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
        /// Gets or sets the new description.
        /// </summary>
        public string NewDescription
        {
            get => this.newDescription;
            set => this.RaiseAndSetIfChanged(ref this.newDescription, value);
        }

        /// <summary>
        /// Gets or sets the new name.
        /// </summary>
        public string NewName
        {
            get => this.newName;
            set => this.RaiseAndSetIfChanged(ref this.newName, value);
        }

        /// <summary>
        /// Gets the next.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Next { get; }

        /// <summary>
        /// Gets or sets the selected data object name.
        /// </summary>
        public string SelectedDataObjectName
        {
            get => this.selectedDataObjectName;
            set => this.RaiseAndSetIfChanged(ref this.selectedDataObjectName, value);
        }

        /// <summary>
        /// Gets or sets the selected data object type.
        /// </summary>
        public string SelectedDataObjectType
        {
            get => this.selectedDataObjectType;
            set => this.RaiseAndSetIfChanged(ref this.selectedDataObjectType, value);
        }

        /// <summary>
        /// Gets or sets the selected data store type.
        /// </summary>
        public string SelectedDataStoreType
        {
            get => this.selectedDataStoreType;
            set => this.RaiseAndSetIfChanged(ref this.selectedDataStoreType, value);
        }

        /// <summary>
        /// Gets or sets the selected module name.
        /// </summary>
        public string SelectedModuleName
        {
            get => this.selectedModuleName;
            set => this.RaiseAndSetIfChanged(ref this.selectedModuleName, value);
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
        /// Gets the update data object.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateDataObject { get; }

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

        /// <summary>
        /// The retrieve info.
        /// </summary>
        private void RetrieveInfo()
        {
            if (!string.IsNullOrWhiteSpace(this.SelectedDataObjectType)
                && string.IsNullOrWhiteSpace(this.SelectedDataObjectName))
            {
                switch (this.SelectedDataObjectType)
                {
                    case "Module":
                        this.AllDataObjectNames = this.AllModuleNames;
                        break;
                    case "Type unit":
                        this.AllDataObjectNames = this.AllTypeUnitNames;
                        break;
                    case "Type function":
                        this.AllDataObjectNames = this.AllTypeFunctionNames;
                        break;
                    case "Type item":
                        this.AllDataObjectNames = this.AllTypeItemNames;
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(this.SelectedDataObjectName))
            {
                switch (this.SelectedDataObjectType)
                {
                    case "Module":
                        Module moduleToEdit = this.syntosaDal.GetModuleByAny(
                            moduleName: this.SelectedDataObjectName,
                            moduleUId: this.AllModuleNamesAndUIds[this.SelectedDataObjectName]).FirstOrDefault();
                        
                        this.HasName = true;
                        this.HasDescription = true;
                        this.CanBeBuiltIn = true;
                        this.CanBeActive = true;

                        this.CurrentName = moduleToEdit.Name;
                        this.CurrentDescription = moduleToEdit.Description;
                        this.IsActive = moduleToEdit.IsActive;
                        this.IsBuiltIn = moduleToEdit.IsBuiltIn;
                        break;
                    case "Type unit":
                        TypeUnit typeUnitToEdit = this.syntosaDal.GetTypeUnitByAny(
                            typeUnitName: this.SelectedDataObjectName,
                            typeUnitUId: this.AllTypeItemNamesAndUIds[this.SelectedDataObjectName]).FirstOrDefault();

                        this.HasName = true;
                        this.HasDescription = true;
                        this.HasModule = true;
                        this.CanBeBuiltIn = true;
                        this.CanBeActive = true;

                        this.CurrentName = typeUnitToEdit.Name;
                        this.CurrentDescription = typeUnitToEdit.Description;
                        this.SelectedModuleName = typeUnitToEdit.ModuleName;
                        this.IsBuiltIn = typeUnitToEdit.IsBuiltIn;
                        this.IsActive = typeUnitToEdit.IsActive;
                        break;
                    case "Type function":
                        TypeFunction typeFunctionToEdit = this.syntosaDal.GetTypeFunctionByAny(
                                typeFunctionName: this.SelectedDataObjectName,
                                typeFunctionUId: this.AllTypeFunctionNamesAndUIds[this.SelectedDataObjectName])
                            .FirstOrDefault();

                        this.HasName = true;
                        this.HasDescription = true;
                        this.HasModule = true;
                        this.CanBeBuiltIn = true;
                        this.CanBeActive = true;

                        this.CurrentName = typeFunctionToEdit.Name;
                        this.CurrentDescription = typeFunctionToEdit.Description;
                        this.SelectedModuleName = this.AllModuleNamesAndUIds
                            .FirstOrDefault(x => x.Value == typeFunctionToEdit.ModuleUId).Key;
                        this.IsBuiltIn = typeFunctionToEdit.IsBuiltIn;
                        this.IsActive = typeFunctionToEdit.IsActive;
                        break;
                    case "Type item":
                        TypeItem typeItemToEdit = this.syntosaDal.GetTypeItemByAny(
                                typeItemName: this.SelectedDataObjectName,
                                typeItemUId: this.AllTypeFunctionNamesAndUIds[this.SelectedDataObjectName])
                            .FirstOrDefault();

                        this.HasName = true;
                        this.HasDescription = true;
                        this.HasTypeUnit = true;
                        this.HasTypeFunction = true;
                        this.CanBeBuiltIn = true;
                        this.CanBeActive = true;
                        this.CanBeAssigned = true;
                        this.CanBeNotified = true;
                        this.CanBeAutoCollect = true;

                        this.CurrentName = typeItemToEdit.Name;
                        this.CurrentDescription = typeItemToEdit.Description;
                        this.SelectedTypeUnitName = typeItemToEdit.TypeUnitName;
                        this.SelectedTypeFunctionName = typeItemToEdit.TypeFunctionName;
                        this.IsAssignable = typeItemToEdit.IsAssignable;
                        this.IsBuiltIn = typeItemToEdit.IsBuiltIn;
                        break;
                }
            }
        }
    }
}