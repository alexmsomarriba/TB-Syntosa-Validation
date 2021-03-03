namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;

    /// <summary>
    /// The edge element element prototype builder view model.
    /// </summary>
    public class EdgeElementElementPrototypeBuilderViewModel : ViewModelBase
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
        /// The source element name.
        /// </summary>
        private string selectedSourceElementName;

        /// <summary>
        /// The target element name.
        /// </summary>
        private string selectedTargetElementName;

        /// <summary>
        /// The selected edge type name.
        /// </summary>
        private string selectedEdgeTypeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeElementElementPrototypeBuilderViewModel"/> class.
        /// </summary>
        public EdgeElementElementPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();
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
        /// Gets the all type item names.
        /// </summary>
        public List<string> AllTypeItemNames
        {
            get
            {
                var typeItemNames = new List<string>();
                foreach (var typeItem in this.AllTypeItemsNamesAndUIds)
                {
                    typeItemNames.Add(typeItem.Key);
                }

                return typeItemNames;
            }
        }

        /// <summary>
        /// Gets or sets the all type items names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllTypeItemsNamesAndUIds
        {
            get => this.GetAllTypeItemNamesAndUIds();
            set => this.GetAllTypeItemNamesAndUIds();
        }

        /// <summary>
        /// Gets or sets the selected source element name.
        /// </summary>
        public string SelectedSourceElementName
        {
            get => this.selectedSourceElementName;
            set => this.RaiseAndSetIfChanged(ref this.selectedSourceElementName, value);
        }

        /// <summary>
        /// Gets or sets the selected target element name.
        /// </summary>
        public string SelectedTargetElementName
        {
            get => this.selectedTargetElementName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTargetElementName, value);
        }

        /// <summary>
        /// Gets or sets the selected edge type name.
        /// </summary>
        public string SelectedEdgeTypeName
        {
            get => this.selectedEdgeTypeName;
            set => this.RaiseAndSetIfChanged(ref this.selectedEdgeTypeName, value);
        }

        /// <summary>
        /// The get all element names and u ids.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary"/>.
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
        /// The get all type item names and u ids.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary"/>.
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
    }
}