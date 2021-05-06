﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurFoodChain.Discord.Bots {

    public class PaginatedMessageServiceOptions :
        IPaginatedMessageServiceOptions {

        public int MaxMessageCount { get; set; } = 50;

        public static PaginatedMessageServiceOptions Default => new PaginatedMessageServiceOptions();

    }

}
