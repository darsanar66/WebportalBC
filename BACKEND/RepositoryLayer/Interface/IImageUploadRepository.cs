using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using DataLayer.Dtos.Image;


namespace RepositoryLayer.Interface
{
    public interface IImageUploadRepository
    {
     Task<Registration> GetImageDataAsync(int Id);
        Task<Registration> UploadImageAsync(ImageDto imageDto);
        Task<bool> CheckUserNameExists(string UserName);
        Task<IEnumerable<ImageDto>> GetAllImagesAsync();
        Task AddComment(int Id, string comment);
        Task EditComment(int Id, string comment);
        Task DeleteComment(int Id);
        Task DeleteImageAndComment(int Id);
    }
}
