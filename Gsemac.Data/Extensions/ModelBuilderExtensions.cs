using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace Gsemac.Data.Extensions {

    public static class ModelBuilderExtensions {

        public static ModelBuilder UseValueConverterForType<T>(this ModelBuilder modelBuilder, ValueConverter converter) {

            return modelBuilder.UseValueConverterForType(typeof(T), converter);

        }
        public static ModelBuilder UseValueConverterForType(this ModelBuilder modelBuilder, Type type, ValueConverter converter) {

            // https://github.com/dotnet/efcore/issues/10784#issuecomment-415769754 (bugproof)

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {

                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == type);

                foreach (var property in properties) {

                    modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion(converter);

                }

            }

            return modelBuilder;
        }

    }

}