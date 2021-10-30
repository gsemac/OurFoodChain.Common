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

        DbSet<CustomSpeciesRelationship> CustomSpeciesRelationships { get; set; }
        DbSet<CustomSpeciesRole> CustomSpeciesRoles { get; set; }

        DbSet<Picture> Pictures { get; set; }
        DbSet<Gallery> Galleries { get; set; }
        DbSet<GalleryPicture> GalleryPictures { get; set; }

        DbSet<Zone> Zones { get; set; }
        DbSet<CustomZoneType> CustomZoneTypes { get; set; }
        DbSet<ZoneField> ZoneFields { get; set; }
        DbSet<ZoneCommonName> ZoneCommonNames { get; set; }

        DbSet<Clade> Clades { get; set; }
        DbSet<CladeCommonName> CladeCommonNames { get; set; }
        DbSet<Species> Species { get; set; }
        DbSet<SpeciesCommonName> SpeciesCommonNames { get; set; }
        DbSet<SpeciesCreator> SpeciesCreators { get; set; }
        DbSet<SpeciesRelationship> SpeciesRelationships { get; set; }
        DbSet<SpeciesRole> SpeciesRoles { get; set; }
        DbSet<SpeciesZone> SpeciesZones { get; set; }
        DbSet<SpeciesZoneEdit> SpeciesZoneHistory { get; set; }
        DbSet<SpeciesField> SpeciesFields { get; set; }

        DbSet<Era> Eras { get; set; }

    }

}