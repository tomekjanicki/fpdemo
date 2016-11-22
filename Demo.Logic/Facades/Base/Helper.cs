namespace Demo.Logic.Facades.Base
{
    using AutoMapper;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public static class Helper
    {
        public static IResult<TDto, Error> GetItem<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<IResult<TObject, Error>>
        {
            if (queryResult.IsFailure)
            {
                return queryResult.Error.ToGeneric<TDto>();
            }

            var result = mediator.Send(queryResult.Value);

            if (result.IsFailure)
            {
                return Result<TDto, Error>.Fail(result.Error);
            }

            var data = mapper.Map<TDto>(result.Value);

            return Result<TDto, Error>.Ok(data);
        }

        public static IResult<TDto, Error> GetItemSimple<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<TObject>
        {
            return GetItems<TDto, TQuery, TObject>(mediator, mapper, queryResult);
        }

        public static IResult<TDto, Error> GetItems<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<TObject>
        {
            if (queryResult.IsFailure)
            {
                return queryResult.Error.ToGeneric<TDto>();
            }

            var result = mediator.Send(queryResult.Value);

            var data = mapper.Map<TDto>(result);

            return Result<TDto, Error>.Ok(data);
        }

        public static IResult<Error> Delete<TCommand>(IMediator mediator, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<IResult<Error>>
        {
            if (commandResult.IsFailure)
            {
                return commandResult.Error.ToGeneric();
            }

            var result = mediator.Send(commandResult.Value);

            return result;
        }

        public static IResult<Error> Put<TCommand>(IMediator mediator, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<IResult<Error>>
        {
            return Delete(mediator, commandResult);
        }

        public static IResult<TDto, Error> Post<TDto, TCommand, TObject>(IMediator mediator, IMapper mapper, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<IResult<TObject, Error>>
        {
            return GetItem<TDto, TCommand, TObject>(mediator, mapper, commandResult);
        }
    }
}
