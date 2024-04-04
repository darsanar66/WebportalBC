using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using Microsoft.Extensions.Logging; 
using DataLayer.Dtos.Image;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using InfrastructureLayer.Interface;

namespace RepositoryLayer.Repository
{
    public class ImageUploadRepository : IImageUploadRepository
    {
        private readonly AppDbContext _context;
        private readonly IGoogleDriveService _googleDriveService;
        private readonly ILogger<ImageUploadRepository> _logger;

        public ImageUploadRepository(AppDbContext context, ILogger<ImageUploadRepository> logger, IGoogleDriveService googleDriveService)
        {
            _context = context;
            _logger = logger;
            _googleDriveService = googleDriveService;
        }

        public async Task<Registration> UploadImageAsync(ImageDto imageDto)
        {
            _logger.LogInformation("Uploading image.");

            try
            {
                if (imageDto.file == null || imageDto.file.Length == 0)
                {
                    _logger.LogError("Invalid file.");
                    throw new ArgumentException("Invalid file");
                }

                var registration = await _context.Registration.FirstOrDefaultAsync(r => r.UserName == imageDto.UserName);
                if (registration == null)
                {
                    _logger.LogError("UserName does not exist, please register first.");
                    throw new ArgumentException("UserName does not exist, please register first.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await imageDto.file.CopyToAsync(memoryStream);
                      var fileId = _googleDriveService.UploadImageAsync(memoryStream, imageDto.file.FileName);

                    // Save image content to the database
                    registration.ImageContent = memoryStream.ToArray();
                    // registration.ImageName = imageDto.file.FileName;
                    // registration.ImageComment = imageDto.comment;
                      registration.ImageName = imageDto.file.FileName;
            registration.GoogleDriveFileId = fileId;
            registration.ImageComment = imageDto.comment;
         

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Image uploaded successfully.");
                    return registration;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error uploading image: {ex.Message}");
                throw new Exception("Error uploading image.", ex);
            }
        }

        public async Task<bool> CheckUserNameExists(string UserName)
        {
            var registration = await _context.Registration.FirstOrDefaultAsync(u => u.UserName == UserName);
            return registration != null;
        }


        public async Task<Registration> GetImageByFileId(string GoogleDriveFileId)
        {
            _logger.LogInformation("Retrieving image.");
            
            try
            {
                var registration = await _context.Registration.FirstOrDefaultAsync(i => i.GoogleDriveFileId == GoogleDriveFileId);

                if (registration == null)
                {
                    _logger.LogWarning("Image not found.");
                    throw new Exception("Image not found");
                }

                return registration;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving image: {ex.Message}");
                throw new Exception("Error retrieving image.", ex);
            }
        }

        
        public async Task AddComment(int Id, string comment)
        {
            _logger.LogInformation("Adding comment.");

            try
            {
                var registration = await _context.Registration.FindAsync(Id);
                registration.ImageComment = comment;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding comment: {ex.Message}");
                throw new Exception("Error adding comment.", ex);
            }
        }

        public async Task EditComment(int Id, string comment)
        {
            _logger.LogInformation("Editing comment.");

            try
            {
                var registration = await _context.Registration.FindAsync(Id);
                registration.ImageComment = comment;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error editing comment: {ex.Message}");
                throw new Exception("Error editing comment.", ex);
            }
        }

        public async Task DeleteComment(int Id)
        {
            _logger.LogInformation("Deleting comment.");

            try
            {
                var registration = await _context.Registration.FindAsync(Id);
                registration.ImageComment = string.Empty;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting comment: {ex.Message}");
                throw new Exception("Error deleting comment.", ex);
            }
        }

        public async Task DeleteImageAndComment(int id)
{
    _logger.LogInformation("Deleting image and comment.");

    try
    {
        var registration = await _context.Registration.FindAsync(id);
        if (registration == null)
        {
            _logger.LogWarning($"Image with ID {id} not found in database.");
            throw new KeyNotFoundException($"Image with ID {id} not found.");
        }

        // Clear the comment from the registration
        registration.ImageComment = null;
        registration.ImageContent = null;
        registration.ImageName = null;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Image and comment deleted successfully.");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Error deleting image and comment: {ex.Message}");
        throw new Exception($"Error deleting image and comment: {ex.Message}", ex);
    }
}

         public async Task<IActionResult> DisplayImageById(int Id)
        {
            _logger.LogInformation("Displaying image from database by ID.");

            try
            {
                var registration = await _context.Registration.FindAsync(Id);
                if (registration == null)
                {
                    _logger.LogWarning($"Image with ID {Id} not found in database.");
                    throw new KeyNotFoundException($"Image with ID {Id} not found.");
                }

                if (registration.ImageContent == null)
                {
                    _logger.LogWarning($"Image content for ID {Id} is null.");
                    throw new Exception($"Image content for ID {Id} is null.");
                }

                // Return the image content as a file
                return new FileStreamResult(new MemoryStream(registration.ImageContent), "image/jpeg");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error displaying image from database by ID: {ex.Message}");
                throw new Exception("Error displaying image from database by ID.", ex);
            }
        }
        public async Task<IEnumerable<ImageDto>> GetAllImagesAsync()
{
    try
    {
        var images = await _context.Registration
            .Where(registration => !string.IsNullOrEmpty(registration.ImageComment) || registration.ImageContent != null)
            .Select(registration => new ImageDto
            {
                UserName = registration.UserName,
                ImageContentBase64 = registration.ImageContent != null ? Convert.ToBase64String(registration.ImageContent) : null,
                comment = registration.ImageComment,
                file = null,
                Id = registration.Id
                // Map other properties as needed
            })
            .ToListAsync();

        return images;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Error retrieving all images: {ex.Message}");
        throw new Exception("Error retrieving all images.", ex);
    }
}


        //  public async Task<IEnumerable<ImageDto>> GetAllImagesAsync()
        // {
        //     try
        //     {
        //         var images = await _context.Registration
        //             .Select(registration => new ImageDto
        //             {
        //                 UserName = registration.UserName,
        //                 ImageContentBase64 = registration.ImageContent != null ? Convert.ToBase64String(registration.ImageContent) : null,
        //                 comment = registration.ImageComment,
        //                 file = null,
        //                 Id = registration.Id
        //                 // Map other properties as needed
        //             })
        //             .ToListAsync();

        //         return images;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, $"Error retrieving all images: {ex.Message}");
        //         throw new Exception("Error retrieving all images.", ex);
        //     }cd 
        public async Task<Registration> GetImageDataAsync(int Id)
{
    try
    {
        var registration = await _context.Registration.FindAsync(Id);
        if (registration == null)
        {
            _logger.LogWarning($"Image with ID {Id} not found in database.");
            throw new KeyNotFoundException($"Image with ID {Id} not found.");
        }
        
        return registration;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Error retrieving image data by ID: {ex.Message}");
        throw new Exception($"Error retrieving image data by ID: {ex.Message}", ex);
    }
}

    }
}
