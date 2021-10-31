using System;

namespace OurFoodChain.Taxonomy {

    public static class RankUtilities {

        // Public members

        public static string ToString(Rank rankId, bool plural = false) {

            switch (rankId) {

                case Rank.Domain:
                    return plural ? "domains" : "domain";

                case Rank.Kingdom:
                    return plural ? "kingdoms" : "kingdom";

                case Rank.Phylum:
                    return plural ? "phyla" : "phylum";

                case Rank.Class:
                    return plural ? "classes" : "class";

                case Rank.Order:
                    return plural ? "orders" : "order";

                case Rank.Family:
                    return plural ? "families" : "family";

                case Rank.Genus:
                    return plural ? "genera" : "genus";

                case Rank.Species:
                    return "species";

                case Rank.Unranked:
                default:
                    return plural ? "unranked taxa" : "unranked taxon";

            }

        }

        public static bool TryParse(string value, out Rank rankId) {

            if (value is null)
                throw new ArgumentNullException(nameof(value));

            rankId = Rank.Unranked;

            switch (value.Trim().ToLowerInvariant()) {

                case "domain":
                case "domains":
                    rankId = Rank.Domain;
                    return true;

                case "kingdom":
                case "kingdoms":
                    rankId = Rank.Kingdom;
                    return true;

                case "phylum":
                case "phyla":
                    rankId = Rank.Phylum;
                    return true;

                case "class":
                case "classes":
                    rankId = Rank.Class;
                    return true;

                case "order":
                case "orders":
                    rankId = Rank.Order;
                    return true;

                case "family":
                case "families":
                    rankId = Rank.Family;
                    return true;

                case "genus":
                case "genera":
                    rankId = Rank.Genus;
                    return true;

                case "species":
                    rankId = Rank.Species;
                    return true;

                case "unranked":
                case "unranked clade":
                case "unranked clades":
                case "clade":
                case "clades":
                case "unranked taxon":
                case "unranked taxa":
                case "taxon":
                case "taxa":
                    rankId = Rank.Unranked;
                    return true;

                default:
                    return false;

            }

        }
        public static Rank Parse(string value) {

            if (TryParse(value, out Rank taxonRankId))
                return taxonRankId;

            throw new ArgumentException(Properties.ExceptionMessages.StringWasNotInTheCorrectFormat, nameof(value));

        }

    }

}