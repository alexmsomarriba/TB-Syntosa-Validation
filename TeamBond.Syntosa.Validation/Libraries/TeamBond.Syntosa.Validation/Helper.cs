namespace TeamBond.Syntosa.Validation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides helper methods for the validation tool.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Retrieves a collection of all regional module identifiers.
        /// </summary>
        /// <returns>
        /// A collection of regional module unique identifiers>.
        /// </returns>
        public static List<Guid> GetAllRegionalModules()
        {
            List<Guid> regionalModules = new List<Guid>();
            
            regionalModules.Add(Guid.Parse(Module.TeamBond));

            return regionalModules;
        }

        /// <summary>
        /// Retrieves a collection of common type definitions
        /// </summary>
        /// <returns>
        /// A collection of common element types
        /// </returns>
        public static List<string> GetCommonTypeDefinitions()
        {
            List<string> commonTypes = new List<string>();

            commonTypes.Add(WorkforceManagement.Worker);
            commonTypes.Add(WorkforceManagement.Broker);
            commonTypes.Add(WorkforceManagement.Employer);
            commonTypes.Add(WorkforceManagement.Candidate);
            commonTypes.Add(WorkforceManagement.Job);
            commonTypes.Add(WorkforceManagement.JobPosting);
            commonTypes.Add(WorkforceManagement.Department);
            commonTypes.Add(WorkforceManagement.OutsideCandidate);
            commonTypes.Add(WorkforceManagement.Division);
            commonTypes.Add(WorkforceManagement.Portfolio);
            commonTypes.Add(WorkforceManagement.Resume);
            commonTypes.Add(WorkforceManagement.Team);

            return commonTypes;
        }

        /// <summary>
        /// Retrieves a SYNTOSA <see cref="Module"/> unique identifier by the provided type item identifier.
        /// </summary>
        /// <param name="typeItemUId">
        /// The type item unique identifier.
        /// </param>
        /// <returns>
        /// A <see cref="Guid"/> representing the SYNTOSA <see cref="Module"/>.
        /// </returns>
        public static Guid GetModule(Guid? typeItemUId)
        {
            // assign common type as default
            Guid moduleUId = Guid.Parse(Module.TeamBond);

            // if it is not a common type
            List<string> result = GetCommonTypeDefinitions();

            if (typeItemUId != null && typeItemUId != default(Guid))
            {
                moduleUId = Guid.Parse(Module.TeamBond);
            }

            return moduleUId;
        }
    }
}