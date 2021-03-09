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
    /// The private property prototype builder view model.
    /// </summary>
    public class PrivatePropertyKeyPrototypeBuilderViewModel : ViewModelBase
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
        /// The selected module auto collect u id.
        /// </summary>
        private string selectedModuleAutoCollectName;

        /// <summary>
        /// The selected element name.
        /// </summary>
        private string selectedElementName;

        /// <summary>
        /// The type key name.
        /// </summary>
        private string selectedTypeKeyName;

        /// <summary>
        /// The type value name.
        /// </summary>
        private string selectedTypeValueName;

        /// <summary>
        /// The type unit name.
        /// </summary>
        private string selectedTypeUnitName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivatePropertyKeyPrototypeBuilderViewModel"/> class.
        /// </summary>
        public PrivatePropertyKeyPrototypeBuilderViewModel()
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
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive
        {
            get => this.isActive;
            set => this.RaiseAndSetIfChanged(ref this.isActive, value);
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