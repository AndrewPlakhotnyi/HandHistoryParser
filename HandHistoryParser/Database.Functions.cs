namespace HandHistoryParser;

public static class DatabaseFunctions {

    public static Database
    AddHand(this Database database, HandHistory handHistory) =>
        database with { HandHistories = database.HandHistories.Add(handHistory) };


    public static Database
    DeleteHand(this Database database, long handId) =>
        database with { 
            HandHistories = database.HandHistories.RemoveAll(hand => hand.HandId == handId),
            DeletedHandIds = database.DeletedHandIds.Add(handId) };
            
    public static ImmutableList<HandHistory>
    GetHandHistories(this Database database) =>
        database.HandHistories;
        
    public static int
    GetHandHistoriesCount(this Database database) =>
        database.HandHistories.Count;
        
    public static long[]
    GetDeletedHandsIds (this Database database) =>
        database.DeletedHandIds.ToArray();

    public static int
    GetPlayersCount (this Database database) =>
        database.HandHistories.SelectMany(hand => hand.Players)
            .Select(player => player.Nickname)
            .Distinct()
            .Count();
        
    public static int
    GetPlayerHandsCount (this Database database, string playerNickname) =>
        database.HandHistories.Count(hand => hand.Players.Any(player => player.Nickname == playerNickname));

    public static ImmutableList<HandHistory>
    GetLastTenPlayerHands (this Database database, string playerNickname) =>
        database.HandHistories
            .Where(hand => hand.Players.Any(player => player.Nickname == playerNickname))
            .Take(10)
            .ToImmutableList();

}

