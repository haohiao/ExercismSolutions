using System.Text;

public static class RomanNumeralExtension
{
    private static int[] RomanValueThresholds = [ 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 ];
    private static Dictionary<int, string> RomanSymbols = new()
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" }
        };

    public static string ToRoman(this int value)
    {
        var roman_builder = new StringBuilder();

        int remainder = value;
        foreach (int value_threshold in RomanValueThresholds)
        {
            string symbol = RomanSymbols[value_threshold];
            while (remainder >= value_threshold)
            {
                remainder -= value_threshold;
                roman_builder.Append(symbol);
            }
        }

        return roman_builder.ToString();
    }
}