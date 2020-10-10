using LightCore;

using Topshelf.HostConfigurators;
using Topshelf.Logging;

namespace Topshelf.LightCore
{
    /// <summary>
    /// Extends the <see cref="HostConfigurator"/> for usage with a LightCore <see cref="IContainer"/>.
    /// </summary>
    public static class HostConfiguratorExtension
    {
        /// <summary>
        /// Configure the <see cref="HostConfigurator"/> to use the given <see cref="IContainer"/>
        /// for resolving.
        /// </summary>
        /// <param name="configurator">The <see cref="HostConfigurator"/> to configure.</param>
        /// <param name="lightCoreContainer">The LightCore <see cref="IContainer"/>.</param>
        /// <returns>The configured <see cref="HostConfigurator"/>.</returns>
        public static HostConfigurator UseLightCore(this HostConfigurator configurator, IContainer lightCoreContainer)
        {
            var log = HostLogger.Get(typeof(HostConfiguratorExtensions));

            log.Info("[Topshelf.LightCore] Integration Started.");

            configurator.AddConfigurator(new LightCoreBuilderConfigurator(lightCoreContainer));
            return configurator;
        }
    }
}
