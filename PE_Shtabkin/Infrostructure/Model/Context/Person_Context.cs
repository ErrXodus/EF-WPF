using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrostructure
{
    public class Person_Context : DbContext
    {
        public DbSet<Person> Persons { get; set; }
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
