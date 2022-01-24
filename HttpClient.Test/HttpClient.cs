using Moq;
using Rabani.Kernel.Utilites.Data.Common;
using RabaniHttpClient.Test.RequestObject.HttpClient.Object;
using RabaniHttpClient.XUnitTest.RequestObject.HttpClient.Object;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HttpClient.Builder;
using HttpClient.Common;
using HttpClient.Interface;
using HttpClient.Services;
using HttpClient.XUnitTest.RequestObject.HttpClient.Mock;
using Xunit;

namespace RabaniHttpClient.XUnitTest
{
    public class HttpClient
    {

        IHttpClientRequest httpClientRequest;
        CreateHttpClientRequest createHttpClientRequest;
        public HttpClient()
        {
            ConcereteHttpRequestBuilder createHttpRequestBuilder = new ConcereteHttpRequestBuilder();
            ICreateHttpRequestBuilder icreateHttpRequestBuilder = createHttpRequestBuilder;

            HttpClientService httpClientService = new HttpClientService(icreateHttpRequestBuilder);
            httpClientRequest = httpClientService;

        }

        [Fact]
        public async Task GetHttpClient()
        {
            //Arrange
            createHttpClientRequest = new CreateHttpClientRequest
            {

                EndPoint = new Uri("https://staging-identity.rabani.com/api/user/exist?username=yasinparsi"),
                SecondTimeOut = 60,
                RequestType = RequestType.Get
            };
            //Act
            ResultModel<object> resultModel = await httpClientRequest.JsonHttpClientRequest<object>(createHttpClientRequest, It.IsAny<CancellationToken>());
            //Assert
            Assert.True(resultModel.IsSucceed);
        }



        [Fact(DisplayName = "Http Client Get For 404 Error")]
        public async Task Fail_GetHttpClient_WithWrongUrl()
        {
            //Arrange
            createHttpClientRequest = new CreateHttpClientRequest
            {

                EndPoint = new Uri("https://reqres.in/api/users/23"),
                SecondTimeOut = 60
            };
            //Act
            ResultModel<object> resultModel = await httpClientRequest.JsonHttpClientRequest<object>(createHttpClientRequest, It.IsAny<CancellationToken>());
            //Assert
            Assert.False(resultModel.IsSucceed);
        }



        [Fact]
        public async Task PostHttpClient()
        {
            //Arrange
            BranchExpressDeliveryModel deliveryModel = new BranchExpressDeliveryModel
            {
                OutwardBranchOrderId = 2251,
                OutwardBranchAddress = "میدان تجریش",
                OutwardBranchCode = "",
                OutwardBranchId = 3,
                OutwardBranchName = "شعبه تجریش",
                OnDate = DateTime.Now
            };

            CreateHttpClientRequest createHttpClientRequest = new CreateHttpClientRequest()
            {

                RequestType = RequestType.Post,
                EndPoint = new Uri("https://staging-express.rabani.com/api/outward-branch-requests"),
                Data = deliveryModel,
                SecondTimeOut = 60,
                ContentTypes = "application/json"
            };
            //Act
            ResultModel<object> resultModel = await httpClientRequest.JsonHttpClientRequest<object>(createHttpClientRequest, It.IsAny<CancellationToken>());
            //Assert
            Assert.True(resultModel.IsSucceed);

        }

        [Fact]
        public async Task Fail_PostHttpClient_WithMissingParameter()
        {
            PostFailErrorObject postFailErrorObject = new PostFailErrorObject()
            {
                email = "sydney@fife"
            };
            //Arrange
            createHttpClientRequest = new CreateHttpClientRequest
            {
                EndPoint = new Uri("https://reqres.in/api/register"),
                RequestType = RequestType.Post,
                SecondTimeOut = 60,
                Data = postFailErrorObject
            };

            //Act 
            ResultModel<object> resultModel = await httpClientRequest.JsonHttpClientRequest<object>(createHttpClientRequest, It.IsAny<CancellationToken>());
            Assert.False(resultModel.IsSucceed);

        }





        [Theory]
        [ClassData(typeof(HttpClient_PutTestCase))]
        public async Task PutHttpClientWithoutHeader(XunitPostObject data)
        {
            //Arrange 

            createHttpClientRequest = new CreateHttpClientRequest
            {
                EndPoint = new Uri("https://jsonplaceholder.typicode.com/posts/1"),
                Data = data,
                RequestType = RequestType.Put,
                SecondTimeOut = 60
            };

            //Act
            ResultModel<object> resultModel = await httpClientRequest.JsonHttpClientRequest<object>(createHttpClientRequest, It.IsAny<CancellationToken>());
            //Assert
            Assert.True(resultModel.IsSucceed);
        }

        [Theory]
        [ClassData(typeof(HttpClient_PatchTestCase))]
        public async Task PatchHttpClient(XunitPostObject data)
        {
            createHttpClientRequest = new CreateHttpClientRequest
            {
                EndPoint = new Uri("https://jsonplaceholder.typicode.com/posts/1"),
                Data = data,
                RequestType = RequestType.Patch,
                SecondTimeOut = 60
            };
            //Act
            ResultModel<object> resultModel = await httpClientRequest.JsonHttpClientRequest<object>(createHttpClientRequest, It.IsAny<CancellationToken>());
            //Assert
            Assert.True(resultModel.IsSucceed);
        }



        public class BranchExpressDeliveryModel
        {
            public int OutwardBranchOrderId { get; set; }
            public int OutwardBranchId { get; set; }
            public string OutwardBranchCode { get; set; }
            public string OutwardBranchName { get; set; }
            public string OutwardBranchAddress { get; set; }
            public DateTime OnDate { get; set; }
        }

    }
}
