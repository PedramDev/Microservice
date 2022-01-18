using Autofac;
using Catalog.Data.DatabaseConfig;
using System.Reflection;
using Module = Autofac.Module;

namespace Catalog.Data
{
    public class DataModule : Module
    {
        private readonly bool _isDevelopment = false;
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public DataModule(bool isDevelopment, Assembly? callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            var currentAssembly = Assembly.GetAssembly(typeof(DataModule));
        
            if (currentAssembly != null)
            {
                _assemblies.Add(currentAssembly);
            }
            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {

            builder
                .RegisterType<CatalogContext>()
                .As<ICatalogContext>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<GetDatabaseConnection>()
                .As<IDatabaseConfig>()
                .SingleInstance();

        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }
    }
}
