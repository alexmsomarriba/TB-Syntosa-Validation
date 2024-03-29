﻿using Syntosa.Core.ObjectModel.CoreClasses;
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
    public class ElementPrototypeEditorViewModel : ViewModelBase
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
        /// The element alias.
        /// </summary>
        private string elementAlias;

        /// <summary>
        /// The type name.
        /// </summary>
        private string elementName;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// A value indicating whether this type has a parent.
        /// </summary>
        private bool hasParent;

        /// <summary>
        /// A value indicating whether the user has selected an element to edit.
        /// </summary>
        private bool hasSelected;

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
        /// The is element selected.
        /// </summary>
        private bool isElementSelected;

        /// <summary>
        /// The selected domain name.
        /// </summary>
        private string selectedDomainName;

        /// <summary>
        /// The selected element to update.
        /// </summary>
        private string selectedElementToUpdateName;

        /// <summary>
        /// The module auto collect name.
        /// </summary>
        private string selectedModuleName;

        /// <summary>
        /// The selected element parent name.
        /// </summary>
        private string selectedParentElementName;

        /// <summary>
        /// The parent type item name.
        /// </summary>
        private string selectedTypeItemName;

        /// <summary>
        /// The selected type record u id name.
        /// </summary>
        private string selectedTypeRecordName;

        /// <summary>
        /// The sort order.
        /// </summary>
        private string sortOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementPrototypeEditorViewModel" /> class.
        /// </summary>
        public ElementPrototypeEditorViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userActivityService = TeamBondEngineContext.Current.Resolve<IUserActivityService>();
            this.IsElementSelected = false;

            this.SelectElement = ReactiveCommand.Create(this.GetElementToUpdate);
            this.InsertElement = ReactiveCommand.Create(this.UpdateElement);
            this.DeleteElement = ReactiveCommand.Create(this.RemoveElement);
        }

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
        /// Gets or sets the current user identifier.
        /// </summary>
        public string CurrentUserIdentifier { get; set; }

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
        /// Gets or sets the element name.
        /// </summary>
        public string ElementName
        {
            get => this.elementName;
            set => this.RaiseAndSetIfChanged(ref this.elementName, value);
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
        /// Gets or sets a value indicating whether the user has selected a type unit to edit.
        /// </summary>
        public bool HasSelected
        {
            get => this.hasSelected;
            set => this.RaiseAndSetIfChanged(ref this.hasSelected, value);
        }

        /// <summary>
        /// Gets the insert type.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertElement { get; }

        /// <summary>
        /// Gets the insert type.
        /// </summary>
        public ReactiveCommand<Unit, Unit> DeleteElement { get; }

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
        /// Gets or sets a value indicating whether is element selected.
        /// </summary>
        public bool IsElementSelected
        {
            get => this.isElementSelected;
            set => this.RaiseAndSetIfChanged(ref this.isElementSelected, value);
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
        /// Gets or sets the selected element to update name.
        /// </summary>
        public string SelectedElementToUpdateName
        {
            get => this.selectedElementToUpdateName;
            set => this.RaiseAndSetIfChanged(ref this.selectedElementToUpdateName, value);
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
        /// Gets or sets the selected type record name.
        /// </summary>
        public string SelectedTypeRecordName
        {
            get => this.selectedTypeRecordName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeRecordName, value);
        }

        /// <summary>
        /// Gets the select element.
        /// </summary>
        public ReactiveCommand<Unit, Unit> SelectElement { get; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public string SortOrder
        {
            get => this.sortOrder;
            set => this.RaiseAndSetIfChanged(ref this.sortOrder, value);
        }

        /// <summary>
        /// Gets all domain names and UIds in the syntosa database.
        /// </summary>
        /// <returns>
        /// All the domain names and UIds in the syntosa database.
        /// </returns>
        private Dictionary<string, Guid> GetAllDomainNamesAndUIds()
        {
            var domains = new List<global::Syntosa.Core.ObjectModel.CoreClasses.Domain>(); //this.syntosaDal.GetDomainByAny();
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
            var elements = new List<Element>(); //this.syntosaDal.GetElementByAny();
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
            var modules = new List<global::Syntosa.Core.ObjectModel.CoreClasses.Module>(); //this.syntosaDal.GetModuleByAny();
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
            var typeItems = new List<TypeItem>(); //this.syntosaDal.GetTypeItemByAny();
            var typeItemNamesUIds = new Dictionary<string, Guid>();
            foreach (var typeFunction in typeItems)
            {
                typeItemNamesUIds.Add(typeFunction.Name, typeFunction.UId);
            }

            return typeItemNamesUIds;
        }

        /// <summary>
        /// The get element to update.
        /// </summary>
        private void GetElementToUpdate()
        {
            Guid elementToUpdateUId = this.AllElementNamesAndUIds[this.SelectedElementToUpdateName];
            Element elementToUpdate = this.syntosaDal.GetElementByAny(elementUId: elementToUpdateUId).FirstOrDefault();

            this.ElementName = elementToUpdate.Name;
            this.SelectedTypeItemName = elementToUpdate.TypeItemName;
            this.SelectedTypeRecordName = elementToUpdate.RecordStatus;
            this.Description = elementToUpdate.Description;
            this.ElementAlias = elementToUpdate.Alias;
            this.SelectedDomainName = elementToUpdate.DomainName;
            this.IsActive = elementToUpdate.IsActive;
            this.HasParent = !elementToUpdate.ParentUId.Equals(Guid.Empty);

            if (this.HasParent)
            {
                this.SelectedParentElementName = elementToUpdate.ParentName;
            }

            this.IsElementSelected = true;
        }

        private void RemoveElement()
        {
            this.syntosaDal.DeleteTypeUnit(this.AllElementNamesAndUIds[this.SelectedElementToUpdateName]);
            this.HasErrors = false;
            this.Errors = string.Empty;
            this.Return();
        }

        /// <summary>
        /// Return this view to its selection configuration.
        /// </summary>
        private void Return()
        {
            this.selectedElementToUpdateName = string.Empty;
            this.HasParent = false;
            this.HasSelected = false;
        }

        /// <summary>
        /// The build type item.
        /// </summary>
        private void UpdateElement()
        {
            var failureMessages = new StringBuilder();
            Guid elementToUpdateUId = this.AllElementNamesAndUIds[this.SelectedElementToUpdateName];
            Element elementToUpdate = this.syntosaDal.GetElementByAny(elementUId: elementToUpdateUId).FirstOrDefault();

            elementToUpdate.DomainUId = this.AllDomainNamesAndUIds[this.SelectedDomainName];
            elementToUpdate.Alias = this.ElementAlias;
            elementToUpdate.IsActive = this.IsActive;
            elementToUpdate.IsBuiltIn = this.IsBuiltIn;
            elementToUpdate.TypeItemUId = this.AllTypeItemNamesAndUIds[this.SelectedTypeItemName];
            elementToUpdate.TypeUIdRecordStatus = this.AllTypeItemNamesAndUIds[this.SelectedTypeRecordName];
            elementToUpdate.ModuleUId = this.AllModuleNamesAndUIds[this.SelectedModuleName];
            elementToUpdate.ModuleRecordKey = string.Empty;
            elementToUpdate.IsAutoCollect = this.IsAutoCollect;
            elementToUpdate.Name = this.ElementName;
            elementToUpdate.Description = this.Description;
            elementToUpdate.ParentUId = Guid.Empty;
            elementToUpdate.ModifiedBy = "alex@teambond.io";

            if (this.HasParent)
            {
                if (string.IsNullOrWhiteSpace(this.SelectedParentElementName))
                {
                    failureMessages.AppendLine("Please select a parent element");
                }
                else
                {
                    elementToUpdate.ParentUId = this.AllTypeItemNamesAndUIds[this.SelectedParentElementName];
                }
            }

            var elementValidator = new ElementValidator();
            ValidationResult validationResult = elementValidator.Validate(elementToUpdate);
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
            this.syntosaDal.UpdateElement(elementToUpdate);

            // this.userActivityService.InsertActivity(
            // this.userContext.CurrentUser,
            // "Type Item Inserted",
            // $"{this.userContext.CurrentUser.Email} has inserted the type item named {createdTypeItem.Name} with UId {createdTypeItem.UId}");
        }
    }
}