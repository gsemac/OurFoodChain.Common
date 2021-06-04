using Discord;

namespace OurFoodChain.Discord.Extensions {

    public static class MessageExtensions {

        public static bool IsDM(this IMessage message) {

            return message.Channel is IDMChannel;

        }

    }

}
