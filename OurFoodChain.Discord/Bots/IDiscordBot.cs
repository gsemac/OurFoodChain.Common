using System;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public interface IDiscordBot :
        IDisposable {

        Task ConnectAsync();
        Task DisconnectAsync();

    }

}