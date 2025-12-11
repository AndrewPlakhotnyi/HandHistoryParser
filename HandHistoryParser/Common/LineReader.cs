namespace HandHistoryParser.Common;
public class LineReader {
    public IList<string> Lines { get; }
    public int Cursor { get; private set; }
    public LineReader(IList<string> lines, int cursor) {
        Lines = lines;
        Cursor = cursor;
    }

    public bool HasNext => Cursor < Lines.Count;
    public string Current => Lines[Cursor];
    public LineReader SkipOne() => Skip(1);
    public IEnumerable<string>
    ReadWhile(Func<string, bool> predicate) {
        while (HasNext && predicate(Current)) {
            yield return Current;
            Cursor++;
        }
    }

    public bool 
    TrySkipUntilStartsWith(string @string) =>
        TrySkipUntil(line => line.StartsWith(@string));

    public bool
    TrySkipUntil(Func<string, bool> predicate) {
        var originalCursor = Cursor;
        SkipUntil(predicate);
        if (HasNext)
            return true;
        Cursor = originalCursor;
        return false;
    }

    public LineReader
    SkipUntilStartsWith(string @string) =>
        SkipUntil(line => line.StartsWith(@string));

    public LineReader
    SkipUntil(Func<string, bool> predicate) {
        while (HasNext && !predicate(Current))
            Cursor++;
        return this;
    }

    public LineReader
    Skip(int count) {
        Cursor += count;
        return this;
    }
}

public static class
LineReaderFunctions {
    public static LineReader
    ToLineReader(this IList<string> lines) => new LineReader(lines, 0);

}