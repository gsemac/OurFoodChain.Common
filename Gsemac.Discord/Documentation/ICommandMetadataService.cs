using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gsemac.Discord.Documentation {

    public interface ICommandMetadataService {

        Task<IEnumerable<ICommandMetadata>> GetMetadataAsync(ICommandContext context);
        Task<IEnumerable<ICommandMetadata>> GetMetadataAsync(string name, ICommandContext context);
        Task<ICommandMetadata> GetMetadataAsync(CommandInfo commandInfo, ICommandContext context);

    }

}