﻿using Microsoft.EntityFrameworkCore;
using OurFoodChain.Models;

namespace OurFoodChain.Data {

    internal static class OurFoodChainDbContextUtilities {

        public static void OnModelCreating(ModelBuilder modelBuilder) {

            // Creators

            modelBuilder.Entity<CreatorWorldPermissions>()
                .HasKey(e => new { e.WorldId, e.CreatorId });
            modelBuilder.Entity<CreatorFavorite>()
                .HasKey(e => new { e.CreatorId, e.SpeciesId });

            // Pictures

            modelBuilder.Entity<Picture>()
                .HasIndex(e => e.Url)
                .IsUnique();
            modelBuilder.Entity<GalleryPicture>()
                .HasKey(e => new { e.GalleryId, e.PictureId });

            // Zones

            modelBuilder.Entity<Zone>()
                .HasOne(e => e.DisplayedCommonName)
                .WithOne(e => e.Zone)
                .HasForeignKey<Zone>(e => e.DisplayedCommonNameId);

            modelBuilder.Entity<ZoneField>()
                .HasKey(e => new { e.ZoneId, e.Name });

            // Clades

            modelBuilder.Entity<Clade>()
                .HasOne(e => e.DisplayedCommonName)
                .WithOne(e => e.Clade)
                .HasForeignKey<Clade>(e => e.DisplayedCommonNameId);

            modelBuilder.Entity<CladeCommonName>()
                .HasIndex(e => new { e.CladeId, e.CommonName })
                .IsUnique();

            modelBuilder.Entity<Species>()
                .HasOne(e => e.DisplayedCommonName)
                .WithOne(e => e.Species)
                .HasForeignKey<Species>(e => e.DisplayedCommonNameId);

            modelBuilder.Entity<SpeciesCommonName>()
                .HasIndex(e => new { e.SpeciesId, e.CommonName })
                .IsUnique();

            modelBuilder.Entity<SpeciesCreator>()
                .HasKey(e => new { e.SpeciesId, e.CreatorId });

            modelBuilder.Entity<SpeciesAncestor>()
                .HasKey(e => new { e.SpeciesId, e.AncestorId });

            modelBuilder.Entity<SpeciesExtinction>()
                .HasOne(e => e.Species)
                .WithOne()
                .HasForeignKey<SpeciesExtinction>(e => e.SpeciesId);

            modelBuilder.Entity<SpeciesRelationship>()
                .HasKey(e => new { e.ActingSpeciesId, e.ReceivingSpeciesId, e.CustomRelationshipId, e.Relationship });

            modelBuilder.Entity<SpeciesRole>()
                .HasKey(e => new { e.SpeciesId, e.CustomRoleId, e.Role });

            modelBuilder.Entity<SpeciesField>()
                .HasKey(e => new { e.SpeciesId, e.Name });

            modelBuilder.Entity<SpeciesZone>()
                .HasKey(e => new { e.SpeciesId, e.ZoneId });

        }

    }

}