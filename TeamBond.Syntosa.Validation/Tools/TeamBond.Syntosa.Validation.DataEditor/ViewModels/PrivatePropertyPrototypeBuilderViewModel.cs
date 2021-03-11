namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;

    /// <summary>
    /// The private property prototype builder view model.
    /// </summary>
    public class PrivatePropertyPrototypeBuilderViewModel : ViewModelBase
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
        /// The selected private property key name.
        /// </summary>
        private string selectedPrivatePropertyKeyName;

        /// <summary>
        /// The is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The private attribute.
        /// </summary>
        private string privateAttribute;

        /// <summary>
        /// The sort order.
        /// </summary>
        private int sortOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivatePropertyPrototypeBuilderViewModel"/> class.
        /// </summary>
        public PrivatePropertyPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();
        }

        /// <summary>
        /// Gets the all private property key names.
        /// </summary>
        public List<string> AllPrivatePropertyKeyNames
        {
            get
            {
                var privatePropertyKeyNames = new List<string>();
                foreach (var privatePropertyKey in this.AllPrivatePropertyKeyNamesAndUIds)
                {
                    privatePropertyKeyNames.Add(privatePropertyKey.Key);
                }

                return privatePropertyKeyNames;
            }
        }

        /// <summary>
        /// Gets or sets the all private property key names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllPrivatePropertyKeyNamesAndUIds
        {
            get => this.GetAllPrivatePropertyKey();
            set => this.GetAllPrivatePropertyKey();
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
        /// Gets or sets the selected private property key name.
        /// </summary>
        public string SelectedPrivatePropertyKeyName
        {
            get => this.selectedPrivatePropertyKeyName;
            set => this.RaiseAndSetIfChanged(ref this.selectedPrivatePropertyKeyName, value);
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
        /// Gets or sets the private attribute.
        /// </summary>
        public string PrivateAttribute
        {
            get => this.privateAttribute;
            set => this.RaiseAndSetIfChanged(ref this.privateAttribute, value);
        }

        /// <summary>
        /// The get all private property key.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        private Dictionary<string, Guid> GetAllPrivatePropertyKey()
        {
            List<ElementPrivatePropertyKey> privatePropertyKeys = this.syntosaDal.GetElementPrivatePropertyKeyByAny();
            var privatePropertyKeyNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var privatePropertyKey in privatePropertyKeys)
            {
                privatePropertyKeyNamesAndUIds.Add(privatePropertyKey.Name, privatePropertyKey.UId);
            }

            return privatePropertyKeyNamesAndUIds;
        }
    }
}