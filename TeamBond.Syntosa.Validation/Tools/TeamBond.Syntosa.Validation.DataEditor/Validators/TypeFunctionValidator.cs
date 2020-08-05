namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The type function validator.
    /// </summary>
    public class TypeFunctionValidator : TeamBondValidatorBase<TypeFunction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFunctionValidator"/> class.
        /// </summary>
        public TypeFunctionValidator()
        {
            this.RuleFor(typeFunction => typeFunction.Name).NotEmpty();
            this.RuleFor(typeFunction => typeFunction.Description).NotEmpty();
            this.RuleFor(typeFunction => typeFunction.IsActive).NotEmpty();
            this.RuleFor(typeFunction => typeFunction.IsBuiltIn).NotEmpty();
            this.RuleFor(typeFunction => typeFunction.ModifiedBy).NotEmpty();
        }
    }
}