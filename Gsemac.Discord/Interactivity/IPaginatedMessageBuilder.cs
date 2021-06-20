using Discord;
using System.Collections.Generic;

namespace Gsemac.Discord.Interactivity {

    public interface IPaginatedMessageBuilder {

        IPaginatedMessageBuilder WithDescription(string value);
        IPaginatedMessageBuilder WithFooter(string value);
        IPaginatedMessageBuilder AddPage(Embed embed);
        IPaginatedMessageBuilder AddPageBreak();
        IPaginatedMessageBuilder WithPageNumbers();
        IPaginatedMessageBuilder AddPaginatedFields(IEnumerable<EmbedField> fields);
        IPaginatedMessageBuilder AddPaginatedListItems(IEnumerable<object> items, IListPaginationOptions options);
        IPaginatedMessageBuilder AddPaginatedText(string value);
        IPaginatedMessageBuilder WithTitle(string value);

        IPaginatedMessage Build();
        void Clear();

    }

}