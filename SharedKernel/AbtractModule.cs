using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace SharedKernel
{
    public abstract class AbtractModule<T> : Module
    {
        private readonly bool _isDevelopment = false;
        protected readonly List<Assembly> _assemblies = new List<Assembly>();

        public AbtractModule(bool isDevelopment)
        {
            _isDevelopment = isDevelopment;

            var currentAssembly = Assembly.GetAssembly(typeof(T));

            if (currentAssembly != null)
            {
                _assemblies.Add(currentAssembly);
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

        protected abstract void RegisterCommonDependencies(ContainerBuilder builder);

        protected virtual void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {

        }

        protected virtual void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {

        }
    }
}
