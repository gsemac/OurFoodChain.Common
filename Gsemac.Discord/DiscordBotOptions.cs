namespace Gsemac.Discord {

    public class DiscordBotOptions :
        IDiscordBotOptions {

        public string Prefix { get; set; } = "?";
        public string Token { get; set; }
        public string Playing { get; set; }
        public bool UseWS4Net { get; set; } = true;
        public bool RespondToDMs { get; set; } = true;

        public static DiscordBotOptions Default => new DiscordBotOptions();

        public DiscordBotOptions() {
        }
        public DiscordBotOptions(string token) {

            Token = token;

        }

    }

}