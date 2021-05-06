namespace OurFoodChain.Discord.Bots {

    public interface IPaginatedMessageBuilder {

        int PageCount { get; }

        IPaginatedMessageBuilder WithTitle(string title);
        IPaginatedMessageBuilder WithDescription(string description);
        IPaginatedMessageBuilder WithFooter(string footer);

        IPaginatedMessageBuilder WithPageNumbers();

        IPaginatedMessage Build();

    }

}