using Discord.Commands;

namespace OurFoodChain.Discord.Modules {

    [NamedArgumentType]
    public class NamedArguments {

        public string Rank { get; set; } = "unranked";

    }

}