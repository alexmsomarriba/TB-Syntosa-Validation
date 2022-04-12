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
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;
    using TeamBond.Services.Users;
    using TeamBond.Syntosa.Validation.DataEditor.Validators;

    /// <summary>
    /// The type proto type builder view model.
    /// </summary>
    public class ElementPrototypeBuilderViewModel : ViewModelBase
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
        /// The element alias.
        /// </summary>
        private string elementAlias;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// A value indicating whether the type is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// A value indicating whether the type  auto collect.
        /// </summary>
        private bool isAutoCollect;

        /// <summary>
        /// A value indicating whether the type is built in.
        /// </summary>
        private bool isBuiltIn;

        /// <summary>
        /// The module auto collect name.
        /// </summary>
        private string selectedModuleName;

        /// <summary>
        /// The selected domain name.
        /// </summary>
        private string selectedDomainName;

        /// <summary>
        /// The parent type item name.
        /// </summary>
        private string selectedTypeItemName;

        /// <summary>
        /// The selected type record u id name.
        /// </summary>
        private string selectedTypeRecordName;

        /// <summary>
        /// The selected element parent name.
        /// </summary>
        private string selectedParentElementName;

        /// <summary>
        /// The sort order.
        /// </summary>
        private string sortOrder;

        /// <summary>
        /// The type name.
        /// </summary>
        private string elementName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementPrototypeBuilderViewModel"/> class.
        /// </summary>
        public ElementPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userActivityService = TeamBondEngineContext.Current.Resolve<IUserActivityService>();

            this.InsertElement = ReactiveCommand.Create(this.BuildElement);
        }

        /// <summary>
        /// Gets or sets the current user identifier.
        /// </summary>
        public string CurrentUserIdentifier { get; set; }

        /// <summary>
        /// Gets the all domain names.
        /// </summary>
        public List<string> AllDomainNames
        {
            get
            {
                var domainNames = new List<string>();
                foreach (var name in this.AllDomainNamesAndUIds.Keys)
                {
                    domainNames.Add(name);
                }

                return domainNames;
            }
        }

        /// <summary>
        /// Gets or sets the all domain names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllDomainNamesAndUIds
        {
            get => this.GetAllDomainNamesAndUIds();
            set => this.GetAllDomainNamesAndUIds();
        }

        /// <summary>
        /// Gets the all element names.
        /// </summary>
        public List<string> AllElementNames
        {
            get
            {
                var elementNames = new List<string>();
                foreach (var name in this.AllElementNamesAndUIds.Keys)
                {
                    elementNames.Add(name);
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
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get => this.description;
            set => this.RaiseAndSetIfChanged(ref this.description, value);
        }

        /// <summary>
        /// Gets or sets the element alias.
        /// </summary>
        public string ElementAlias
        {
            get => this.elementAlias;
            set => this.RaiseAndSetIfChanged(ref this.elementAlias, value);
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
        /// Gets or sets the selected type record name.
        /// </summary>
        public string SelectedTypeRecordName
        {
            get => this.selectedTypeRecordName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeRecordName, value);
        }

        /// <summary>
        /// Gets the insert type.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertElement { get; }

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
        /// Gets or sets a value indicating whether is built in.
        /// </summary>
        public bool IsBuiltIn
        {
            get => this.isBuiltIn;
            set => this.RaiseAndSetIfChanged(ref this.isBuiltIn, value);
        }

        /// <summary>
        /// The is user admin.
        /// </summary>
        public bool IsUserAdmin => true;

        /// <summary>
        /// Gets or sets the selected domain name.
        /// </summary>
        public string SelectedDomainName
        {
            get => this.selectedDomainName;
            set => this.RaiseAndSetIfChanged(ref this.selectedDomainName, value);
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
        /// Gets or sets the selected parent element name.
        /// </summary>
        public string SelectedParentElementName
        {
            get => this.selectedParentElementName;
            set => this.RaiseAndSetIfChanged(ref this.selectedParentElementName, value);
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
        /// Gets or sets the sort order.
        /// </summary>
        public string SortOrder
        {
            get => this.sortOrder;
            set => this.RaiseAndSetIfChanged(ref this.sortOrder, value);
        }

        /// <summary>
        /// Gets or sets the element name.
        /// </summary>
        public string ElementName
        {
            get => this.elementName;
            set => this.RaiseAndSetIfChanged(ref this.elementName, value);
        }

        /// <summary>
        /// The build type item.
        /// </summary>
        private void BuildElement()
        {
            var failureMessages = new StringBuilder();
            var createdElement = new Element
            {
                DomainUId = this.AllDomainNamesAndUIds[this.SelectedDomainName],
                Alias = this.ElementAlias,
                IsActive = this.IsActive,
                IsBuiltIn = this.IsBuiltIn,
                TypeItemUId = this.AllTypeItemNamesAndUIds[this.SelectedTypeItemName],
                TypeUIdRecordStatus = this.AllTypeItemNamesAndUIds[this.SelectedTypeRecordName],
                ModuleUId = this.AllModuleNamesAndUIds[this.SelectedModuleName],
                ModuleRecordKey = string.Empty,
                IsAutoCollect = this.IsAutoCollect,
                Name = this.ElementName,
                Description = this.Description,
                ParentUId = Guid.Empty,
                ModifiedBy = "alex@teambond.io"
            };

            if (this.HasParent)
            {
                if (string.IsNullOrWhiteSpace(this.SelectedParentElementName))
                {
                    failureMessages.AppendLine("Please select a parent element");
                }
                else
                {
                    createdElement.ParentUId = this.AllTypeItemNamesAndUIds[this.SelectedParentElementName];
                }
            }

            var elementValidator = new ElementValidator();
            ValidationResult validationResult = elementValidator.Validate(createdElement);
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
            this.syntosaDal.CreateElement(createdElement);
            createdElement = this.syntosaDal.GetElementByAny(
                elementName: this.ElementName,
                elementDesc: this.Description,
                isActive: this.IsActive, 
                isBuiltIn: this.IsBuiltIn).FirstOrDefault();

            // this.userActivityService.InsertActivity(
            //    this.userContext.CurrentUser,
            //    "Type Item Inserted",
            //    $"{this.userContext.CurrentUser.Email} has inserted the type item named {createdTypeItem.Name} with UId {createdTypeItem.UId}");
        }

        /// <summary>
        /// Gets all domain names and UIds in the syntosa database.
        /// </summary>
        /// <returns>
        /// All the domain names and UIds in the syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllDomainNamesAndUIds()
        {
            var domains = this.syntosaDal.GetDomainByAny();
            var domainNamesUIds = new Dictionary<string, Guid>();
            foreach (var domain in domains)
            {
                domainNamesUIds.Add(domain.Name, domain.UId);
            }

            return domainNamesUIds;
        }

        /// <summary>
        /// Gets all element names and UIds in the syntosa database.
        /// </summary>
        /// <returns>
        /// All element names and UIds in the syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllElementNamesAndUIds()
        {
            var elements = this.syntosaDal.GetElementByAny();
            var elementNamesUIds = new Dictionary<string, Guid>();
            foreach (var element in elements)
            {
                elementNamesUIds.Add(element.Name, element.UId);
            }

            return elementNamesUIds;
        }

        /// <summary>
        /// Gets all modules names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All modules names and UIds in the Syntosa database.
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
        private Dictionary<string, Guid> GetAllTypeItemNamesAndUIds()
        {
            var typeItems = this.syntosaDal.GetTypeItemByAny();
            var typeItemNamesUIds = new Dictionary<string, Guid>();
            foreach (var typeFunction in typeItems)
            {
                typeItemNamesUIds.Add(typeFunction.Name, typeFunction.UId);
            }

            return typeItemNamesUIds;
        }
    }
}