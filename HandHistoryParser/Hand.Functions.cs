using HandHistoryParser.Common;

namespace HandHistoryParser;

public static class Hand {

    public static Currencies
    ParseCurrency(this char symbol) {
        foreach (Currencies currency in Enum.GetValues(typeof(Currencies))) {
            if (currency.GetEnumAttribute<SymbolAttribute>().Value == symbol) 
                return currency;
        }
        throw new InvalidOperationException($"Currency with symbol '{symbol}' not found.");
    }
}
