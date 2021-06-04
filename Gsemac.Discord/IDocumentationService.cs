using Discord.Commands;
using System.Threading.Tasks;

namespace Gsemac.Discord {

    public interface IDocumentationService {

        Task<ICommandDocumentation> GetCommandInfoAsync(string commandName);
        Task<ICommandDocumentation> GetCommandDocumentationAsync(CommandInfo commandInfo);

    }

}