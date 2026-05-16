using System.Globalization;
using System.Numerics;

public class Player
{
    public Player()
    {
        Random random = new Random();
        Interval<int> dice_point_interval = Interval<int>.CreateInterval("[1, 18]");
        Interval<double> spell_strength_interval = Interval<double>.CreateInterval("[0, 100)");
        dice_point_random = new IntervalRandom<int>(dice_point_interval, random);
        spell_strength_random = new IntervalRandom<double>(spell_strength_interval, random);
    }
    private IntervalRandom<int> dice_point_random;
    private IntervalRandom<double> spell_strength_random;

    public int RollDie()
    {
        return dice_point_random.Next();
    }

    public double GenerateSpellStrength()
    {
        return spell_strength_random.Next();
    }
}

public class IntervalRandom<T>
    where T : INumber<T>
{
    public IntervalRandom(Interval<T> interval, Random random)
    {
        Interval = interval;
        Random = random;
    }
    public IntervalRandom(Interval<T> interval):
        this(interval, new Random())
    {
    }

    public Interval<T> Interval;
    public Random Random;

    public T Next()
    {
        Type type = typeof(T);
        if (type == typeof(int))
        {
            int min = Convert.ToInt32(Interval.Min);
            int max = Convert.ToInt32(Interval.Max);
            if (!Interval.IncludeMin) min++;
            if (!Interval.IncludeMax) max--;
            return (T)(object)Random.Next(min, max + 1);
        }
        else if (type == typeof(double))
        {
            double min = Convert.ToDouble(Interval.Min);
            double max = Convert.ToDouble(Interval.Max);
            if (!Interval.IncludeMin) min = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(min) + 1);
            if (!Interval.IncludeMax) max = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(max) - 1);
            return (T)(object)(min + Random.NextDouble() * (max - min));
        }
        else
        {
            throw new NotSupportedException("Unsupported type for IntervalRandom.");
        }
    }
}

public record struct Interval<T> 
    where T : INumber<T>
{
    private Interval(T Min, T Max, bool IncludeMin = true, bool IncludeMax = true)
    {
        this.Min = Min;
        this.Max = Max;
        this.IncludeMin = IncludeMin;
        this.IncludeMax = IncludeMax;
    }

    public T Min { get; init; }
    public T Max { get; init; }
    public bool IncludeMin { get; init; }
    public bool IncludeMax { get; init; }

    public bool Contains(T value)
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
    public static Interval<T> CreateInterval(string s)
    {
        Type type = typeof(T);

        NumberStyles number_style = type switch
        {
            _ when type == typeof(int) => NumberStyles.Integer,
            _ when type == typeof(double) => NumberStyles.Float | NumberStyles.AllowThousands,
            _ => throw new FormatException("Unsupported type for Interval.")
        };

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

        T min = T.Parse(parts[0], null);
        T max = T.Parse(parts[1], null);
        return new Interval<T>(min, max, include_min, include_max);
    }
    public override string ToString()
    {
        string min_bracket = IncludeMin ? "[" : "(";
        string max_bracket = IncludeMax ? "]" : ")";
        return $"{min_bracket}{Min}, {Max}{max_bracket}";
    }
}