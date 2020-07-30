namespace TeamBond.Syntosa.Validation.DataEditor
{
    using System;

    using Avalonia.Controls;
    using Avalonia.Controls.Templates;

    using TeamBond.Syntosa.Validation.DataEditor.ViewModels;

    /// <summary>
    /// The view locator.
    /// </summary>
    public class ViewLocator : IDataTemplate
    {
        /// <summary>
        /// A value indicating whether recycling is supported.
        /// </summary>
        public bool SupportsRecycling => false;

        /// <summary>
        /// Builds the link between a model and its view.
        /// </summary>
        /// <param name="data">
        /// The data object to link.
        /// </param>
        /// <returns>
        /// The link between a model and its view.
        /// </returns>
        public IControl Build(object data)
        {
            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        /// <summary>
        /// Checks if the given data object is a view model.
        /// </summary>
        /// <param name="data">
        /// The data object to check.
        /// </param>
        /// <returns>
        /// <c>True</c> if the data object is a view model, <c>false</c> otherwise.
        /// </returns>
        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}