using Discord;
using System.Collections.Generic;

namespace Gsemac.Discord {

    public interface IPaginatedMessageBuilder {

        IPaginatedMessageBuilder WithDescription(string value);
        IPaginatedMessageBuilder WithFooter(string value);
        IPaginatedMessageBuilder WithPage(Embed embed);
        IPaginatedMessageBuilder WithPageBreak();
        IPaginatedMessageBuilder WithPageNumbers();
        IPaginatedMessageBuilder WithPaginatedFields(IEnumerable<EmbedField> fields);
        IPaginatedMessageBuilder WithPaginatedListItems(IEnumerable<object> items, IListPaginationOptions options);
        IPaginatedMessageBuilder WithPaginatedText(string value);
        IPaginatedMessageBuilder WithTitle(string value);

        IPaginatedMessage Build();
        void Clear();

    }

}