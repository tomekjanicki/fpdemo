namespace Demo.Logic.Tests.CQ.GetProductById
{
    using Demo.Common.Shared;
    using Demo.Logic.CQ.GetProductById;
    using Demo.Logic.CQ.GetProductById.Interfaces;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class QueryHandlerTests
    {
        private QueryHandler _queryHandler;
        private IRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository>();
            _queryHandler = new QueryHandler(_repository);
        }

        [Test]
        public void ProductFound_ShouldBeSuccess()
        {
            var query = GetValidQuery();

            var product = new Product((PositiveInt)5, (NonEmptyString)"code", (NonEmptyString)"name", (PositiveInt)5);

            _repository.GetProductById(Arg.Any<PositiveInt>()).Returns(product);

            var result = _queryHandler.Handle(query);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(product);
        }

        [Test]
        public void ProductNotFound_ShouldBeFailure()
        {
            var query = GetValidQuery();

            _repository.GetProductById(Arg.Any<PositiveInt>()).Returns((Maybe<Product>)null);

            var result = _queryHandler.Handle(query);

            result.IsFailure.ShouldBeTrue();
            result.Error.ErrorType.ShouldBe(ErrorType.NotFound);
        }

        private static Query GetValidQuery()
        {
            var queryResult = Query.Create(1);
            queryResult.EnsureIsNotFaliure();
            return queryResult.Value;
        }
    }
}
