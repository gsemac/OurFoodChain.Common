﻿using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Gsemac.Discord.Interactivity {

    public interface IPaginatedMessageService {

        Task<IUserMessage> SendPaginatedMessageAsync(ICommandContext context, IPaginatedMessage message, IPaginatedMessageOptions options = null);

    }

}