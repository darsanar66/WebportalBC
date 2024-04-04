using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DataLayer.Models;
using RepositoryLayer.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace RepositoryLayer.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthRepository> _logger;


        public AuthRepository(AppDbContext context, IConfiguration configuration, ILogger<AuthRepository> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GenerateJwtToken(Registration registration)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, registration.UserName),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var key = new byte[64]; // 512 bits
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(key);
                }
                var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating JWT token");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<Registration> Register(Registration registration)
        {
            try
            {
                if (await UserExistsAsync(registration.UserName))
                {   
                    throw new InvalidOperationException("Username is already taken.");
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(registration.Password, out passwordHash, out passwordSalt);

                var newRegistration = new Registration
                {
                    UserName = registration.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = registration.Email,
                    Password = registration.Password,
                    Id = registration.Id

                };

                _context.Registration.Add(newRegistration);
                await _context.SaveChangesAsync();

                return newRegistration;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<Registration> Login(string username, string password)
        {
            try
            {
                var registration = await _context.Registration.FirstOrDefaultAsync(u => u.UserName == username);

                if (registration == null || !VerifyPasswordHash(password, registration.PasswordHash, registration.PasswordSalt))
                {
                    return null;
                }

                return registration;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            try
            {
                return await _context.Registration.AnyAsync(u => u.UserName == username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if user exists");
                throw; // Re-throw the exception after logging
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating password hash");
                throw; // Re-throw the exception after logging
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512(storedSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(storedHash);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying password hash");
                throw; // Re-throw the exception after logging
            }
        }
    }
}
// using System;
// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.Linq;
// using System.Security.Claims;
// using System.Security.Cryptography;
// using System.Text;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
// using DataLayer.Models;
// using RepositoryLayer.Interface;

// namespace RepositoryLayer.Repository
// {
//     public class AuthRepository : IAuthRepository
//     {
//         private readonly AppDbContext _context;
//         private readonly IConfiguration _configuration;

//         private readonly ILogger _logger;

//         public AuthRepository(AppDbContext context, IConfiguration configuration, ILogger logger)
//         {
//             _context = context;
//             _configuration = configuration;
//             _logger = logger;
//         }

//         public async Task<string> GenerateJwtToken(Registration registration)
//         {
//             List<Claim> claims = new List<Claim>
//             {
//                 new Claim(ClaimTypes.Name, registration.UserName),
//                 new Claim(ClaimTypes.Role, "Admin")
//             };

//             var key = new byte[64]; // 512 bits
//             using (var rng = RandomNumberGenerator.Create())
//             {
//                 rng.GetBytes(key);
//             }
//             var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

//             var token = new JwtSecurityToken(
//                 issuer: _configuration["Jwt:Issuer"],
//                 audience: _configuration["Jwt:Audience"],
//                 claims: claims,
//                 expires: DateTime.Now.AddDays(1),
//                 signingCredentials: creds);

//             var jwt = new JwtSecurityTokenHandler().WriteToken(token);

//             return jwt;
//         }

//         public async Task<Registration> Register(Registration registration)
//         {
//             if (await UserExistsAsync(registration.UserName))
//             {   
//                 throw new InvalidOperationException("Username is already taken.");
//             }

//             byte[] passwordHash, passwordSalt;
//             CreatePasswordHash(registration.Password, out passwordHash, out passwordSalt);

//             var newRegistration = new Registration
//             {
//                 UserName = registration.UserName,
//                 PasswordHash = passwordHash,
//                 PasswordSalt = passwordSalt
//             };

//             _context.Registration.Add(newRegistration);
//             await _context.SaveChangesAsync();

//             return newRegistration;
//         }

//         public async Task<Registration> Login(string username, string password)
//         {
//             var registration = await _context.Registration.FirstOrDefaultAsync(u => u.UserName == username);

//             if (registration == null || !VerifyPasswordHash(password, registration.PasswordHash, registration.PasswordSalt))
//             {
//                 return null;
//             }

//             return registration;
//         }

//         public async Task<bool> UserExistsAsync(string username)
//         {
//             return await _context.Registration.AnyAsync(u => u.UserName == username);
//         }

//         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//         {
//             using (var hmac = new HMACSHA512())
//             {
//                 passwordSalt = hmac.Key;
//                 passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//             }
//         }

//         private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
//         {
//             using (var hmac = new HMACSHA512(storedSalt))
//             {
//                 var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//                 return computedHash.SequenceEqual(storedHash);
//             }
//         }
//     }
// }
