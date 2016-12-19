namespace Demo.Api.Infrastructure
{
    using Demo.Common.IoC.Interfaces;
    using SimpleInjector;

    public sealed class IoCContainer : IIoCContainer
    {
        private readonly Container _container;

        public IoCContainer(Container container)
        {
            _container = container;
        }

        public T Get<T>()
            where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}