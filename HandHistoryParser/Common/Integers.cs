using System.Globalization;

namespace HandHistoryParser.Common;

public static class 
Integers {
    
    public static bool
    TryParseLong(this string @string, out long result) =>
         long.TryParse(@string, out result);

    public static bool
    TryParseInt (this string @string, out int result) =>
        int.TryParse(@string, out result);

    public static int
    ConvertToInt(this char @char) => @char - '0';

    public static int
    ConvertToInt(this string @string) => System.Convert.ToInt32(@string);

    public static long
    ConvertToLong(this string @string) => System.Convert.ToInt64(@string);

    public static double
    ConvertToDouble(this string @text){
        double.TryParse(@text, NumberStyles.Any, CultureInfo.InvariantCulture, out double result);
        return result;
    }
}

