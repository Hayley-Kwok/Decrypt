using Microsoft.EntityFrameworkCore;

namespace Privasight.Model.Facebook.Data
{
    public class FbContext : DbContext
    {
        public DbSet<InteractedAdvertiser> InteractedAdvertisers { get; set; } = default!;
        // public DbSet<SharedAdvertiser> SharedAdvertisers { get; set; } = default!;

        public FbContext(DbContextOptions<FbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<SharedAdvertiser>(advertiser =>
            // {
            //     advertiser.HasKey(e => e.Id);
            //     advertiser.Property(e => e.Id).ValueGeneratedOnAdd();
            // });

            modelBuilder.Entity<InteractedAdvertiser>(advertiser =>
            {
                advertiser.HasKey(e => e.Id);
                advertiser.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
