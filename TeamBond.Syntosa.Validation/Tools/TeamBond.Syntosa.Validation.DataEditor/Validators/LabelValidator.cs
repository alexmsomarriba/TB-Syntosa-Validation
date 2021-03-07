namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The label validator.
    /// </summary>
    public class LabelValidator : TeamBondValidatorBase<ElementLabel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelValidator"/> class.
        /// </summary>
        public LabelValidator()
        {
            this.RuleFor(label => label.Name).NotEmpty();
            this.RuleFor(label => label.Description).NotEmpty();
            this.RuleFor(label => label.IsGlobalEdit).NotEqual(label => label.IsPrivate);
            this.RuleFor(label => label.DomainUId).NotEmpty();
        }
    }
}