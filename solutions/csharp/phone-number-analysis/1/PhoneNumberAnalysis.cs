using System.Text.RegularExpressions;

public static class PhoneNumber
{
    public static (bool IsNewYork, bool IsFake, string LocalNumber) Analyze(string phone_number)
    {
        Match match = Regex.Match(phone_number, @"(?<area>\d{3})-(?<exchange>\d{3})-(?<line>\d{4})");
        if (!match.Success) 
        {
            throw new ArgumentException("It is not a valid phone number", nameof(phone_number));
        }

        string area = match.Groups["area"].ToString();
        string exchange = match.Groups["exchange"].ToString();
        string line = match.Groups["line"].ToString();

        bool is_new_york = area == "212";
        bool is_fake = exchange == "555";
        
        return (is_new_york, is_fake, line);
    }

    public static bool IsFake((bool IsNewYork, bool IsFake, string LocalNumber) phone_number_info)
    {
        return phone_number_info.IsFake;
    }
}
