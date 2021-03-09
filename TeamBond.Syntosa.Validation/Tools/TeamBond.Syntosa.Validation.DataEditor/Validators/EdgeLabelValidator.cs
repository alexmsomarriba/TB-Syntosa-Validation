namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses.Edge;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The edge label validator.
    /// </summary>
    public class EdgeLabelValidator : TeamBondValidatorBase<EdgeElementLabel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeLabelValidator"/> class.
        /// </summary>
        public EdgeLabelValidator()
        {
            this.RuleFor(edgeLabel => edgeLabel.ElementUId).NotEmpty();
            this.RuleFor(edgeLabel => edgeLabel.LabelUId).NotEmpty();
            this.RuleFor(edgeLabel => edgeLabel.TypeItemUId).NotEmpty();
        }
    }
}