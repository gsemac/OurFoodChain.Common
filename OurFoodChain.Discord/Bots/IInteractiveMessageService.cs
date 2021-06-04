using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public interface IInteractiveMessageService {

        Task<IUserMessage> GetNextMessageAsync(ICommandContext context, IInteractionOptions options = null);     

    }

}