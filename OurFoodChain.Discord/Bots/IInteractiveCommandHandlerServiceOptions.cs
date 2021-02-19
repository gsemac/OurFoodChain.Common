namespace OurFoodChain.Discord.Bots {

    public interface IInteractiveCommandHandlerServiceOptions {

        int MaxInteractiveMessages { get; }
        string CancellationKeyword { get; }
        bool IgnoreCommandsInResponseMessages { get; }

    }

}