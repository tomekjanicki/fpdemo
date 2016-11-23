namespace Demo.Common.Handlers
{
    using System;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Handlers.Internal;

    public sealed class Mediator : IMediator
    {
        private readonly SingleInstanceFactory _instanceFactory;

        public Mediator(SingleInstanceFactory instanceFactory)
        {
            _instanceFactory = instanceFactory;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var defaultHandler = GetHandler(request);

            var result = defaultHandler.Handle(request);

            return result;
        }

        public void Send(IRequest request)
        {
            var defaultHandler = GetHandler(request);

            defaultHandler.Handle(request);
        }

        private static InvalidOperationException BuildException(object message, Exception inner)
        {
            return new InvalidOperationException($"Handler was not found for request of type {message.GetType()}.\r\nContainer or service locator not configured properly or handlers not registered with your container.", inner);
        }

        private AbstractVoidRequestHandlerWrapper GetHandler(IRequest request)
        {
            return GetWrapper<AbstractVoidRequestHandlerWrapper>(request, typeof(IVoidRequestHandler<>), typeof(VoidRequestHandlerWrapper<>));
        }

        private AbstractRequestHandlerWrapper<TResponse> GetHandler<TResponse>(IRequest<TResponse> request)
        {
            return GetWrapper<AbstractRequestHandlerWrapper<TResponse>, TResponse>(request, typeof(IRequestHandler<,>), typeof(RequestHandlerWrapper<,>));
        }

        private TWrapper GetWrapper<TWrapper>(object request, Type handlerType, Type wrapperType)
        {
            var requestType = request.GetType();
            var genericHandlerType = handlerType.MakeGenericType(requestType);
            var genericWrapperType = wrapperType.MakeGenericType(requestType);
            return GetWrapperInstance<TWrapper>(request, genericHandlerType, genericWrapperType);
        }

        private TWrapper GetWrapper<TWrapper, TResponse>(object request, Type handlerType, Type wrapperType)
        {
            var requestType = request.GetType();
            var genericHandlerType = handlerType.MakeGenericType(requestType, typeof(TResponse));
            var genericWrapperType = wrapperType.MakeGenericType(requestType, typeof(TResponse));
            return GetWrapperInstance<TWrapper>(request, genericHandlerType, genericWrapperType);
        }

        private TWrapper GetWrapperInstance<TWrapper>(object request, Type genericHandlerType, Type genericWrapperType)
        {
            var handler = GetHandler(request, genericHandlerType);

            return (TWrapper)Activator.CreateInstance(genericWrapperType, handler);
        }

        private object GetHandler(object request, Type handlerType)
        {
            try
            {
                return _instanceFactory(handlerType);
            }
            catch (Exception e)
            {
                throw BuildException(request, e);
            }
        }
    }
}