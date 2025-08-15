using MediaTracker.Server.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MediaTracker.Server.Data
{
    public class LibraryDbContext : DbContext
    {
        /*public LibraryDbContext() { }*/

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Series> aSeries { get; set; }

        // May not need OnModelCreating - remove if not necessary
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer("AZURE_SQL_CONNECTIONSTRING",
                    options => options.EnableRetryOnFailure());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }*/




    }
}
