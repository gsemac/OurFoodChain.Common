using Discord.Commands;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public interface IDocumentationService {

        Task<ICommandDocumentation> GetCommandInfoAsync(string commandName);
        Task<ICommandDocumentation> GetCommandDocumentationAsync(CommandInfo commandInfo);

    }

}