using System;
using System.Threading.Tasks;

namespace Gsemac.Discord {

    public interface IDiscordBot :
        IDisposable {

        Task ConnectAsync();
        Task DisconnectAsync();

    }

}