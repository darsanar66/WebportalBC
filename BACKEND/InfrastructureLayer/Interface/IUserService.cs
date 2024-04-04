using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models;



namespace InfrastructureLayer.Interface
{
    public interface IUserService
    {
        string GetMyName();
      
        void UpdateRegistration(Registration registration);
    }
}