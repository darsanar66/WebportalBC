using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;


namespace RepositoryLayer.Interface
{
    public interface IAuthRepository
    {
         Task<Registration> Register(Registration registration);
        Task<Registration> Login(string userName, string password);
      
        Task<string> GenerateJwtToken(Registration registration);
    }
}