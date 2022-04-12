using System.Globalization;

namespace PhotorealisticRenderer;

public static class StringExtensions
{
    public static bool TryParseDouble(this string s, out double result) 
        => double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out result);

    public static bool TryParseInt(this string s, out int result)
        => int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result);
}