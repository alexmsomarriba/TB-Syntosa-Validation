namespace TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels
{
    using System.Collections.Generic;
    using System.Reactive;

    using NJsonSchema.CodeGeneration.CSharp;

    using ReactiveUI;

    using TeamBond.Syntosa.Validation.JsonToSyntosa.Services;

    /// <summary>
    /// The json converter view model.
    /// </summary>
    public class JsonConverterViewModel : ViewModelBase
    {
        /// <summary>
        /// The inputted json doc.
        /// </summary>
        private string jsonDoc;

        /// <summary>
        /// The converted json.
        /// </summary>
        private string convertedJson;

        /// <summary>
        /// The class name.
        /// </summary>
        private string classNamespace;

        /// <summary>
        /// The class name.
        /// </summary>
        private string className;

        /// <summary>
        /// The is read only poco.
        /// </summary>
        private bool isReadOnlyPoco;

        private CSharpClassStyle selectedStyle = CSharpClassStyle.Poco;

        /// <summary>
        /// A value indicating whether to serialize methods from the converted json.
        /// </summary>
        private bool serializeMethod = true;

        /// <summary>
        /// A value indicating whether to generate data annotations.
        /// </summary>
        private bool generateDataAnnotations = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConverterViewModel"/> class.
        /// </summary>
        public JsonConverterViewModel()
        {
            var isOkkEnabled = this.WhenAnyValue(
                x => x.JsonDoc,
                x => !string.IsNullOrWhiteSpace(x));

            this.Ok = ReactiveCommand.Create(
                () =>
                    {
                        this.GeneratorSetting.GenerateJsonMethods = this.SerializeMethod;
                        this.GeneratorSetting.GenerateDataAnnotations = this.GenerateDataAnnotations;
                        this.GeneratorSetting.ClassStyle =
                            this.IsReadOnlyPoco ? CSharpClassStyle.Record : CSharpClassStyle.Poco;

                        this.ConvertedJson = JsonToClassConverter.ParseJson(
                            this.JsonDoc,
                            this.ClassNamespace,
                            this.ClassName,
                            this.GeneratorSetting);
                    }, 
                isOkkEnabled);
        }

        /// <summary>
        /// Gets or sets the converted json.
        /// </summary>
        public string ConvertedJson
        {
            get => this.convertedJson;
            set => this.RaiseAndSetIfChanged(ref this.convertedJson, value);
        }

        /// <summary>
        /// Gets or sets the json doc to convert.
        /// </summary>
        public string JsonDoc
        {
            get => this.jsonDoc;
            set => this.RaiseAndSetIfChanged(ref this.jsonDoc, value);
        }

        /// <summary>
        /// Gets or sets the class name.
        /// </summary>
        public string ClassNamespace
        {
            get => this.classNamespace;
            set => this.RaiseAndSetIfChanged(ref this.classNamespace, value);
        }

        /// <summary>
        /// Gets or sets the class name.
        /// </summary>
        public string ClassName
        {
            get => this.className;
            set => this.RaiseAndSetIfChanged(ref this.className, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is read only poco.
        /// </summary>
        public bool IsReadOnlyPoco
        {
            get => this.isReadOnlyPoco;
            set => this.RaiseAndSetIfChanged(ref this.isReadOnlyPoco, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to serialize method from the converted json.
        /// </summary>
        public bool SerializeMethod
        {
            get => this.serializeMethod;
            set => this.RaiseAndSetIfChanged(ref this.serializeMethod, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether generate data annotations.
        /// </summary>
        public bool GenerateDataAnnotations
        {
            get => this.generateDataAnnotations;
            set => this.RaiseAndSetIfChanged(ref this.generateDataAnnotations, value);
        }

        /// <summary>
        /// Gets the generator setting.
        /// </summary>
        public CSharpGeneratorSettings GeneratorSetting { get; } = new CSharpGeneratorSettings
        {
            ClassStyle = CSharpClassStyle.Poco,
            GenerateJsonMethods = false,
            GenerateDataAnnotations = true,
            PropertyNameGenerator = new PropertyNameGenerator(),
            TypeNameGenerator = new PropertyNameGenerator(),
            Namespace = string.Empty,
            ArrayBaseType = string.Empty,
            ArrayInstanceType = string.Empty,
            HandleReferences = true,
        };

        public CSharpClassStyle SelectedStyle
        {
            get => this.selectedStyle;
            set => this.RaiseAndSetIfChanged(ref this.selectedStyle, value);
        }

        public IEnumerable<CSharpClassStyle> ClassStyles;

        /// <summary>
        /// Gets the if the correct contents of the button are there.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Ok { get; }
    }
}