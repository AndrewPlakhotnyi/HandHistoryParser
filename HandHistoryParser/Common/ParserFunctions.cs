using HandHistoryParser.Common;
using System.Globalization;

namespace HandHistoryParser.Common; 

public class 
FluentParser {
    public string Text { get; }
    public int Position { get; private set; }
    public bool IsEnd => Position >= Text.Length;
    public char NextChar => Text[Position];
    public bool HasNext => Position < Text.Length;
    public int Length => Text.Length;
    public FluentParser(string @text) => Text = @text;

    public FluentParser
    Skip(int skipCharacters) {
        Position += skipCharacters;
        return this;
    }

    public FluentParser
    SkipOne() {
        Position++;
        return this;
    }

    public FluentParser
    SkipAfter(string skipString) {
        var startIndex = Text.IndexOf(skipString, Position, StringComparison.Ordinal);      // нашли начало этого текста 
        Position = startIndex == -1 ? Text.Length : startIndex + skipString.Length;         //если не нашли, то в конец строки, а если нашли, то старт + длина нашей строки Seat 4: what?NOpair? (€204.60 in chips) 
        return this;
    }

    public FluentParser
    SkipSpaces() {
        while (HasNext && NextChar == ' ')
            SkipOne();
        return this;
    }

    public FluentParser
    SkipUntil(char stopChar) {
        if (NextChar == stopChar)
            return this;
        for (int i = Position; i < Length; i++)
            if (Text[i] == stopChar) {
                Position = i;
                return this;
            }
        Position = Length;
        return this;
    }

    public FluentParser
    SkipToDigit() {
        while (char.IsDigit(NextChar))
            SkipOne();
        return this;
    }

    public FluentParser
    SkipWhitespace() {
        while (HasNext && NextChar.IsWhiteSpace())
            SkipOne();
        return this;
    }

    public char
    ReadNext() {
        var result = NextChar;
        Position++;
        return result;
    }

    public string
    Read(int count) {
        var result = Text.Substring(Position, count);
        Position += count;
        return result;
    }

    public string
    ReadUntil(char stopChar) {
        if (NextChar == stopChar)                                                       // если мы уже на этом чаре, то возвращаем пустоту, если после цикла прошлись и не нашли нужный чар, то тоже пустота
            return string.Empty;
        for (int i = Position; i < Length; i++)
            if (Text[i] == stopChar) {
                var result = Text.Substring(Position, i - Position);
                Position = i;
                return result;
            }
        return string.Empty;
    }

    public char
    ReadChar() {
        var result = NextChar;
        SkipOne();
        return result;
    }

    public double
    ReadDouble() {
        SkipSpaces();
        int start = Position;
        while (!IsEnd && (char.IsDigit(NextChar) || NextChar == '.' || NextChar == ','))
            SkipOne();
        string result = Text.Substring(start, Position - start);
        return result.ConvertToDouble();
    }

    public int
    ReadInt() {
        SkipSpaces();
        int start = Position;
        while (!IsEnd && (char.IsDigit(NextChar)))
            SkipOne();
        var result = Text.Substring(start, Position - start);
        return result.ConvertToInt();
    }

    public long
    ReadLong() {
        SkipSpaces();
        int start = Position;
        while (!IsEnd && (char.IsDigit(NextChar)))
            SkipOne();
        var result = Text.Substring(start, Position - start);
        return result.ConvertToLong();
    }
      }

public static class
ParserHelper {
    public static FluentParser
    ToFluentParser(this string text) => new FluentParser(text);                                  // ????????????????????????????????

} 


 
