using Microsoft.EntityFrameworkCore;
using PolutionSensor.v2.Models;
using PolutionSensor.v2.ViewModels;

namespace PolutionSensor.v2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Entry>().Property(t => t.EntryId).ValueGeneratedOnAdd();
        }

        public DbSet<Entry> Entries { get; set; }
        public DbQuery<PolutionStatisticsViewModel> PolutionStatisticsViewModel { get; set; }
    }
}
