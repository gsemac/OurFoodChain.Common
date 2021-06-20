namespace OurFoodChain {

    public static class CharUtilities {

        public static bool IsTerminalPunctuation(char value) {

            switch (value) {

                case '.':
                case '!':
                case '?':
                    return true;

                default:
                    return false;

            }

        }

    }

}