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

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementValidator"/> class.
        /// </summary>
        /// <param name="syntosaDal">
        /// The syntosa dal.
        /// </param>
        public ElementValidator(SyntosaDal syntosaDal)
            : base(syntosaDal)
        {
        }

        /// <inheritdoc />
        public override Dictionary<string, string> GetPrototypeTypes()
        {
            return new Dictionary<string, string>();
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

                case WorkforceManagement.Resume:
                    {
                        validationResults = this.ValidateResume(record);
                        break;
                    }

                case WorkforceManagement.JobPosting:
                    {
                        validationResults = this.ValidateJobPosting(record);
                        break;
                    }

                case WorkforceManagement.Employer:
                    {
                        validationResults = this.ValidateEmployer(record);
                        break;
                    }

                case WorkforceManagement.Division:
                    {
                        validationResults = this.ValidateDivision(record);
                        break;
                    }
            }

            return validationResults;
        }

        /// <inheritdoc />
        public override Element GetPrototypeByTypeUId(string typeUId)
        {
            return ElementFactory.GetPrototype(typeUId, this.syntoDal);
        }

        /// <inheritdoc />
        public override Element GetPrototypeByTypeName(string typeName)
        {
            // Lookup type by name from an internal dictionary
            List<TypeItem> typeItems = this.syntoDal.GetTypeItemByAny(typeItemName: typeName);

            if (typeItems.Count > 1)
            {
                throw new Exception("Search returned more than one Type UId for string: " + typeItems);
            }

            if (typeItems.Count == 0)
            {
                throw new Exception("Search did not return any Type UId for string: " + typeName);
            }

            return ElementFactory.GetPrototype(typeItems[0].UId.ToString(), this.syntoDal);
        }

        /// <inheritdoc />
        public override Element GetPrototypeByUId(Guid uid)
        {
            Element element = this.syntoDal.GetElementByUId(uid);

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

            Element edgeTargetElement = this.syntoDal.GetElementByUId(
                edgeElementElement.TargetElementUId,
                asComposite: false);

            return edgeTargetElement?.TypeItemUId.ToString();
        }

        /// <summary>
        /// Validates the given Division <paramref name="element"/>
        /// </summary>
        /// <param name="element">
        /// The element to validate
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult{Element}"/> indicating whether or not the validation succeeded.
        /// </returns>
        private ValidationResult<Element> ValidateDivision(Element element)
        {
            var validationResult = new ValidationResult<Element>();

            ElementGlobalProperty divisionAcronym = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.DivisionAcronym,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(divisionAcronym.Attribute))
            {
                validationResult.MemberNames.Add(divisionAcronym.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{divisionAcronym.Name} was missing a value"));
            }

            ElementGlobalProperty divisionLeadName = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.DivisionLeadName,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(divisionLeadName.Attribute))
            {
                validationResult.MemberNames.Add(divisionLeadName.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{divisionLeadName.Name} was missing a value"));
            }

            ElementGlobalProperty divisionLeadContactEmail = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.DivisionLeadContactEmail,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(divisionLeadContactEmail.Attribute))
            {
                validationResult.MemberNames.Add(divisionLeadContactEmail.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{divisionLeadContactEmail.Name} was missing a value"));
            }

            // Validate mandatory edges
            var edges = new List<EdgeClass>
                            {
                                new EdgeClass
                                    {
                                        Id = 1,
                                        TypeFunctionUId = null,
                                        TypeItemUId = WorkforceManagement.Employer,
                                        EdgeElementUId = EdgeTypes.DependsOn,
                                        EdgeCount = 0,
                                        TypeName = "Employer",
                                        EdgeName = "Depends On"
                                    }
                            };

            this.ValidateMandatoryEdges(element, edges, ref validationResult);

            validationResult.Success = validationResult.Exceptions.Count == 0;
            return validationResult;
        }

        /// <summary>
        /// Validates the given Employer <paramref name="element"/>
        /// </summary>
        /// <param name="element">
        /// The element to validate
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult{Element}"/> indicating whether or not the validation succeeded.
        /// </returns>
        private ValidationResult<Element> ValidateEmployer(Element element)
        {
            var validationResult = new ValidationResult<Element>();

            ElementGlobalProperty employerName = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.EmployerName,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(employerName.Attribute))
            {
                validationResult.MemberNames.Add(employerName.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{employerName.Name} was missing a value"));
            }

            ElementGlobalProperty employerContactName = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.EmployerContactName,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(employerContactName.Attribute))
            {
                validationResult.MemberNames.Add(employerContactName.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{employerContactName.Name} was missing a value"));
            }

            ElementGlobalProperty employerAddress = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.EmployerAddress,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(employerAddress.Attribute))
            {
                validationResult.MemberNames.Add(employerAddress.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{employerAddress.Name} was missing a value"));
            }

            validationResult.Success = validationResult.Exceptions.Count == 0;
            return validationResult;
        }

        /// <summary>
        /// Validates the given Team <paramref name="element"/>
        /// </summary>
        /// <param name="element">
        /// The element to validate
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult{Element}"/> indicating whether or not the validation succeeded.
        /// </returns>
        private ValidationResult<Element> ValidateTeam(Element element)
        {
            var validationResult = new ValidationResult<Element>();

            ElementGlobalProperty teamAcronym = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.TeamAcronym,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(teamAcronym.Attribute))
            {
                validationResult.MemberNames.Add(teamAcronym.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{teamAcronym.Name} was missing a value"));
            }

            ElementGlobalProperty teamLeadName = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.TeamLeadName,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(teamLeadName.Attribute))
            {
                validationResult.MemberNames.Add(teamLeadName.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{teamLeadName.Name} was missing a value"));
            }

            ElementGlobalProperty teamLeadContactEmail = element.GlobalProperties.FirstOrDefault(
                property => property.Value.TypeItemUId.ToString().Equals(
                    GlobalPropertyTypes.TeamLeadContactEmail,
                    StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrWhiteSpace(teamLeadContactEmail.Attribute))
            {
                validationResult.MemberNames.Add(teamLeadContactEmail.Attribute);
                validationResult.Exceptions.Add(
                    new Exception($"{teamLeadContactEmail.Name} was missing a value"));
            }

            // Validate mandatory edges
            var edges = new List<EdgeClass>
                            {
                                new EdgeClass
                                    {
                                        Id = 1,
                                        TypeFunctionUId = null,
                                        TypeItemUId = WorkforceManagement.Division,
                                        EdgeElementUId = EdgeTypes.BelongsTo,
                                        EdgeCount = 0,
                                        TypeName = "Division",
                                        EdgeName = "Belongs To"
                                    }
                            };

            this.ValidateMandatoryEdges(element, edges, ref validationResult);

            validationResult.Success = validationResult.Exceptions.Count == 0;
            return validationResult;
        }

        /// <summary>
        /// Validates the given worker element.
        /// </summary>
        /// <param name="element">
        /// The element to validate.
        /// </param>
        /// <returns>
        /// The validated element.
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
                                        EdgeName = "Enabled By"
                                    }
                            };

            this.ValidateMandatoryEdges(element, edges, ref validationResult);

            validationResult.Success = validationResult.Exceptions.Count == 0;
            return validationResult;
        }

        /// <summary>
        /// Validates the given resume element.
        /// </summary>
        /// <param name="element">
        /// The element to validate.
        /// </param>
        /// <returns>
        /// The validated element.
        /// </returns>
        private ValidationResult<Element> ValidateResume(Element element)
        {
            var validationResult = new ValidationResult<Element>();

            // Validate mandatory edges
            var edges = new List<EdgeClass>
                            {
                                new EdgeClass
                                    {
                                        Id = 1,
                                        TypeFunctionUId = null,
                                        TypeItemUId = WorkforceManagement.Worker,
                                        EdgeElementUId = EdgeTypes.AssignedTo,
                                        EdgeCount = 0,
                                        TypeName = "Worker",
                                        EdgeName = "Assigned To"
                                    },

                                new EdgeClass
                                    {
                                        Id = 2,
                                        TypeFunctionUId = null,
                                        TypeItemUId = WorkforceManagement.JobPosting,
                                        EdgeElementUId = EdgeTypes.AssignedTo,
                                        EdgeCount = 0,
                                        TypeName = "Job Posting",
                                        EdgeName = "Assigned To"
                                    }
                            };

            this.ValidateMandatoryEdges(element, edges, ref validationResult);
            return validationResult;
        }

        /// <summary>
        /// Validates the given resume element.
        /// </summary>
        /// <param name="element">
        /// The element to validate.
        /// </param>
        /// <returns>
        /// The validated element.
        /// </returns>
        private ValidationResult<Element> ValidateJobPosting(Element element)
        {
            var validationResult = new ValidationResult<Element>();

            // Validate mandatory edges
            var edges = new List<EdgeClass>
                            {
                                new EdgeClass
                                    {
                                        Id = 1,
                                        TypeFunctionUId = null,
                                        TypeItemUId = WorkforceManagement.Job,
                                        EdgeElementUId = EdgeTypes.DependsOn,
                                        EdgeCount = 0,
                                        TypeName = "Job",
                                        EdgeName = "Depends On"
                                    }
                            };

            this.ValidateMandatoryEdges(element, edges, ref validationResult);
            return validationResult;
        }

        /// <summary>
        /// The edge class matches edge element element.
        /// </summary>
        /// <param name="edgeClass">
        /// The edge class.
        /// </param>
        /// <param name="edgeElementElement">
        /// The edge element element.
        /// </param>
        /// <param name="edgeElementTypeItemUId">
        /// The edge element type item u id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool EdgeClassMatchesEdgeElementElement(
            EdgeClass edgeClass,
            EdgeElementElement edgeElementElement,
            Guid edgeElementTypeItemUId)
        {
            if (edgeClass.TypeFunctionUId != null && edgeElementTypeItemUId != Guid.Empty)
            {
                TypeItem typeItem = this.syntoDal.GetTypeItemByUId(edgeElementTypeItemUId);

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
                if (string.Equals(edgeClass.TypeItemUId, edgeElementTypeItemUId.ToString().ToUpperInvariant()) && string.Equals(
                        edgeClass.EdgeElementUId,
                        edgeElementElement.TypeItemUId.ToString().ToUpperInvariant()))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The validate mandatory edges.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="mandatoryEdges">
        /// The mandatory edges.
        /// </param>
        /// <param name="validationResult">
        /// The validation result.
        /// </param>
        /// <param name="blockedEdges">
        /// The blocked edges.
        /// </param>
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
                            $"Component '{element.Name}' is missing or contains inactive mandatory edge for '{mandatoryEdge.TypeName}'."));
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