namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The element validator.
    /// </summary>
    public class ElementValidator : TeamBondValidatorBase<Element>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementValidator"/> class.
        /// </summary>
        public ElementValidator()
        {
            this.RuleFor(element => element.Name).NotEmpty();
            this.RuleFor(element => element.Description).NotEmpty();
            this.RuleFor(element => element.ModuleUId).NotEmpty();
            this.RuleFor(element => element.TypeItemUId).NotEmpty();
            this.RuleFor(element => element.DomainUId).NotEmpty();
        }
    }
}