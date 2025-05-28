using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRW_DAL
{
    internal  class MRWContext : DbContext
    {
        private string connectionString;
        public DbSet<Projects> Projects { get; set; }
        public DbSet<KeyWords> KeyWords { get; set; }
        public DbSet<ProjectsKW> ProjectsKW { get; set; }   
        public MRWContext(DbContextOptions<MRWContext> options) : base(options) {
            var sqlServerOptionsExtension =
                       options.FindExtension<SqlServerOptionsExtension>();
            if (sqlServerOptionsExtension != null)
            {
                connectionString = sqlServerOptionsExtension.ConnectionString;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
