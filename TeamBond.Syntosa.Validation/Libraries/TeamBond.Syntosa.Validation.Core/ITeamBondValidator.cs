namespace TeamBond.Syntosa.Validation.Core
{
    using global::Syntosa.Core.ObjectModel;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    public interface ITeamBondValidator 
    {
        Element GetElementPrototype();

        ValidationResult<Element> Validate(Element element);
    }
}