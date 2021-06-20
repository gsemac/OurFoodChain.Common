using Discord;

namespace Gsemac.Discord.Extensions {

    public static class EmbedBuilderExtensions {

        public static EmbedBuilder WithPropertiesFrom(this EmbedBuilder builder, Embed embed) {

            if (embed.Author.HasValue) {

                builder.WithAuthor(new EmbedAuthorBuilder()
                    .WithIconUrl(embed.Author.Value.IconUrl)
                    .WithName(embed.Author.Value.Name)
                    .WithIconUrl(embed.Author.Value.Url));

            }

            if (embed.Color.HasValue)
                builder.WithColor(embed.Color.Value);

            foreach (EmbedField field in embed.Fields)
                builder.AddField(field.Name, field.Value, field.Inline);

            if (embed.Footer.HasValue) {

                builder.WithFooter(new EmbedFooterBuilder()
                    .WithIconUrl(embed.Footer.Value.IconUrl)
                    .WithText(embed.Footer.Value.Text));

            }

            if (embed.Image.HasValue)
                builder.WithImageUrl(embed.Image.Value.Url);

            if(embed.Thumbnail.HasValue)
                builder.WithThumbnailUrl(embed.Thumbnail.Value.Url);

            if (embed.Timestamp.HasValue)
                builder.WithTimestamp(embed.Timestamp.Value);

            builder.WithDescription(embed.Description)
                .WithTitle(embed.Title)
                .WithUrl(embed.Url);

            return builder;

        }

    }

}