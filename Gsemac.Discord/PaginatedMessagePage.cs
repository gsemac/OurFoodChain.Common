using Discord;

namespace Gsemac.Discord {

    public class PaginatedMessagePage :
        IPaginatedMessagePage {

        public string Content { get; }
        public Embed Embed { get; }

        public PaginatedMessagePage(string content, Embed embed) {

            Content = content;
            Embed = embed;

        }

    }

}