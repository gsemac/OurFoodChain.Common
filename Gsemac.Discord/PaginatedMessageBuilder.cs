namespace Gsemac.Discord {

    public class PaginatedMessageBuilder :
        IPaginatedMessageBuilder {

        // Public members

        public int PageCount => Build().PageCount;

        public PaginatedMessageBuilder() {
        }

        public IPaginatedMessageBuilder WithTitle(string title) {

            this.title = title;

            return this;

        }
        public IPaginatedMessageBuilder WithDescription(string description) {

            this.description = description;

            return this;

        }
        public IPaginatedMessageBuilder WithFooter(string footer) {

            this.footer = footer;

            return this;

        }

        public IPaginatedMessageBuilder WithPageNumbers() {

            withPageNumbers = true;

            return this;

        }

        public IPaginatedMessage Build() {

            return new PaginatedMessage(new[] {
                new PaginatedMessagePage("hello world 1", null),
                new PaginatedMessagePage("hello world 2", null),
            });

        }

        // Private members

        private string title;
        private string description;
        private string footer;
        private bool withPageNumbers = false;

    }

}