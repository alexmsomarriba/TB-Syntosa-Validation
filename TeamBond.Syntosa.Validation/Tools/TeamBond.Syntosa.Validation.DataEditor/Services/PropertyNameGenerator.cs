namespace TeamBond.Syntosa.Validation.JsonToSyntosa.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using NJsonSchema;
    using NJsonSchema.CodeGeneration;

    /// <summary>
    /// The property name generator.
    /// </summary>
    public class PropertyNameGenerator : IPropertyNameGenerator, ITypeNameGenerator
    {
        /// <inheritdoc />
        public string Generate(JsonSchemaProperty property)
        {
            string textToChange = property.Name;

            try
            {
                StringBuilder resultBuilder = new StringBuilder();

                foreach (var c in textToChange)
                {
                    if (!char.IsLetterOrDigit(c))
                    {
                        resultBuilder.Append(" ");
                    }
                    else
                    {
                        if (char.IsUpper(c))
                        {
                            resultBuilder.Append(" " + c);
                        }
                        else
                        {
                            resultBuilder.Append(c);
                        }
                    }
                }

                string result = resultBuilder.ToString().ToLower();

                TextInfo textInfo = new CultureInfo("en-us", false).TextInfo;

                result = textInfo.ToTitleCase(result).Replace(" ", string.Empty);

                return result;
            }
            catch
            {
                return "Please input valid json";
            }
        }

        /// <inheritdoc />
        public string Generate(JsonSchema schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            if (typeNameHint.EndsWith("ie"))
            {
                typeNameHint = typeNameHint.Substring(0, typeNameHint.Length - 2) + "y";
            }

            return typeNameHint;
        }
    }
}