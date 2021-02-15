using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;

namespace OurFoodChain.Data {

    internal class UnixTimestampDateTimeOffsetValueConverter :
        ValueConverter<DateTimeOffset, long> {

        // Public members

        public UnixTimestampDateTimeOffsetValueConverter(ConverterMappingHints mappingHints = null) :
            base(ConvertToProviderArgument, ConvertFromProviderArgument, mappingHints) {
        }

        // Private members

        private static readonly Expression<Func<DateTimeOffset, long>> ConvertToProviderArgument = x => ConvertToProviderImpl(x);
        private static readonly Expression<Func<long, DateTimeOffset>> ConvertFromProviderArgument = x => ConvertFromProviderImpl(x);

        private static long ConvertToProviderImpl(DateTimeOffset value) {

            return value.ToUniversalTime().ToUnixTimeSeconds();

        }
        private static DateTimeOffset ConvertFromProviderImpl(long value) {

            return DateTimeOffset.FromUnixTimeSeconds(value).ToUniversalTime();

        }

    }

}