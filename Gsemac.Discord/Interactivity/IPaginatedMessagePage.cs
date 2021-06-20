using Discord;

namespace Gsemac.Discord.Interactivity {

    public interface IPaginatedMessagePage {

        string Content { get; }
        Embed Embed { get; }

    }

}