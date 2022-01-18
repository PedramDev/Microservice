using Autofac;
using MediatR;
using MediatR.Pipeline;
using SharedKernel;

namespace Catalog.Infrastructure
{
    public class DefaultInfrastructureModule : AbtractModule<DefaultInfrastructureModule>
    {
       
        public DefaultInfrastructureModule(bool isDevelopment)
            :base(isDevelopment)
        {
        }

        protected override void RegisterCommonDependencies(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(EfRepository<>))
            //    .As(typeof(IRepository<>))
            //    .As(typeof(IReadRepository<>))
            //    .InstancePerLifetimeScope();

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                .RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
            }

            //builder.RegisterType<EmailSender>().As<IEmailSender>()
            //    .InstancePerLifetimeScope();
        }
    }
}
