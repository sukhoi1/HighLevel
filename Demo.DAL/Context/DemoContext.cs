using Demo.DAL.EntityConfiguration;
using Demo.DAL.Model;
using System.Data.Entity;

namespace Demo.DAL.Context
{
    public class DemoContext : DbContext
    {
        public DemoContext() : base("DemoConnection")
        {
        }

        public DbSet<Human> Humans { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<TModel> Entities<TModel>() where TModel : class
        {
            return Set<TModel>();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Configurations.AddFromAssembly(typeof(DemoContext).Assembly);
            modelBuilder.Configurations.Add(new HumanConfiguration());
        }

        public int Save()
        {
            return SaveChanges();
        }
    }
}
