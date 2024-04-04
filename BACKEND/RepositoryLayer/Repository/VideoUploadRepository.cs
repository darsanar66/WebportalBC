
// using System;
// using System.IO;
// using System.Threading.Tasks;
// using DataLayer.Models;
// using Microsoft.EntityFrameworkCore;
// using RepositoryLayer.Interface;
// using Microsoft.AspNetCore.Http;
// using InfrastructureLayer.Services;
// using InfrastructureLayer.Interface;
// using Microsoft.Extensions.Logging; // Import the ILogger namespace

// namespace RepositoryLayer.Repository
// {
//     public class VideoUploadRepository : IVideoUploadRepository
//     {
//         private readonly AppDbContext _context;
//         private readonly IGoogleDriveService _googleDriveService;
//         private readonly ILogger<VideoUploadRepository> _logger; // Inject ILogger
//         private string? googleDriveFileId;

//         public VideoUploadRepository(AppDbContext context, IGoogleDriveService googleDriveService, ILogger<VideoUploadRepository> logger) // Inject ILogger in constructor
//         {
//             _context = context;
//             _googleDriveService = googleDriveService;
//             _logger = logger;
//         }

//         public async Task<Registration> UploadVideoAsync(IFormFile file, string UserName)
//         {
//             _logger.LogInformation("Uploading video."); // Log information message
            
//             try
//             {
//                 if (file == null || file.Length == 0)
//                 {
//                     _logger.LogError("Invalid file."); // Log error message
//                     throw new ArgumentException("Invalid file");
//                 }

//                 // Retrieve user by username
//                 var registration = await _context.Registration.FirstOrDefaultAsync(r => r.UserName == UserName);
//                 if (registration == null)
//                 {
//                     _logger.LogError("User not found."); // Log error message
//                     throw new ArgumentException("User not found");
//                 }

//                 using (var memoryStream = new MemoryStream())
//                 {
//                     await file.CopyToAsync(memoryStream);

//                     // Upload video to Google Drive
//                     var fileId = _googleDriveService.UploadVideoAsync(memoryStream, file.FileName);

//                     // Save video details to the registration entity
//                     registration.VideoName = file.FileName;
//                     registration.googleDriveFileId = fileId;
//                     registration.VideoComment = registration.VideoComment;
//                     registration.VideoContent = null;

//                     // Save the Registration entity to the database
//                     await _context.SaveChangesAsync();

//                     _logger.LogInformation("Video uploaded successfully."); // Log information message
//                     return registration;
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, $"Error uploading video: {ex.Message}"); // Log error message with exception details
//                 throw new Exception("Error uploading video.", ex);
//             }
//         }

//         public async Task<Registration> GetVideoByFileId(string VGoogleDriveFileId)
//         {
//             _logger.LogInformation("Retrieving video."); // Log information message
            
//             try
//             {
//                 var registration = await _context.Registration.FirstOrDefaultAsync(i => i.googleDriveFileId == googleDriveFileId);

//                 if (registration == null)
//                 {
//                     _logger.LogWarning("Video not found."); // Log warning message
//                     throw new Exception("Video not found");
//                 }

//                 return registration;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, $"Error retrieving video: {ex.Message}"); // Log error message with exception details
//                 throw new Exception("Error retrieving video.", ex);
//             }
//         }

//         public async Task AddComment(string VGoogleDriveFileId, string comment)
//         {
//             _logger.LogInformation("Adding comment."); // Log information message
            
//             try
//             {
//                 var registration = await GetVideoByFileId(VGoogleDriveFileId);
//                 registration.VideoComment = comment;
//                 await _context.SaveChangesAsync();
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, $"Error adding comment: {ex.Message}"); // Log error message with exception details
//                 throw new Exception("Error adding comment.", ex);
//             }
//         }

//         public async Task EditComment(string VGoogleDriveFileId, string comment)
//         {
//             _logger.LogInformation("Editing comment."); // Log information message
            
//             try
//             {
//                 var registration = await GetVideoByFileId(VGoogleDriveFileId);
//                 registration.VideoComment = comment;
//                 await _context.SaveChangesAsync();
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, $"Error editing comment: {ex.Message}"); // Log error message with exception details
//                 throw new Exception("Error editing comment.", ex);
//             }
//         }

//         public async Task DeleteComment(string VGoogleDriveFileId)
//         {
//             _logger.LogInformation("Deleting comment."); // Log information message
            
//             try
//             {
//                 var registration = await GetVideoByFileId(VGoogleDriveFileId);
//                 registration.VideoComment = string.Empty;
//                 await _context.SaveChangesAsync();
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, $"Error deleting comment: {ex.Message}"); // Log error message with exception details
//                 throw new Exception("Error deleting comment.", ex);
//             }
//         }
//     }
// }
// // using System;
// // using System.IO;
// // using System.Threading.Tasks;
// // using DataLayer.Models;
// // using Microsoft.EntityFrameworkCore;
// // using RepositoryLayer.Interface;
// // using Microsoft.AspNetCore.Http;
// // using InfrastructureLayer.Services;
// // using InfrastructureLayer.Interface;

// //  // Add this line
// // // Import the Google Drive service namespace

// // namespace RepositoryLayer.Repository
// // {
// //     public class VideoUploadRepository : IVideoUploadRepository
// //     {
// //         private readonly AppDbContext _context;
// //         private readonly IGoogleDriveService _googleDriveService;

// //         public VideoUploadRepository(AppDbContext context, IGoogleDriveService googleDriveService)
// //         {
// //             _context = context;
// //             _googleDriveService = googleDriveService;
// //         }

// //        public async Task<Registration> UploadVideoAsync(IFormFile file, string userName)
// // {
// //     try
// //     {
// //         if (file == null || file.Length == 0)
// //         {
// //             throw new ArgumentException("Invalid file");
// //         }

// //         // Retrieve user by username
// //         var registration = await _context.Registration.FirstOrDefaultAsync(r => r.UserName == userName);
// //         if (registration == null)
// //         {
// //             throw new ArgumentException("User not found");
// //         }

// //         using (var memoryStream = new MemoryStream())
// //         {
// //             await file.CopyToAsync(memoryStream);

// //             // Upload image to Google Drive
// //             var fileId =  _googleDriveService.UploadVideoAsync(memoryStream, file.FileName);

// //             // Save image details to the registration entity
// //             registration.VideoName = file.FileName;
// //             registration.VGoogleDriveFileId = fileId;
// //             registration.VideoComment = string.Empty;
// //             registration.VideoContent = null;

// //             // Save the Registration entity to the database
// //             await _context.SaveChangesAsync();

// //             return registration;
// //         }
// //     }
// //     catch (Exception ex)
// //     {
// //         throw new Exception("Error uploading image.", ex);
// //     }
// // }


// //         public async Task<Registration> GetVideoByFileId(string VGoogleDriveFileId)
// //         {
// //             try
// //             {
// //                 var registration = await _context.Registration.FirstOrDefaultAsync(i => i.VGoogleDriveFileId == VGoogleDriveFileId);

// //                 if (registration == null)
// //                 {
// //                     throw new Exception("Image not found");
// //                 }

// //                 return registration;
// //             }
// //             catch (Exception ex)
// //             {
// //                 throw new Exception("Error retrieving image.", ex);
// //             }
// //         }

// //         public async Task AddComment(string VGoogleDriveFileId, string comment)
// //         {
// //             try
// //             {
// //                 var registration = await GetVideoByFileId(VGoogleDriveFileId);
// //                 registration.VideoComment = comment;
// //                 await _context.SaveChangesAsync();
// //             }
// //             catch (Exception ex)
// //             {
// //                 throw new Exception("Error adding comment.", ex);
// //             }
// //         }

// //         public async Task EditComment(string VGoogleDriveFileId, string comment)
// //         {
// //             try
// //             {
// //                 var registration = await GetVideoByFileId(VGoogleDriveFileId);
// //                 registration.VideoComment = comment;
// //                 await _context.SaveChangesAsync();
// //             }
// //             catch (Exception ex)
// //             {
// //                 throw new Exception("Error editing comment.", ex);
// //             }
// //         }

// //         public async Task DeleteComment(string VGoogleDriveFileId)
// //         {
// //             try
// //             {
// //                 var registration = await GetVideoByFileId(VGoogleDriveFileId);
// //                 registration.VideoComment = string.Empty;
// //                 await _context.SaveChangesAsync();
// //             }
// //             catch (Exception ex)
// //             {
// //                 throw new Exception("Error deleting comment.", ex);
// //             }
// //         }
// //     }
// // }
