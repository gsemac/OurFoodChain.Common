using System;

namespace Gsemac.Discord {

    public interface IInteractiveMessageOptions {

        bool AllowCancellation { get; }
        bool RequireSourceUser { get; }
        bool RequireSourceChannel { get; }
        TimeSpan? Timeout { get; }

    }

}