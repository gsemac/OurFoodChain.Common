namespace OurFoodChain.Discord.Extensions {

    public static class MarkupExtensions {

        public static string ToBold(this string str) {

            return $"**{str}**";

        }
        public static string ToItalic(this string str) {

            return $"*{str}*";

        }
        public static string ToUnderline(this string str) {

            return $"__{str}__";

        }
        public static string ToCode(this string str) {

            return $"`{str}`";

        }

    }

}