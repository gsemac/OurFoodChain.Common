using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Models;

namespace OurFoodChain.Data {

    public class OfcIdentityDbContext :
        IdentityDbContext<ApplicationUser>,
        IOfcDbContext {

        public DbSet<World> Worlds { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<CreatorWorldPermissions> CreatorPermissions { get; set; }
        public DbSet<CreatorFavorite> CreatorFavorites { get; set; }
        public DbSet<CustomRelationship> CustomRelationships { get; set; }
        public DbSet<CustomRole> CustomRoles { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryPicture> GalleryPictures { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<CustomBiome> CustomBiomes { get; set; }
        public DbSet<ZoneField> ZoneFields { get; set; }
        public DbSet<ZoneCommonName> ZoneCommonNames { get; set; }
        public DbSet<Clade> Clades { get; set; }
        public DbSet<CladeAncestor> CladeAncestors { get; set; }
        public DbSet<CladeCommonName> CladeCommonNames { get; set; }
        public DbSet<CladeCreator> CladeCreators { get; set; }
        public DbSet<CladeField> CladeFields { get; set; }
        public DbSet<CladeStatus> CladeStatuses { get; set; }
        public DbSet<CladeRelationship> CladeRelationships { get; set; }
        public DbSet<CladeRole> CladeRoles { get; set; }
        public DbSet<CladeZone> CladeZones { get; set; }
        public DbSet<CladeZoneChange> CladeZoneChanges { get; set; }
        public DbSet<Era> Eras { get; set; }

        public OfcIdentityDbContext(DbContextOptions options) :
            base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            OfcDbContextUtilities.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(e => e.Creator)
                .WithOne()
                .HasForeignKey<ApplicationUser>(e => e.CreatorId);

            base.OnModelCreating(modelBuilder);

        }

    }

}