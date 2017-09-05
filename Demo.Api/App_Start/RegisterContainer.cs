namespace Demo.Api
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Reflection;
    using System.Web.Http;
    using AutoMapper;
    using Demo.Common.Handlers;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Logic;
    using Demo.Logic.Facades.Apis;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;

    public static class RegisterContainer
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.RegisterWebApiControllers(configuration);

            RegisterScoped(container);

            RegisterSingletons(container);

            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterSingletons(Container container)
        {
            container.RegisterSingleton<IMediator, Mediator>();
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(GetMapper);
        }

        private static void RegisterScoped(Container container)
        {
            var assemblies = GetAssemblies();
            var lifeStyle = Lifestyle.Scoped;
            container.Register(typeof(IRequestHandler<,>), assemblies, lifeStyle);
            container.Register(typeof(IVoidRequestHandler<>), assemblies, lifeStyle);
            container.Register<ProductGetFacade>(lifeStyle);
            container.Register<ProductPutFacade>(lifeStyle);
            container.Register<Logic.CQ.GetProductById.Interfaces.IRepository, Logic.CQ.GetProductById.Repository>(lifeStyle);
            container.Register<Logic.CQ.UpdateProduct.Interfaces.IRepository, Logic.CQ.UpdateProduct.Repository>(lifeStyle);
        }

        private static ImmutableList<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(AutoMapperConfiguration).GetTypeInfo().Assembly }.ToImmutableList();
        }

        private static IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(AutoMapperConfiguration.Configure);
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}