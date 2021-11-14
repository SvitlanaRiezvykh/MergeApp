using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MergeApp.UnitTests
{
    public class TuneInServiceTests
    {
        [Fact]
        public async Task GetJsonSuccessfullAsync()
        {
            var configuredResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
            Mock<HttpMessageHandler> handlerMock = SetUpHandler(configuredResponse);

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new TuneInService(httpClient);

            var response = await service.GetJsonResponse();

            Assert.Null(response);
        }

        [Fact]
        public async Task GetJsonSuccessfullWithContentProvided()
        {
            var configuredResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{\"ranked\":[{\"priority\":2,\"vals\":{\"timeout\":\"3s\",\"num_threads\":500,\"buffer_size\":4000,\"use_sleep\":true}}]}"),
            };
            Mock<HttpMessageHandler> handlerMock = SetUpHandler(configuredResponse);

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new TuneInService(httpClient);

            var response = await service.GetJsonResponse();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetJsonReturnedErrorAsync()
        {
            var configuredResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
            Mock<HttpMessageHandler> handlerMock = SetUpHandler(configuredResponse);

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new TuneInService(httpClient);

            await Assert.ThrowsAsync<HttpRequestException>(() => service.GetJsonResponse());
        }

        private static Mock<HttpMessageHandler> SetUpHandler(HttpResponseMessage configuredResponse)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(configuredResponse);
            return handlerMock;
        }
    }
}
