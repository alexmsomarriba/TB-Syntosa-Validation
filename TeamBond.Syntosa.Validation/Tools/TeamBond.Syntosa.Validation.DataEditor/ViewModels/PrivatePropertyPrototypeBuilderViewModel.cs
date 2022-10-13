namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;
    using System.Text;

    using FluentValidation.Results;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;
    using TeamBond.Syntosa.Validation.DataEditor.Validators;

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
        /// The error.
        /// </summary>
        private string error;

        /// <summary>
        /// The has error.
        /// </summary>
        private bool hasError;

        /// <summary>
        /// The has parent.
        /// </summary>
        private bool hasParent;

        /// <summary>
        /// The is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The private attribute.
        /// </summary>
        private string privateAttribute;

        /// <summary>
        /// The selected parent private property name.
        /// </summary>
        private string selectedParentPrivatePropertyName;

        /// <summary>
        /// The selected private property key name.
        /// </summary>
        private string selectedPrivatePropertyKeyName;

        /// <summary>
        /// The sort order.
        /// </summary>
        private int sortOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivatePropertyPrototypeBuilderViewModel" /> class.
        /// </summary>
        public PrivatePropertyPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();

            this.InsertPrivateProperty = ReactiveCommand.Create(this.CreatePrivateProperty);
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
            get => this.GetAllPrivatePropertyKeyNamesAndUIds();
            set => this.GetAllPrivatePropertyKeyNamesAndUIds();
        }

        /// <summary>
        /// Gets the all private property names.
        /// </summary>
        public List<string> AllPrivatePropertyNames
        {
            get
            {
                var privatePropertyNames = new List<string>();
                foreach (var privateProperty in this.AllPrivatePropertyNamesAndUIds)
                {
                    privatePropertyNames.Add(privateProperty.Key);
                }

                return privatePropertyNames;
            }
        }

        /// <summary>
        /// Gets or sets the all private property names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllPrivatePropertyNamesAndUIds
        {
            get => this.GetAllPrivatePropertyNamesAndUIds();
            set => this.GetAllPrivatePropertyNamesAndUIds();
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public string Error
        {
            get => this.error;
            set => this.RaiseAndSetIfChanged(ref this.error, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has error.
        /// </summary>
        public bool HasError
        {
            get => this.hasError;
            set => this.RaiseAndSetIfChanged(ref this.hasError, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has parent.
        /// </summary>
        public bool HasParent
        {
            get => this.hasParent;
            set => this.RaiseAndSetIfChanged(ref this.hasParent, value);
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
        /// Gets the insert private property.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertPrivateProperty { get; }

        /// <summary>
        /// Gets or sets the private attribute.
        /// </summary>
        public string PrivateAttribute
        {
            get => this.privateAttribute;
            set => this.RaiseAndSetIfChanged(ref this.privateAttribute, value);
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
        /// Gets or sets the selected private property name.
        /// </summary>
        public string SelectedPrivatePropertyName
        {
            get => this.selectedParentPrivatePropertyName;
            set => this.RaiseAndSetIfChanged(ref this.selectedParentPrivatePropertyName, value);
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
        /// The create private property.
        /// </summary>
        private void CreatePrivateProperty()
        {
            var failureMessage = new StringBuilder();
            var createdPrivateProperty = new ElementPrivateProperty
            {
                PrivatePropertyKeyUId =
                    this.AllPrivatePropertyKeyNamesAndUIds[this.SelectedPrivatePropertyKeyName],
                IsActive = this.IsActive,
                Attribute = this.PrivateAttribute,
                SortOrder = this.SortOrder,
                ModifiedBy = "alex@teambond.io"
            };

            if (this.HasParent)
            {
                createdPrivateProperty.ParentUId =
                    this.AllPrivatePropertyNamesAndUIds[this.SelectedPrivatePropertyName];
            }

            var privatePropertyValidator = new PrivatePropertyValidator();
            ValidationResult validationResult = privatePropertyValidator.Validate(createdPrivateProperty);
            if (!validationResult.IsValid)
            {
                foreach (var validationFailure in validationResult.Errors)
                {
                    failureMessage.AppendLine(
                        $"Property {validationFailure.PropertyName} has failed validation with error {validationFailure.ErrorMessage}");
                }

                this.HasError = true;
                this.Error = failureMessage.ToString();
                return;
            }

            this.HasError = false;
            this.Error = string.Empty;
            this.syntosaDal.CreateElementPrivateProperty(createdPrivateProperty);
        }

        /// <summary>
        /// The get all private property key names and UIds.
        /// </summary>
        /// <returns>
        /// All private property key names and UIds
        /// </returns>
        private Dictionary<string, Guid> GetAllPrivatePropertyKeyNamesAndUIds()
        {
            List<ElementPrivatePropertyKey> privatePropertyKeys = new List<ElementPrivatePropertyKey>();//this.syntosaDal.GetElementPrivatePropertyKeyByAny();
            var privatePropertyKeyNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var privatePropertyKey in privatePropertyKeys)
            {
                privatePropertyKeyNamesAndUIds.Add(privatePropertyKey.Name, privatePropertyKey.UId);
            }

            return privatePropertyKeyNamesAndUIds;
        }

        /// <summary>
        /// The get all private property names and u ids.
        /// </summary>
        /// <returns>
        /// All private property names and u ids.
        /// </returns>
        private Dictionary<string, Guid> GetAllPrivatePropertyNamesAndUIds()
        {
            List<ElementPrivateProperty> privateProperties = new List<ElementPrivateProperty>();//this.syntosaDal.GetElementPrivatePropertyByAny();
            var privatePropertyNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var privateProperty in privateProperties)
            {
                privatePropertyNamesAndUIds.Add(privateProperty.Name, privateProperty.UId);
            }

            return privatePropertyNamesAndUIds;
        }
    }
}