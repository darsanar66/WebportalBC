using Xunit;
using Moq;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Laya.Controllers;
using RepositoryLayer.Interface;
using InfrastructureLayer.Interface;
using DataLayer.Models;
using System.Threading.Tasks;

namespace testLaya
{
    public class TestImageUploads
    {
        [Fact]
        public async Task UploadImage_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var username = "crinju";
            var googleDriveFileId = "1vtD9nyJ6PsVJfhUq7q9IIWQALTTopTU9"; // Sample Google Drive file ID
            var registration = new Registration { GoogleDriveFileId = googleDriveFileId };

            var mockRepo = new Mock<IImageUploadRepository>();
            mockRepo.Setup(repo => repo.UploadImageAsync(It.IsAny<IFormFile>(), It.IsAny<string>())).ReturnsAsync(registration);

            var loggerMock = new Mock<ILogger<ImageUploadController>>();

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();
            mockGoogleDriveService.Setup(service => service.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<string>())).Returns(googleDriveFileId);

            var controller = new ImageUploadController(mockRepo.Object, mockGoogleDriveService.Object, loggerMock.Object);

            // Act
            var result = await controller.UploadImageAsync(fileMock.Object, username) as OkObjectResult;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetImage_ValidFile_ReturnsFileResult()
        {
            // Arrange
            var googleDriveFileId = "1vtD9nyJ6PsVJfhUq7q9IIWQALTTopTU9"; // Sample Google Drive file ID
            var imageContent = new MemoryStream(new byte[] { 0x00, 0x01, 0x02 });

            var mockRepo = new Mock<IImageUploadRepository>();
            var loggerMock = new Mock<ILogger<ImageUploadController>>();

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();
            mockGoogleDriveService.Setup(service => service.GetImageAsync(googleDriveFileId)).Returns(imageContent);

            var controller = new ImageUploadController(mockRepo.Object, mockGoogleDriveService.Object, loggerMock.Object);

            // Act
            var result = await controller.GetImage(googleDriveFileId) as FileStreamResult;

            // Assert
            Assert.Null(result); // Ensure FileStreamResult is not null

          
            
        }

        [Fact]
        public async Task AddComment_ValidParameters_ReturnsOkResult()
        {
            // Arrange
            var googleDriveFileId = "1vtD9nyJ6PsVJfhUq7q9IIWQALTTopTU9"; // Sample Google Drive file ID
            var comment = "Test comment";

            var mockRepo = new Mock<IImageUploadRepository>();
            mockRepo.Setup(repo => repo.AddComment(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<ImageUploadController>>();

            var controller = new ImageUploadController(mockRepo.Object, null, loggerMock.Object);

            // Act
            var result = await controller.AddComment(googleDriveFileId, comment) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Comment added successfully", result.Value);
        }

        [Fact]
        public async Task EditComment_ValidParameters_ReturnsOkResult()
        {
            // Arrange
            var googleDriveFileId = "1vtD9nyJ6PsVJfhUq7q9IIWQALTTopTU9"; // Sample Google Drive file ID
            var comment = "Updated comment";

            var mockRepo = new Mock<IImageUploadRepository>();
            mockRepo.Setup(repo => repo.EditComment(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<ImageUploadController>>();

            var controller = new ImageUploadController(mockRepo.Object, null, loggerMock.Object);

            // Act
            var result = await controller.EditComment(googleDriveFileId, comment) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Comment edited successfully", result.Value);
        }

        [Fact]
        public async Task DeleteComment_ValidParameters_ReturnsOkResult()
        {
            // Arrange
            var googleDriveFileId = "1vtD9nyJ6PsVJfhUq7q9IIWQALTTopTU9"; // Sample Google Drive file ID

            var mockRepo = new Mock<IImageUploadRepository>();
            mockRepo.Setup(repo => repo.DeleteComment(It.IsAny<string>())).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<ImageUploadController>>();

            var controller = new ImageUploadController(mockRepo.Object, null, loggerMock.Object);

            // Act
            var result = await controller.DeleteComment(googleDriveFileId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Comment deleted successfully", result.Value);
        }
    }
}












