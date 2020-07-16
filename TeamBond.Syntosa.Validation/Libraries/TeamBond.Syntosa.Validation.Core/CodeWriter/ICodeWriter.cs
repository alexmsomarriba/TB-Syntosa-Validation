namespace TeamBond.Syntosa.Validation.Core.CodeWriter
{
    using System.IO;

    /// <summary>
    /// Defines methods for .
    /// </summary>
    public interface ICodeWriter
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// The get type name.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetTypeName(JsonType type, IJsonClassGeneratorConfig config);

        /// <summary>
        /// Writes the class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="stringWriter">
        /// The string writer.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        void WriteClass(IJsonClassGeneratorConfig config, TextWriter stringWriter, JsonType type);

        /// <summary>
        /// Writes the file start.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="stringWriter">
        /// The string writer.
        /// </param>
        void WriteFileStart(IJsonClassGeneratorConfig config, TextWriter stringWriter);

        /// <summary>
        /// Writes the file end.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="stringWriter">
        /// The string writer.
        /// </param>
        void WriteFileEnd(IJsonClassGeneratorConfig config, TextWriter stringWriter);

        /// <summary>
        /// Writes the namespace start.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="stringWriter">
        /// The string writer.
        /// </param>
        /// <param name="root">
        /// The root.
        /// </param>
        void WriteNamespaceStart(IJsonClassGeneratorConfig config, TextWriter stringWriter, bool root);

        /// <summary>
        /// Writes the namespace end.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="stringWriter">
        /// The string writer.
        /// </param>
        /// <param name="root">
        /// The root.
        /// </param>
        void WriteNamespaceEnd(IJsonClassGeneratorConfig config, TextWriter stringWriter, bool root);
    }
}