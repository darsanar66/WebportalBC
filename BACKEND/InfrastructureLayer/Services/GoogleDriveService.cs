using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using InfrastructureLayer.Interface;
namespace InfrastructureLayer.Services
{
    public class GoogleDriveService : IGoogleDriveService

    {
        private readonly string[] _scopes = { DriveService.Scope.Drive };
        private readonly string _serviceAccountCredentialFilePath;
        private readonly string _folderId;

        public GoogleDriveService(string serviceAccountCredentialFilePath, string folderId)
        {
            _serviceAccountCredentialFilePath = serviceAccountCredentialFilePath;
            _folderId = folderId;
        }

        public DriveService GetDriveService()
        {
            GoogleCredential credential;
            using (var stream = new FileStream(_serviceAccountCredentialFilePath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(_scopes);
            }

            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Sett",
            });
        }

        public string UploadImageAsync(Stream imageStream, string fileName)
        {
            // Initialize Drive API service
            var service = GetDriveService();

            // Upload image to specified folder in Google Drive
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string> { _folderId }
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new MemoryStream())
            {
                imageStream.CopyTo(stream);
                request = service.Files.Create(fileMetadata, stream, "image/jpg");
                request.Fields = "id";
                request.Upload();
            }
            var file = request.ResponseBody;
            return file.Id;
        }

        public Stream GetImageAsync(string fileId)
        {
            // Initialize Drive API service
            var service = GetDriveService();

            // Download image from Google Drive
            var request = service.Files.Get(fileId);
            var stream = new MemoryStream();
            request.Download(stream);
            stream.Position = 0;
            return stream;
        }
        public string UploadVideoAsync(Stream videoStream, string fileName)
        {
            // Initialize Drive API service
            var service = GetDriveService();

            // Upload image to specified folder in Google Drive
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string> { _folderId }
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new MemoryStream())
            {
                videoStream.CopyTo(stream);
                request = service.Files.Create(fileMetadata, stream, "video/mp4");
                request.Fields = "id";
                request.Upload();
            }
            var file = request.ResponseBody;
            return file.Id;
        }

        public Stream GetVideoAsync(string fileId)
        {
            // Initialize Drive API service
            var service = GetDriveService();

            // Download image from Google Drive
            var request = service.Files.Get(fileId);
            var stream = new MemoryStream();
            request.Download(stream);
            stream.Position = 0;
            return stream;
        }
    }
}
