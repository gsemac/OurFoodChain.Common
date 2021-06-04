namespace Gsemac.Discord {

    public interface IInteractiveMessageServiceOptions {

        int MaxMessageCount { get; }
        string CancellationKeyword { get; }
        bool IgnorePrefixInReplies { get; }

    }

}