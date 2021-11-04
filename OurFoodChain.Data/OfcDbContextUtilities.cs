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
                .HasKey(e => new { e.CreatorId, e.TaxonId });

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

            modelBuilder.Entity<Taxon>()
                .HasOne(e => e.DisplayCommonName)
                .WithOne(e => e.Taxon)
                .HasForeignKey<Taxon>(e => e.DisplayCommonNameId);

            modelBuilder.Entity<Taxon>()
                .HasOne(e => e.Parent)
                .WithMany()
                .HasForeignKey(e => e.ParentId);

            // It is possible for multiple taxa of the same rank to share the same name.
            // While the most common example would be species, such instances exist for other ranks as well.
            // https://species.wikimedia.org/wiki/List_of_valid_homonyms
            // This should be allowed to occur, but only for taxa with different parents (e.g. there should not be two of the same species in the same genus).

            modelBuilder.Entity<Taxon>()
                .HasIndex(e => new { e.WorldId, e.ParentId, e.Name })
                .IsUnique();

            modelBuilder.Entity<Taxon>()
                .Property(e => e.Name)
                .HasConversion(new CaseConversionStringConverter(StringCasing.Lower));

            ConfigureCladeAncestors(modelBuilder);

            modelBuilder.Entity<TaxonCommonName>()
                .HasIndex(e => new { e.TaxonId, e.CommonName })
                .IsUnique();

            modelBuilder.Entity<TaxonCreator>()
                .HasKey(e => new { e.TaxonId, e.CreatorId });

            modelBuilder.Entity<TaxonField>()
                .HasKey(e => new { e.TaxonId, e.Name });

            modelBuilder.Entity<TaxonRelationship>()
                .HasKey(e => new { e.AgentId, e.PatientId, e.RelationshipId });

            modelBuilder.Entity<TaxonRole>()
                .HasKey(e => new { e.TaxonId, e.RoleId });

            modelBuilder.Entity<TaxonStatus>()
                .HasOne(e => e.Taxon)
                .WithOne()
                .HasForeignKey<TaxonStatus>(e => e.TaxonId);

            ConfigureTaxonTags(modelBuilder);

            modelBuilder.Entity<TaxonZone>()
                .HasKey(e => new { e.TaxonId, e.ZoneId });

        }

        // Private members

        private static void ConfigureCladeAncestors(ModelBuilder modelBuilder) {

            modelBuilder.Entity<TaxonAncestor>()
                .HasKey(e => new { e.AncestorId, e.TaxonId });

            modelBuilder.Entity<TaxonAncestor>()
                .HasOne(e => e.Ancestor)
                .WithMany(e => e.Ancestors)
                .HasForeignKey(e => e.AncestorId);

            modelBuilder.Entity<TaxonAncestor>()
                .HasOne(e => e.Taxon)
                .WithMany()
                .HasForeignKey(e => e.TaxonId);

        }
        private static void ConfigureTaxonTags(ModelBuilder modelBuilder) {

            modelBuilder.Entity<TaxonTag>()
                .HasKey(e => new { e.TaxonId, e.TagId });

        }

    }

}