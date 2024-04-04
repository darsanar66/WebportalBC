using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using DataLayer.Models;
using RepositoryLayer.Interface;
using Laya.Controllers;
using Xunit;

namespace testLaya
{
    public class AuthTest
    {
        [Fact]
        
        public async Task Register_ValidRegistration_ReturnsOk()
        {
            // Arrange
            var registration = new Registration { /* Initialize registration properties */ };
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.Register(It.IsAny<Registration>())).ReturnsAsync(registration);
            var loggerMock = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(authRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await controller.Register(registration);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Register_InvalidRegistration_ReturnsBadRequest()
        {
            // Arrange
            var registration = new Registration { /* Initialize registration properties */ };
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.Register(It.IsAny<Registration>())).ThrowsAsync(new InvalidOperationException());
            var loggerMock = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(authRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await controller.Register(registration);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        [Fact]
    
       public async Task Login_ValidLogin_ReturnsToken()
{
    // Arrange
    var registration = new Registration { /* Initialize registration properties */ };
 // Mock a user object
    var token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZGlsc2hhIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3MTAyMjY3MzF9.APd0oqckAwI7XckdacjnM5uvdjIUInts-zG8jl942hCCkakv8xHXL7lE5p701GliGpPTf5njAGIA8mqgramjcQ"; // Mocked JWT token

    var authRepositoryMock = new Mock<IAuthRepository>();
    authRepositoryMock.Setup(repo => repo.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(registration);
    authRepositoryMock.Setup(repo => repo.GenerateJwtToken(registration)).ReturnsAsync(token); // Mock the token generation

    var loggerMock = new Mock<ILogger<AuthController>>();
    var controller = new AuthController(authRepositoryMock.Object, loggerMock.Object);

    // Act
    var result = await controller.Login(registration);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    var resultToken = okResult.Value.GetType().GetProperty("token").GetValue(okResult.Value, null) as string;
    Assert.Equal(token, resultToken);
}
    [Fact]
    
public async Task Login_InvalidLogin_ReturnsBadRequest()
{
    // Arrange
    var registration = new Registration { /* Initialize registration properties */ };
 
    var authRepositoryMock = new Mock<IAuthRepository>();
     authRepositoryMock.Setup(repo => repo.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Registration)null); // Return null for invalid login
   
    var loggerMock = new Mock<ILogger<AuthController>>();
    var controller = new AuthController(authRepositoryMock.Object, loggerMock.Object);

    // Act
    var result = await controller.Login(registration);

    // Assert
     Assert.IsType<BadRequestObjectResult>(result.Result);
}




        // [Fact]
        
        // public async Task Login_ValidLogin_ReturnsOk()
        // {
        //     // Arrange
        //     var registration = new Registration { /* Initialize registration properties */ };
        //     var authRepositoryMock = new Mock<IAuthRepository>();
        //     authRepositoryMock.Setup(repo => repo.Register(It.IsAny<Registration>())).ReturnsAsync((Registration)null);


        //     var loggerMock = new Mock<ILogger<AuthController>>();
        //     var controller = new AuthController(authRepositoryMock.Object, loggerMock.Object);

        //     // Act
        //     var result = await controller.Login(registration);

        //    var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        //     Assert.Equal("eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZGlsc2hhIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3MTAyMjY3MzF9.APd0oqckAwI7XckdacjnM5uvdjIUInts-zG8jl942hCCkakv8xHXL7lE5p701GliGpPTf5njAGIA8mqgramjcQ", BadRequestResult.Value);
        // }



    }
}
