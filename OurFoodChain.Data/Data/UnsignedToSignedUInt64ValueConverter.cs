using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;

namespace OurFoodChain.Data {

    internal class UnsignedToSignedUInt64ValueConverter :
        ValueConverter<ulong, long> {

        // Public members

        public UnsignedToSignedUInt64ValueConverter(ConverterMappingHints mappingHints = null) :
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