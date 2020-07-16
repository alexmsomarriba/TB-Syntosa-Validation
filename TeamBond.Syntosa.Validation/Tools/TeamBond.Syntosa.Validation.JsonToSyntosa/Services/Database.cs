namespace TeamBond.Syntosa.Validation.JsonToSyntosa.Services
{
    using System.Collections.Generic;

    using TeamBond.Syntosa.Validation.JsonToSyntosa.Models;

    public class Database
    {
        public IEnumerable<TodoItem> GetItems() =>
            new[]
                {
                    new TodoItem { Description = "Walk the dog" }, 
                    new TodoItem { Description = "Buy some milk" },
                    new TodoItem { Description = "Learn Avalonia", IsChecked = true },
                };
    }
}