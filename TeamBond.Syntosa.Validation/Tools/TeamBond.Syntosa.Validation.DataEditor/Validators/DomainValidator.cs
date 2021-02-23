namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using TeamBond.Services.Validators;

    /// <summary>
    /// Validation class for syntosa domains.
    /// </summary>
    public class DomainValidator : TeamBondValidatorBase<Domain>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidator"/> class.
        /// </summary>
        public DomainValidator()
        {
            this.RuleFor(domain => domain.Name).NotEmpty();
            this.RuleFor(domain => domain.Description).NotEmpty();
            this.RuleFor(domain => domain.IsActive).NotEmpty();
            this.RuleFor(domain => domain.IsBuiltIn).NotEmpty();
            this.RuleFor(domain => domain.ModifiedBy).NotEmpty();
        }
    }
}