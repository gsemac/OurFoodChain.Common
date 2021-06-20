namespace Gsemac.Discord.Interactivity {

    public class InteractiveMessageServiceOptions :
        IInteractiveMessageServiceOptions {

        public int MaxMessageCount { get; set; } = 50;
        public string CancellationKeyword { get; set; } = "cancel";
        public bool IgnorePrefixInReplies { get; set; } = true;

        public static InteractiveMessageServiceOptions Default => new InteractiveMessageServiceOptions();

    }

}