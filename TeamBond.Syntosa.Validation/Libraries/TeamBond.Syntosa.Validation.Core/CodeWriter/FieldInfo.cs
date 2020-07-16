namespace TeamBond.Syntosa.Validation.Core.CodeWriter
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the methods for code generation based on fields.
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// The generator config.
        /// </summary>
        private IJsonClassGeneratorConfig generatorConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldInfo"/> class.
        /// </summary>
        /// <param name="generatorConfig">
        /// The generator config.
        /// </param>
        /// <param name="jsonMemberName">
        /// The json member name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="usePascalCase">
        /// The use pascal case.
        /// </param>
        /// <param name="examples">
        /// The examples.
        /// </param>
        public FieldInfo(
            IJsonClassGeneratorConfig generatorConfig,
            string jsonMemberName,
            JsonType type,
            bool usePascalCase,
            IList<object> examples)
        {
            this.generatorConfig = generatorConfig;
            this.JsonMemberName = jsonMemberName;
            this.MemberName = jsonMemberName;
            if (usePascalCase)
            {
                this.MemberName = JsonClassGenerator.ToTitleCase(this.MemberName);
            }

            this.Type = type;
            this.Examples = examples;
        }

        /// <summary>
        /// Gets the member name.
        /// </summary>
        public string MemberName { get; private set; }

        /// <summary>
        /// Gets the json member name.
        /// </summary>
        public string JsonMemberName { get; private set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public JsonType Type { get; set; }

        /// <summary>
        /// Gets the examples.
        /// </summary>
        public IList<object> Examples { get; private set; }

        /// <summary>
        /// Builds the command needed for code generation.
        /// </summary>
        /// <param name="jsonObject">
        /// The json object.
        /// </param>
        /// <returns>
        /// The command needed for code generation.
        /// </returns>
        public string GetGenerationCode(string jsonObject)
        {
            FieldInfo field = this;

            if (field.Type.Type == SupportedJsonTypes.Array)
            {
                JsonType innermost = field.Type.GetInnerMostType();

                return
                    $"({field.Type.GetTypeName()})JsonClassHelper.ReadArray<{innermost.GetTypeName()}>(JsonClassHelper.GetJToken<JArray>({jsonObject}, \"{field.JsonMemberName}\"), JsonClassHelper.{innermost.GetReaderName()}, typeof({field.Type.GetTypeName()}))";
            }
            else if (field.Type.Type == SupportedJsonTypes.Dictionary)
            {
                return
                    $"({field.Type.GetTypeName()})JsonClassHelper.ReadDictionary<{field.Type.InternalType.GetTypeName()}>(JsonClassHelper.GetJToken<JObject>({jsonObject}, \"{field.JsonMemberName}\"))";
            }
            else
            {
                return
                    $"JsonClassHelper.{field.Type.GetReaderName()}(JsonClassHelper.GetJToken<{field.Type.GetJTokenType()}>({jsonObject}, \"{field.JsonMemberName}\"))";
            }
        }

        /// <summary>
        /// Converts the example json into a string.
        /// </summary>
        /// <returns>
        /// The example json as a string.
        /// </returns>
        public string GetExamplesText()
        {
            return string.Join(", ", this.Examples.Take(5).Select(JsonConvert.SerializeObject).ToArray());
        }
    }
}