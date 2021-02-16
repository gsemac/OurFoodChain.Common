using System;

namespace OurFoodChain.Text {

    public static class StringUtilities {

        public static int ComputeLevenshteinDistance(string str1, string str2) {

            // This algorithm was adapted from the one given here: https://stackoverflow.com/a/57961456 (ilike2breakthngs)
            // It is a C# port of the Java inplementation given here: http://web.archive.org/web/20120526085419/http://www.merriampark.com/ldjava.htm

            str1 = str1 ?? "";
            str2 = str2 ?? "";

            int n = str1.Length;
            int m = str2.Length;

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            int[] p = new int[n + 1]; //'previous' cost array, horizontally
            int[] d = new int[n + 1]; // cost array, horizontally

            int i; // iterates through s
            int j; // iterates through t

            for (i = 0; i <= n; i++) {
                p[i] = i;
            }

            for (j = 1; j <= m; j++) {

                char tJ = str2[j - 1]; // jth character of t

                d[0] = j;

                for (i = 1; i <= n; i++) {

                    int cost = str1[i - 1] == tJ ? 0 : 1; // cost

                    // minimum of cell to the left+1, to the top+1, diagonally left and up +cost

                    d[i] = Math.Min(Math.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + cost);

                }

                // copy current distance counts to 'previous row' distance counts

                int[] dPlaceholder = p; // placeholder to assist in swapping p and d

                p = d;
                d = dPlaceholder;
            }

            // our last action in the above loop was to switch d and p, so p now 
            // actually has the most recent cost counts

            return p[n];

        }
        public static double ComputeSimilarity(string str1, string str2) {

            if (str1.Equals(str2))
                return 1.0;

            int levenshteinDistance = ComputeLevenshteinDistance(str1, str2);
            int maxLength = Math.Max(str1.Length, str2.Length);

            return 1.0 - (levenshteinDistance / maxLength);

        }

    }

}