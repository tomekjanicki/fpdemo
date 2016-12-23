namespace Demo.Common.IoC
{
    using System;
    using Demo.Common.IoC.Interfaces;

    public static class IoCContainerProvider
    {
        private static IIoCContainer _iocContainer;

        public static void SetContainer(IIoCContainer iocContainer)
        {
            if (_iocContainer != null)
            {
                throw new InvalidOperationException("IoC container can be set only once at startup");
            }

            _iocContainer = iocContainer;
        }

        public static IIoCContainer GetContainer()
        {
            if (_iocContainer == null)
            {
                throw new InvalidOperationException("IoC container has not been set at startup");
            }

            return _iocContainer;
        }
    }
}