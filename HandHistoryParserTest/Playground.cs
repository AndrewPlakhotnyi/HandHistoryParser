using HandHistoryParser;
using HandHistoryParserTest.Common;

namespace HandHistoryParserTest;

public static class 
Playground {

    [Test] public static void
    Run() {
        var file = @"C:\h2n\handhistorysample\PokerStars\Cash. Holdem. NL25. 2013. angrypaca. 88k. 107MB\2013\01\03\2019HH20130103 Fortuna III Fast - $0.10-$0.25 - USD No Limit Hold'em.txt";

        var hands = file.GetHandHistoryFromFile().ToList();
        hands.Count.AssertGreaterZero();
    }

}
