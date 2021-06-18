using Gsemac.Text.Extensions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace OurFoodChain.Taxonomy {

    public sealed class BinomialName :
       IBinomialName {

        // Public members

        public string Genus { get; }
        public string Species { get; }
        public bool IsAbbreviated => GetIsAbbreviated(Genus);

        public BinomialName(string genus, string species) {

            if (genus is null)
                throw new ArgumentNullException(nameof(genus));

            if (species is null)
                throw new ArgumentNullException(nameof(species));

            Genus = FormatGenus(genus);
            Species = FormatSpecies(species);

        }

        public override string ToString() {

            return ToString(IsAbbreviated ? BinomialNameFormat.Abbreviated : BinomialNameFormat.Full);

        }
        public string ToString(BinomialNameFormat format) {

            if (string.IsNullOrEmpty(Genus) || format == BinomialNameFormat.Specific) {

                return Species.ToLowerInvariant();

            }
            else if (format == BinomialNameFormat.Abbreviated) {

                return string.Format("{0}. {1}", char.ToUpperInvariant(Genus.First()), Species.ToLowerInvariant());

            }
            else {

                return string.Format("{0} {1}", Genus.TrimEnd('.').ToProper(), Species);

            }

        }

        public static bool TryParse(string input, out BinomialName result) {

            result = null;

            // Attempt to split the string into two parts. If we have more than two parts, it is not a binomial name.

            string[] parts = Regex.Split(input, @"[\s\.]+")
                .Where(part => !string.IsNullOrWhiteSpace(part))
                .ToArray();

            if (parts.Count() != 2)
                return false;

            result = new BinomialName(parts[0], parts[1]);

            return true;

        }
        public static BinomialName Parse(string input) {

            if (TryParse(input, out BinomialName result))
                return result;

            throw new ArgumentException(Properties.ExceptionMessages.StringWasNotInTheCorrectFormat, nameof(input));

        }

        // Private members

        private static string FormatGenus(string genus) {

            return genus.Trim()
                .TrimEnd('.')
                .Trim()
                .ToProper();

        }
        private static string FormatSpecies(string species) {

            return species.ToLowerInvariant();

        }
        private static bool GetIsAbbreviated(string genus) {

            return Regex.IsMatch(genus.ToLowerInvariant(), @"^[a-zA-Z]\.?$", RegexOptions.IgnoreCase);

        }

    }

}