namespace Demo.Logic.Tests.Facades.Base
{
    using AutoMapper;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Logic.Facades.Base;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class HelperTests
    {
        private IMediator _mediator;
        private IMapper _mapper;
        private IResult<IRequest<IResult<Error>>, NonEmptyString> _deleteCommandResult;
        private IResult<IRequest<string>, NonEmptyString> _getItemsQueryResult;
        private IResult<IRequest<IResult<NonEmptyString, Error>>, NonEmptyString> _getItemQueryResult;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _mapper = Substitute.For<IMapper>();
            _deleteCommandResult = Substitute.For<IResult<IRequest<IResult<Error>>, NonEmptyString>>();
            _getItemsQueryResult = Substitute.For<IResult<IRequest<string>, NonEmptyString>>();
            _getItemQueryResult = Substitute.For<IResult<IRequest<IResult<NonEmptyString, Error>>, NonEmptyString>>();
        }

        [Test]
        public void Delete_NoErrors_ShouldSucceed()
        {
            _mediator.Send(Arg.Any<IRequest<IResult<Error>>>()).Returns(Result<Error>.Ok());

            var result = Helper.Delete(_mediator, _deleteCommandResult);

            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void Delete_CommandResultFail_ShouldFail()
        {
            var error = (NonEmptyString)"error";

            _deleteCommandResult.IsFailure.Returns(true);

            _deleteCommandResult.Error.Returns(error);

            var result = Helper.Delete(_mediator, _deleteCommandResult);

            result.IsFailure.ShouldBeTrue();

            result.Error.ErrorType.ShouldBe(ErrorType.Generic);

            result.Error.Message.ShouldBe(error);
        }

        [Test]
        public void Delete_MediatorResultFail_ShouldFail()
        {
            var error = (NonEmptyString)"error";

            _mediator.Send(Arg.Any<IRequest<IResult<Error>>>()).Returns(error.ToGeneric());

            var result = Helper.Delete(_mediator, _deleteCommandResult);

            result.IsFailure.ShouldBeTrue();

            result.Error.ErrorType.ShouldBe(ErrorType.Generic);

            result.Error.Message.ShouldBe(error);
        }

        [Test]
        public void GetItems_NoErrors_ShouldSucceed()
        {
            const string value = "value";

            _mapper.Map<string>(Arg.Any<string>()).Returns(value);

            var result = Helper.GetItems<string, IRequest<string>, string>(_mediator, _mapper, _getItemsQueryResult);

            result.IsSuccess.ShouldBeTrue();

            result.Value.ShouldBe(value);
        }

        [Test]
        public void GetItems_QueryResultFail_ShouldFail()
        {
            const string value = "value";

            _getItemsQueryResult.Error.Returns((NonEmptyString)value);

            _getItemsQueryResult.IsFailure.Returns(true);

            var result = Helper.GetItems<string, IRequest<string>, string>(_mediator, _mapper, _getItemsQueryResult);

            result.IsFailure.ShouldBeTrue();

            result.Error.ErrorType.ShouldBe(ErrorType.Generic);

            result.Error.Message.ShouldBe(value);
        }

        [Test]
        public void GetItem_NoErrors_ShouldSucceed()
        {
            const string value = "value";

            _mediator.Send(Arg.Any<IRequest<IResult<NonEmptyString, Error>>>()).Returns(Result<NonEmptyString, Error>.Ok((NonEmptyString)value));

            _mapper.Map<string>(Arg.Any<NonEmptyString>()).Returns(value);

            var result = Helper.GetItem<string, IRequest<IResult<NonEmptyString, Error>>, NonEmptyString>(_mediator, _mapper, _getItemQueryResult);

            result.IsSuccess.ShouldBeTrue();

            result.Value.ShouldBe(value);
        }

        [Test]
        public void GetItem_QueryResultFail_ShouldFail()
        {
            const string value = "value";

            _getItemQueryResult.Error.Returns((NonEmptyString)value);

            _getItemQueryResult.IsFailure.Returns(true);

            var result = Helper.GetItem<string, IRequest<IResult<NonEmptyString, Error>>, NonEmptyString>(_mediator, _mapper, _getItemQueryResult);

            result.IsFailure.ShouldBeTrue();

            result.Error.ErrorType.ShouldBe(ErrorType.Generic);

            result.Error.Message.ShouldBe(value);
        }

        [Test]
        public void GetItem_MediatorResultFail_ShouldFail()
        {
            const string error = "error";

            _mediator.Send(Arg.Any<IRequest<IResult<NonEmptyString, Error>>>()).Returns(((NonEmptyString)error).ToGeneric<NonEmptyString>());

            var result = Helper.GetItem<string, IRequest<IResult<NonEmptyString, Error>>, NonEmptyString>(_mediator, _mapper, _getItemQueryResult);

            result.IsFailure.ShouldBeTrue();

            result.Error.ErrorType.ShouldBe(ErrorType.Generic);

            result.Error.Message.ShouldBe(error);
        }
    }
}
