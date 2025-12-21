namespace HandHistoryParser;
public struct 
Card {
    public CardRank Rank { get; }
    public CardSuit Suit { get; }
    public Card(CardRank rank, CardSuit suit) {
        Rank = rank;
        Suit = suit;
    }

    public override string
    ToString() => $"{Rank.GetSymbol()}{Suit.GetSymbol()}";
}
public enum

CardSuit {
    [Symbol('h')] Hearts,
    [Symbol('s')] Spades,
    [Symbol('d')] Diamonds,
    [Symbol('c')] Clubs,

}

public enum
CardRank {
    [Symbol('2')] Deuce,
    [Symbol('3')] Three,
    [Symbol('4')] Four,
    [Symbol('5')] Five,
    [Symbol('6')] Six,
    [Symbol('7')] Seven,
    [Symbol('8')] Eight,
    [Symbol('9')] Nine,
    [Symbol('T')] Ten,
    [Symbol('J')] Jack,
    [Symbol('Q')] Queen,
    [Symbol('K')] King,
    [Symbol('A')] Ace
}
