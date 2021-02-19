using System;

namespace OurFoodChain.Discord.Bots {

    public interface IInteractionOptions {

        bool AllowCancellation { get; }
        bool RequireSourceUser { get; }
        bool RequireSourceChannel { get; }
        TimeSpan? Timeout { get; }

    }

}