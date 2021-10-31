using Gsemac.Data.Extensions;
using Gsemac.Data.ValueConverters;
using Gsemac.Text;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Models;
using System;

namespace OurFoodChain.Data {

    internal static class OfcDbContextUtilities {

        // Public members

        public static void OnModelCreating(ModelBuilder modelBuilder) {

            // Custom value converters are used to work with types not supported by SQLite.
            // https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations

            modelBuilder.UseValueConverterForType<DateTimeOffset>(new UnixTimestampDateTimeOffsetConverter());
            modelBuilder.UseValueConverterForType<ulong>(new SignedUInt64Converter());

            // Creators

            modelBuilder.Entity<CreatorWorldPermissions>()
                .HasKey(e => new { e.WorldId, e.CreatorId });

            modelBuilder.Entity<CreatorFavorite>()
                .HasKey(e => new { e.CreatorId, e.CladeId });

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
                .HasOne(e => e.DisplayCommonName)
                .WithOne(e => e.Clade)
                .HasForeignKey<Clade>(e => e.DisplayCommonNameId);

            modelBuilder.Entity<Clade>()
                .HasOne(e => e.Parent)
                .WithMany()
                .HasForeignKey(e => e.ParentId);

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

            ConfigureCladeAncestor(modelBuilder);

            modelBuilder.Entity<CladeCommonName>()
                .HasIndex(e => new { e.CladeId, e.CommonName })
                .IsUnique();

            modelBuilder.Entity<CladeCreator>()
                .HasKey(e => new { e.CladeId, e.CreatorId });

            modelBuilder.Entity<CladeField>()
                .HasKey(e => new { e.CladeId, e.Name });

            modelBuilder.Entity<CladeRelationship>()
                .HasKey(e => new { e.AgentId, e.PatientId, e.CustomRelationshipId, e.RelationshipType });

            modelBuilder.Entity<CladeRole>()
                .HasKey(e => new { e.CladeId, e.CustomRoleId, e.RoleType });

            modelBuilder.Entity<CladeStatus>()
                .HasOne(e => e.Clade)
                .WithOne()
                .HasForeignKey<CladeStatus>(e => e.CladeId);

            modelBuilder.Entity<CladeZone>()
                .HasKey(e => new { e.CladeId, e.ZoneId });

        }

        // Private members

        private static void ConfigureCladeAncestor(ModelBuilder modelBuilder) {

            modelBuilder.Entity<CladeAncestor>()
                .HasKey(e => new { e.AncestorId, e.CladeId });

            modelBuilder.Entity<CladeAncestor>()
                .HasOne(e => e.Ancestor)
                .WithMany(e => e.Ancestors)
                .HasForeignKey(e => e.AncestorId);

            modelBuilder.Entity<CladeAncestor>()
                .HasOne(e => e.Clade)
                .WithMany()
                .HasForeignKey(e => e.CladeId);

        }

    }

}