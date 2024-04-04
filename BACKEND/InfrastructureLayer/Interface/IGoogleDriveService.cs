using System;
using Google.Apis.Drive.v3;
using System.IO;


namespace InfrastructureLayer.Interface
{
    public interface IGoogleDriveService
    {
        DriveService GetDriveService();
        string UploadImageAsync(Stream imageStream, string fileName);
        Stream GetImageAsync(string fileId);
        string UploadVideoAsync(Stream videoStream, string fileName);
        Stream GetVideoAsync(string fileId);

    }
}