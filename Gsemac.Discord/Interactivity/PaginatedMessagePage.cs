using Discord;

namespace Gsemac.Discord.Interactivity {

    public class PaginatedMessagePage :
        IPaginatedMessagePage {

        public string Content { get; }
        public Embed Embed { get; }

        public PaginatedMessagePage(Embed embed) :
            this(null, embed) {
        }
        public PaginatedMessagePage(string content, Embed embed) {

            Content = content;
            Embed = embed;

        }

    }

}