namespace Demo.Logic.CQ.UpdateProduct
{
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Logic.CQ.UpdateProduct.Interfaces;
    using Demo.Types.FunctionalExtensions;

    public sealed class CommandHandler : IRequestHandler<Command, IResult<Error>>
    {
        private readonly IRepository _repository;

        public CommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public IResult<Error> Handle(Command message)
        {
            var id = message.Id;

            var exists = _repository.ExistsById(id);

            if (!exists)
            {
                return ErrorResultExtensions.ToNotFound();
            }

            _repository.Update(message);

            return Result<Error>.Ok();
        }
    }
}
