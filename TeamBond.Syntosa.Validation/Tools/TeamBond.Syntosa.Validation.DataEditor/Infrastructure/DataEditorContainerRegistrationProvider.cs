namespace TeamBond.Syntosa.Validation.DataEditor.Infrastructure
{
    using Autofac;

    using TeamBond.Core.Configuration;
    using TeamBond.Core.DependencyInjection;
    using TeamBond.Domain.User;

    /// <summary>
    /// The data editor container registration provider.
    /// </summary>
    public class DataEditorContainerRegistrationProvider : ITeamBondRegistrationProvider
    {
        /// <inheritdoc />
        public int Order => 1;

        /// <inheritdoc />
        public void Register(ContainerBuilder builder, ITeamBondTypeLocator typeLocator, TeamBondConfig config)
        {
        }
    }
}