using Autofac;
using SharedKernel;

namespace Catalog.Repository
{
    public class RepositoryModule : AbtractModule<RepositoryModule>
    {
        public RepositoryModule(bool isDevelopment) : base(isDevelopment)
        {
        }

        protected override void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder
                .RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
