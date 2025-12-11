namespace HandHistoryParser.Common;

public static class Files {

    public static string 
    GetAllTextFromFile(this string file) => File.ReadAllText(file);
}
