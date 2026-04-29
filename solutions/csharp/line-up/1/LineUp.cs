public static class LineUp
{
    public static string Format(string name, int number)
    {
        string number_suffix = (number % 10) switch
        { 
            1 when number%100 != 11 => "st",
            2 when number%100 != 12 => "nd",
            3 when number%100 != 13 => "rd",
            _ => "th",
        };

        string message = $"{name}, you are the {number}{number_suffix} customer we serve today. Thank you!";

        return message;
    }
}
