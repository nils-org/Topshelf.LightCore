using LightCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Topshelf.Builders;
using Topshelf.Configurators;
using Topshelf.HostConfigurators;
using Topshelf.Logging;

namespace Topshelf.LightCore
{
    internal class LightCoreBuilderConfigurator : HostBuilderConfigurator
    {
        private static IContainer lightCoreContainer;

        public LightCoreBuilderConfigurator(IContainer lightCoreContainer)
        {
            LightCoreBuilderConfigurator.lightCoreContainer = lightCoreContainer;
        }

        public HostBuilder Configure(HostBuilder builder)
        {
            // do nothing special
            return builder;
        }

        public IEnumerable<ValidateResult> Validate()
        {
            // do nothing special
            return Enumerable.Empty<ValidateResult>();
        }

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
                    string.Format(CultureInfo.InvariantCulture,
                        "[Topshelf.LightCore] Error constructing {0} : {1}.", typeof(T).Name, e.Message), e);
                throw;
            }
        }
    }
}
