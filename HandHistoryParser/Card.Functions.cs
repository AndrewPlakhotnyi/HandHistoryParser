using HandHistoryParser.Common;

namespace HandHistoryParser;

public static class 
CardFunctions {

   public static IEnumerable<Card>
   ParseCards(this string cardText) {
        var line = cardText.ToFluentParser();
        while (line.SkipWhitespace().TryReadCard(out var card))
            yield return card;
    }

    public static bool
    TryReadCard(this FluentParser fluentParser, out Card card) {
        if (fluentParser.HasNext && fluentParser.NextChar.TryParseEnumSymbol<CardRank>(out var rank)) {
            card = new Card(
                rank: rank,
                suit: fluentParser.SkipOne().ReadChar().ParseEnumSymbol<CardSuit>());
            return true;
        }
        card = default;
        return false;
    }
}