namespace TeamBond.Syntosa.Validation.DataEditor
{
    using System;
    using System.IO;

    using Amazon;
    using Amazon.Runtime;
    using Amazon.Runtime.CredentialManagement;

    using Avalonia;
    using Avalonia.Logging.Serilog;
    using Avalonia.ReactiveUI;

    using Microsoft.Extensions.Configuration;

    using TeamBond.Application.Framework;
    using TeamBond.Core;
    using TeamBond.Services.Security.AWS;

    /// <summary>
    /// Kick starts the Data Editor application.
    /// </summary>
    public class DataEditor : TeamBondEngineApplicationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditor" /> class.
        /// </summary>
        protected DataEditor()
        {
        }

        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            new DataEditor().BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        /// <summary>
        /// The Avalonia app builder.
        /// </summary>
        /// <returns>
        /// The <see cref="AppBuilder"/>.
        /// </returns>
        public AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>().UsePlatformDetect().LogToDebug().UseReactiveUI();
        }

        /// <inheritdoc />
        protected override void Configure(IConfigurationBuilder configBuilder)
        {
            string profileName = Environment.GetEnvironmentVariable("AWS_PROFILE");
            string regionName = Environment.GetEnvironmentVariable("AWS_REGION");
            string appName = Environment.GetEnvironmentVariable("TEAMBOND_APP_NAME");

            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new TeamBondException("Could not retrieve APP_NAME from Environment Variables.");
            }

            // Add the optional appsettings.json in the base directory
            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            string environmentName = Environment.GetEnvironmentVariable("TEAMBOND_APP_ENVIRONMENT");

            if (string.IsNullOrWhiteSpace(environmentName))
            {
                throw new TeamBondException("Could not retrieve APP_ENVIRONMENT from Environment Variables");
            }

            // Add an optional appsettings.{environmentName}.json
            configBuilder.AddJsonFile($"appsettings.{environmentName}.json", true, true);

            // If a profile name is given, add the secrets manager configuration source
            // created with the credentials and region associated with the profile
            if (!string.IsNullOrWhiteSpace(profileName))
            {
                var chain = new CredentialProfileStoreChain();
                if (!chain.TryGetProfile("default", out CredentialProfile profile) || profile is null)
                {
                    throw new TeamBondException("Could not locate your AWS Credential Profile");
                }

                AWSCredentials credentials = profile.GetAWSCredentials(profile.CredentialProfileStore);
                configBuilder.AddSecretsManager(
                    credentials,
                    profile.Region,
                    options =>
                        {
                            options.SecretsFilter = entry =>
                                entry.Name.Contains($"{appName}_{environmentName}");
                            options.KeyGenerator = (entry, key) => key.Replace(
                                $"{appName}_{environmentName}:",
                                string.Empty);
                        });
            }
            else if (!string.IsNullOrWhiteSpace(regionName))
            {
                // If the region name is given, add the default secrets manager
                configBuilder.AddSecretsManager(
                    null,
                    RegionEndpoint.GetBySystemName(regionName),
                    options =>
                    {
                        options.SecretsFilter = entry =>
                            entry.Name.Contains($"{appName}_{environmentName}");
                        options.KeyGenerator = (entry, key) => key.Replace(
                            $"{appName}_{environmentName}:",
                            string.Empty);
                    });
            }
        }
    }
}