namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;

    /// <summary>
    /// The label prototype builder view model.
    /// </summary>
    public class LabelPrototypeBuilderViewModel : ViewModelBase
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
        /// The is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The is built in.
        /// </summary>
        private bool isBuiltIn;

        /// <summary>
        /// The is global edit.
        /// </summary>
        private bool isGlobalEdit;

        /// <summary>
        /// The is private.
        /// </summary>
        private bool isPrivate;

        /// <summary>
        /// The label description.
        /// </summary>
        private string labelDescription;

        /// <summary>
        /// The label name.
        /// </summary>
        private string labelName;

        /// <summary>
        /// The selected domain name.
        /// </summary>
        private string selectedDomainName;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelPrototypeBuilderViewModel" /> class.
        /// </summary>
        public LabelPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();
        }

        /// <summary>
        /// Gets the all domain names.
        /// </summary>
        public List<string> AllDomainNames
        {
            get
            {
                var domainNames = new List<string>();
                foreach (var domain in this.AllDomainNamesAndUIds)
                {
                    domainNames.Add(domain.Key);
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
        /// Gets or sets a value indicating whether is global edit.
        /// </summary>
        public bool IsGlobalEdit
        {
            get => this.isGlobalEdit;
            set => this.RaiseAndSetIfChanged(ref this.isGlobalEdit, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is private.
        /// </summary>
        public bool IsPrivate
        {
            get => this.isPrivate;
            set => this.RaiseAndSetIfChanged(ref this.isPrivate, value);
        }

        /// <summary>
        /// Gets or sets the label description.
        /// </summary>
        public string LabelDescription
        {
            get => this.labelDescription;
            set => this.RaiseAndSetIfChanged(ref this.labelDescription, value);
        }

        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        public string LabelName
        {
            get => this.labelName;
            set => this.RaiseAndSetIfChanged(ref this.labelName, value);
        }

        /// <summary>
        /// Gets or sets the selected domain name.
        /// </summary>
        public string SelectedDomainName
        {
            get => this.selectedDomainName;
            set => this.RaiseAndSetIfChanged(ref this.selectedDomainName, value);
        }

        private void CreateLabel()
        {
            var failureMessage = new StringBuilder();
            var createdLabel = new ElementLabel { };
        }

        /// <summary>
        /// The get all domain names and u ids.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary" />.
        /// </returns>
        private Dictionary<string, Guid> GetAllDomainNamesAndUIds()
        {
            List<Domain> domains = this.syntosaDal.GetDomainByAny();
            var domainNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var domain in domains)
            {
                domainNamesAndUIds.Add(domain.Name, domain.UId);
            }

            return domainNamesAndUIds;
        }
    }
}