namespace TeamBond.Syntosa.Validation
{
    using System.Collections.Generic;

    using global::Syntosa.Core.ObjectModel.CoreClasses.Edge;

    /// <summary>
    /// Object to represent a Syntosa <see cref="EdgeElementElement"/>
    /// </summary>
    internal class EdgeClass
    {
        /// <summary>
        /// Gets or sets the edge count
        /// </summary>
        public int EdgeCount { get; set; }

        /// <summary>
        /// Gets or sets the edge element unique identifier
        /// </summary>
        public string EdgeElementUId { get; set; }

        /// <summary>
        /// Gets or sets edge name
        /// </summary>
        public string EdgeName { get; set; }

        /// <summary>
        /// Gets or sets a collection of <see cref="EdgeElementElementProperty"/> for the element
        /// </summary>
        public List<EdgeElementElementProperty> EdgeProperties { get; set; }

        /// <summary>
        /// Gets or sets the identifier 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type function unique identifier
        /// </summary>
        public string TypeFunctionUId { get; set; }

        /// <summary>
        /// Gets or sets the type item uid.
        /// </summary>
        public string TypeItemUId { get; set; }

        /// <summary>
        /// Gets or sets the edge type name.
        /// </summary>
        public string TypeName { get; set; }
    }
}