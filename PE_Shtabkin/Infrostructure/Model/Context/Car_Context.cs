using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace Infrostructure
{
    public class Car_Context : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            optionsBuilder
                .UseMySql(connectionString, serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
