namespace TeamBond.Syntosa.Validation.JsonToSyntosa.Infrastructure
{
    using System;

    using Autofac;

    using TeamBond.Core.Configuration;
    using TeamBond.Core.DependencyInjection;

    /// <summary>
    /// Registers the AWS services required by this application.
    /// </summary>
    public class ContainerRegistrationProvider : ITeamBondRegistrationProvider
    {
        /// <inheritdoc />
        public int Order => 1;

        /// <inheritdoc />
        public void Register(ContainerBuilder builder, ITeamBondTypeLocator typeLocator, TeamBondConfig config)
        {
        }
    }
}