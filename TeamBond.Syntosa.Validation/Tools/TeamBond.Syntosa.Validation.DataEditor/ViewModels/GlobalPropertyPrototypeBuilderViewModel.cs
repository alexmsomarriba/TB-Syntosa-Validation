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
    /// The global property prototype builder view model.
    /// </summary>
    public class GlobalPropertyPrototypeBuilderViewModel : ViewModelBase
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
        /// The element name.
        /// </summary>
        private string selectedElementName;

        /// <summary>
        /// The type item name.
        /// </summary>
        private string selectedTypeItemName;

        /// <summary>
        /// The is auto collect.
        /// </summary>
        private bool isAutoCollect;

        /// <summary>
        /// The is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The module auto collect name.
        /// </summary>
        private string selectedModuleAutoCollectName;

        /// <summary>
        /// The sort order.
        /// </summary>
        private int sortOrder;

        /// <summary>
        /// The global attribute.
        /// </summary>
        private string globalAttribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalPropertyPrototypeBuilderViewModel"/> class.
        /// </summary>
        public GlobalPropertyPrototypeBuilderViewModel()
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
        /// Gets the all module names.
        /// </summary>
        public List<string> AllModuleNames
        {
            get
            {
                var moduleNames = new List<string>();
                foreach (var module in this.AllModuleNamesAndUIds)
                {
                    moduleNames.Add(module.Key);
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
        /// Gets or sets the global attribute.
        /// </summary>
        public string GlobalAttribute
        {
            get => this.globalAttribute;
            set => this.RaiseAndSetIfChanged(ref this.globalAttribute, value);
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
        /// Gets or sets the selected type item name.
        /// </summary>
        public string SelectedTypeItemName
        {
            get => this.selectedTypeItemName;
            set => this.RaiseAndSetIfChanged(ref this.selectedTypeItemName, value);
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
        /// The build global property.
        /// </summary>
        private void BuildGlobalProperty()
        {
            var failureMessage = new StringBuilder();
            var createdGlobalProperty = new ElementGlobalProperty
                                            {
                                                Attribute = this.GlobalAttribute,
                                                ElementUId = this.AllElementNamesAndUIds[this.SelectedElementName],
                                                TypeItemUId = this.AllTypeItemNamesAndUIds[this.selectedTypeItemName],
                                                IsActive = this.IsActive,
                                                IsAutoCollect = this.isAutoCollect,
                                                ModifiedBy = this.userContext.CurrentUser.Email,
                                            };

            if (this.isAutoCollect)
            {
                createdGlobalProperty.ModuleUIdAutoCollect =
                    this.AllModuleNamesAndUIds[this.selectedModuleAutoCollectName];
            }


        }

        /// <summary>
        /// Returns a dictionary containing all element UId's keyed by their names
        /// </summary>
        /// <returns>
        /// The dictionary containing all element UIds keyed by their names
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
        /// Returns a dictionary containing all type item UIds keyed by their names
        /// </summary>
        /// <returns>
        /// The dictionary containing all type item UIds keyed by their names.
        /// </returns>
        private Dictionary<string, Guid> GetAllTypeItemNamesAndUIds()
        {
            List<TypeItem> types = this.syntosaDal.GetTypeItemByAny();
            var typeNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var type in types)
            {
                typeNamesAndUIds.Add(type.Name, type.UId);
            }

            return typeNamesAndUIds;
        }

        /// <summary>
        /// Returns a dictionary containing all module UIds keyed by their names
        /// </summary>
        /// <returns>
        /// The dictionary containing all module UIds keyed by their names
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
    }
}