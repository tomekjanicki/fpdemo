namespace Demo.Logic.Tests.CQ.GetProductById
{
    using Demo.Logic.CQ.GetProductById;
    using NUnit.Framework;
    using Shouldly;

    public class QueryTests
    {
        [Test]
        public void ValidParameters_ShouldPass()
        {
            var queryResult = Query.Create(1);
            queryResult.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void InvalidId_ShouldFail()
        {
            var queryResult = Query.Create(-1);
            queryResult.IsFailure.ShouldBeTrue();
        }
    }
}
