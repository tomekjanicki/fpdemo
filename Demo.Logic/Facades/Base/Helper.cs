namespace Demo.Logic.Facades.Base
{
    using AutoMapper;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public static class Helper
    {
        // public static IResult<TDto, Error> GetItem<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
        //    where TQuery : IRequest<IResult<TObject, Error>>
        // {
        //    if (queryResult.IsFailure)
        //    {
        //        return queryResult.Error.ToGeneric<TDto>();
        //    }
        //    var result = mediator.Send(queryResult.Value);
        //    return result.IsFailure ? Result<TDto, Error>.Fail(result.Error) : GetMappedResult<TDto, TObject>(result.Value, mapper);
        // }
        public static IResult<TDto, Error> GetItem<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : class, IRequest<IResult<TObject, Error>>
        {
            return ErrorResultExtensions.
                OnSuccess(queryResult, mediator.Send, Error.CreateGeneric).
                OnSuccess(dto => GetMappedResult<TDto, TObject>(dto, mapper));
        }

        public static IResult<TDto, Error> GetItemSimple<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<TObject>
        {
            return GetItems<TDto, TQuery, TObject>(mediator, mapper, queryResult);
        }

        public static IResult<TDto, Error> GetItems<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<TObject>
        {
            return queryResult.
                OnSuccess(query => GetMappedResult<TDto, TObject>(mediator.Send(query), mapper), Error.CreateGeneric);
        }

        public static IResult<Error> Delete<TCommand>(IMediator mediator, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<IResult<Error>>
        {
            return commandResult.
                OnSuccess(command => mediator.Send(command), Error.CreateGeneric);
        }

        public static IResult<Error> Put<TCommand>(IMediator mediator, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<IResult<Error>>
        {
            return Delete(mediator, commandResult);
        }

        public static IResult<TDto, Error> Post<TDto, TCommand, TObject>(IMediator mediator, IMapper mapper, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : class, IRequest<IResult<TObject, Error>>
        {
            return GetItem<TDto, TCommand, TObject>(mediator, mapper, commandResult);
        }

        private static IResult<TDto, Error> GetMappedResult<TDto, TObject>(TObject obj, IMapper mapper)
        {
            return Result<TDto, Error>.Ok(mapper.Map<TDto>(obj));
        }
    }
}
