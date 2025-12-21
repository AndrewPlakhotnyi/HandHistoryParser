namespace HandHistoryParser.Common;

public class 
SymbolAttribute : Attribute {
    public char Value { get; }
    public SymbolAttribute(char value) => Value = value;
}

public class
CommandAttribute : Attribute {
    public string Name { get; }
    public string Description { get; }
    public CommandAttribute (string name, string description) {
        Name = name;
        Description = description;
    }
}

public class
CommandParameterAttribute : Attribute {
    public string Name { get; }
    public string ShortName { get; }
    public CommandParameterAttribute (string name, string shortName) {
        Name = name;
        ShortName = shortName;
    }
}

public static class
AttributesFunctions {

    public static char
    GetSymbol<TEnum>(this TEnum value) where TEnum : Enum =>
        value.GetEnumAttribute<SymbolAttribute>().Value;

    public static bool
    TryParseEnumSymbol<TEnum>(this char symbol, out TEnum result) where TEnum : Enum {
        var enumType = typeof(TEnum);
        foreach (var enumValue in Enum.GetValues(enumType).Cast<TEnum>())
            if (enumValue.GetSymbol() == symbol) {
                result = enumValue;
                return true;
            }
        result = default!;
        return false;
    }

   public static TEnum
   ParseEnumSymbol<TEnum>(this char symbol) where TEnum : Enum {
        var enumType = typeof(TEnum).VerifyIsEnum();
        foreach (var enumValue in Enum.GetValues(enumType).Cast<TEnum>())
            if (enumValue.GetSymbol() == symbol)
                return enumValue;
        throw new InvalidOperationException($"The symbol '{symbol}' does not correspond to any value of the enum {enumType.Name}");
    }


    public static TAttribute
    GetEnumAttribute<TAttribute>(this object value)
    where TAttribute : Attribute {
        if (!value.TryGetEnumAttribute<TAttribute>(out var result))
            throw new InvalidOperationException($"The enum value {value} does not have the attribute {typeof(TAttribute).Name}");
        return result;
    }

    public static bool
    TryGetEnumAttribute<TAttribute>(this object value, out TAttribute result)
    where TAttribute : Attribute {
        var valueType = value.GetType().VerifyIsEnum();
        var memberInfo = value.GetType().GetMember(value.ToString()!);
        if (memberInfo.Length == 0) {
            result = null!;
            return false;
        }
        var attributes = memberInfo[0].GetCustomAttributes(typeof(TAttribute), false);
        if (attributes.Length == 0) {
            result = null!;
            return false;
        }
        result = (TAttribute)attributes[0];
        return true;
    }

    public static Type
    VerifyIsEnum(this Type type) {
        if (!type.IsEnum)
            throw new InvalidOperationException($"The type {type.Name} is not an enum");
        return type;
    }
}


