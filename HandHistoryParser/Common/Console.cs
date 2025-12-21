namespace HandHistoryParser.Common;

public static class 
ConsoleFunctions {

    public static string
    WriteToConsole (this string @string) {
        System.Console.WriteLine (@string);
        return @string;
    }
}

    