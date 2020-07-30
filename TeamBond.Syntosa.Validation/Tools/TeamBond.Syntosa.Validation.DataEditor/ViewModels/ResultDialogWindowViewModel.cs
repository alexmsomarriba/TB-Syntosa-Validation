namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System.Reactive;

    using ReactiveUI;

    /// <summary>
    /// The result dialog window view model.
    /// </summary>
    public class ResultDialogWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultDialogWindowViewModel"/> class.
        /// </summary>
        /// <param name="resultMessage">
        /// The result message.
        /// </param>
        /// <param name="hasErrors">
        /// The has errors.
        /// </param>
        /// <param name="errorLog">
        /// The error log.
        /// </param>
        public ResultDialogWindowViewModel(string resultMessage, bool hasErrors, string errorLog = null)
        {
            this.ResultMessage = resultMessage;
            this.ErrorLog = errorLog;
            this.HasErrors = hasErrors;
        }

        /// <summary>
        /// Gets or sets the result message.
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// Gets or sets the error log.
        /// </summary>
        public string ErrorLog { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the event has errors.
        /// </summary>
        public bool HasErrors { get; set; }
    }
}