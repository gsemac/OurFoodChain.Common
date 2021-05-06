using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public interface IPaginatedMessageService {

        Task<IMessage> SendPaginatedMessageAsync(ICommandContext context, IPaginatedMessage message, IPaginationOptions options = null);

    }

}