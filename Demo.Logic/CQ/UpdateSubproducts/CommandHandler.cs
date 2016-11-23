namespace Demo.Logic.CQ.UpdateSubproducts
{
    using System.Data.SqlClient;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class CommandHandler : IRequestHandler<Command, IResult<Error>>
    {
        private const int DatabaseDownErrorNumber = 5000;

        public IResult<Error> Handle(Command message)
        {
            try
            {
                // logic
                return Result<Error>.Ok();
            }
            catch (SqlException ex)
            {
                if (ex.Number == DatabaseDownErrorNumber)
                {
                    return ((NonEmptyString)"Database is down").ToGeneric();
                }

                throw;
            }
        }
    }
}
