using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.PersistModels
{
    public class AppContext : DbContext
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=yestdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static AppContext _instance;

        public DbSet<AssociatePersist> Associates { get; set; }
        public DbSet<AssociateLoginPersist> AssociateLogins { get; set; }
        public DbSet<AssociateRolePersist> AssociateRoles { get; set; }
        public DbSet<CarPersist> Cars { get; set; }
        public DbSet<OrderPersist> Orders { get; set; }
        public DbSet<RolePersist> Roles { get; set; }

        public static AppContext GetInstance()
        {
            if (_instance == null)
                _instance = new AppContext();
            return _instance;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSqlServer(connectionString);
        }
    }
}
