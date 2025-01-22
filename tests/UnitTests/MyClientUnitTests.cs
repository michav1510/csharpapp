using Castle.Core.Logging;
using CSharpApp.Application.Implementations;
using CSharpApp.Core.Dtos.Contracts;
using CSharpApp.Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;

namespace UnitTests
{
    public class MyClientUnitTests
    {
        private Mock<ILogger<IMyClient>> _loggerMock;
        private Mock<ITokenStorage> _tokenStorage;
        private Mock<ICredsStorage> _credsStorage;
        public MyClientUnitTests() 
        {
            
            _loggerMock = new Mock<ILogger<IMyClient>>();
            _tokenStorage = new Mock<ITokenStorage>();
            _credsStorage = new Mock<ICredsStorage>();
        }


        [Fact]
        public async Task Client_WhenRequestIsValidAndTheTokenIsValid_ReturnsCorrectlySerializedResponse()
        {
            //Arrange
            var json = "[ {\"id\": 17, \"title\": \"New Product\", \"price\": 10, \"description\": \"A description\", \"images\": [\"[\\\"https://placeimg.com/640/480/any\\\"]\"], \"creationAt\": \"2025-01-22T08:10:41.000Z\", \"updatedAt\": \"2025-01-22T17:52:50.000Z\", \"category\": {  \"id\": 1, \"name\": \"Clothes\",\r\n            \"image\": \"https://i.imgur.com/QkIa5tT.jpeg\",  \"creationAt\": \"2025-01-22T08:10:41.000Z\"," +
                " \"updatedAt\": \"2025-01-22T08:10:41.000Z\" }}]";


            _tokenStorage.Setup(r => r.IsTokenValid()).Returns(true);
            _tokenStorage.Setup(r => r.GetToken()).Returns("arandomtoken");
            _credsStorage.Setup(r => r.GetEmail()).Returns("anemail");
            _credsStorage.Setup(r => r.GetPassword()).Returns("apassword");
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json)
                }); 
            
            Mock<HttpClient> _httpClient = new Mock<HttpClient>(mockHttpMessageHandler.Object);

            MyClient<IMyClient> myClient = new MyClient<IMyClient>(_httpClient.Object, _loggerMock.Object, _tokenStorage.Object, _credsStorage.Object);


            //Act
            IEnumerable<CreateProductResponse> result = await myClient.Request<GetAllProductsRequest, IEnumerable<CreateProductResponse>>(new GetAllProductsRequest());


            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.Count().Should().Be(1);
            result.FirstOrDefault().Description.Should().Be("A description");
            result.FirstOrDefault().Title.Should().Be("New Product");
            mockHttpMessageHandler
               .Protected()
               .Verify(
                   "SendAsync",
                   Times.Exactly(1), // Expected number of calls
                   ItExpr.IsAny<HttpRequestMessage>(),
                   ItExpr.IsAny<CancellationToken>()
               );
            _tokenStorage.Verify(r => r.IsTokenValid(), Times.Exactly(1));
            _tokenStorage.Verify(r => r.SaveToken(It.IsAny<string>()), Times.Exactly(0));
            _tokenStorage.Verify(r => r.GetToken(), Times.Exactly(1));
        }

        [Fact]
        public async Task Client_WhenRequestIsValidAndTheTokenIsInValid_ReturnsCorrectlySerializedResponse()
        {
            //Arrange
            var jsonGetAcceTokens = "{\r\n    \"access_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOjEsImlhdCI6MTczNzU3NzcxNywiZXhwIjoxNzM5MzA1NzE3fQ.-70wCNeqPOio9MhqxL8RSMbikoYqvnEeDZN2K0lLlQ0\",\r\n    \"refresh_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOjEsImlhdCI6MTczNzU3NzcxNywiZXhwIjoxNzM3NjEzNzE3fQ.gvz9PhYbQbeX3-kTzzjWXRCHiAN_Y7cq1O--QVG4wGI\"\r\n}";
            var jsonGetAllProductsResponse = "[ {\"id\": 17, \"title\": \"New Product\", \"price\": 10, \"description\": \"A description\", \"images\": [\"[\\\"https://placeimg.com/640/480/any\\\"]\"], \"creationAt\": \"2025-01-22T08:10:41.000Z\", \"updatedAt\": \"2025-01-22T17:52:50.000Z\", \"category\": {  \"id\": 1, \"name\": \"Clothes\",\r\n            \"image\": \"https://i.imgur.com/QkIa5tT.jpeg\",  \"creationAt\": \"2025-01-22T08:10:41.000Z\"," +
                " \"updatedAt\": \"2025-01-22T08:10:41.000Z\" }}]";
            var mockHttpMessageCallsCount = 0;


            _tokenStorage.Setup(r => r.IsTokenValid()).Returns(false);
            _tokenStorage.Setup(r => r.GetToken()).Returns("arandomtoken");
            _credsStorage.Setup(r => r.GetEmail()).Returns("anemail");
            _credsStorage.Setup(r => r.GetPassword()).Returns("apassword");
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(() =>
            {
                mockHttpMessageCallsCount++;
                return mockHttpMessageCallsCount == 1
                    ? new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(jsonGetAcceTokens)
                    }
                    : new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(jsonGetAllProductsResponse)
                    };
            });

            Mock<HttpClient> _httpClient = new Mock<HttpClient>(mockHttpMessageHandler.Object);

            MyClient<IMyClient> myClient = new MyClient<IMyClient>(_httpClient.Object, _loggerMock.Object, _tokenStorage.Object, _credsStorage.Object);


            //Act
            IEnumerable<CreateProductResponse> result = await myClient.Request<GetAllProductsRequest, IEnumerable<CreateProductResponse>>(new GetAllProductsRequest());


            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.Count().Should().Be(1);
            result.FirstOrDefault().Description.Should().Be("A description");
            result.FirstOrDefault().Title.Should().Be("New Product");
            mockHttpMessageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(2), // Expected number of calls
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                );
            _tokenStorage.Verify(r => r.IsTokenValid(), Times.Exactly(1));
            _tokenStorage.Verify(r => r.SaveToken(It.IsAny<string>()), Times.Exactly(1));
            _tokenStorage.Verify(r => r.GetToken(), Times.Exactly(1));
        }


    }
}
