namespace TeamBond.Syntosa.Validation.DataEditor.Services
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json;

    using NJsonSchema;
    using NJsonSchema.CodeGeneration.CSharp;

    /// <summary>
    /// The json to class converter.
    /// </summary>
    public class JsonToClassConverter
    {
        /// <summary>
        /// The generate.
        /// </summary>
        /// <param name="json">
        /// The json.
        /// </param>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Generate(string json, CSharpGeneratorSettings settings, string name)
        {
            var elementSchema = JsonSchema.FromFileAsync(
                    "Y:\\Work\\TeamBond\\teambond-syntosa-validation\\TeamBond.Syntosa.Validation\\Tools\\TeamBond.Syntosa.Validation.DataEditor\\Services\\Element.schema.json")
                .Result;

            var path =
                $"Y:\\Work\\TeamBond\\teambond-syntosa-validation\\TeamBond.Syntosa.Validation\\Tools\\TeamBond.Syntosa.Validation.DataEditor\\GeneratedClasses\\{name}.cs";

            var errors = elementSchema.Validate(json);

            if (errors != null && errors.Any())
            {
                var defaultTemplate = "Invalid JSON document. Please fix these errors:";
                var sb = new StringBuilder();
                sb.AppendLine(defaultTemplate);

                foreach (var error in errors)
                {
                    if (error.LineNumber == 1)
                    {
                        continue;
                    }

                    sb.AppendLine($"Error on Line number {error.LineNumber} of type {error}");
                }

                if (!sb.ToString().Equals(defaultTemplate))
                {
                    return sb.ToString();
                }
            }

            var schema = JsonSchema.FromSampleJson(json);
            schema.AllowAdditionalProperties = false;
            schema.Title = name;
            var generator = new CSharpGenerator(schema, settings);
            var code = generator.GenerateFile();

            //Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            //dynamic expandoClass = new ExpandoObject();
            //foreach (var (key, value) in values)
            //{
            //    AddProperty(expandoClass, key, value);
            //}

            //try
            //{
            //    File.WriteAllText(path, code);
            //}
            //catch (DirectoryNotFoundException)
            //{
            //    return "The specified namespace does not exist";
            //}
            //catch (IOException)
            //{
            //    return $"Error during file write\n {code}";
            //}
            //catch (Exception)
            //{
            //    return "A fatal error has occured";
            //}

            return code;
        }

        /// <summary>
        /// The parse json.
        /// </summary>
        /// <param name="rawJson">
        /// The raw json.
        /// </param>
        /// <param name="classNamespace">
        /// The class Namespace.
        /// </param>
        /// <param name="className">
        /// The class Name.
        /// </param>
        /// <param name="generatorSettings">
        /// The generator Settings.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ParseJson(string rawJson, string classNamespace, string className, CSharpGeneratorSettings generatorSettings)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                className = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(classNamespace))
            {
                classNamespace = "TeamBond.Syntosa.Validation.DataEditor.GeneratedClasses";
            }

            string collection = "System.Collection.Generic.List";

            generatorSettings.ArrayBaseType = collection;
            generatorSettings.ArrayInstanceType = collection;
            generatorSettings.Namespace = classNamespace;

            string generatedCode = Generate(rawJson, generatorSettings, className);

            for (int i = 0; i < 15; i++)
            {
                generatedCode += "\n";
            }

            return generatedCode;
        }

        /// <summary>
        /// The add property.
        /// </summary>
        /// <param name="expando">
        /// The expando.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="propertyValue">
        /// The property value.
        /// </param>
        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            if (propertyValue is null)
            {
                return;
            }

            if (propertyValue.GetType() == typeof(Array))
            {
            }

            var expandoDictionary = expando as IDictionary<string, object>;

            if (expandoDictionary.ContainsKey(propertyName))
            {
                expandoDictionary[propertyName] = propertyValue;
            }
            else
            {
                expandoDictionary.Add(propertyName, propertyValue);
            }
        }
    }
}
