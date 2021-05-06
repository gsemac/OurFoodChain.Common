namespace OurFoodChain.Discord.Bots {

    public class DocumentationServiceOptions :
        IDocumentationServiceOptions {

        // Public members

        public string DocumentationDirectoryPath { get; set; }

        public static DocumentationServiceOptions Default => new DocumentationServiceOptions();

    }

}