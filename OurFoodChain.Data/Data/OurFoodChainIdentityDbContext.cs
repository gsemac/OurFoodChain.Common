﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Models;

namespace OurFoodChain.Data {

    public class OurFoodChainIdentityDbContext :
        IdentityDbContext<ApplicationUser>,
        IOurFoodChainDbContext {

        public DbSet<World> Worlds { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<CreatorWorldPermissions> CreatorPermissions { get; set; }
        public DbSet<CreatorFavorite> CreatorFavorites { get; set; }
        public DbSet<CustomSpeciesRelationship> CustomSpeciesRelationships { get; set; }
        public DbSet<CustomSpeciesRole> CustomSpeciesRoles { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PictureGallery> PictureGalleries { get; set; }
        public DbSet<GalleryPicture> GalleryPictures { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<CustomZoneType> CustomZoneTypes { get; set; }
        public DbSet<ZoneField> ZoneFields { get; set; }
        public DbSet<ZoneCommonName> ZoneCommonNames { get; set; }
        public DbSet<Clade> Clades { get; set; }
        public DbSet<CladeCommonName> CladeCommonNames { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<SpeciesCommonName> SpeciesCommonNames { get; set; }
        public DbSet<SpeciesCreator> SpeciesCreators { get; set; }
        public DbSet<SpeciesAncestor> SpeciesAncestors { get; set; }
        public DbSet<SpeciesExtinction> SpeciesExtinctions { get; set; }
        public DbSet<SpeciesRelationship> SpeciesRelationships { get; set; }
        public DbSet<SpeciesRole> SpeciesRoles { get; set; }
        public DbSet<SpeciesZone> SpeciesZones { get; set; }
        public DbSet<SpeciesZoneEdit> SpeciesZoneHistory { get; set; }
        public DbSet<SpeciesField> SpeciesFields { get; set; }
        public DbSet<Era> Eras { get; set; }

        public OurFoodChainIdentityDbContext(DbContextOptions<OurFoodChainIdentityDbContext> options) :
            base(options) {
        }
        public OurFoodChainIdentityDbContext(DbContextOptions options) :
            base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            OurFoodChainDbContextUtilities.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(e => e.Creator)
                .WithOne()
                .HasForeignKey<ApplicationUser>(e => e.CreatorId);

            base.OnModelCreating(modelBuilder);

        }

    }

}