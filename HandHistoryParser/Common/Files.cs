namespace HandHistoryParser;

public static class Files {

    public static string
    GetAllTextFromFile(this string filePath) => File.ReadAllText(filePath);

}