namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The module validator.
    /// </summary>
    public class ModuleValidator : TeamBondValidatorBase<Module>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleValidator"/> class.
        /// </summary>
        public ModuleValidator()
        {
            this.RuleFor(module => module.Name).NotEmpty();
            this.RuleFor(module => module.Description).NotEmpty();
            this.RuleFor(module => module.IsActive).NotEmpty();
            this.RuleFor(module => module.IsBuiltIn).NotEmpty();
            this.RuleFor(module => module.ModifiedBy).NotEmpty();
        }
    }
}