namespace HandHistoryParserTest.Common;
using NUnitAssert = NUnit.Framework.Assert;

public static class Asserts {

    public static T
    Assert<T>(this T actual, T expected) {
        NUnitAssert.That(actual, Is.EqualTo(expected));
        return actual;
    }

    public static int 
    AssertGreaterZero(this int value) {
        NUnitAssert.That(value, Is.GreaterThan(0));
        return value;
    }

    public static void 
    AssertTrue(this bool value) =>
        NUnitAssert.That(value, Is.True);

    public static void
    AssertFalse(this bool value) =>
        NUnitAssert.That(value, Is.False);

    public static void
    AssertNotEmpty<T>(this IEnumerable<T> collection) =>
        NUnitAssert.That(collection, Is.Not.Empty);

    public static void
    AssertSequence<T>(this IEnumerable<T> actualItems, IEnumerable<T> expectedItems) =>
        NUnitAssert.That(actualItems, Is.EqualTo(expectedItems));

}
