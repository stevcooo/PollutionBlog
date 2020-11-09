using Microsoft.EntityFrameworkCore;
using PollutionSensor.v2.Models;
using PollutionSensor.v2.ViewModels;

namespace PollutionSensor.v2.Data
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
        public DbQuery<PollutionStatisticsViewModel> PollutionStatisticsViewModel { get; set; }
    }
}
