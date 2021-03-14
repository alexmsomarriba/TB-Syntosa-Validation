namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The private property validator.
    /// </summary>
    public class PrivatePropertyValidator : TeamBondValidatorBase<ElementPrivateProperty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivatePropertyValidator"/> class.
        /// </summary>
        public PrivatePropertyValidator()
        {
            this.RuleFor(privateProperty => privateProperty.Name).NotEmpty();
            this.RuleFor(privateProperty => privateProperty.ElementUId).NotEmpty();
            this.RuleFor(privateProperty => privateProperty.TypeKeyUId).NotEmpty();
            this.RuleFor(privateProperty => privateProperty.SortOrder).NotEmpty();
            this.RuleFor(privateProperty => privateProperty.ModifiedBy).NotEmpty();
        }
    }
}