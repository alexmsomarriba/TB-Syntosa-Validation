namespace TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels
{
    using System.Reactive;

    using ReactiveUI;

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
        /// Initializes a new instance of the <see cref="JsonConverterViewModel"/> class.
        /// </summary>
        public JsonConverterViewModel()
        {
            var isOkkEnabled = this.WhenAnyValue(
                x => x.JsonDoc,
                x => !string.IsNullOrWhiteSpace(x));

            this.Ok = ReactiveCommand.Create(() => new string(this.JsonDoc), isOkkEnabled);
            this.theNewOK = ReactiveCommand.Create(this.UpdateConverted, isOkkEnabled);
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
        /// Gets the if the correct contents of the button are there.
        /// </summary>
        public ReactiveCommand<Unit, string> Ok { get; }

        /// <summary>
        /// Gets the the new ok.
        /// </summary>
        public ReactiveCommand<Unit, Unit> theNewOK { get; }

        public void UpdateConverted()
        {
            this.ConvertedJson = this.JsonDoc;
        }
    }
}