using System.Collections.Generic;

namespace OurFoodChain.Discord.Bots {

    public interface ICommandDocumentation {

        string Name { get; set; }
        IEnumerable<string> Aliases { get; set; }
        string Summary { get; set; }
        string Category { get; set; }
        IEnumerable<string> Examples { get; set; }

    }

}