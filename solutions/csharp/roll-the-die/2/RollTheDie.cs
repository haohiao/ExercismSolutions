using System.Globalization;
using System.Numerics;

public class Player
{
    public Player()
    {
        Random random = new Random();
        Interval dice_point_interval = Interval.CreateInterval("[1, 18]", true);
        Interval spell_strength_interval = Interval.CreateInterval("[0, 100)", false);
        dice_point_random = new IntervalRandom(dice_point_interval, random);
        spell_strength_random = new IntervalRandom(spell_strength_interval, random);
    }
    private IntervalRandom dice_point_random;
    private IntervalRandom spell_strength_random;

    public int RollDie()
    {
        return (int)dice_point_random.Next();
    }

    public double GenerateSpellStrength()
    {
        return spell_strength_random.Next();
    }
}

public class IntervalRandom
{
    public IntervalRandom(Interval interval, Random random)
    {
        Interval = interval;
        Random = random;
    }
    public IntervalRandom(Interval interval):
        this(interval, new Random())
    {
    }

    public Interval Interval;
    public Random Random;

    public double Next()
    {
        if (Interval.IsIntger)
        {
            int min = Convert.ToInt32(Interval.Min);
            int max = Convert.ToInt32(Interval.Max);
            if (!Interval.IncludeMin) min++;
            if (!Interval.IncludeMax) max--;
            return Random.Next(min, max + 1);
        }
        else
        {
            double min = Interval.Min;
            double max = Interval.Max;
            if (!Interval.IncludeMin) min = min + 1;
            if (!Interval.IncludeMax) max = max - 1;
            return min + Random.NextDouble() * (max - min);
        }
    }
}

public record struct Interval
{
    private Interval(double Min, double Max, bool IncludeMin = true, bool IncludeMax = true, bool IsInteger = false)
    {
        this.Min = Min;
        this.Max = Max;
        this.IncludeMin = IncludeMin;
        this.IncludeMax = IncludeMax;
        this.IsIntger = IsInteger;
    }

    public double Min { get; init; }
    public double Max { get; init; }
    public bool IncludeMin { get; init; }
    public bool IncludeMax { get; init; }
    public bool IsIntger { get; init; }

    public bool Contains(double value)
    {
        return value switch
        {
            _ when IncludeMin && IncludeMax => 
                value >= Min && value <= Max,

            _ when IncludeMin => 
                value >= Min && value < Max,

            _ when IncludeMax => 
                value > Min && value <= Max,

            _ => 
                value > Min && value < Max
        };
    }
    public static Interval CreateInterval(string s, bool isInteger = false)
    {
        s = s.Trim();
        if (s.Length < 5)
            throw new FormatException("Invalid format for Interval.");
        char min_bracket = s[0];
        char max_bracket = s[^1];
        bool include_min = min_bracket switch
        {
            '[' => true,
            '(' => false,
            _   => throw new FormatException("Invalid format for Interval.")
        };
        bool include_max = max_bracket switch
        {
            ']' => true,
            ')' => false,
            _   => throw new FormatException("Invalid format for Interval.")
        };

        string[] parts = s[1..^1].Split(',');
        if (parts.Length != 2)
            throw new FormatException("Invalid format for Interval.");

        double min = double.Parse(parts[0], null);
        double max = double.Parse(parts[1], null);
        return new Interval(min, max, include_min, include_max, isInteger);
    }
    public override string ToString()
    {
        string min_bracket = IncludeMin ? "[" : "(";
        string max_bracket = IncludeMax ? "]" : ")";
        return $"{min_bracket}{Min}, {Max}{max_bracket}({(IsIntger ? "int" : "double")})";
    }
}