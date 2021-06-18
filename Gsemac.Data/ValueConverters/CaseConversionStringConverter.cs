using Gsemac.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gsemac.Data.ValueConverters {

    public class CaseConversionStringConverter :
       ValueConverter<string, string> {

        // Public members

        public CaseConversionStringConverter(StringCasing casing, ConverterMappingHints mappingHints = null) :
            base(v => ConvertToProviderImpl(v, casing), v => v, mappingHints) {
        }

        // Private members

        private static string ConvertToProviderImpl(string value, StringCasing casing) {

            return CaseConverter.ToCase(value, casing);

        }

    }

}