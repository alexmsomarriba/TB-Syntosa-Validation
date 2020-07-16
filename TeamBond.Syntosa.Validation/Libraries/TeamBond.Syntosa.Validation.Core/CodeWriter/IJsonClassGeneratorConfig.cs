namespace TeamBond.Syntosa.Validation.Core.CodeWriter
{
    /// <summary>
    /// Defines needed 
    /// </summary>
    public interface IJsonClassGeneratorConfig
    {
        /// <summary>
        /// Gets or sets the namespace of the generated class.
        /// </summary>
        string Namespace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use properties.
        /// </summary>
        bool UseProperties { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the generated class has internal visibility.
        /// </summary>
        bool InternalVisibility { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use explicit deserialization.
        /// </summary>
        bool ExplicitDeserialization { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the generated class has any associated helper classes.
        /// </summary>
        bool HasHelperClass { get; set; }

        /// <summary>
        /// Gets or sets the main class.
        /// </summary>
        string MainClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use pascal case.
        /// </summary>
        bool UsePascalCase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use nested classes.
        /// </summary>
        bool UseNestedClasses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to apply obfuscation attributes to the class.
        /// </summary>
        bool ApplyObfuscationAttributes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether single file.
        /// </summary>
        bool SingleFile { get; set; }

        /// <summary>
        /// Gets or sets the code writer.
        /// </summary>
        ICodeWriter CodeWriter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the generated class has secondary classes.
        /// </summary>
        bool HasSecondaryClasses { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to always use nullable values.
        /// </summary>
        bool AlwaysUseNullableValues { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use namespaces.
        /// </summary>
        bool UseNamespaces { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to have examples in documentation.
        /// </summary>
        bool ExamplesInDocumentation { get; set; }
    }
}