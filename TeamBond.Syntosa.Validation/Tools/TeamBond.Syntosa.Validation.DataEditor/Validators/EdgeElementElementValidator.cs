namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses.Edge;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The edge element element validator.
    /// </summary>
    public class EdgeElementElementValidator : TeamBondValidatorBase<EdgeElementElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeElementElementValidator"/> class.
        /// </summary>
        public EdgeElementElementValidator()
        {
            this.RuleFor(edgeElementElement => edgeElementElement.SourceElementUId).NotEmpty();
            this.RuleFor(edgeElementElement => edgeElementElement.TargetElementUId).NotEmpty();
            this.RuleFor(edgeElementElement => edgeElementElement.SourceElementUId)
                .NotEqual(edgeElementElement => edgeElementElement.TargetElementUId);
            this.RuleFor(edgeElementElement => edgeElementElement.TypeItemUId).NotEmpty();
        }
    }
}