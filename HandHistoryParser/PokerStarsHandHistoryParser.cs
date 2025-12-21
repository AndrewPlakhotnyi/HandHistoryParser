namespace HandHistoryParser;

public static class
PokerStarsHandHistoryParser {

    public static
    HandHistory ParseHandHistoryText(this string handHistoryText) {
        var showdownPlayers = new Dictionary<string, ImmutableList<Card>>();
        var reader = new LineReader(lines: handHistoryText.GetLines(), 0);
        var handId = reader.Current.ParseHeaderLine();
        var seats = reader.SkipUntil(line => line.StartsWith("Seat "))
            .ReadWhile(line => line.StartsWith("Seat "))
            .Select(line => ParsePlayerLine(line))
            .ToList();

        reader = reader.SkipUntilStartsWith("*** HOLE CARDS ***").SkipOne();
        var (heroNickname, heroCards) = reader.Current.IsDealtToLine() ? reader.Current.ParseDealtToLine() : default;

        reader = reader.SkipUntilStartsWith("*** SHOW DOWN ***");
        if (reader.HasNext) {

            reader.SkipOne();

            while (!reader.IfContains("*** SUMMARY ***")) {
                if (reader.Current.TryParseShowDownLine(out var showDownResult)) {
                    var (playerNickname, playerCards) = showDownResult;
                    showdownPlayers[playerNickname] = playerCards;
                }
                reader.SkipOne();
            }
        }
        return new HandHistory(
            handId: handId,
            players: seats.MapToImmutableList(seat => new HandHistoryPlayer(
            seatNumber: seat.seatNumber,
            nickname: seat.nickname,
            stackSize: seat.stackSize,
            currency: seat.currency,
            dealtCards: seat.nickname == heroNickname ? heroCards :
                        showdownPlayers.TryGetValue(seat.nickname, out var hands) ? hands :
                        ImmutableList<Card>.Empty)));
    }


    public static string
    ParseNickname(this Parser rawLine) {
        var result = rawLine.Skip("Seat #: ".Length).ReadUntil('(');
        return result.Trim();
    }

    public static Currencies
    ParseCurrency(this Parser rawLine) {
        var result = rawLine.SkipUntil('(').ReadNext();
        return result switch {
            '$' => Currencies.Dollar,
            '€' => Currencies.Euro,
            _ => Currencies.Undefined,
        };
    }

    public static double
    ParseStackSize(this Parser rawLine) {
        var result = rawLine.SkipUntil('(').SkipOne().SkipOne().ReadDouble();
        return result;
    }

    public static int
    ParseSeatPosition(this Parser rawLine) {
        var result = rawLine.Skip("Seat ".Length).ReadInt();
        return result;
    }

    public static long
    ParseHandId(this Parser rawline) {
        var result = rawline.SkipUntil('#').SkipOne().SkipSpaces().ReadLong();
        return result;
    }

    public static (string nickname, ImmutableList<Card> cards)
    ParseDealtToLine(this string handHistoryLine) {
        var parsedLine = handHistoryLine.ToParserFormat().Skip("Dealt to ".Length);
        var nickname = parsedLine.ReadUntil('[').Trim();
        var cards = parsedLine.SkipOne().ReadUntil(']').ParseCards();
        return (nickname, [.. cards]);
    }

    public static bool
    TryParseShowDownLine(this string handHistoryLine, out (string nickname, ImmutableList<Card> cards) result) {
        result = default;
        while (!handHistoryLine.Contains(" shows ["))
            return false;
        var parsedLine = handHistoryLine.ToParserFormat();
        var nickname = parsedLine.ReadUntil(':').Trim();
        var cards = parsedLine.Skip(": shows [".Length).ReadUntil(']').ParseCards();
        result = (nickname, [.. cards]);
        return true;
    }

    public static long
    ParseHeaderLine(this string line) {
        Parser parserLine = line.ToParserFormat();
        return ParseHandId(parserLine);
    }

    public static (int seatNumber, string nickname, double stackSize, Currencies currency)
    ParsePlayerLine(string line) {
        return (
        seatNumber: ParseSeatPosition(line.ToParserFormat()),
        nickname: ParseNickname(line.ToParserFormat()),
        stackSize: ParseStackSize(line.ToParserFormat()),
        currency: ParseCurrency(line.ToParserFormat()));
    }

    public static bool
    IsShowdownLine(this string handHistoryLine) =>
        handHistoryLine.Contains("*** SHOW DOWN ***");

    public static bool
    IsDealtToLine(this string handHistoryLine) =>
        handHistoryLine.Contains("Dealt to");
}


