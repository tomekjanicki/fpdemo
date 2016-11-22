namespace Demo.Common.Api.Tests.Infrastructure
{
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Demo.Common.Api.Infrastructure;
    using Demo.Common.Shared;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class WebApiControllerHelperTests
    {
        [Test]
        public void GetHttpActionResult_Ok_ShouldReturnOkNegotiatedContentResult()
        {
            const string content = "Ok";
            var result = Result<string, Error>.Ok(content);
            var actionResult = WebApiControllerHelper.GetHttpActionResult(result, Substitute.For<ApiController>());
            var okNegotiatedContentResult = actionResult as OkNegotiatedContentResult<string>;
            okNegotiatedContentResult.ShouldNotBeNull();
            okNegotiatedContentResult?.Content.ShouldBe(content);
        }

        [Test]
        public void GetHttpActionResult_Generic_ShouldReturnBadRequestErrorMessageResult()
        {
            const string error = "error";
            var result = ((NonEmptyString)error).ToGeneric<string>();
            var actionResult = WebApiControllerHelper.GetHttpActionResult(result, Substitute.For<ApiController>());
            var badRequestErrorMessageResult = actionResult as BadRequestErrorMessageResult;
            badRequestErrorMessageResult.ShouldNotBeNull();
            badRequestErrorMessageResult?.Message.ShouldBe(error);
        }

        [Test]
        public void GetHttpActionResult_NotFound_ShouldReturnNotFoundResult()
        {
            var result = ErrorResultExtensions.ToNotFound<string>();
            var actionResult = WebApiControllerHelper.GetHttpActionResult(result, Substitute.For<ApiController>());
            var notFoundResult = actionResult as NotFoundResult;
            notFoundResult.ShouldNotBeNull();
        }

        [Test]
        public void GetHttpActionResultForDelete_Ok_ShouldReturnStatusCodeResult()
        {
            var result = Result<Error>.Ok();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForDelete(result, Substitute.For<ApiController>());
            var statusCodeResult = actionResult as StatusCodeResult;
            statusCodeResult.ShouldNotBeNull();
            statusCodeResult?.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Test]
        public void GetHttpActionResultForDelete_Generic_ShouldReturnBadRequestErrorMessageResult()
        {
            const string error = "error";
            var result = ((NonEmptyString)error).ToGeneric();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForDelete(result, Substitute.For<ApiController>());
            var badRequestErrorMessageResult = actionResult as BadRequestErrorMessageResult;
            badRequestErrorMessageResult.ShouldNotBeNull();
            badRequestErrorMessageResult?.Message.ShouldBe(error);
        }

        [Test]
        public void GetHttpActionResultForDelete_NotFound_ShouldReturnNotFoundResult()
        {
            var result = ErrorResultExtensions.ToNotFound();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForDelete(result, Substitute.For<ApiController>());
            var notFoundResult = actionResult as NotFoundResult;
            notFoundResult.ShouldNotBeNull();
        }

        [Test]
        public void GetHttpActionResultForPut_Ok_ShouldReturnOkResult()
        {
            var result = Result<Error>.Ok();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForPut(result, Substitute.For<ApiController>());
            var okResult = actionResult as OkResult;
            okResult.ShouldNotBeNull();
        }

        [Test]
        public void GetHttpActionResultForPut_Generic_ShouldReturnBadRequestErrorMessageResult()
        {
            const string error = "error";
            var result = ((NonEmptyString)error).ToGeneric();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForPut(result, Substitute.For<ApiController>());
            var badRequestErrorMessageResult = actionResult as BadRequestErrorMessageResult;
            badRequestErrorMessageResult.ShouldNotBeNull();
            badRequestErrorMessageResult?.Message.ShouldBe(error);
        }

        [Test]
        public void GetHttpActionResultForPut_NotFound_ShouldReturnNotFoundResult()
        {
            var result = ErrorResultExtensions.ToNotFound();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForPut(result, Substitute.For<ApiController>());
            var notFoundResult = actionResult as NotFoundResult;
            notFoundResult.ShouldNotBeNull();
        }
    }
}
