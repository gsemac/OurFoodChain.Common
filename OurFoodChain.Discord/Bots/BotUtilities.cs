using Discord;
using Discord.Commands;
using Gsemac.IO.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurFoodChain.Discord {

    internal static class BotUtilities {

        // Public members

        internal static ILogMessage ConvertLogMessage(global::Discord.LogMessage discordLogMessage) {

            return new Gsemac.IO.Logging.LogMessage(ConvertLogLevel(discordLogMessage.Severity), discordLogMessage.Source, discordLogMessage.Message);

        }

        internal static async Task<IUserMessage> ReplySuccessAsync(IMessageChannel channel, string message) {

            return await channel.SendMessageAsync(string.Empty, false, BuildSuccessEmbed(message).Build());

        }
        internal static async Task<IUserMessage> ReplyWarningAsync(IMessageChannel channel, string message) {

            return await channel.SendMessageAsync(string.Empty, false, BuildWarningEmbed(message).Build());

        }
        internal static async Task<IUserMessage> ReplyErrorAsync(IMessageChannel channel, string message) {

            return await channel.SendMessageAsync(string.Empty, false, BuildErrorEmbed(message).Build());

        }
        internal static async Task<IUserMessage> ReplyInfoAsync(IMessageChannel channel, string message) {

            return await channel.SendMessageAsync(string.Empty, false, BuildInfoEmbed(message).Build());

        }

        internal static EmbedBuilder BuildSuccessEmbed(string message) {

            return new EmbedBuilder()
                .WithDescription($"✅ {message}")
                .WithColor(Color.Green);

        }
        internal static EmbedBuilder BuildWarningEmbed(string message) {

            return new EmbedBuilder()
                .WithDescription($"⚠️ {message}")
                .WithColor(Color.Orange);

        }
        internal static EmbedBuilder BuildErrorEmbed(string message) {

            return new EmbedBuilder()
                .WithDescription($"❌ {message}")
                .WithColor(Color.Red);

        }
        internal static EmbedBuilder BuildInfoEmbed(string message) {

            return new EmbedBuilder()
                .WithDescription(message)
                .WithColor(Color.LightGrey);

        }

        // Private members

        private static LogLevel ConvertLogLevel(LogSeverity discordLogLevel) {

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