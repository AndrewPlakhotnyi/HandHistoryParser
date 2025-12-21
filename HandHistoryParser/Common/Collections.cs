using System.Collections.Immutable;

namespace HandHistoryParser;

public static class Collections {
    public static ImmutableList<T>
    MapToImmutableList<TSource, T>(this IEnumerable<TSource> source, Func<TSource, T> selector) =>
        source.Select(selector).ToImmutableList();
}