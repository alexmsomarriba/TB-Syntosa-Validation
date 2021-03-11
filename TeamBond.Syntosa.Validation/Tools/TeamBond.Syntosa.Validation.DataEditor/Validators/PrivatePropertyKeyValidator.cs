namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The private property key validator.
    /// </summary>
    public class PrivatePropertyKeyValidator : TeamBondValidatorBase<ElementPrivatePropertyKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivatePropertyKeyValidator"/> class.
        /// </summary>
        public PrivatePropertyKeyValidator()
        {
            this.RuleFor(privatePropertyKey => privatePropertyKey.Name).NotEmpty();
            this.RuleFor(privatePropertyKey => privatePropertyKey.ElementUId).NotEmpty();
            this.RuleFor(privatePropertyKey => privatePropertyKey.ModuleUIdAutoCollect).NotEmpty();
            this.RuleFor(privatePropertyKey => privatePropertyKey.TypeKeyUId).NotEmpty();
            this.RuleFor(privatePropertyKey => privatePropertyKey.TypeValueUId).NotEmpty();
            this.RuleFor(privatePropertyKey => privatePropertyKey.SortOrder).NotEmpty();
            this.RuleFor(privatePropertyKey => privatePropertyKey.ModifiedBy).NotEmpty();
        }
    }
}