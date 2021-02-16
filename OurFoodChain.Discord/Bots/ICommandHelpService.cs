using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public interface ICommandHelpService {

        Task<ICommandHelpInfo> GetCommandHelpInfoAsync(string commandName);

    }

}