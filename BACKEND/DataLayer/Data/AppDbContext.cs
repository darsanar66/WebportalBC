using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;



namespace DataLayer.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        private readonly IConfiguration _configuration;
          private const int commandTimeoutInSeconds = 120; // Define your desired command timeout value

         
        public DbSet<Registration> Registration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {       
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(
                    connectionString,
                    options => options.CommandTimeout(commandTimeoutInSeconds)
                );
            }
        }
  
    
    }
}
