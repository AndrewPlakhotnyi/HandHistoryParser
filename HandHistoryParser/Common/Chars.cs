namespace HandHistoryParser.Common;

public static class Chars {
    public static bool
    IsWhiteSpace(this char @char) => @char == ' ' || @char == '\t' || @char == '\n' || @char == '\r';
}

