namespace TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels
{
    using System;
    using System.Collections.Generic;

    using ReactiveUI;

    /// <summary>
    /// The type proto type builder view model.
    /// </summary>
    public class TypePrototypeBuilderViewModel : ViewModelBase
    {
        /// <summary>
        /// The type name.
        /// </summary>
        private string typeName;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The is built in.
        /// </summary>
        private bool isBuiltIn;

        /// <summary>
        /// The is assignable.
        /// </summary>
        private bool isAssignable;

        /// <summary>
        /// The is notifiable.
        /// </summary>
        private bool isNotifiable;

        /// <summary>
        /// The is auto collect.
        /// </summary>
        private bool isAutoCollect;

        /// <summary>
        /// The module auto collect u id.
        /// </summary>
        private string moduleAutoCollectUId;

        /// <summary>
        /// The sort order.
        /// </summary>
        private int sortOrder;

        /// <summary>
        /// The type function u id.
        /// </summary>
        private string typeFunctionUId;

        /// <summary>
        /// The type unit u id.
        /// </summary>
        private string typeUnitUId;

        /// <summary>
        /// The selected type.
        /// </summary>
        private string selectedType;

        /// <summary>
        /// Gets or sets the type name.
        /// </summary>
        public string TypeName
        {
            get => this.typeName;
            set => this.RaiseAndSetIfChanged(ref this.typeName, value);
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
        /// Gets or sets a value indicating whether is assignable.
        /// </summary>
        public bool IsAssignable
        {
            get => this.isAssignable;
            set => this.RaiseAndSetIfChanged(ref this.isAssignable, value);
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
        /// Gets or sets a value indicating whether is auto collect.
        /// </summary>
        public bool IsAutoCollect
        {
            get => this.isAutoCollect;
            set => this.RaiseAndSetIfChanged(ref this.isAutoCollect, value);
        }

        /// <summary>
        /// Gets or sets the module auto collect u id.
        /// </summary>
        public string ModuleAutoCollectUId
        {
            get => this.moduleAutoCollectUId;
            set => this.RaiseAndSetIfChanged(ref this.moduleAutoCollectUId, value);
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
        /// Gets or sets the type function u id.
        /// </summary>
        public string TypeFunctionUId
        {
            get => this.typeFunctionUId;
            set => this.RaiseAndSetIfChanged(ref this.typeFunctionUId, value);
        }

        /// <summary>
        /// Gets or sets the type unit u id.
        /// </summary>
        public string TypeUnitUId
        {
            get => this.typeUnitUId;
            set => this.RaiseAndSetIfChanged(ref this.typeUnitUId, value);
        }

        /// <summary>
        /// The database types.
        /// </summary>
        public List<string> DatabaseTypes => new List<string>
                                                {
                                                    "Relational",
                                                    "Key value",
                                                    "In memory",
                                                    "Graph",
                                                    "Document",
                                                    "Ledger",
                                                    "TimeSeries",
                                                    "Search"
                                                };

        public List<string>

        /// <summary>
        /// Gets or sets the selected type.
        /// </summary>
        public string SelectedType
        {
            get => this.selectedType;
            set => this.RaiseAndSetIfChanged(ref this.selectedType, value);
        }

        private List<string> GetTypeFunctionNames()
        {
            var syntoDal =
        }
    }
}