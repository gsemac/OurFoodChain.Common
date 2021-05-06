﻿namespace OurFoodChain.Discord.Bots {

    public interface IPaginatedMessage {

        int PageCount { get; }
        int PageIndex { get; set; }
        int ChapterIndex { get; set; }

        IPaginatedMessagePage GetCurrentPage();

    }

}