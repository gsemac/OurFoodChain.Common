using Gsemac.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OurFoodChain {

    public class StringPaginator :
        IStringPaginator {

        // Public members

        public StringPaginator(string content) :
            this(content, StringPaginationOptions.Default) {
        }
        public StringPaginator(string content, IStringPaginationOptions options) {

            if (options is null)
                throw new ArgumentNullException(nameof(options));

            int ellipsisCount = (options.AddLeadingEllipsis ? 1 : 0) +
                (options.AddTrailingEllipsis ? 1 : 0);

            int hyphenCount = (options.AddLeadingHyphen ? 1 : 0) +
                (options.AddTrailingHyphen ? 1 : 0);

            int minimumPageLength = ellipsisCount > 0 ?
                (ellipsis.Length * ellipsisCount) + 2 :// +2 for space and additional char
                1;

            minimumPageLength += hyphenCount;

            if (options.MaxPageLength < minimumPageLength)
                throw new ArgumentOutOfRangeException(nameof(options), $"'{nameof(options.MaxPageLength)}' must be greater than or equal to {minimumPageLength}.");

            this.content = (content ?? "").TrimEnd();
            this.options = options;

        }

        public IEnumerator<string> GetEnumerator() {

            return GeneratePaginatedContent().GetEnumerator();

        }
        IEnumerator IEnumerable.GetEnumerator() {

            return GetEnumerator();

        }

        // Private members

        private const string ellipsis = "...";
        private readonly string content;
        private readonly IStringPaginationOptions options;

        private IEnumerable<string> GeneratePaginatedContent() {

            int startIndex = 0;
            bool brokeMidSentence = false;
            bool brokeMidWord = false;

            while (startIndex < content.Length) {

                int maxPageLength = options.MaxPageLength;

                // Skip leading whitspace.

                while (char.IsWhiteSpace(content[startIndex]) && startIndex < content.Length)
                    ++startIndex;

                if (startIndex >= content.Length)
                    break;

                // Select the next chunk of text, leaving room for leading punctuation.

                bool hasLeadingEllipsis = brokeMidSentence && options.AddLeadingEllipsis;
                bool hasLeadingHyphen = brokeMidWord && options.AddLeadingHyphen;

                if (hasLeadingEllipsis) {

                    maxPageLength -= ellipsis.Length + 1; // "... "

                }
                else if (hasLeadingHyphen) {

                    maxPageLength -= 1; // "-"

                }

                // Find an appropriate place to split the string.

                int endIndex = Math.Min(startIndex + maxPageLength, content.Length);

                endIndex = StepBackToWordBreakIndex(startIndex, endIndex);

                // Make room for trailing punctuation.

                bool hasTrailingEllipsis = false;
                bool hasTrailingHyphen = false;

                if (endIndex < content.Length) {

                    char trailingChar = content[endIndex];
                    char lastChar = content[endIndex - 1];
                    int pageLength = endIndex - startIndex;

                    hasTrailingEllipsis = options.AddTrailingEllipsis && char.IsWhiteSpace(trailingChar) && !CharUtilities.IsTerminalPunctuation(lastChar);
                    hasTrailingHyphen = options.AddTrailingHyphen && !char.IsWhiteSpace(trailingChar) && !char.IsWhiteSpace(lastChar) && !CharUtilities.IsTerminalPunctuation(lastChar);

                    if (hasTrailingEllipsis && (pageLength + ellipsis.Length) > maxPageLength) {

                        endIndex = StepBackToWordBreakIndex(startIndex, endIndex - (pageLength + ellipsis.Length - maxPageLength)); // "..."

                    }
                    else if (hasTrailingHyphen && pageLength >= maxPageLength) {

                        endIndex -= 1; // "-"

                    }

                }

                string substr = content[startIndex..endIndex];

                // We need to recalculate these flags because adjusting the string to make them fit might have rendered them no longer relevant.

                hasTrailingEllipsis = hasTrailingEllipsis && !CharUtilities.IsTerminalPunctuation(substr.Last());
                hasTrailingHyphen = hasTrailingHyphen && !char.IsWhiteSpace(substr.Last()) && !CharUtilities.IsTerminalPunctuation(substr.Last());

                brokeMidSentence = hasTrailingEllipsis;
                brokeMidWord = hasTrailingHyphen;

                // Build the final string.

                StringBuilder sb = new();

                if (hasLeadingEllipsis)
                    sb.Append(ellipsis + " ");
                else if (hasLeadingHyphen)
                    sb.Append('-');

                sb.Append(substr.TrimEnd());

                if (hasTrailingEllipsis) {

                    // Add leading space before ellipses following punctuation.
                    // Note that we won't have a trailing ellipsis at all for terminal punctuation.

                    if (char.IsPunctuation(substr.Last()))
                        sb.Append(' ');

                    sb.Append(ellipsis);

                }
                else if (hasTrailingHyphen)
                    sb.Append('-');

                yield return sb.ToString();

                // Update indices for the next iteration of the loop.

                startIndex = endIndex;

            }

        }

        private int StepBackToWordBreakIndex(int startIndex, int endIndex) {

            if (!options.SplitOnWordBoundaries)
                return endIndex;

            if (endIndex < content.Length) {

                // Skip back until we find an appropriate place to break.

                int originalEndIndex = endIndex;

                while (endIndex > startIndex && !char.IsWhiteSpace(content[endIndex]))
                    --endIndex;

                // If we don't find an appropriate place to break, we'll break where we are.

                if (endIndex == startIndex)
                    endIndex = originalEndIndex;

            }

            return endIndex;

        }

    }

}