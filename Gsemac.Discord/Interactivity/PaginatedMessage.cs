using System.Collections.Generic;
using System.Linq;

namespace Gsemac.Discord.Interactivity {

    internal class PaginatedMessage :
        IPaginatedMessage {

        // Public members

        public int PageCount => pages.Count();
        public int PageIndex {
            get => pageIndex;
            set => SetPageIndex(value);
        }
        public int ChapterIndex {
            get => chapterIndex;
            set => SetChapterIndex(value);
        }

        public IPaginatedMessagePage GetCurrentPage() {

            return pages.Skip(PageIndex).FirstOrDefault();

        }

        // Internal members

        internal PaginatedMessage(IEnumerable<IPaginatedMessagePage> pages) {

            this.pages = pages;

        }

        // Private members

        private readonly IEnumerable<IPaginatedMessagePage> pages;
        private int pageIndex = 0;
        private int chapterIndex = 0;

        private void SetPageIndex(int value) {

            if (PageCount <= 0)
                value = 0;
            else
                value = (value % PageCount + PageCount) % PageCount;

            pageIndex = value;

        }
        private void SetChapterIndex(int value) {

            chapterIndex = 0;

        }

    }

}