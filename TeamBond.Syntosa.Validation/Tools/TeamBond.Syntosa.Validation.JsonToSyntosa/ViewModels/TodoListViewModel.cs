namespace TeamBond.Syntosa.Validation.JsonToSyntosa.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using TeamBond.Syntosa.Validation.JsonToSyntosa.Models;

    public class TodoListViewModel : ViewModelBase
    {
        public TodoListViewModel(IEnumerable<TodoItem> items)
        {
            this.Items = new ObservableCollection<TodoItem>(items);
        }

        public ObservableCollection<TodoItem> Items { get; }
    }
}