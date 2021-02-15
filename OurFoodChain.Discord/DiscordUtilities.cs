using Gsemac.IO.Logging;

namespace OurFoodChain.Discord {

    public static class DiscordUtilities {

        // Public members

        internal static ILogMessage ConvertLogMessage(global::Discord.LogMessage discordLogMessage) {

            return new LogMessage(ConvertLogLevel(discordLogMessage.Severity), discordLogMessage.Source, discordLogMessage.Message);

        }

        // Private members

        private static LogLevel ConvertLogLevel(global::Discord.LogSeverity discordLogLevel) {

            switch (discordLogLevel) {

                case global::Discord.LogSeverity.Debug:
                    return LogLevel.Debug;

                case global::Discord.LogSeverity.Warning:
                    return LogLevel.Warning;

                case global::Discord.LogSeverity.Error:
                    return LogLevel.Error;

                default:
                    return LogLevel.Info;

            }

        }

    }

}