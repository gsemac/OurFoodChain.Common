using Discord.Commands;
using Gsemac.Text.Extensions;
using OurFoodChain.Data;
using OurFoodChain.Data.Models;
using OurFoodChain.Discord.Extensions;
using OurFoodChain.Taxonomy;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Modules {

    [Group("clades"), Alias("clade")]
    public sealed class CladesModule :
        OfcModuleBase {

        // Public members

        public CladesModule(IOfcDbContext dbContext) :
            base(dbContext) {
        }

        [Command("add"), Alias("a")]
        public async Task AddAsync(string name, NamedArguments arguments = null) {

            arguments ??= new NamedArguments();

            if (TaxonRank.TryParse(arguments.Rank, out TaxonRankId parsedRank) && parsedRank != TaxonRankId.Species) {

                if (!(await Db.Clades.GetCladesAsync(name, parsedRank)).Any()) {

                    Clade clade = new() {
                        Name = name,
                        Rank = parsedRank,
                    };

                    await Db.Clades.AddCladeAsync(clade);

                    await Db.SaveChangesAsync();

                    await ReplySuccessAsync($"Successfully created new {TaxonRank.ToString(parsedRank)}, {name.ToProper().ToBold()}.");

                }
                else {

                    await ReplyWarningAsync($"The {TaxonRank.ToString(parsedRank)} '{name.ToProper()}' already exists.");

                }

            }
            else if (parsedRank == TaxonRankId.Species) {

                await ReplyErrorAsync(Properties.CommandResultMessages.UsedCladeCommandWithSpecies);

            }
            else {

                await ReplyErrorAsync(string.Format(Properties.CommandResultMessages.InvalidRankWithRankName, arguments.Rank));

            }

        }

    }

}