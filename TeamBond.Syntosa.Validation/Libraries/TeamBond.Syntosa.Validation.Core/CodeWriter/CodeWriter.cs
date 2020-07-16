namespace TeamBond.Syntosa.Validation.Core.CodeWriter
{
    using System;
    using System.IO;

    /// <summary>
    /// The code writer.
    /// </summary>
    public class CodeWriter : ICodeWriter
    {
        /// <summary>
        /// The no rename attribute.
        /// </summary>
        private const string NoRenameAttribute = "[Obfuscation(Feature = \"remaing\", Eclude = true)]";

        /// <summary>
        /// The no prune attribute.
        /// </summary>
        private const string NoPruneAttribute = "[Obfuscation(Feature = \"trigger\", Exclude = false)]";

        /// <inheritdoc />
        public string DisplayName => "C#";

        /// <inheritdoc />
        public string FileExtension => ".cs";

        /// <inheritdoc />
        public string GetTypeName(JsonType type, IJsonClassGeneratorConfig config)
        {
            bool arraysAsLists = !config.ExplicitDeserialization;

            switch (type.Type)
            {
                case SupportedJsonTypes.Anything: return "object";
                case SupportedJsonTypes.Array:
                    return arraysAsLists
                               ? $"IList<{this.GetTypeName(type.InternalType, config)}>"
                               : this.GetTypeName(type.InternalType, config) + "[]";
                case SupportedJsonTypes.Dictionary:
                    return $"Dictionary<string, {GetTypeName(type.InternalType, config)}>";
                case SupportedJsonTypes.Boolean: return "bool";
                case SupportedJsonTypes.Float: return "double";
                case SupportedJsonTypes.Integer: return "int";
                case SupportedJsonTypes.Long: return "long";
                case SupportedJsonTypes.Date: return "DateTime";
                case SupportedJsonTypes.NonConstrained: return "object";
                case SupportedJsonTypes.NullableBoolean: return "bool?";
                case SupportedJsonTypes.NullableFloat: return "double?";
                case SupportedJsonTypes.NullableInteger: return "int?";
                case SupportedJsonTypes.NullableLong: return "long?";
                case SupportedJsonTypes.NullableDate: return "DateTime?";
                case SupportedJsonTypes.NullableSomething: return "object";
                case SupportedJsonTypes.Object: return type.AssignedName;
                case SupportedJsonTypes.String: return "string";
                default: throw new NotSupportedException("Unsupported json type"); 
            }
        }

        /// <inheritdoc />
        public void WriteClass(IJsonClassGeneratorConfig config, TextWriter stringWriter, JsonType type)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void WriteFileEnd(IJsonClassGeneratorConfig config, TextWriter stringWriter)
        {
            if (config.UseNamespaces)
            {
            }
        }

        /// <inheritdoc />
        public void WriteFileStart(IJsonClassGeneratorConfig config, TextWriter stringWriter)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void WriteNamespaceEnd(IJsonClassGeneratorConfig config, TextWriter stringWriter, bool root)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void WriteNamespaceStart(IJsonClassGeneratorConfig config, TextWriter stringWriter, bool root)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value indicating whether the no renaming attribute should be applied.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <returns>
        /// A value indicating whether the no renaming attribute should be applied.
        /// </returns>
        private bool ShouldApplyNoRenamingAttribute(IJsonClassGeneratorConfig config)
        {
            return config.ApplyObfuscationAttributes && !config.ExplicitDeserialization && !config.UsePascalCase;
        }

        /// <summary>
        /// Gets a value indicating whether the no prune attribute should be applied.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <returns>
        /// A value indicating whether the no prune attribute should be applied.
        /// </returns>
        private bool ShouldApplyNoPruneAttribute(IJsonClassGeneratorConfig config)
        {
            return config.ApplyObfuscationAttributes && !config.ExplicitDeserialization && config.UseProperties;
        }
    }
}