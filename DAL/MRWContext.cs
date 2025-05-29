using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRealWorld.DAL
{
    internal  class MRWContext : DbContext
    {
        private string connectionString=string.Empty;
        public DbSet<Project> Projects { get; set; }
        public DbSet<KeyWords> KeyWords { get; set; }
        public DbSet<ProjectsKW> ProjectsKW { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Project_Pictures> Project_Pictures { get; set; }
        public MRWContext()
        {
        }
        public MRWContext(DbContextOptions<MRWContext> options) : base(options) {
            if (options != null)
            {
                var sqlServerOptionsExtension =
                           options.FindExtension<SqlServerOptionsExtension>();
                if (sqlServerOptionsExtension != null)
                {
                    connectionString = sqlServerOptionsExtension.ConnectionString;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
