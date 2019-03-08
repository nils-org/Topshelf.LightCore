﻿using LightCore;
using System.Globalization;
using Topshelf.Logging;
using Topshelf.ServiceConfigurators;

namespace Topshelf.LightCore
{
    /// <summary>
    /// Extends the <see cref="ServiceConfigurator"/> for usage with LightCore.
    /// </summary>
    public static class LightCoreServiceConfiguratorExtensions
    {
        /// <summary>
        /// Construct a new service for a <see cref="ServiceConfigurator"/> using the pre-configured LightCore <see cref="IContainer"/>
        /// <seealso cref="HostConfiguratorExtension.UseLightCore(HostConfigurators.HostConfigurator, IContainer)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configurator"></param>
        /// <returns></returns>
        public static ServiceConfigurator<T> ConstructUsingLightCore<T>(this ServiceConfigurator<T> configurator) where T : class
        {
            var log = HostLogger.Get(typeof(HostConfiguratorExtensions));
            log.Info(
                string.Format(CultureInfo.InvariantCulture,
                "[Topshelf.LightCore] Service configured to construct {0} using LightCore.", typeof(T).Name));

            configurator.ConstructUsing(serviceFactory => LightCoreBuilderConfigurator.Resolve<T>());
            return configurator;
        }

    }
}
