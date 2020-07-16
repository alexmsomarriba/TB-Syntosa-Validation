namespace TeamBond.Syntosa.Validation.Core.CodeWriter
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Pluralization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The json class generator.
    /// </summary>
    public class JsonClassGenerator : IJsonClassGeneratorConfig
    {
        /// <summary>
        /// The pluralization service.
        /// </summary>
        private EnglishPluralizationService pluralizationService = new EnglishPluralizationService();

        /// <summary>
        /// The a value indicating whether this instance of JsonClassGenerator has already been used.
        /// </summary>
        private bool used = false;

        private HashSet<string> names = new HashSet<string>();

        /// <summary>
        /// Gets or sets the target folder of the generated class.
        /// </summary>
        public string TargetFolder { get; set; }

        public string Example { get; set; }

        /// <inheritdoc />
        public bool AlwaysUseNullableValues { get; set; }

        /// <inheritdoc />
        public bool ApplyObfuscationAttributes { get; set; }

        /// <inheritdoc />
        public ICodeWriter CodeWriter { get; set; }

        /// <inheritdoc />
        public bool ExamplesInDocumentation { get; set; }

        /// <inheritdoc />
        public bool ExplicitDeserialization { get; set; }

        /// <inheritdoc />
        public bool HasHelperClass { get; set; }

        /// <inheritdoc />
        public bool HasSecondaryClasses => Types.Count > 1;

        /// <inheritdoc />
        public bool InternalVisibility { get; set; }

        /// <inheritdoc />
        public string MainClass { get; set; }

        /// <inheritdoc />
        public string Namespace { get; set; }

        /// <inheritdoc />
        public bool SingleFile { get; set; }

        /// <inheritdoc />
        public bool UseNamespaces => this.Namespace != null;

        /// <inheritdoc />
        public bool UseNestedClasses { get; set; }

        /// <inheritdoc />
        public bool UsePascalCase { get; set; }

        /// <inheritdoc />
        public bool UseProperties { get; set; }

        /// <summary>
        /// Gets the supported types.
        /// </summary>
        public IList<JsonType> Types { get; private set; }

        /// <summary>
        /// Generates a new classes using the <see cref="CodeWriter"/>.
        /// </summary>
        public void GenerateClasses()
        {
            if (this.CodeWriter == null)
            {
                this.CodeWriter = new CodeWriter();
            }

            if (this.ExplicitDeserialization && !(this.CodeWriter is CodeWriter))
            {
                throw new ArgumentException(
                    "Explicit deserialization is obsolete and is only supported by the C# provider.");
            }

            if (this.used)
            {
                throw new InvalidOperationException(
                    "This instance of JsonClassGenerator has already been used. Please create a new instance");
            }

            bool writeToDisk = string.IsNullOrWhiteSpace(this.TargetFolder);
            
            if (writeToDisk && !Directory.Exists(this.TargetFolder))
            {
                Directory.CreateDirectory(this.TargetFolder);
            }

            JObject[] examples;
            var example = this.Example.StartsWith("HTTP/")
                              ? this.Example.Substring(
                                  this.Example.IndexOf("\r\n\r\n", StringComparison.InvariantCultureIgnoreCase))
                              : this.Example;

            using (var stringReader = new StringReader(example))
            using (var reader = new JsonTextReader(stringReader))
            {
                JToken json = JToken.ReadFrom(reader);

                if (json is JArray)
                {
                    examples = ((JArray)json).Cast<JObject>().ToArray();
                }
                else if (json is JObject)
                {
                    examples = new[] { (JObject)json };
                }
                else
                {
                    throw new Exception("Sample JSON must be either a JSON array, or JSON object");
                }
            }

            this.Types = new List<JsonType>();
            this.names.Add(this.MainClass);
            var rootType = new JsonType(this, example[0])
                               {
                                   IsRoot = true
                               };
            rootType.AssignName(this.MainClass);
            this.GenerateClass(examples, rootType);
        }

        /// <summary>
        /// The generate class.
        /// </summary>
        /// <param name="examples">
        /// The examples.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        private void GenerateClass(JObject[] examples, JsonType type)
        {
            var jsonFields = new Dictionary<string, JsonType>();
            var fieldExamples = new Dictionary<string, IList<object>>();
            bool first = true;

            foreach (JObject obj in examples)
            {
                foreach (JProperty property in obj.Properties())
                {
                    JsonType fieldType;
                    var currentType = new JsonType(this, property.Value);
                    string propertyName = property.Name;
                    if (jsonFields.TryGetValue(propertyName, out fieldType))
                    {
                        var commonType = fieldType.GetCommonType(currentType);
                        jsonFields[propertyName] = commonType;
                    }
                    else
                    {
                        var commonType = currentType;
                        if (first)
                        {
                            commonType = commonType.MaybeMakeNullable(this);
                        }
                        else
                        {
                            commonType = commonType.GetCommonType(JsonType.GetNull(this));
                        }

                        jsonFields.Add(propertyName, commonType);
                        fieldExamples[propertyName] = new List<object>();
                    }

                    var fieldExample = fieldExamples[propertyName];
                    var value = property.Value;

                    if (value.Type == JTokenType.Null || value.Type == JTokenType.Undefined)
                    {
                        if (!fieldExample.Contains(null))
                        {
                            fieldExample.Insert(0, null);
                        }
                    }
                    else
                    {
                        var valueType = value.Type == JTokenType.Array || value.Type == JTokenType.Object
                                            ? value
                                            : value.Values<object>();

                        if (!fieldExample.Any(x => valueType.Equals(x)))
                        {
                            fieldExample.Add(valueType);
                        }
                    }
                }

                first = false;
            }

            if (this.UseNestedClasses)
            {
                foreach (var field in jsonFields)
                {
                    this.names.Add(field.Key.ToLower());
                }
            }

            foreach (var field in jsonFields)
            {
                var filedType = field.Value;

                if (filedType.Type == SupportedJsonTypes.Object)
                {
                    var subExamples = new List<JObject>(examples.Length);
                    foreach (var obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Object)
                            {
                                subExamples.Add((JObject)value);
                            }
                        }
                    }

                    filedType.AssignName(this.CreateUniqueClassName(field.Key));
                    this.GenerateClass(subExamples.ToArray(), filedType);
                }

                if (filedType.InternalType != null && filedType.InternalType.Type == SupportedJsonTypes.Object)
                {
                    var subExamples = new List<JObject>(examples.Length);
                    foreach (var obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Array)
                            {
                                foreach (var item in (JArray)value)
                                {
                                    if (!(item is JObject))
                                    {
                                        throw new NotSupportedException(
                                            "Arrays of non-objects are not supported yet");
                                    }

                                    subExamples.Add((JObject)item);
                                }
                            }
                        }
                    }

                    field.Value.InternalType.AssignName(this.CreateUniqueClassNameFromPlural(field.Key));
                    this.GenerateClass(subExamples.ToArray(), field.Value.InternalType);
                }
            }

            type.Fields = jsonFields
                .Select(x => new FieldInfo(this, x.Key, x.Value, this.UsePascalCase, fieldExamples[x.Key])).ToArray();

            this.Types.Add(type);
        }

        private string CreateUniqueClassName(string name)
        {
            name = ToTitleCase(name);

            string finalName = name;
            int i = 2;
            while (this.names.Any(x => x.Equals(finalName, StringComparison.OrdinalIgnoreCase)))
            {
                finalName = name + i;
                i++;
            }

            this.names.Add(finalName);
            return finalName;
        }

        private string CreateUniqueClassNameFromPlural(string plural)
        {
            plural = ToTitleCase(plural);
            return CreateUniqueClassName(this.pluralizationService.Singularize(plural));
        }

        internal static string ToTitleCase(string str)
        {
            var stringBuilder = new StringBuilder(str.Length);
            bool flag = true;

            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (char.IsLetterOrDigit(c))
                {
                    stringBuilder.Append(flag ? char.ToUpper(c) : c);
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }

            return stringBuilder.ToString();
        }
    }
}