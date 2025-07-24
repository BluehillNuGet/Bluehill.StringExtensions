using JetBrains.Annotations;

namespace Bluehill;

/// <summary>
/// Provides a set of extension methods for manipulating and querying string objects.
/// </summary>
public static class StringExtensions {
    /// <summary>
    /// Retrieves a substring from the start of the input string with the specified length.
    /// </summary>
    /// <param name="source">
    /// The input string from which the substring will be extracted.
    /// </param>
    /// <param name="length">
    /// The number of characters to be extracted from the beginning of the string.
    /// </param>
    /// <returns>
    /// A substring that starts from the beginning of the input string and has the specified length.
    /// If the length is greater than the source string's length, the entire string is returned.
    /// </returns>
    [UsedImplicitly]
    public static string Left(this string source, int length) => source.Substring(0, length);

    /// <summary>
    /// Retrieves a substring from the end of the input string with the specified length.
    /// </summary>
    /// <param name="source">
    /// The input string from which the substring will be extracted.
    /// </param>
    /// <param name="length">
    /// The number of characters to be extracted from the end of the string.
    /// </param>
    /// <returns>
    /// A substring that ends at the last character of the input string and has the specified length.
    /// If the length is greater than the source string's length, the entire string is returned.
    /// </returns>
    [UsedImplicitly]
    public static string Right(this string source, int length) => source.Substring(source.Length - length);

    /// <summary>
    /// Removes a specified prefix from the input string if it exists.
    /// </summary>
    /// <param name="source">
    /// The input string from which the prefix will be removed.
    /// </param>
    /// <param name="trimString">
    /// The prefix to be removed from the input string.
    /// </param>
    /// <returns>
    /// The input string without the specified prefix if the prefix exists; otherwise, the original string.
    /// If the prefix is null or not found in the input string, the original string is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the input string or the trim string is null.
    /// </exception>
    [UsedImplicitly]
    public static string WithoutPrefix(this string source, string trimString) => WithoutPrefix(source, trimString, StringComparison.CurrentCulture);

    /// <summary>
    /// Removes a specified prefix from the input string if it exists, using the specified string comparison type.
    /// </summary>
    /// <param name="source">
    /// The input string from which the prefix will be removed.
    /// </param>
    /// <param name="trimString">
    /// The prefix to be removed from the input string.
    /// </param>
    /// <param name="comparisonType">
    /// The string comparison type used to determine if the input string starts with the specified prefix.
    /// </param>
    /// <returns>
    /// The input string without the specified prefix if the prefix exists and matches using the given comparison type;
    /// otherwise, the original string. If the prefix is null or not matched, the original string is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the input string or the trim string is null.
    /// </exception>
    [UsedImplicitly]
    public static string WithoutPrefix(this string source, string trimString, StringComparison comparisonType) {
        if (source is null) {
            throw new ArgumentNullException(nameof(source));
        }

        if (trimString is null) {
            throw new ArgumentNullException(nameof(trimString));
        }

        if (source.Length == 0 || source.Length < trimString.Length) {
            return source;
        }

        return source.StartsWith(trimString, comparisonType) ? source.Substring(trimString.Length) : source;
    }

    /// <summary>
    /// Removes the specified suffix from the end of the input string, if it exists.
    /// </summary>
    /// <param name="source">
    /// The input string from which the suffix will be removed.
    /// </param>
    /// <param name="trimString">
    /// The string value to be removed from the end of the input string.
    /// </param>
    /// <returns>
    /// The input string with the specified suffix removed, if it exists.
    /// If the suffix does not exist, or the input string is shorter than the suffix, the original string is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the input string or trim string is null.
    /// </exception>
    [UsedImplicitly]
    public static string WithoutSuffix(this string source, string trimString) => WithoutSuffix(source, trimString, StringComparison.CurrentCulture);

    /// <summary>
    /// Removes the specified suffix from the end of the input string, if it exists.
    /// </summary>
    /// <param name="source">
    /// The input string from which the suffix will be removed.
    /// </param>
    /// <param name="trimString">
    /// The string value to be removed from the end of the input string.
    /// </param>
    /// <param name="comparisonType">
    /// The type of string comparison to use when determining if the input string ends with the specified suffix.
    /// </param>
    /// <returns>
    /// The input string with the specified suffix removed, if it exists.
    /// If the suffix does not exist, or the input string is shorter than the suffix, the original string is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the input string or trim string is null.
    /// </exception>
    [UsedImplicitly]
    public static string WithoutSuffix(this string source, string trimString, StringComparison comparisonType) {
        if (source is null) {
            throw new ArgumentNullException(nameof(source));
        }

        if (trimString is null) {
            throw new ArgumentNullException(nameof(trimString));
        }

        if (source.Length == 0 || source.Length < trimString.Length) {
            return source;
        }

        return source.EndsWith(trimString, comparisonType) ? source.Substring(0, source.Length - trimString.Length) : source;
    }

    /// <summary>
    /// Counts the number of non-overlapping occurrences of a specified substring within the input string.
    /// </summary>
    /// <param name="source">
    /// The input string in which to search for the occurrences of the specified substring.
    /// </param>
    /// <param name="sub">
    /// The substring to be counted within the input string.
    /// </param>
    /// <returns>
    /// The number of times the specified substring occurs within the input string. If the input string is null, an exception is thrown.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the input string or the specified substring is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the specified substring is an empty string.
    /// </exception>
    [UsedImplicitly]
    public static int CountOccurrences(this string source, string sub) {
        if (source == null) {
            throw new ArgumentNullException(nameof(source));
        }

        if (string.IsNullOrEmpty(sub)) {
            throw new ArgumentException($"'{nameof(sub)}' cannot be null or empty");
        }

        return source.Split([sub], StringSplitOptions.None).Length - 1;
    }

    /// <summary>
    /// Splits the input string into parts using the specified delimiter and retrieves the part
    /// at the specified position. Allows limiting the number of splits and customizing comparison behavior.
    /// </summary>
    /// <param name="source">
    /// The input string to be split.
    /// </param>
    /// <param name="delimiter">
    /// The character used to delimit the parts in the input string.
    /// </param>
    /// <param name="position">
    /// The zero-based index of the part to be retrieved. A negative value retrieves parts from the end
    /// (e.g., -1 for the last part, -2 for the second last part).
    /// </param>
    /// <returns>
    /// The part at the specified position after splitting the input string. Returns an empty string
    /// if the specified position is out of range.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the source string or delimiter is null.
    /// </exception>
    [UsedImplicitly]
    public static string SplitPart(this string source, char delimiter, int position) => SplitPart(source, delimiter, position, 0);

    /// <summary>
    /// Splits the input string into parts using the specified delimiter and retrieves the part
    /// at the specified position. Allows limiting the number of splits and customizing comparison behavior.
    /// </summary>
    /// <param name="source">
    /// The input string to be split.
    /// </param>
    /// <param name="delimiter">
    /// The string used to delimit the parts in the input string.
    /// </param>
    /// <param name="position">
    /// The zero-based index of the part to be retrieved. A negative value retrieves parts from the end
    /// (e.g., -1 for the last part, -2 for the second last part).
    /// </param>
    /// <returns>
    /// The part at the specified position after splitting the input string. Returns an empty string
    /// if the specified position is out of range.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the source string or delimiter is null.
    /// </exception>
    [UsedImplicitly]
    public static string SplitPart(this string source, string delimiter, int position) => SplitPart(source, delimiter, position, 0);

    /// <summary>
    /// Splits the input string into parts using the specified delimiter and retrieves the part
    /// at the specified position. Allows limiting the number of splits and customizing comparison behavior.
    /// </summary>
    /// <param name="source">
    /// The input string to be split.
    /// </param>
    /// <param name="delimiter">
    /// The character used to delimit the parts in the input string.
    /// </param>
    /// <param name="position">
    /// The zero-based index of the part to be retrieved. A negative value retrieves parts from the end
    /// (e.g., -1 for the last part, -2 for the second last part).
    /// </param>
    /// <param name="limit">
    /// The maximum number of splits allowed. A value of 0 results in unlimited splits.
    /// </param>
    /// <returns>
    /// The part at the specified position after splitting the input string. Returns an empty string
    /// if the specified position is out of range.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the source string or delimiter is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the limit value is negative.
    /// </exception>
    [UsedImplicitly]
    public static string SplitPart(this string source, char delimiter, int position, int limit)
        => SplitPart(source, delimiter, position, limit, StringComparison.CurrentCulture);

    /// <summary>
    /// Splits the input string into parts using the specified delimiter and retrieves the part
    /// at the specified position. Allows limiting the number of splits and customizing comparison behavior.
    /// </summary>
    /// <param name="source">
    /// The input string to be split.
    /// </param>
    /// <param name="delimiter">
    /// The string used to delimit the parts in the input string.
    /// </param>
    /// <param name="position">
    /// The zero-based index of the part to be retrieved. A negative value retrieves parts from the end
    /// (e.g., -1 for the last part, -2 for the second last part).
    /// </param>
    /// <param name="limit">
    /// The maximum number of splits allowed. A value of 0 results in unlimited splits.
    /// </param>
    /// <returns>
    /// The part at the specified position after splitting the input string. Returns an empty string
    /// if the specified position is out of range.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the source string or delimiter is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the limit value is negative.
    /// </exception>
    [UsedImplicitly]
    public static string SplitPart(this string source, string delimiter, int position, int limit)
        => SplitPart(source, delimiter, position, limit, StringComparison.CurrentCulture);

    /// <summary>
    /// Splits the input string into parts using the specified delimiter and retrieves the part
    /// at the specified position. Allows limiting the number of splits and customizing comparison behavior.
    /// </summary>
    /// <param name="source">
    /// The input string to be split.
    /// </param>
    /// <param name="delimiter">
    /// The character used to delimit the parts in the input string.
    /// </param>
    /// <param name="position">
    /// The zero-based index of the part to be retrieved. A negative value retrieves parts from the end
    /// (e.g., -1 for the last part, -2 for the second last part).
    /// </param>
    /// <param name="limit">
    /// The maximum number of splits allowed. A value of 0 results in unlimited splits.
    /// </param>
    /// <param name="comparisonType">
    /// Specifies the rules for comparing the delimiter with substrings in the input string.
    /// </param>
    /// <returns>
    /// The part at the specified position after splitting the input string. Returns an empty string
    /// if the specified position is out of range.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the source string or delimiter is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the limit value is negative.
    /// </exception>
    [UsedImplicitly]
    public static string SplitPart(this string source, char delimiter, int position, int limit, StringComparison comparisonType)
        => SplitPart(source, delimiter.ToString(), position, limit, comparisonType);

    /// <summary>
    /// Splits the input string into parts using the specified delimiter and retrieves the part
    /// at the specified position. Allows limiting the number of splits and customizing comparison behavior.
    /// </summary>
    /// <param name="source">
    /// The input string to be split.
    /// </param>
    /// <param name="delimiter">
    /// The string used to delimit the parts in the input string.
    /// </param>
    /// <param name="position">
    /// The zero-based index of the part to be retrieved. A negative value retrieves parts from the end
    /// (e.g., -1 for the last part, -2 for the second last part).
    /// </param>
    /// <param name="limit">
    /// The maximum number of splits allowed. A value of 0 results in unlimited splits.
    /// </param>
    /// <param name="comparisonType">
    /// Specifies the rules for comparing the delimiter with substrings in the input string.
    /// </param>
    /// <returns>
    /// The part at the specified position after splitting the input string. Returns an empty string
    /// if the specified position is out of range.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the source string or delimiter is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the limit value is negative.
    /// </exception>
    [UsedImplicitly]
    public static string SplitPart(this string source, string delimiter, int position, int limit, StringComparison comparisonType) {
        if (source is null) {
            throw new ArgumentNullException(nameof(source));
        }

        if (delimiter is null) {
            throw new ArgumentNullException(nameof(delimiter));
        }

        if (limit < 0) {
            throw new ArgumentOutOfRangeException(nameof(limit), "Limit cannot be negative");
        }

        if (source.Length == 0 || delimiter.Length == 0) {
            return position is 0 or -1 ? source : string.Empty;
        }

        List<string> parts = [];
        var startIndex = 0;
        var count = 0;

        while (true) {
            var index = source.IndexOf(delimiter, startIndex, comparisonType);

            if (index == -1 || (limit > 0 && count >= limit - 1)) {
                parts.Add(source.Substring(startIndex));

                break;
            }

            parts.Add(source.Substring(startIndex, index - startIndex));
            startIndex = index + delimiter.Length;
            count++;
        }

        if (position < 0) {
            var adjustedPosition = parts.Count + position;

            return adjustedPosition >= 0 && adjustedPosition < parts.Count ? parts[adjustedPosition] : string.Empty;
        }

        return position < parts.Count ? parts[position] : string.Empty;
    }
}
