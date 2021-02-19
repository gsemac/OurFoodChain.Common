namespace OurFoodChain.Discord.Bots {

    public class InteractiveCommandHandlerServiceOptions :
        IInteractiveCommandHandlerServiceOptions {

        public int MaxInteractiveMessages { get; set; } = 50;
        public string CancellationKeyword { get; set; } = "cancel";

        public static InteractiveCommandHandlerServiceOptions Default => new InteractiveCommandHandlerServiceOptions();

    }

}