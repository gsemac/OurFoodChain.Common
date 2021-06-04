using Discord;

namespace Gsemac.Discord {

    public interface IPaginatedMessagePage {

        string Content { get; }
        Embed Embed { get; }

    }

}