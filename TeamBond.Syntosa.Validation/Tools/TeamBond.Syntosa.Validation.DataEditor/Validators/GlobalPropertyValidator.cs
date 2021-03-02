namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The global property validator.
    /// </summary>
    public class GlobalPropertyValidator : TeamBondValidatorBase<ElementGlobalProperty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalPropertyValidator"/> class.
        /// </summary>
        public GlobalPropertyValidator()
        {
            this.RuleFor(globalProperty => globalProperty.Name).NotEmpty();
            this.RuleFor(globalProperty => globalProperty.ElementUId).NotEmpty();
            this.RuleFor(globalProperty => globalProperty.TypeItemUId).NotEmpty();
            this.RuleFor(globalProperty => globalProperty.ModifiedBy).NotEmpty();
        }
    }
}
