namespace Demo.Common.Handlers.Interfaces
{
    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);

        void Send(IRequest request);
    }
}