namespace TeamBond.Syntosa.Validation.DataEditor.Validators
{
    using FluentValidation;

    using global::Syntosa.Core.ObjectModel.CoreClasses;

    using TeamBond.Services.Validators;

    /// <summary>
    /// Validator for a <see cref="TypeItem"/> to ensure it is able to be inserted to
    /// or updated in the Syntosa database.
    /// </summary>
    public class TypeItemValidator : TeamBondValidatorBase<TypeItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeItemValidator"/> class.
        /// </summary>
        public TypeItemValidator()
        {
            this.RuleFor(typeItem => typeItem.Name).NotEmpty();
            this.RuleFor(typeItem => typeItem.Description).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsActive).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsAssignable).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsAutoCollect).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsBuiltIn).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsNotifiable).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsRelational).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsGraph).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsInMemory).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsKeyValue).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsLedger).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsSearch).NotEmpty();
            this.RuleFor(typeItem => typeItem.IsTimeSeries).NotEmpty();
            this.RuleFor(typeItem => typeItem.ModifiedBy).NotEmpty();
        }
    }
}