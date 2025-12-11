using HandHistoryParser.Common;
using System.Collections.Immutable;
using System.Text;
namespace HandHistoryParser;

public static class
PokerStarsHandHistoryParser {
   
    public static IEnumerable<HandHistory>
    GetHandHistoryFromFile(this string file) => 
        file.GetHandHistoryTextFromFile().Select(ParseHandHistoryText);

    public static IEnumerable<string>
    GetHandHistoryTextFromFile(this string file) =>
        file.GetAllTextFromFile().SplitByEmptyLines();

    public static HandHistory 
    ParseHandHistoryText(this string handHistoryText) {
        var reader = new LineReader(lines: handHistoryText.GetLines(), 0);
        var handId = reader.Current.ParseHeaderLine();
        var seats = reader.SkipUntil(line => line.StartsWith("Seat "))
            .ReadWhile(line => line.StartsWith("Seat "))
            .Select(line => ParsePlayerLine(line))
            .ToList();

        reader = reader.SkipUntilStartsWith("*** HOLE CARDS ***").SkipOne();
        var (heroNickname, heroCards) = reader.Current.IsDealtToLine() ? reader.Current.ParseDealtToLine() : default;
        
        var playerNickname = default(string);
        var playerCards = ImmutableList<Card>.Empty;
        if (reader.TrySkipUntilStartsWith("*** SHOW DOWN ***")) 
            (playerNickname, playerCards) = reader.SkipOne().Current.TryParseShowDownLine(out var showDownResult) ? showDownResult : default;
       
        return new HandHistory(
            handId: handId,
            players: seats.MapToImmutableList(seat => new HandPlayer(
                seatNumber: seat.seatNumber,
                nickname: seat.nickname,
                stackSize: seat.stackSize,
                currency: seat.currency,
                dealtCards: seat.nickname == heroNickname ? heroCards :
                            seat.nickname == playerNickname ? playerCards :
                            ImmutableList<Card>.Empty)));
    }

    public static string
    ParseNickname(this FluentParser rawLine) {
        var result = rawLine.Skip("Seat #: ".Length).ReadUntil('(');
        return result.Trim();
    }

    public static double
    ParseStackSize(this FluentParser parser) =>
        parser.SkipUntil('(').SkipOne().SkipOne().ReadDouble();

    public static int
    ParseSeatPosition(this FluentParser parser) =>
        parser.Skip("Seat ".Length).ReadInt();

    public static long
    ParseHandId(this FluentParser rawline) =>
        rawline.SkipUntil('#').SkipOne().SkipSpaces().ReadLong();

    public static (string nickname, ImmutableList<Card> cards)
    ParseDealtToLine(this string handHistoryLine) {
        var parser = handHistoryLine.ToFluentParser().Skip("Dealt to ".Length);
        var nickname = parser.ReadUntil('[').Trim();
        var cards = parser.SkipOne().ReadUntil(']').ParseCards();
        return (nickname, [.. cards]);
    }

    public static bool
    TryParseShowDownLine(this string handHistoryLine, out (string nickname, ImmutableList<Card> cards) result) {
        result = default;
        if (!handHistoryLine.Contains(" shows ["))
            return false;
        var parser = handHistoryLine.ToFluentParser();
        var nickname = parser.ReadUntil(':').Trim();
        var cards = parser.Skip(": shows [".Length).ReadUntil(']').ParseCards();
        result = (nickname, [.. cards]);
        return true;
    }

    public static long
    ParseHeaderLine(this string line) {
        var fluentParserLine = line.ToFluentParser();
        return fluentParserLine.ParseHandId();
    }

    public static (int seatNumber, string nickname, Currencies currency, double stackSize)
    ParsePlayerLine(string line) {
        var parser = line.ToFluentParser();
        return (
            seatNumber: parser.Skip("Seat ".Length).ReadInt(), 
            nickname: parser.Skip(": ".Length).ReadUntil('('),
            currency: parser.Skip("(".Length).ReadNext().ParseCurrency(),
            stackSize: parser.ReadDouble());
    }

    public static bool
    IsShowdownLine(this string handHistoryLine) =>
        handHistoryLine.Contains("*** SHOW DOWN ***");

    public static bool
    IsDealtToLine(this string handHistoryLine) =>
        handHistoryLine.Contains("Dealt to");
}


