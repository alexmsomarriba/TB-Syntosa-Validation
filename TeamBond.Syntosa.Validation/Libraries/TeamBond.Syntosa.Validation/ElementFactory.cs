namespace TeamBond.Syntosa.Validation
{
    using System;

    using global::Syntosa.Core.DataAccessLayer;
    using global::Syntosa.Core.ObjectModel.CoreClasses;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Edge;
    using global::Syntosa.Core.ObjectModel.CoreClasses.Element;
    using global::Syntosa.Core.ObjectModel.Enums;

    /// <summary>
    /// Generates an empty, fully 
    /// </summary>
    public class ElementFactory
    {
        /// <summary>
        /// Gets the element prototype of a given type.
        /// </summary>
        /// <param name="type">
        /// The type to get the prototype of.
        /// </param>
        /// <param name="syntosaDal">
        /// The syntosa data access layer.
        /// </param>
        /// <returns>
        /// The element prototype of a given type.
        /// </returns>
        public static Element GetPrototype(string type, SyntosaDal syntosaDal)
        {
            Element element = new Element()
                                  {
                                      Name = string.Empty,
                                      Alias = string.Empty,
                                      Description = string.Empty,
                                      ModuleRecordKey = string.Empty,
                                      TypeUIdRecordStatus = new Guid(RecordStatuses.Active),
                                      ModuleUId = Helper.GetModule(null),
                                      DomainUId = new Guid(Domain.TeamBond),
                                      ModifiedBy = "API User",
                                      IsActive = true
                                  };

            // Build new fully composite mock elements
            switch (type.ToUpperInvariant())
            {
                case WorkforceManagement.Candidate:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Candidate);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);

                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.MemberOf)
                                });
                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.EnabledBy)
                                });
                        break;
                    }

                case WorkforceManagement.Broker:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Broker);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);

                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.AssignedTo)
                                });
                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.BelongsTo)
                                });
                        break;
                    }

                case WorkforceManagement.Department:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Department);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);
                        
                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.BelongsTo)
                                });
                        break;
                    }

                case WorkforceManagement.Division:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Division);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);

                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.DependsOn),
                                    LabelUId = new Guid(EdgeLabelTypes.Employer),
                                    LabelName = syntosaDal.GetTypeItemByUId(new Guid(EdgeLabelTypes.Employer)).Name
                                });

                        break;
                    }

                case WorkforceManagement.Team:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Team);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        GenerateGlobalProperties(element, syntosaDal);
                        
                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.BelongsTo),
                                    LabelUId = new Guid(EdgeLabelTypes.Division),
                                    LabelName = syntosaDal.GetTypeItemByUId(new Guid(EdgeLabelTypes.Division)).Name
                                });
                        break;
                    }

                case WorkforceManagement.Squad:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Squad);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        GenerateGlobalProperties(element, syntosaDal);

                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.BelongsTo),
                                    LabelUId = new Guid(EdgeLabelTypes.Team),
                                    LabelName = syntosaDal.GetTypeItemByUId(new Guid(EdgeLabelTypes.Team)).Name
                                });

                        break;
                    }

                case WorkforceManagement.Employer:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Employer);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);

                        break;
                    }

                case WorkforceManagement.Job:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Job);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);

                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.BelongsTo)
                                });

                        break;
                    }

                case WorkforceManagement.Employee:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Employee);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);

                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.BelongsTo),
                                });
                        break;
                    }

                case WorkforceManagement.Worker:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.Worker);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);

                        element.ElementEdges.Add(
                            new EdgeElementElement
                                {
                                    TypeItemUId = new Guid(EdgeTypes.EnabledBy)
                                });

                        break;
                    }

                case WorkforceManagement.JobPosting:
                    {
                        element.TypeItemUId = new Guid(WorkforceManagement.JobPosting);
                        element.ModuleUId = Helper.GetModule(element.TypeItemUId);

                        // Get all global properties associated with the element type
                        GenerateGlobalProperties(element, syntosaDal);

                        element.ElementEdges.Add(
                            new EdgeElementElement
                            {
                                TypeItemUId = new Guid(EdgeTypes.DependsOn)
                            });

                        break;
                    }

                default:
                    {
                        throw new Exception("Unknown Element Type provided.");
                    }
            }

            return element;
        }

        /// <summary>
        /// Generates required global properties for the provided element
        /// </summary>
        /// <param name="element">
        /// The SYNTOSA <see cref="Element"/>
        /// </param>
        /// <param name="syntosaDal">
        /// The SYNTOSA <see cref="SyntosaDal"/>
        /// </param>
        private static void GenerateGlobalProperties(Element element, SyntosaDal syntosaDal)
        {
            var globalPropertiesTypeFunctionUid = new Guid(TypeFunction.GlobalProperties);

            // Retrieve all global properties based on the type item hierarchy
            TypeItem typeItem = syntosaDal.GetTypeItemHierarchy(element.TypeItemUId, HierarchyPath.Down);

            foreach (TypeItem item in typeItem.Children)
            {
                if (item.TypeFunctionUId != globalPropertiesTypeFunctionUid)
                {
                    continue;
                }

                var elementGlobalProperty = new ElementGlobalProperty
                                                {
                                                    TypeItemUId = item.UId,
                                                    Name = item.Name,
                                                    Attribute = string.Empty,
                                                    ModuleUIdAutoCollect = element.ModuleUId
                                                };

                element.GlobalProperties.Add(elementGlobalProperty.NameAsKey, elementGlobalProperty);
                element.GlobalPropertyList.Add(elementGlobalProperty);
            }
        }
    }
}