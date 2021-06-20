using Discord;
using Gsemac.Discord.Extensions;
using OurFoodChain;
using System.Collections.Generic;
using System.Linq;

namespace Gsemac.Discord.Interactivity {

    public class PaginatedMessageBuilder :
        IPaginatedMessageBuilder {

        // Public members

        public PaginatedMessageBuilder() { }
        public PaginatedMessageBuilder(Embed template) {

            this.template = template;

        }

        public IPaginatedMessageBuilder WithDescription(string value) {

            description = value;

            return this;

        }
        public IPaginatedMessageBuilder WithFooter(string value) {

            footer = value;

            return this;

        }
        public IPaginatedMessageBuilder WithTitle(string value) {

            title = value;

            return this;

        }

        public IPaginatedMessageBuilder AddPaginatedText(string value) {

            paginatedContent.Add(new TextContent(value));

            return this;

        }
        public IPaginatedMessageBuilder AddPaginatedFields(IEnumerable<EmbedField> fields) {

            paginatedContent.Add(new FieldsContent(fields));

            return this;

        }
        public IPaginatedMessageBuilder AddPaginatedListItems(IEnumerable<object> items, IListPaginationOptions options) {

            paginatedContent.Add(new ListItemsContent(items, options));

            return this;

        }
        public IPaginatedMessageBuilder AddPage(Embed embed) {

            paginatedContent.Add(new PageContent(embed));

            // Add a page break after pages added manually so that they're not appended to.

            AddPageBreak();

            return this;

        }
        public IPaginatedMessageBuilder AddPageBreak() {

            paginatedContent.Add(new PageBreakContent());

            return this;

        }

        public IPaginatedMessageBuilder WithPageNumbers() {

            withPageNumbers = true;

            return this;

        }

        public void Clear() {

            paginatedContent.Clear();

        }

        public IPaginatedMessage Build() {

            List<PaginatedMessagePage> pages = new();

            if (!paginatedContent.Any()) {

                // If no paginated content has been added, just use the template as the first and only page.

                pages.Add(new PaginatedMessagePage(GetTemplate()));

            }
            else {

                foreach (IPaginatedContent content in paginatedContent) {

                    switch (content.ContentType) {

                        case PaginatedContentType.Text:
                            AddPaginatedText(pages, (TextContent)content);
                            break;

                    }

                }

            }

            if (withPageNumbers) {

                for (int i = 0; i < pages.Count; ++i) {

                    pages[i] = AddPageNumber(pages[i], i + 1, pages.Count);

                }

            }

            return new PaginatedMessage(pages);

        }

        // Private members

        private enum PaginatedContentType {
            Text,
            Fields,
            ListItems,
            Page,
            PageBreak,
        }

        private interface IPaginatedContent {

            PaginatedContentType ContentType { get; }

        }

        private class TextContent :
            IPaginatedContent {

            public string Text { get; }
            public PaginatedContentType ContentType => PaginatedContentType.Text;

            public TextContent(string text) {

                Text = text;

            }

        }

        private class FieldsContent :
          IPaginatedContent {

            public IEnumerable<EmbedField> Fields { get; }
            public PaginatedContentType ContentType => PaginatedContentType.Fields;

            public FieldsContent(IEnumerable<EmbedField> fields) {

                Fields = fields;

            }

        }

        private class ListItemsContent :
            IPaginatedContent {

            public IEnumerable<object> Items { get; }
            public IListPaginationOptions Options { get; }
            public PaginatedContentType ContentType => PaginatedContentType.ListItems;

            public ListItemsContent(IEnumerable<object> items, IListPaginationOptions options) {

                Items = items;
                Options = options;

            }

        }

        private class PageContent :
         IPaginatedContent {

            public Embed Page { get; }
            public PaginatedContentType ContentType => PaginatedContentType.Page;

            public PageContent(Embed page) {

                Page = page;

            }

        }

        private class PageBreakContent :
            IPaginatedContent {

            public PaginatedContentType ContentType => PaginatedContentType.PageBreak;

        }

        private readonly Embed template;
        private readonly List<IPaginatedContent> paginatedContent = new List<IPaginatedContent>();
        private string description;
        private string footer;
        private string title;
        private bool withPageNumbers = false;

        private Embed GetTemplate() {

            if (template is object)
                return template;

            return new EmbedBuilder()
                .WithDescription(description)
                .WithFooter(footer)
                .WithTitle(title)
                .Build();

        }

        private void AddPaginatedText(ICollection<PaginatedMessagePage> pages, TextContent textContent) {

            if (textContent.Text.Length <= 0)
                return;

            Embed template = GetTemplate();
            int maxPageLength = DiscordUtilities.MaxEmbedLength - template.Length;

            IStringPaginationOptions options = new StringPaginationOptions() {
                MaxPageLength = maxPageLength,
                AddLeadingEllipsis = true,
                AddTrailingEllipsis = true,
                AddLeadingHyphen = true,
                AddTrailingHyphen = true,
                SplitOnWordBoundaries = true,
            };

            foreach (string page in new StringPaginator(textContent.Text, options)) {

                Embed embed = new EmbedBuilder()
                    .WithPropertiesFrom(template)
                    .WithDescription(template.Description + page)
                    .Build();

                pages.Add(new PaginatedMessagePage(embed));

            }

        }

        private static PaginatedMessagePage AddPageNumber(PaginatedMessagePage page, int currentPage, int totalPages) {

            if (page.Embed is null)
                return page;

            string footer = $"Page {currentPage} of {totalPages}";

            if (!string.IsNullOrWhiteSpace(page.Embed.Footer?.Text))
                footer = footer + " — " + page.Embed.Footer.Value.Text;

            return new PaginatedMessagePage(new EmbedBuilder()
                .WithPropertiesFrom(page.Embed)
                .WithFooter(footer)
                .Build());

        }

    }

}