namespace Demo.Common.Handlers.Internal
{
    using Demo.Common.Handlers.Interfaces;

    internal abstract class AbstractVoidRequestHandlerWrapper
    {
        public abstract void Handle(IRequest message);
    }
}