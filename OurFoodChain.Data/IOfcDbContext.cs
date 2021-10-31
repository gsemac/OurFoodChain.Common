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

        DbSet<CustomRelationship> CustomRelationships { get; set; }
        DbSet<CustomRole> CustomRoles { get; set; }

        DbSet<Picture> Pictures { get; set; }
        DbSet<Gallery> Galleries { get; set; }
        DbSet<GalleryPicture> GalleryPictures { get; set; }

        DbSet<Zone> Zones { get; set; }
        DbSet<CustomBiome> CustomBiomes { get; set; }
        DbSet<ZoneField> ZoneFields { get; set; }
        DbSet<ZoneCommonName> ZoneCommonNames { get; set; }

        DbSet<Clade> Clades { get; set; }
        DbSet<CladeAncestor> CladeAncestors { get; set; }
        DbSet<CladeCommonName> CladeCommonNames { get; set; }
        DbSet<CladeCreator> CladeCreators { get; set; }
        DbSet<CladeField> CladeFields { get; set; }
        DbSet<CladeStatus> CladeStatuses { get; set; }
        DbSet<CladeRelationship> CladeRelationships { get; set; }
        DbSet<CladeRole> CladeRoles { get; set; }
        DbSet<CladeZone> CladeZones { get; set; }
        DbSet<CladeZoneChange> CladeZoneChanges { get; set; }

        DbSet<Era> Eras { get; set; }

    }

}