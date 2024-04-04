using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;



namespace RepositoryLayer.Interface
{
    public interface IVideoUploadRepository
    {
       Task<Registration> UploadVideoAsync(IFormFile file, string UserName);
        Task<Registration> GetVideoByFileId(string VGoogleDriveFileId);
        Task AddComment(string VGoogleDriveFileId, string comment);
        Task EditComment(string VGoogleDriveFileId, string comment);
        Task DeleteComment(string VGoogleDriveFileId);
    }       

            
}