namespace TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels
{
    using ReactiveUI;

    /// <summary>
    /// The type proto type builder view model.
    /// </summary>
    public class TypeProtoTypeBuilderViewModel : ViewModelBase
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
        /// The is relational.
        /// </summary>
        private bool isRelational;

        /// <summary>
        /// The is key value.
        /// </summary>
        private bool isKeyValue;

        /// <summary>
        /// The is in memory.
        /// </summary>
        private bool isInMemory;

        /// <summary>
        /// The is graph.
        /// </summary>
        private bool isGraph;

        /// <summary>
        /// The is document.
        /// </summary>
        private bool isDocument;

        /// <summary>
        /// The is ledger.
        /// </summary>
        private bool isLedger;

        /// <summary>
        /// The is time series.
        /// </summary>
        private bool isTimeSeries;

        /// <summary>
        /// The is search.
        /// </summary>
        private bool isSearch;

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
        /// Gets or sets a value indicating whether is relational.
        /// </summary>
        public bool IsRelational
        {
            get => this.isRelational;
            set => this.RaiseAndSetIfChanged(ref this.isRelational, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is key value.
        /// </summary>
        public bool IsKeyValue
        {
            get => this.isKeyValue;
            set => this.RaiseAndSetIfChanged(ref this.isKeyValue, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is in memory.
        /// </summary>
        public bool IsInMemory
        {
            get => this.isInMemory;
            set => this.RaiseAndSetIfChanged(ref this.isInMemory, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is graph.
        /// </summary>
        public bool IsGraph
        {
            get => this.isGraph;
            set => this.RaiseAndSetIfChanged(ref this.isGraph, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is document.
        /// </summary>
        public bool IsDocument
        {
            get => this.isDocument;
            set => this.RaiseAndSetIfChanged(ref this.isDocument, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is ledger.
        /// </summary>
        public bool IsLedger
        {
            get => this.isLedger;
            set => this.RaiseAndSetIfChanged(ref this.isLedger, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is time series.
        /// </summary>
        public bool IsTimeSeries
        {
            get => this.isTimeSeries;
            set => this.RaiseAndSetIfChanged(ref this.isTimeSeries, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is search.
        /// </summary>
        public bool IsSearch
        {
            get => this.isSearch;
            set => this.RaiseAndSetIfChanged(ref this.isSearch, value);
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
    }
}