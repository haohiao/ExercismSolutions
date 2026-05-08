using System.Text;

public static class RomanNumeralExtension
{
    private static int[] RomanNumerals = [ 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 ];
    private static Dictionary<int, string> RomanNumeralsMap = new()
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
        StringBuilder builder = new StringBuilder();

        int current = value;
        foreach (int numeral in RomanNumerals)
        {
            while (current >= numeral)
            {
                current -= numeral;
                builder.Append(RomanNumeralsMap[numeral]);
            }

            if (current < 0)
            {
                break;
            }
        }
        
        return builder.ToString();
    }
}