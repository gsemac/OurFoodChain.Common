namespace OurFoodChain.Discord.Bots {

    public interface IDiscordBotConfiguration {

        string Prefix { get; set; }
        string Token { get; set; }
        string Playing { get; set; }
        bool UseWS4Net { get; set; }
        bool RespondToDMs { get; set; }

    }

}