using System;

namespace Gsemac.Discord {

    public class InteractiveMessageOptions :
        IInteractiveMessageOptions {

        public bool AllowCancellation { get; set; } = true;
        public bool RequireSourceUser { get; set; } = true;
        public bool RequireSourceChannel { get; set; } = true;
        public TimeSpan? Timeout { get; set; }

        public static InteractiveMessageOptions Default => new InteractiveMessageOptions();

    }

}