namespace OurFoodChain.Taxonomy {

    interface IBinomialName {

        string Genus { get; }
        string Species { get; }
        bool IsAbbreviated { get; }

    }

}