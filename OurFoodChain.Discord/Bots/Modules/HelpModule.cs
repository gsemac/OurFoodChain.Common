using Discord.Commands;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots.Modules {

    internal class HelpModule :
        ModuleBase {

        [Command("test", RunMode = RunMode.Async)]
        public async Task Test() {

           // await ReplyAsync((await HelpService.GetCommandHelpInfoAsync("test")).Name);

        }

    }

}