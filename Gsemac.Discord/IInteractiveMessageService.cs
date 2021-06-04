using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Gsemac.Discord {

    public interface IInteractiveMessageService {

        Task<IUserMessage> GetNextMessageAsync(ICommandContext context, IInteractiveMessageOptions options = null);

    }

}