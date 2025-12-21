namespace HandHistoryParser;

public record
Database(
    ImmutableList<HandHistory> HandHistories,
    ImmutableList<long> DeletedHandIds );