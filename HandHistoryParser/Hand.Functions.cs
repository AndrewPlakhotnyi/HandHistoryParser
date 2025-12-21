namespace HandHistoryParser;

public static class
HandFunctions {

    public static IEnumerable<string>
    GetHandHistoriesTextFromFile(this string handHistoriesText) =>
        handHistoriesText.GetLines().SplitByEmptyLines();

    public static IEnumerable<HandHistory>
    GetHandHistoriesFromFile(this string file) =>
        file.GetHandHistoriesTextFromFile().Select(text => text.ParseHandHistoryText());
        
    public static string
    HandPlayerToString (this HandHistory hand, string playerNickname) {
        var player = hand.Players.FirstOrDefault(p => string.Equals(p.Nickname, playerNickname, StringComparison.OrdinalIgnoreCase));

        var cards = player.DealtCards is { Count: > 0 }
            ? string.Join(" ", player.DealtCards)
            : "карты неизвестны";

        return
            $"Hand ID: {hand.HandId}, Stack: {player.StackSize}, Cards: {cards}";
    }
}