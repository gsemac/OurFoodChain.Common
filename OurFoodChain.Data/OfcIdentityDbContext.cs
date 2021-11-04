﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<Relationship> Relationships { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryPicture> GalleryPictures { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Biome> Biomes { get; set; }
        public DbSet<ZoneField> ZoneFields { get; set; }
        public DbSet<ZoneCommonName> ZoneCommonNames { get; set; }
        public DbSet<Taxon> Taxa { get; set; }
        public DbSet<TaxonAncestor> TaxonAncestors { get; set; }
        public DbSet<TaxonCommonName> TaxonCommonNames { get; set; }
        public DbSet<TaxonCreator> TaxonCreators { get; set; }
        public DbSet<TaxonField> TaxonFields { get; set; }
        public DbSet<TaxonRelationship> TaxonRelationships { get; set; }
        public DbSet<TaxonRole> TaxonRoles { get; set; }
        public DbSet<TaxonStatus> TaxonStatuses { get; set; }
        public DbSet<TaxonTag> TaxonTags { get; set; }
        public DbSet<TaxonZone> TaxonZones { get; set; }
        public DbSet<TaxonZoneChange> TaxonZoneChanges { get; set; }
        public DbSet<Era> Eras { get; set; }
        public DbSet<Tag> Tags { get; set; }

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