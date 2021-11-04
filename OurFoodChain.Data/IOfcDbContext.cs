using Gsemac.Data;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Data.Models;

namespace OurFoodChain.Data {

    public interface IOfcDbContext :
        IDbContext {

        DbSet<World> Worlds { get; set; }

        DbSet<Creator> Creators { get; set; }
        DbSet<CreatorWorldPermissions> CreatorPermissions { get; set; }
        DbSet<CreatorFavorite> CreatorFavorites { get; set; }

        DbSet<Relationship> Relationships { get; set; }
        DbSet<Role> Roles { get; set; }

        DbSet<Picture> Pictures { get; set; }
        DbSet<Gallery> Galleries { get; set; }
        DbSet<GalleryPicture> GalleryPictures { get; set; }

        DbSet<Zone> Zones { get; set; }
        DbSet<Biome> Biomes { get; set; }
        DbSet<ZoneField> ZoneFields { get; set; }
        DbSet<ZoneCommonName> ZoneCommonNames { get; set; }

        DbSet<Taxon> Taxa { get; set; }
        DbSet<TaxonAncestor> TaxonAncestors { get; set; }
        DbSet<TaxonCommonName> TaxonCommonNames { get; set; }
        DbSet<TaxonCreator> TaxonCreators { get; set; }
        DbSet<TaxonField> TaxonFields { get; set; }
        DbSet<TaxonStatus> TaxonStatuses { get; set; }
        DbSet<TaxonRelationship> TaxonRelationships { get; set; }
        DbSet<TaxonRole> TaxonRoles { get; set; }
        DbSet<TaxonZone> TaxonZones { get; set; }
        DbSet<TaxonZoneChange> TaxonZoneChanges { get; set; }

        DbSet<Era> Eras { get; set; }

    }

}