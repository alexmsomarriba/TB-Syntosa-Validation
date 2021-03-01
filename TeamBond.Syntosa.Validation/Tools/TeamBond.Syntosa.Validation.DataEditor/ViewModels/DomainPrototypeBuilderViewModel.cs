namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;
    using System.Text;

    using FluentValidation.Results;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;
    using TeamBond.Syntosa.Validation.DataEditor.Validators;

    /// <summary>
    /// The module prototype builder view model.
    /// </summary>
    public class DomainPrototypeBuilderViewModel : ViewModelBase
    {
        /// <summary>
        /// The syntosa dal.
        /// </summary>
        private readonly SyntosaDal syntosaDal;

        /// <summary>
        /// The user context.
        /// </summary>
        private readonly IUserContext userContext;

        /// <summary>
        /// The account information.
        /// </summary>
        private string accountInformation;

        /// <summary>
        /// The module description.
        /// </summary>
        private string description;

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
        private string name;

        /// <summary>
        /// The name of the selected parent domain name.
        /// </summary>
        private string selectedDomainName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainPrototypeBuilderViewModel" /> class.
        /// </summary>
        public DomainPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();

            this.CreateDomain = ReactiveCommand.Create(this.BuildDomain);
        }

        /// <summary>
        /// Gets the all Domain names.
        /// </summary>
        public List<string> AllDomainNames
        {
            get
            {
                var moduleNames = new List<string>();
                foreach (var moduleName in this.AllDomainNamesAndUIds.Keys)
                {
                    moduleNames.Add(moduleName);
                }

                return moduleNames;
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
        /// Gets or sets the account information.
        /// </summary>
        public string AccountInformation
        {
            get => this.accountInformation;
            set => this.RaiseAndSetIfChanged(ref this.accountInformation, value);
        }

        /// <summary>
        /// Gets the create domain button interaction.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CreateDomain { get; }

        /// <summary>
        /// Gets or sets the description of the module.
        /// </summary>
        public string Description
        {
            get => this.description;
            set => this.RaiseAndSetIfChanged(ref this.description, value);
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
        public string Name
        {
            get => this.name;
            set => this.RaiseAndSetIfChanged(ref this.name, value);
        }

        /// <summary>
        /// Gets or sets the selected parent module name.
        /// </summary>
        public string SelectedDomainName
        {
            get => this.selectedDomainName;
            set => this.RaiseAndSetIfChanged(ref this.selectedDomainName, value);
        }

        /// <summary>
        /// The build domain based off the given inputs.
        /// </summary>
        private void BuildDomain()
        {
            var failureMessage = new StringBuilder();
            var createdDomain = new Domain
                                    {
                                        Name = this.Name,
                                        AccountInformation = this.AccountInformation,
                                        Description = this.Description,
                                        IsActive = this.IsActive,
                                        IsBuiltIn = this.IsBuiltIn,
                                        ParentUId = Guid.Empty,
                                        ModifiedBy = this.userContext.CurrentUser.Email
                                    };

            if (string.IsNullOrWhiteSpace(this.AccountInformation))
            {
                createdDomain.AccountInformation = string.Empty;
            }

            if (this.HasParent)
            {
                if (string.IsNullOrWhiteSpace(this.SelectedDomainName))
                {
                    failureMessage.AppendLine(
                        "Please select a parent module or deselect the 'Has parent domain' check box");
                }
                else
                {
                    createdDomain.ParentUId = this.AllDomainNamesAndUIds[this.SelectedDomainName];
                }
            }

            var domainValidator = new DomainValidator();
            ValidationResult validationResult = domainValidator.Validate(createdDomain);
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

            this.syntosaDal.CreateDomain(createdDomain);

            // createdModule = this.syntosaDal.GetModuleByAny(
            //    moduleName: this.Name,
            //    moduleDesc: this.Description,
            //    isActive: this.IsActive,
            //    isBuiltIn: this.isBuiltIn).FirstOrDefault();
            // this.userActivityService.InsertActivity(
            //    this.userContext.CurrentUser,
            //    "Module Inserted",
            //    $"{this.userContext.CurrentUser.Email} has inserted the module named {createdModule.Name} with UId {createdModule.UId}");
            this.errors = string.Empty;
            this.hasErrors = false;
        }

        /// <summary>
        /// Gets all domain names and UIds in the Syntosa database.
        /// </summary>
        /// <returns>
        /// All domain names and UIds in the Syntosa database.
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
    }
}