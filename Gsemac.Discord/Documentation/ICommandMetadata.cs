using System.Collections.Generic;

namespace Gsemac.Discord.Documentation {

    public interface ICommandMetadata {

        IEnumerable<string> Aliases { get; }
        string Category { get; }
        IEnumerable<string> Examples { get; }
        string FullName { get; }
        string Group { get; }
        string Name { get; }
        string Summary { get; }

    }

}