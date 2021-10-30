using Discord;
using Discord.Commands;
using OurFoodChain.Data;
using OurFoodChain.Data.Models;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Modules {

    [Group("world")]
    public sealed class WorldModule :
        OfcModuleBase {

        public WorldModule(IOfcDbContext dbContext) :
            base(dbContext) {
        }

        [Command]
        public async Task WorldAsync() {

            World world = await Db.Worlds.GetOrCreateWorldAsync();

            Embed embed = new EmbedBuilder()
                .WithTitle(world.Name)
                .Build();

            await ReplyAsync(embed: embed);

        }

    }

}