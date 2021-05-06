using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public interface IInteractiveMessageService {

        Task<IMessage> GetNextMessageAsync(ICommandContext context, IInteractionOptions options = null);     

    }

}