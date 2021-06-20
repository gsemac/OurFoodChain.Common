namespace Gsemac.Discord.Interactivity {

    public class PaginatedMessageServiceOptions :
        IPaginatedMessageServiceOptions {

        public int MaxMessageCount { get; set; } = 50;

        public static PaginatedMessageServiceOptions Default => new PaginatedMessageServiceOptions();

    }

}
