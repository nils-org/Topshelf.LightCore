using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using LightCore;

using Topshelf.Builders;
using Topshelf.Configurators;
using Topshelf.HostConfigurators;
using Topshelf.Logging;

namespace Topshelf.LightCore
{
    /// <summary>
    /// Internal extension of <see cref="HostBuilderConfigurator"/> to setup the use of LightCore.
    /// </summary>
    internal class LightCoreBuilderConfigurator : HostBuilderConfigurator
    {
        private static IContainer lightCoreContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightCoreBuilderConfigurator"/> class.
        /// </summary>
        /// <param name="lightCoreContainer">The <see cref="IContainer"/> to use when resolving a contract.</param>
        public LightCoreBuilderConfigurator(IContainer lightCoreContainer)
        {
            LightCoreBuilderConfigurator.lightCoreContainer = lightCoreContainer;
        }

        /// <inheritdoc />
        public HostBuilder Configure(HostBuilder builder)
        {
            // do nothing special
            return builder;
        }

        /// <inheritdoc />
        public IEnumerable<ValidateResult> Validate()
        {
            // do nothing special
            return Enumerable.Empty<ValidateResult>();
        }

        /// <summary>
        /// Resolves a new Service from the current LightCore-container.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <returns>The resolved service-instance.</returns>
        internal static T Resolve<T>()
            where T : class
        {
            try
            {
                return lightCoreContainer.Resolve<T>();
            }
            catch (Exception e)
            {
                var log = HostLogger.Get(typeof(HostConfiguratorExtensions));
                log.Fatal(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "[Topshelf.LightCore] Error constructing {0} : {1}.",
                        typeof(T).Name,
                        e.Message), e);
                throw;
            }
        }
    }
}
