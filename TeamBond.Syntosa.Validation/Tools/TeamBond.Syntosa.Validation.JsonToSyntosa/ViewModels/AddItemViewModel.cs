namespace TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels
{
    using System.Reactive;

    using ReactiveUI;

    using TeamBond.Syntosa.Validation.JsonToSyntosa.Models;

    class AddItemViewModel : ViewModelBase
    {
        private string description;

        public AddItemViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Description, 
                x => !string.IsNullOrWhiteSpace(x));

            this.Ok = ReactiveCommand.Create(() => new TodoItem { Description = this.Description }, okEnabled);
            this.Cancel = ReactiveCommand.Create(() => { });
        }

        public string Description
        {
            get => this.description;
            set => this.RaiseAndSetIfChanged(ref this.description, value);
        }

        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}