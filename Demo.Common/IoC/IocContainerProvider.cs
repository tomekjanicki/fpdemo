namespace Demo.Common.IoC
{
    using Demo.Common.IoC.Interfaces;

    public static class IoCContainerProvider
    {
        private static IIoCContainer _iocContainer;

        public static void SetContainer(IIoCContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        public static IIoCContainer GetContainer()
        {
            return _iocContainer;
        }
    }
}