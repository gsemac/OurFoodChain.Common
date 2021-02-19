namespace OurFoodChain.Discord.Bots {

    public class InteractiveCommandHandlerServiceOptions :
        IInteractiveCommandHandlerServiceOptions {

        public int MaxInteractiveMessages { get; set; } = 50;
        public string CancellationKeyword { get; set; } = "cancel";
        public bool IgnoreCommandsInResponseMessages { get; set; } = true;

        public static InteractiveCommandHandlerServiceOptions Default => new InteractiveCommandHandlerServiceOptions();

    }

}