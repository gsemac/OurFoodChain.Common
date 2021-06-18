using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;

namespace Gsemac.Data.ValueConverters {

    public class SignedUInt64Converter :
        ValueConverter<ulong, long> {

        // Public members

        public SignedUInt64Converter(ConverterMappingHints mappingHints = null) :
            base(ConvertToProviderArgument, ConvertFromProviderArgument, mappingHints) {
        }

        // Private members

        private static readonly Expression<Func<ulong, long>> ConvertToProviderArgument = x => ConvertToProviderImpl(x);
        private static readonly Expression<Func<long, ulong>> ConvertFromProviderArgument = x => ConvertFromProviderImpl(x);

        private static long ConvertToProviderImpl(ulong value) {

            return (long)value;

        }
        private static ulong ConvertFromProviderImpl(long value) {

            return (ulong)value;

        }

    }

}