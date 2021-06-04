namespace Gsemac.Discord {

    public interface IDiscordBotOptions {

        string Prefix { get; set; }
        string Token { get; set; }
        string Playing { get; set; }
        bool UseWS4Net { get; set; }
        bool RespondToDMs { get; set; }

    }

}