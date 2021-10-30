using Gsemac.Data.Extensions;
using Gsemac.Data.ValueConverters;
using Gsemac.Text;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Models;
using System;

namespace OurFoodChain.Data {

    internal static class OfcDbContextUtilities {

        public static void OnModelCreating(ModelBuilder modelBuilder) {

            // Custom value converters are used to work with types not supported by SQLite.
            // https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations

            modelBuilder.UseValueConverterForType<DateTimeOffset>(new UnixTimestampDateTimeOffsetConverter());
            modelBuilder.UseValueConverterForType<ulong>(new SignedUInt64Converter());

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

            // It is possible for multiple taxa of the same rank to share the same name.
            // While the most common example would be species, such instances exist for other ranks as well.
            // https://species.wikimedia.org/wiki/List_of_valid_homonyms
            // This should be allowed to occur, but only for taxa with different parents (e.g. there should not be two of the same species in the same genus).

            modelBuilder.Entity<Clade>()
                .HasIndex(e => new { e.WorldId, e.ParentId, e.Name })
                .IsUnique();

            modelBuilder.Entity<Clade>()
                .Property(e => e.Name)
                .HasConversion(new CaseConversionStringConverter(StringCasing.Lower));

            modelBuilder.Entity<CladeCommonName>()
                .HasIndex(e => new { e.CladeId, e.CommonName })
                .IsUnique();

            // Species

            modelBuilder.Entity<Species>()
                .HasOne(e => e.Clade)
                .WithOne(e => e.Species)
                .HasForeignKey<Species>(e => e.CladeId);

            modelBuilder.Entity<Species>()
                .HasIndex(e => new { e.CladeId })
                .IsUnique();

            modelBuilder.Entity<SpeciesCreator>()
                .HasKey(e => new { e.SpeciesId, e.CreatorId });

            modelBuilder.Entity<SpeciesRelationship>()
                .HasKey(e => new { e.ObjectSpeciesId, e.SubjectSpeciesId, e.CustomRelationshipId, e.Relationship });

            modelBuilder.Entity<SpeciesRole>()
                .HasKey(e => new { e.SpeciesId, e.CustomRoleId, e.Role });

            modelBuilder.Entity<SpeciesField>()
                .HasKey(e => new { e.SpeciesId, e.Name });

            modelBuilder.Entity<SpeciesZone>()
                .HasKey(e => new { e.SpeciesId, e.ZoneId });

        }

    }

}