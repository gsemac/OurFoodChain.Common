using System;

namespace OurFoodChain.Taxonomy {

    public static class TaxonRank {

        public static string ToString(TaxonRankId rankId, bool plural = false) {

            switch (rankId) {

                case TaxonRankId.Domain:
                    return plural ? "domains" : "domain";

                case TaxonRankId.Kingdom:
                    return plural ? "kingdoms" : "kingdom";

                case TaxonRankId.Phylum:
                    return plural ? "phyla" : "phylum";

                case TaxonRankId.Class:
                    return plural ? "classes" : "class";

                case TaxonRankId.Order:
                    return plural ? "orders" : "order";

                case TaxonRankId.Family:
                    return plural ? "families" : "family";

                case TaxonRankId.Genus:
                    return plural ? "genera" : "genus";

                case TaxonRankId.Species:
                    return "species";

                case TaxonRankId.Unranked:
                default:
                    return plural ? "unranked clades" : "unranked clade";

            }

        }

        public static bool TryParse(string value, out TaxonRankId rankId) {

            if (value is null)
                throw new ArgumentNullException(nameof(value));

            rankId = TaxonRankId.Unranked;

            switch (value.Trim().ToLowerInvariant()) {

                case "domain":
                case "domains":
                    rankId = TaxonRankId.Domain;
                    return true;

                case "kingdom":
                case "kingdoms":
                    rankId = TaxonRankId.Kingdom;
                    return true;

                case "phylum":
                case "phyla":
                    rankId = TaxonRankId.Phylum;
                    return true;

                case "class":
                case "classes":
                    rankId = TaxonRankId.Class;
                    return true;

                case "order":
                case "orders":
                    rankId = TaxonRankId.Order;
                    return true;

                case "family":
                case "families":
                    rankId = TaxonRankId.Family;
                    return true;

                case "genus":
                case "genera":
                    rankId = TaxonRankId.Genus;
                    return true;

                case "species":
                    rankId = TaxonRankId.Species;
                    return true;

                case "unranked":
                case "unranked clade":
                case "unranked clades":
                case "clade":
                case "clades":
                    rankId = TaxonRankId.Unranked;
                    return true;

                default:
                    return false;

            }

        }
        public static TaxonRankId Parse(string value) {

            if (TryParse(value, out TaxonRankId taxonRankId))
                return taxonRankId;

            throw new ArgumentException(Properties.ExceptionMessages.StringWasNotInTheCorrectFormat, nameof(value));

        }

    }

}