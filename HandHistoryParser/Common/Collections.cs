using System.Collections.Immutable;

namespace HandHistoryParser.Common;

public static class Collections {
    public static ImmutableList<T>
    MapToImmutableList<TSource, T>(this IEnumerable<TSource> source, Func<TSource, T> selector) =>
        source.Select(selector).ToImmutableList();

    public static string 
    JoinStrings<T>(this IEnumerable<T> items) =>
        items.Select(item => item?.ToString()).Join();
}