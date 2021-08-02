using System.Data.Entity;
using System.Diagnostics;
using TurkishHomeApi.Models.Business;

namespace TurkishHomeApi
{
    public class AppDbContext : System.Data.Entity.DbContext
    {
        public AppDbContext() : base("name=remote")
        {
            Database.Log = e => Debug.WriteLine(e);
        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().ToTable("Exams");
            modelBuilder.Entity<Level>().ToTable("Levels");
            modelBuilder.Entity<Material>().ToTable("Materials");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<Unit>().ToTable("Units");
            modelBuilder.Entity<Setting>().ToTable("Settings");
        }
    }
}