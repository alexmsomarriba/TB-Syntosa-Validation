namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Reactive;

    using DynamicData.Binding;

    using ReactiveUI;

    using TeamBond.Syntosa.Validation.DataEditor.GeneratedClasses;

    using Element = global::Syntosa.Core.ObjectModel.CoreClasses.Element.Element;

    /// <summary>
    /// The element prototype builder model view.
    /// </summary>
    public class ElementPrototypeBuilderModelView : ViewModelBase
    {
        /// <summary>
        /// The name.
        /// </summary>
        private string name;

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
        /// The alias.
        /// </summary>
        private string alias;

        /// <summary>
        /// The is auto collect.
        /// </summary>
        private bool isAutoCollect;

        /// <summary>
        /// The module u id.
        /// </summary>
        private string moduleUId;

        /// <summary>
        /// The module record key.
        /// </summary>
        private string moduleRecordKey;

        /// <summary>
        /// The type record status.
        /// </summary>
        private string typeUIdRecordStatus;

        /// <summary>
        /// The domain u id.
        /// </summary>
        private string domainUId;

        /// <summary>
        /// The type item u id.
        /// </summary>
        private string typeItemUId;

        /// <summary>
        /// The modified by.
        /// </summary>
        private string modifiedBy;

        public ElementPrototypeBuilderModelView()
        {

            this.SaveElement = ReactiveCommand.Create(
                () => new Element
                          {
                              Name = this.Name,
                              Description = this.Description,
                              IsActive = this.IsActive,
                              IsBuiltIn = this.IsBuiltIn,
                              Alias = this.Alias,
                              IsAutoCollect = this.IsAutoCollect,
                              ModuleUId = new Guid(this.ModuleUId),
                              ModuleRecordKey = this.ModuleRecordKey,
                              TypeUIdRecordStatus = new Guid(this.TypeUIdRecordStatus),
                              DomainUId = new Guid(this.ModuleUId),
                              TypeItemUId = new Guid(this.TypeItemUId),
                              ModifiedBy = this.ModifiedBy
                          });
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.RaiseAndSetIfChanged(ref this.name, value);
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
        /// Gets or sets the alias.
        /// </summary>
        public string Alias
        {
            get => this.alias;
            set => this.RaiseAndSetIfChanged(ref this.alias, value);
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
        /// Gets or sets the module u id.
        /// </summary>
        public string ModuleUId
        {
            get => this.moduleUId;
            set => this.RaiseAndSetIfChanged(ref this.moduleUId, value);
        }

        /// <summary>
        /// Gets or sets the module record key.
        /// </summary>
        public string ModuleRecordKey
        {
            get => this.moduleRecordKey;
            set => this.RaiseAndSetIfChanged(ref this.moduleRecordKey, value);
        }

        /// <summary>
        /// Gets or sets the type u id record status.
        /// </summary>
        public string TypeUIdRecordStatus
        {
            get => this.typeUIdRecordStatus;
            set => this.RaiseAndSetIfChanged(ref this.typeUIdRecordStatus, value);
        }

        /// <summary>
        /// Gets or sets the domain u id.
        /// </summary>
        public string DomainUId
        {
            get => this.domainUId;
            set => this.RaiseAndSetIfChanged(ref this.domainUId, value);
        }

        /// <summary>
        /// Gets or sets the type item u id.
        /// </summary>
        public string TypeItemUId
        {
            get => this.typeItemUId;
            set => this.RaiseAndSetIfChanged(ref this.typeItemUId, value);
        }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy
        {
            get => this.modifiedBy;
            set => this.RaiseAndSetIfChanged(ref this.modifiedBy, value);
        }

        /// <summary>
        /// Gets the save element.
        /// </summary>
        public ReactiveCommand<Unit, Element> SaveElement { get; }
    }
}