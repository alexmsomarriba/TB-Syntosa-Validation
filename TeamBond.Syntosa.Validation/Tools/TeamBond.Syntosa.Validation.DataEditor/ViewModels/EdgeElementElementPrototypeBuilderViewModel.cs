using TeamBond.Services.Users;

namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;
    using System.Text;

    using FluentValidation.Results;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Edge;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using ReactiveUI;

    using TeamBond.Core.Engine;
    using TeamBond.Domain.User;
    using TeamBond.Syntosa.Validation.DataEditor.Validators;

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
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeElementElementPrototypeBuilderViewModel"/> class.
        /// </summary>
        public EdgeElementElementPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();

            this.InsertEdgeElementElement = ReactiveCommand.Create(this.CreateEdgeElementElement);
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
        /// Gets the insert edge element element.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertEdgeElementElement { get; }

        /// <summary>
        /// Gets or sets the selected edge type name.
        /// </summary>
        public string SelectedEdgeTypeName
        {
            get => this.selectedEdgeTypeName;
            set => this.RaiseAndSetIfChanged(ref this.selectedEdgeTypeName, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has errors.
        /// </summary>
        public bool HasErrors
        {
            get => this.hasErrors;
            set => this.RaiseAndSetIfChanged(ref this.hasErrors, value);
        }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public string Errors
        {
            get => this.errors;
            set => this.RaiseAndSetIfChanged(ref this.errors, value);
        }

        /// <summary>
        /// The create edge element element.
        /// </summary>
        private void CreateEdgeElementElement()
        {
            var failureMessage = new StringBuilder();
            var createdEdgeElementElement = new EdgeElementElement
                                                {
                                                    SourceElementUId =
                                                        this.AllElementNamesAndUIds[this.SelectedSourceElementName],
                                                    TargetElementUId =
                                                        this.AllElementNamesAndUIds[this.SelectedTargetElementName],
                                                    TypeItemUId =
                                                        this.AllTypeItemsNamesAndUIds[this.selectedEdgeTypeName],
                                                    ModifiedBy = "alex@teambond.io"
                                                };

            var edgeElementElementValidator = new EdgeElementElementValidator();
            ValidationResult validationResult = edgeElementElementValidator.Validate(createdEdgeElementElement);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    failureMessage.AppendLine(
                        $"Property ({error.PropertyName}) has failed validation with error ({error.ErrorMessage})");
                }

                this.HasErrors = true;
                this.Errors = failureMessage.ToString();
                return;
            }

            this.HasErrors = false;
            this.Errors = string.Empty;
            this.syntosaDal.CreateEdgeElementElement(createdEdgeElementElement);
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