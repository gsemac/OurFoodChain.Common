namespace OurFoodChain {

    public class StringPaginationOptions :
        IStringPaginationOptions {

        // Public members

        public bool AddLeadingEllipsis { get; set; } = false;
        public bool AddTrailingEllipsis { get; set; } = true;
        public bool AddLeadingHyphen { get; set; } = false;
        public bool AddTrailingHyphen { get; set; } = true;
        public int MaxPageLength { get; set; } = defaultMaxPageLength;
        public bool SplitOnWordBoundaries { get; set; } = true;

        public static StringPaginationOptions Default => new StringPaginationOptions();

        // Private members

        private const int defaultMaxPageLength = 2048;

    }

}