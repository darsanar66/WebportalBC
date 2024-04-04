
// using System;
// using System.Data.SqlClient;
// using System.Threading.Tasks;
// using DataLayer.Models;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging; // Import the ILogger namespace
// using RepositoryLayer.Interface;

// namespace RepositoryLayer.Repository
// {
//     public class RegistrationRepository : IRegistrationRepository
//     {
//         private readonly IConfiguration _configuration;
//         private readonly ILogger<RegistrationRepository> _logger; // Inject ILogger

//         public RegistrationRepository(IConfiguration configuration, ILogger<RegistrationRepository> logger) // Inject ILogger in constructor
//         {
//             _configuration = configuration;
//             _logger = logger;
//         }

//         public async Task<bool> UserExistsAsync(string userName)
//         {
//             _logger.LogInformation("Checking if user exists."); // Log information message
            
//             try
//             {
//                 using (SqlConnection con = await GetSqlConnectionAsync())
//                 {
//                     await con.OpenAsync();

//                     string query = "SELECT COUNT(*) FROM registration WHERE UserName = @UserName";

//                     using (SqlCommand cmd = new SqlCommand(query, con))
//                     {
//                         cmd.Parameters.AddWithValue("@UserName", userName);

//                         int count = (int)await cmd.ExecuteScalarAsync();

//                         return count > 0;
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "An error occurred while checking if user exists."); // Log error message with exception details
//                 throw; // Re-throw exception to be handled by caller
//             }
//         }

//         public async Task<bool> EmailExistsAsync(string email)
//         {
//             _logger.LogInformation("Checking if email exists."); // Log information message
            
//             try
//             {
//                 using (SqlConnection con = await GetSqlConnectionAsync())
//                 {
//                     await con.OpenAsync();

//                     string query = "SELECT COUNT(*) FROM registration WHERE Email = @Email";

//                     using (SqlCommand cmd = new SqlCommand(query, con))
//                     {
//                         cmd.Parameters.AddWithValue("@Email", email);

//                         int count = (int)await cmd.ExecuteScalarAsync();

//                         return count > 0;
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "An error occurred while checking if email exists."); // Log error message with exception details
//                 throw; // Re-throw exception to be handled by caller
//             }
//         }

//         public async Task<bool> RegisterAsync(Registration registration)
//         {
//             _logger.LogInformation("Registering user."); // Log information message
            
//             try
//             {
//                 using (SqlConnection con = await GetSqlConnectionAsync())
//                 {
//                     await con.OpenAsync();

//                     string query = "INSERT INTO registration (UserName, Password, Email) VALUES (@UserName, @Password, @Email)";

//                     using (SqlCommand cmd = new SqlCommand(query, con))
//                     {
//                         cmd.Parameters.AddWithValue("@UserName", registration.UserName);
//                         cmd.Parameters.AddWithValue("@Password", registration.Password);
//                         cmd.Parameters.AddWithValue("@Email", registration.Email);

//                         int rowsAffected = await cmd.ExecuteNonQueryAsync();

//                         return rowsAffected > 0;
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "An error occurred during user registration."); // Log error message with exception details
//                 throw; // Re-throw exception to be handled by caller
//             }
//         }

//         public async Task<bool> LoginAsync(Login login)
//         {
//             _logger.LogInformation("Performing user login."); // Log information message
            
//             try
//             {
//                 using (SqlConnection con = await GetSqlConnectionAsync())
//                 {
//                     await con.OpenAsync();

//                     string query = "SELECT UserName FROM registration WHERE UserName = @UserName AND Password = @Password";

//                     using (SqlCommand cmd = new SqlCommand(query, con))
//                     {
//                         cmd.Parameters.AddWithValue("@UserName", login.UserName);
//                         cmd.Parameters.AddWithValue("@Password", login.Password);

//                         SqlDataReader reader = await cmd.ExecuteReaderAsync();

//                         return reader.HasRows;
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "An error occurred during user login."); // Log error message with exception details
//                 throw; // Re-throw exception to be handled by caller
//             }
//         }

//         private async Task<SqlConnection> GetSqlConnectionAsync()
//         {
//             string connectionString = _configuration.GetConnectionString("DefaultConnection");
//             return new SqlConnection(connectionString);
//         }
//     }
// }
// // // RegistrationRepository.cs
// // using System.Data.SqlClient;
// // using System.Threading.Tasks;
// // using DataLayer.Models;
// // using Microsoft.Extensions.Configuration;
// // using RepositoryLayer.Interface;

// // namespace RepositoryLayer.Repository
// // {
// //     public class RegistrationRepository : IRegistrationRepository
// //     {
// //         private readonly IConfiguration _configuration;

// //         public RegistrationRepository(IConfiguration configuration)
// //         {
// //             _configuration = configuration;
// //         }

// //         public async Task<bool> UserExistsAsync(string userName)
// //         {
// //             using (SqlConnection con = await GetSqlConnectionAsync())
// //             {
// //                 await con.OpenAsync();

// //                 string query = "SELECT COUNT(*) FROM registration WHERE UserName = @UserName";

// //                 using (SqlCommand cmd = new SqlCommand(query, con))
// //                 {
// //                     cmd.Parameters.AddWithValue("@UserName", userName);

// //                     int count = (int)await cmd.ExecuteScalarAsync();

// //                     return count > 0;
// //                 }
// //             }
// //         }

// //         public async Task<bool> EmailExistsAsync(string email)
// //         {
// //             using (SqlConnection con = await GetSqlConnectionAsync())
// //             {
// //                 await con.OpenAsync();

// //                 string query = "SELECT COUNT(*) FROM registration WHERE Email = @Email";

// //                 using (SqlCommand cmd = new SqlCommand(query, con))
// //                 {
// //                     cmd.Parameters.AddWithValue("@Email", email);

// //                     int count = (int)await cmd.ExecuteScalarAsync();

// //                     return count > 0;
// //                 }
// //             }
// //         }

// //         public async Task<bool> RegisterAsync(Registration registration)
// //         {
// //             using (SqlConnection con = await GetSqlConnectionAsync())
// //             {
// //                 await con.OpenAsync();

// //                 string query = "INSERT INTO registration (UserName, Password, Email) VALUES (@UserName, @Password, @Email)";

// //                 using (SqlCommand cmd = new SqlCommand(query, con))
// //                 {
// //                     cmd.Parameters.AddWithValue("@UserName", registration.UserName);
// //                     cmd.Parameters.AddWithValue("@Password", registration.Password);
// //                     cmd.Parameters.AddWithValue("@Email", registration.Email);
             

// //                     int rowsAffected = await cmd.ExecuteNonQueryAsync();

// //                     return rowsAffected > 0;
// //                 }
// //             }
// //         }

// //         public async Task<bool> LoginAsync(Login login)
// //         {
// //             using (SqlConnection con = await GetSqlConnectionAsync())
// //             {
// //                 await con.OpenAsync();

// //                 string query = "SELECT UserName FROM registration WHERE UserName = @UserName AND Password = @Password";

// //                 using (SqlCommand cmd = new SqlCommand(query, con))
// //                 {
// //                     cmd.Parameters.AddWithValue("@UserName", login.UserName);
// //                     cmd.Parameters.AddWithValue("@Password", login.Password);

// //                     SqlDataReader reader = await cmd.ExecuteReaderAsync();

// //                     return reader.HasRows;
// //                 }
// //             }
// //         }

// //         private async Task<SqlConnection> GetSqlConnectionAsync()
// //         {
// //             string connectionString = _configuration.GetConnectionString("DefaultConnection");
// //             return new SqlConnection(connectionString);
// //         }
// //     }
// // }
