using System;
using System.Collections.Generic;
using System.Linq;

namespace OurFoodChain.Discord.Bots {

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

        public PaginatedMessage(IEnumerable<IPaginatedMessagePage> pages) {

            this.pages = pages;

        }

        public IPaginatedMessagePage GetCurrentPage() {

            return pages.Skip(PageIndex).FirstOrDefault();

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