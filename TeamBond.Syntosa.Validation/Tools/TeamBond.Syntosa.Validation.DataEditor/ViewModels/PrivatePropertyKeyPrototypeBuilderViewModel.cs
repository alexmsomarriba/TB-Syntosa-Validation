namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;
    using System.Text;

    using FluentValidation.Results;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;
    using TeamBond.Syntosa.Validation.DataEditor.Validators;

    /// <summary>
    /// The private property prototype builder view model.
    /// </summary>
    public class PrivatePropertyKeyPrototypeBuilderViewModel : ViewModelBase
    {
        /// <summary>
        /// The syntosa dal.
        /// </summary>
        private readonly SyntosaDal syntosaDal;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// The is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The is auto collect.
        /// </summary>
        private bool isAutoCollect;

        /// <summary>
        /// The private property name.
        /// </summary>
        private string privatePropertyKeyName;

        /// <summary>
        /// The selected element name.
        /// </summary>
        private string selectedElementName;

        /// <summary>
        /// The selected module auto collect u id.
        /// </summary>
        private string selectedModuleAutoCollectName;

        /// <summary>
        /// The type key name.
        /// </summary>
        private string selectedTypeKeyName;

        /// <summary>
        /// The type unit name.
        /// </summary>
        private string selectedTypeUnitName;

        /// <summary>
        /// The type value name.
        /// </summary>
        private string selectedTypeValueName;

        /// <summary>
        /// The sort order.
        /// </summary>
        private int sortOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivatePropertyKeyPrototypeBuilderViewModel" /> class.
        /// </summary>
        public PrivatePropertyKeyPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();

            this.InsertPrivatePropertyKey = ReactiveCommand.Create(this.CreatePrivatePropertyKey);
        }

        /// <summary>
        /// Gets the all element names.
        /// </summary>
        public List<string> AllElementNames
        {
            get
            {
                var elementNames = new List<string>();
                foreach (var element in this.AllElementNamesAndUIds)
                {
                    elementNames.Add(element.Key);
                }

                return elementNames;
            }
        }

        /// <summary>
        /// Gets or sets the all element names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllElementNamesAndUIds
        {
            get => this.GetAllElementNamesAndUIds();
            set => this.GetAllElementNamesAndUIds();
        }

        /// <summary>
        /// Gets the all module names.
        /// </summary>
        public List<string> AllModuleNames
        {
            get
            {
                var elementNames = new List<string>();
                foreach (var module in this.AllModuleNamesAndUIds)
                {
                    elementNames.Add(module.Key);
                }

                return elementNames;
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
        /// Gets the all type item names.
        /// </summary>
        public List<string> AllTypeItemNames
        {
            get
            {
                var typeItemNames = new List<string>();
                foreach (var typeItem in this.AllTypeItemNamesAndUIds)
                {
                    typeItemNames.Add(typeItem.Key);
                }

                return typeItemNames;
            }
        }

        /// <summary>
        /// Gets or sets the all type item names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeItemNamesAndUIds
        {
            get => this.GetAllTypeItemNamesAndUIds();
            set => this.GetAllTypeItemNamesAndUIds();
        }

        /// <summary>
        /// Gets the all type unit names.
        /// </summary>
        public List<string> AllTypeUnitNames
        {
            get
            {
                var typeUnitNames = new List<string>();
                foreach (var typeUnit in this.AllTypeUnitNamesAndUIds)
                {
                    typeUnitNames.Add(typeUnit.Key);
                }

                return typeUnitNames;
            }
        }

        /// <summary>
        /// Gets or sets the all type unit names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeUnitNamesAndUIds
        {
            get => this.GetAllTypeUnitNamesAndUIds();
            set => this.GetAllTypeUnitNamesAndUIds();
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
        /// Gets or sets a value indicating whether is auto collect.
        /// </summary>
        public bool IsAutoCollect
        {
            get => this.isAutoCollect;
            set => this.RaiseAndSetIfChanged(ref this.isAutoCollect, value);
        }

        /// <summary>
        /// Gets the insert private property key.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertPrivatePropertyKey { get; }

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
        /// Gets or sets the private property key name.
        /// </summary>
        public string PrivatePropertyKeyName
        {
            get => this.privatePropertyKeyName;
            set => this.RaiseAndSetIfChanged(ref this.privatePropertyKeyName, this.privatePropertyKeyName);
        }

        /// <summary>
        /// Gets or sets the selected element name.
        /// </summary>
        public string SelectedElementName
        {
            get => this.selectedElementName;
            set => this.RaiseAndSetIfChanged(ref this.selectedElementName, value);
        }

        /// <summary>
        /// Gets or sets the selected module auto collect name.
        /// </summary>
        public string SelectedModuleAutoCollectName
        {
            get => this.selectedModuleAutoCollectName;
            set => this.RaiseAndSetIfChanged(ref this.selectedModuleAutoCollectName, value);
        }

        /// <summary>
        /// Gets or sets the selected type key name.
        /// </summary>
        public string SelectedTypeKeyName
        {
            get => this.selectedTypeKeyName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeKeyName, value);
        }

        /// <summary>
        /// Gets or sets the selected type unit name.
        /// </summary>
        public string SelectedTypeUnitName
        {
            get => this.selectedTypeUnitName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeUnitName, value);
        }

        /// <summary>
        /// Gets or sets the selected type value name.
        /// </summary>
        public string SelectedTypeValueName
        {
            get => this.selectedTypeValueName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeValueName, value);
        }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public int SortOrder
        {
            get => this.sortOrder;
            set => this.RaiseAndSetIfChanged(ref this.sortOrder, value);
        }

        /// <summary>
        /// The create private property key.
        /// </summary>
        private void CreatePrivatePropertyKey()
        {
            var failureMessage = new StringBuilder();
            var createdPrivatePropertyKey = new ElementPrivatePropertyKey
            {
                Name = this.PrivatePropertyKeyName,
                IsActive = this.IsActive,
                ElementUId = this.AllElementNamesAndUIds[this.SelectedElementName],
                TypeKeyUId = this.AllTypeItemNamesAndUIds[this.SelectedTypeKeyName],
                TypeValueUId = this.AllTypeItemNamesAndUIds[this.SelectedTypeValueName],
                TypeUnitUId = this.AllTypeUnitNamesAndUIds[this.SelectedTypeUnitName],
                IsAutoCollect = this.IsAutoCollect,
                SortOrder = this.SortOrder,
                ModifiedBy = "alex@teambond.io"
            };

            if (this.IsAutoCollect)
            {
                createdPrivatePropertyKey.ModuleUIdAutoCollect =
                    this.AllModuleNamesAndUIds[this.SelectedModuleAutoCollectName];
            }

            var privatePropertyKeyValidator = new PrivatePropertyKeyValidator();
            ValidationResult validationResult = privatePropertyKeyValidator.Validate(createdPrivatePropertyKey);
            if (!validationResult.IsValid)
            {
                foreach (var validationFailure in validationResult.Errors)
                {
                    failureMessage.AppendLine(
                        $"Property {validationFailure.PropertyName} has failed validation with error {validationFailure.ErrorMessage}");
                }

                this.HasErrors = true;
                this.Errors = failureMessage.ToString();
                return;
            }

            this.HasErrors = false;
            this.Errors = string.Empty;
            this.syntosaDal.CreateElementPrivatePropertyKey(createdPrivatePropertyKey);
        }

        /// <summary>
        /// The get all element names and u ids.
        /// </summary>
        /// <returns>
        /// All elements names and UIds
        /// </returns>
        private Dictionary<string, Guid> GetAllElementNamesAndUIds()
        {
            List<Element> elements = this.syntosaDal.GetElementByAny();
            var elementNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var element in elements)
            {
                elementNamesAndUIds.Add(element.Name, element.UId);
            }

            return elementNamesAndUIds;
        }

        /// <summary>
        /// The get all module names and u ids.
        /// </summary>
        /// <returns>
        /// All module names and u ids.
        /// </returns>
        private Dictionary<string, Guid> GetAllModuleNamesAndUIds()
        {
            List<Module> modules = this.syntosaDal.GetModuleByAny();
            var moduleNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var module in modules)
            {
                moduleNamesAndUIds.Add(module.Name, module.UId);
            }

            return moduleNamesAndUIds;
        }

        /// <summary>
        /// The get all type item names and u ids.
        /// </summary>
        /// <returns>
        /// All type item names and u ids.
        /// </returns>
        private Dictionary<string, Guid> GetAllTypeItemNamesAndUIds()
        {
            List<TypeItem> typeItems = this.syntosaDal.GetTypeItemByAny();
            var typeItemNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var typeItem in typeItems)
            {
                typeItemNamesAndUIds.Add(typeItem.Name, typeItem.UId);
            }

            return typeItemNamesAndUIds;
        }

        /// <summary>
        /// The get all type unit names and u ids.
        /// </summary>
        /// <returns>
        /// All type unit names and u ids.
        /// </returns>
        private Dictionary<string, Guid> GetAllTypeUnitNamesAndUIds()
        {
            List<TypeUnit> typeUnits = this.syntosaDal.GetTypeUnitByAny();
            var typeUnitNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var typeUnit in typeUnits)
            {
                typeUnitNamesAndUIds.Add(typeUnit.Name, typeUnit.UId);
            }

            return typeUnitNamesAndUIds;
        }
    }
}