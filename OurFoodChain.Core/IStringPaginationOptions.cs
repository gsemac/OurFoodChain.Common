namespace OurFoodChain {

    public interface IStringPaginationOptions {

        bool AddLeadingEllipsis { get; }
        bool AddTrailingEllipsis { get; }
        bool AddLeadingHyphen { get; }
        bool AddTrailingHyphen { get; }
        int MaxPageLength { get; }
        bool SplitOnWordBoundaries { get; }

    }

}