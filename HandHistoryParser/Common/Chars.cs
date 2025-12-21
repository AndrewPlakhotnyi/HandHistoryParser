namespace HandHistoryParser.Common;

public static class Chars {
    public static bool
    IsWhiteSpace(this char @char) => @char == ' ' || @char == '\t' || @char == '\n' || @char == '\r';

    public static bool
    IsDigit(this char @char) => @char >= '0' && @char <= '9';

    public static int
    ToDigit(this char @char) => @char - '0';
}

