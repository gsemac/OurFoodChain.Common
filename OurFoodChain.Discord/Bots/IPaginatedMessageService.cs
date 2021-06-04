using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public interface IPaginatedMessageService {

        Task<IUserMessage> SendPaginatedMessageAsync(ICommandContext context, IPaginatedMessage message, IPaginationOptions options = null);

    }

}