namespace TeamBond.Syntosa.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel;
    using global::Syntosa.Core.ObjectModel.CoreClasses;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Edge;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;
    using global::Syntosa.Core.ObjectModel.UtilityClasses;

    /// <summary>
    /// Provides validation methods for SYNTOSA <see cref="Element"/> types
    /// </summary>
    public class ElementValidator : SyntosaRecordValidatorBase<Element>
    {
        /// <summary>
        /// The _synto dal.
        /// </summary>
        private SyntosaDal _syntoDal;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementValidator"/> class.
        /// </summary>
        /// <param name="syntoDal">
        /// The synto Dal.
        /// </param>
        public ElementValidator(SyntosaDal syntoDal)
        {
            this._syntoDal = syntoDal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementValidator"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to the Syntosa DAL.
        /// </param>
        public ElementValidator(string connectionString, SyntosaDal syntoDal)
            : base(connectionString)
        {
            this._syntoDal = syntoDal;
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
                        validationResults = this.ValidateWorker(record);
                        break;
                    }
            }

            return validationResults;
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

        /// <summary>
        /// Retrieves the type item unique identifier for the provided <see cref="EdgeElementElement"/>
        /// </summary>
        /// <param name="edgeElementElement">
        /// The <see cref="EdgeElementElement"/> used to derive the type item unique identifier
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value representing the edge element element type item unique identifier
        /// </returns>
        private string GetEdgeElementTargetElementTypeUId(EdgeElementElement edgeElementElement)
        {
            if (edgeElementElement.TargetElementTypeUId != Guid.Empty)
            {
                return edgeElementElement.TargetElementTypeUId.ToString();
            }

            Element edgeTargetElement = this._syntoDal.GetElementByUId(
                edgeElementElement.TargetElementUId,
                asComposite: false);

            return edgeTargetElement?.TypeItemUId.ToString();
        }

        /// <summary>
        /// The validate worker.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        private ValidationResult<Element> ValidateWorker(Element element)
        {
            var validationResult = new ValidationResult<Element>();

            // Validate mandatory edges
            var edges = new List<EdgeClass>
                            {
                                new EdgeClass
                                    {
                                        Id = 1,
                                        TypeFunctionUId = null,
                                        TypeItemUId = WorkforceManagement.Resume,
                                        EdgeElementUId = EdgeTypes.EnabledBy,
                                        EdgeCount = 0,
                                        TypeName = "Resume",
                                        EdgeName = "Assigned To"
                                    }
                            };

            this.ValidateMandatoryEdges(element, edges, ref validationResult);

            validationResult.Success = validationResult.Exceptions.Count == 0;
            return validationResult;
        }

        private bool EdgeClassMatchesEdgeElementElement(
            EdgeClass edgeClass,
            EdgeElementElement edgeElementElement,
            Guid edgeElementTypeItemUId)
        {
            if (edgeClass.TypeFunctionUId != null && edgeElementTypeItemUId != Guid.Empty)
            {
                TypeItem typeItem = this._syntoDal.GetTypeItemByUId(edgeElementTypeItemUId);

                if (typeItem is null)
                {
                    return false;
                }

                if (string.Equals(edgeClass.TypeItemUId, edgeElementTypeItemUId.ToString()) && string.Equals(
                        edgeClass.EdgeElementUId,
                        edgeElementElement.TypeItemUId.ToString()))
                {
                    return true;
                }
            }
            else
            {
                if (string.Equals(edgeClass.TypeItemUId, edgeElementTypeItemUId.ToString()) && string.Equals(
                        edgeClass.EdgeElementUId,
                        edgeElementElement.TypeItemUId.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private void ValidateMandatoryEdges(
            Element element,
            List<EdgeClass> mandatoryEdges,
            ref ValidationResult<Element> validationResult,
            List<EdgeClass> blockedEdges = null)
        {
            if (element.ElementEdges.Count < mandatoryEdges.Count)
            {
                validationResult.MemberNames.Add(element.Name);
                validationResult.Exceptions.Add(
                    new Exception($"Component: '{element.Name}' is missing one or more mandatory edge associations."));
            }

            foreach (var mandatoryEdge in mandatoryEdges)
            {
                List<EdgeElementElement> elementActiveMandatoryEdges = element.ElementEdges.Where(
                    edgeElement => edgeElement.TargetElementTypeUId != Guid.Empty && !edgeElement.IsDeleted
                                   && this.EdgeClassMatchesEdgeElementElement(
                                       mandatoryEdge,
                                       edgeElement,
                                       Guid.Parse(this.GetEdgeElementTargetElementTypeUId(edgeElement)))).ToList();

                if (!elementActiveMandatoryEdges.Any())
                {
                    validationResult.Exceptions.Add(
                        new Exception(
                            $"Component 'element.Name' is missing or contains inactive mandatory edge for '{mandatoryEdge.TypeName}'."));
                }

                if (mandatoryEdge.EdgeProperties is null)
                {
                    continue;
                }

                List<EdgeElementElementProperty> mandatoryEdgeProperties =
                    mandatoryEdge.EdgeProperties.Where(edgeProperty => edgeProperty != null).ToList();

                if (!mandatoryEdgeProperties.Any())
                {
                    continue;
                }

                foreach (var mandatoryEdgeProperty in mandatoryEdgeProperties)
                {
                    List<EdgeElementElement> activeMandatoryEdgeWithMandatoryProperty = elementActiveMandatoryEdges
                        .Where(
                            edgeElement => edgeElement.TargetElementTypeUId != Guid.Empty && !edgeElement.IsDeleted
                                           && this.EdgeClassMatchesEdgeElementElement(
                                               mandatoryEdge,
                                               edgeElement,
                                               Guid.Parse(this.GetEdgeElementTargetElementTypeUId(edgeElement))))
                        .ToList();

                    if (!activeMandatoryEdgeWithMandatoryProperty.Any())
                    {
                        validationResult.Exceptions.Add(
                            new Exception(
                                $"Component '{element.Name}' is missing or contains inactive mandatory edge properties for '{mandatoryEdgeProperty.Name}'."));
                    }
                }
            }

            if (blockedEdges != null)
            {
                foreach (var blockedEdge in blockedEdges)
                {
                    List<EdgeElementElement> elementBlockedEdges = element.ElementEdges.Where(
                        edgeElement => edgeElement.TargetElementTypeUId != Guid.Empty
                                       && this.EdgeClassMatchesEdgeElementElement(
                                           blockedEdge,
                                           edgeElement,
                                           Guid.Parse(this.GetEdgeElementTargetElementTypeUId(edgeElement)))).ToList();

                    if (elementBlockedEdges.Count >= 1)
                    {
                        validationResult.Exceptions.Add(
                            new Exception(
                                $"Component: {element.Name} cannot have an association (of type: {blockedEdge.EdgeName} to a component of type {blockedEdge.TypeName}"));
                    }
                }
            }
        }
    }
}