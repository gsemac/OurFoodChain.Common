using Discord;
using Discord.Commands;
using OurFoodChain.Data.Models;
using OurFoodChain.Discord.Extensions;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Modules {

    public sealed class WorldModule :
        OfcModuleBase {

        [Command("world")]
        public async Task WorldAsync() {

            World world = await Db.EnsureWorldCreatedAsync(Context.Guild);

            Embed embed = new EmbedBuilder()
                .WithTitle(world.Name)
                .Build();

            await ReplyAsync(embed: embed);

        }

    }

}