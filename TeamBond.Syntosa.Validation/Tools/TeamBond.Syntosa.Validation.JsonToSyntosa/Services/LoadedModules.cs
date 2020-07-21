namespace TeamBond.Syntosa.Validation.JsonToSyntosa.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;

    /// <summary>
    /// The loaded modules.
    /// </summary>
    public class LoadedModules
    {
        /// <summary>
        /// The syntosa dal.
        /// </summary>
        private readonly SyntosaDal syntosaDal;

        /// <summary>
        /// The modules.
        /// </summary>
        private IList<Module> modules;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedModules"/> class.
        /// </summary>
        /// <param name="syntosaDal">
        /// The syntosa dal.
        /// </param>
        public LoadedModules(SyntosaDal syntosaDal)
        {
            this.syntosaDal = syntosaDal;
            this.modules = this.LoadAllModules();
        }

        /// <summary>
        /// Loads the collection of all modules from the syntosa database.
        /// </summary>
        /// <returns>
        /// The collection of all modules from the syntosa database.
        /// </returns>
        public IList<Module> LoadAllModules()
        {
            return this.syntosaDal.GetModuleByAny().ToList();
        }

        /// <summary>
        /// The add modules to syntosa.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        public void AddModulesToSyntosa(Module module)
        {
            this.syntosaDal.UpsertModule(module);
            this.modules = this.LoadAllModules();
        }
    }
}