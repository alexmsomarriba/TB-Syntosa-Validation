namespace TeamBond.Syntosa.Validation.Core
{
    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;

    /// <summary>
    /// Interface that must be implemented by classes that generate TeamBond <see cref="Element"/> prototypes
    /// </summary>
    public interface ITeamBondElementFactory
    {
        /// <summary>
        /// Retrieves a new SYNTOSA <see cref="Element"/> prototype using the given
        /// <see cref="SyntosaDal"/> and type name.
        /// </summary>
        /// <param name="type">
        /// The SYNTOSA <see cref="Element"/> type to generate.
        /// </param>
        /// <param name="syntosaDal">
        /// The SYNTOSA data access layer.
        /// </param>
        /// <returns>
        /// The SYNTOSA <see cref="Element"/> prototype.
        /// </returns>
        public Element GetPrototype(string type, SyntosaDal syntosaDal);
    }
}