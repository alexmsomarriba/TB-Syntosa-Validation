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
    /// The edge label prototype builder view model.
    /// </summary>
    public class EdgeLabelPrototypeBuilderViewModel : ViewModelBase
    {
        /// <summary>
        /// The syntosa dal.
        /// </summary>
        private readonly SyntosaDal syntosaDal;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// The selected edge name.
        /// </summary>
        private string selectedElementName;

        /// <summary>
        /// The selected label name.
        /// </summary>
        private string selectedLabelName;

        /// <summary>
        /// The selected type item name.
        /// </summary>
        private string selectedTypeItemName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeLabelPrototypeBuilderViewModel" /> class.
        /// </summary>
        public EdgeLabelPrototypeBuilderViewModel()
        {
            this.syntosaDal = TeamBondEngineContext.Current.Resolve<SyntosaDal>();

            this.InsertEdgeLabel = ReactiveCommand.Create(this.CreateEdgeLabel);
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
        /// Gets the all label names.
        /// </summary>
        public List<string> AllLabelNames
        {
            get
            {
                var labelNames = new List<string>();
                foreach (var label in this.AllLabelNamesAndUIds)
                {
                    labelNames.Add(label.Key);
                }

                return labelNames;
            }
        }

        /// <summary>
        /// Gets or sets the all label names and u ids.
        /// </summary>
        public Dictionary<string, Guid> AllLabelNamesAndUIds
        {
            get => this.GetAllLabelNamesAndUIds();
            set => this.GetAllLabelNamesAndUIds();
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
        /// Gets or sets the errors.
        /// </summary>
        public string Errors
        {
            get => this.errors;
            set => this.RaiseAndSetIfChanged(ref this.errors, value);
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
        /// Gets the create edge label.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InsertEdgeLabel { get; }

        /// <summary>
        /// Gets or sets the selected element name.
        /// </summary>
        public string SelectedElementName
        {
            get => this.selectedElementName;
            set => this.RaiseAndSetIfChanged(ref this.selectedElementName, value);
        }

        /// <summary>
        /// Gets or sets the selected label name.
        /// </summary>
        public string SelectedLabelName
        {
            get => this.selectedLabelName;
            set => this.RaiseAndSetIfChanged(ref this.selectedLabelName, value);
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
        /// The create edge label.
        /// </summary>
        private void CreateEdgeLabel()
        {
            var failureMessage = new StringBuilder();
            var createdEdgeLabel = new EdgeElementLabel
            {
                ElementUId = this.AllElementNamesAndUIds[this.SelectedElementName],
                LabelUId = this.AllLabelNamesAndUIds[this.SelectedLabelName],
                TypeItemUId = this.AllTypeItemNamesAndUIds[this.SelectedTypeItemName],
                ModifiedBy = "alex@teambond.io"
            };

            var labelValidator = new EdgeLabelValidator();
            ValidationResult validationResult = labelValidator.Validate(createdEdgeLabel);
            if (!validationResult.IsValid)
            {
                foreach (var validationFailure in validationResult.Errors)
                {
                    failureMessage.AppendLine(
                        $"Property ({validationFailure.PropertyName}) failed validation with error message: ({validationFailure.ErrorMessage})");
                }

                this.HasErrors = true;
                this.Errors = failureMessage.ToString();
                return;
            }

            this.HasErrors = false;
            this.Errors = string.Empty;
            this.syntosaDal.CreateEdgeElementLabel(createdEdgeLabel);
        }

        /// <summary>
        /// Get all element names and u ids.
        /// </summary>
        /// <returns>
        /// All element names and u ids.
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
        /// Gets all label names and u ids.
        /// </summary>
        /// <returns>
        /// All label names and u ids.
        /// </returns>
        private Dictionary<string, Guid> GetAllLabelNamesAndUIds()
        {
            List<EdgeElementLabel> labels = this.syntosaDal.GetEdgeElementLabelByAny();
            var labelNamesAndUIds = new Dictionary<string, Guid>();
            foreach (var label in labels)
            {
                labelNamesAndUIds.Add(label.Name, label.UId);
            }

            return labelNamesAndUIds;
        }

        /// <summary>
        /// The get all type item names and u ids.
        /// </summary>
        /// <returns>
        /// All type item names and UIds
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