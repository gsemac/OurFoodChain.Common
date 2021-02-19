namespace OurFoodChain.Discord.Bots {

    public class DiscordBotConfiguration :
        IDiscordBotConfiguration {

        public string Prefix { get; set; } = "?";
        public string Token { get; set; }
        public string Playing { get; set; }
        public bool UseWS4Net { get; set; } = true;
        public bool RespondToDMs { get; set; } = true;

        public static DiscordBotConfiguration Default => new DiscordBotConfiguration();

        public DiscordBotConfiguration() {
        }
        public DiscordBotConfiguration(string token) {

            Token = token;

        }

    }

}