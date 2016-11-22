namespace Demo.Common.Handlers.Internal
{
    using Demo.Common.Handlers.Interfaces;

    internal abstract class AbstractRequestHandlerWrapper<TResult>
    {
        public abstract TResult Handle(IRequest<TResult> message);
    }
}