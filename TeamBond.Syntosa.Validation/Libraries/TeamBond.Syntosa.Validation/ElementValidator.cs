namespace TeamBond.Syntosa.Validation
{
    using System;
    using System.Collections.Generic;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel;
    using global::Syntosa.Core.ObjectModel.CoreClasses;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;
    using global::Syntosa.Core.ObjectModel.UtilityClasses;

    /// <summary>
    /// Provides validation methods for SYNTOSA <see cref="Element"/> types
    /// </summary>
    public class ElementValidator : SyntosaRecordValidatorBase<Element>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementValidator"/> class.
        /// </summary>
        public ElementValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementValidator"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to the Syntosa DAL.
        /// </param>
        public ElementValidator(string connectionString)
            : base(connectionString)
        {
        }

        /// <inheritdoc />
        public override Dictionary<string, string> GetPrototypeTypes()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override ValidationResult<Element> Validate(Element record)
        {
            var validationResults = new ValidationResult<Element>();

            switch (record.TypeItemUId.ToString().ToUpper())
            {
                case WorkforceManagement.Worker:
                    {
                        validationResults = this.Validate()
                    }
            }
        }

        /// <inheritdoc />
        public override Element GetPrototypeByTypeUId(string typeUId)
        {
            return ElementFactory.GetPrototype(typeUId, this._syntoDal);
        }

        /// <inheritdoc />
        public override Element GetPrototypeByTypeName(string typeName)
        {
            // Lookup type by name from an internal dictionary
            List<TypeItem> typeItems = this._syntoDal.GetTypeItemByAny(typeItemName: typeName);

            if (typeItems.Count > 1)
            {
                throw new Exception("Search returned more than one Type UId for string: " + typeItems);
            }

            if (typeItems.Count == 0)
            {
                throw new Exception("Search did not return any Type UId for string: " + typeName);
            }


            return ElementFactory.GetPrototype(typeItems[0].UId.ToString(), this._syntoDal);
        }

        /// <inheritdoc />
        public override Element GetPrototypeByUId(Guid uid)
        {
            Element element = this._syntoDal.GetElementByUId(uid);

            if (element != null)
            {
                // utility method, takes the primary syntosa record and n-number of collections to zero-out
                Utilities.SetSyntosaRecordIdsToZero(
                    element,
                    element.GlobalProperties.Values,
                    element.PrivatePropertyKeys,
                    element.PrivateProperties,
                    element.ElementEdges,
                    element.GlobalPropertyEdges);

                return element;
            }

            throw new Exception("Element UId: " + uid + " provided as a source for cloning does not exist");
        }

        private ValidationResult<Element> ValidateWorker(Element element)
        {
            var validationResult = new ValidationResult<Element>();

            // Check all global properties that need to be validated
            foreach (string key in element.GlobalProperties.Keys)
            {
                switch (element.GlobalProperties[key].TypeItemUId.ToString())
                {
                    case GlobalPropertyTypes.IsActive:
                        {
                            if(element.)
                        }
                }
            }
        }
    }
}