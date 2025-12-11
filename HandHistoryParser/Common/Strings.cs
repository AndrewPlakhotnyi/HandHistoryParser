namespace HandHistoryParser.Common;

public static class 
Strings {

    public static string[]
    GetLines(this string @string) =>
        @string.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

    public static IEnumerable<string>
    SplitByEmptyLines(this string @string) {
        var result = string.Empty;
        foreach(var line in @string.GetLines()) {
            if (string.IsNullOrWhiteSpace(line) && result != string.Empty) {
                yield return result;
                result = string.Empty;
            }
            result = result.AppendLine(line);
        }
    } 

    public static string 
    AppendLine(this string @string, string line) =>
        @string + (string.IsNullOrEmpty(@string) ? string.Empty : Environment.NewLine) + line;
   
    public static string 
    Join(this IEnumerable<string> strings) => 
        string.Join(string.Empty, strings);
}