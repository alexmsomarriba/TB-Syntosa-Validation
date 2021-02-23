namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using TeamBond.Services.Validators;

    /// <summary>
    /// The type unit validator.
    /// </summary>
    public class TypeUnitValidator : TeamBondValidatorBase<TypeUnit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeUnitValidator"/> class.
        /// </summary>
        public TypeUnitValidator()
        {
            this.RuleFor(typeUnit => typeUnit.Name).NotEmpty();
            this.RuleFor(typeUnit => typeUnit.Description).NotEmpty();
            this.RuleFor(typeUnit => typeUnit.IsActive).NotEmpty();
            this.RuleFor(typeUnit => typeUnit.IsBuiltIn).NotEmpty();
            this.RuleFor(typeUnit => typeUnit.ModifiedBy).NotEmpty();
        }
    }
}   