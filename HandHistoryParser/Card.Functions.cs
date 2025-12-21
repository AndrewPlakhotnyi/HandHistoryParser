namespace HandHistoryParser;

public static class 
CardFunctions {

   public static IEnumerable<Card>
   ParseCards(this string cardText) {
        var line = cardText.ToParserFormat();
        while (line.SkipWhitespace().TryReadCard(out var card))
            yield return card;
    }

    public static bool
    TryReadCard(this Parser parser, out Card card) {
        if (parser.HasNext && parser.NextChar.TryParseEnumSymbol<CardRank>(out var rank)) {
            card = new Card(
                rank: rank,
                suit: parser.SkipOne().ReadChar().ParseEnumSymbol<CardSuit>());
            return true;
        }
        card = default;
        return false;
    }
}