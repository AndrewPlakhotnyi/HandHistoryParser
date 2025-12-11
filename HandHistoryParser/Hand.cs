using HandHistoryParser.Common;
using System.Collections.Immutable;

namespace HandHistoryParser;

public class
HandHistory {
    public long HandId { get; }
    public ImmutableList<HandPlayer> Players { get; }
    public HandHistory(long handId, ImmutableList<HandPlayer> players) {
        HandId = handId;
        Players = players;
    }
}

public class
HandPlayer {
    public int SeatNumber { get; }
    public string Nickname { get; }
    public double StackSize { get; }
    public Currencies Currency { get; }
    public ImmutableList<Card> DealtCards { get; }
    public HandPlayer(int seatNumber, string nickname, double stackSize, Currencies currency, ImmutableList<Card> dealtCards) {
        SeatNumber = seatNumber;
        Nickname = nickname;
        StackSize = stackSize;
        Currency = currency;
        DealtCards = dealtCards;
    }

    public override string 
    ToString() =>
       $"Seat #{SeatNumber}: {Nickname} {Currency}{StackSize} [{DealtCards.JoinStrings()}]";
}

public enum
Currencies {
    [Symbol('$')] Dollar,
    [Symbol('€')] Euro,
}
