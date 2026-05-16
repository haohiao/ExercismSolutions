public static class Darts
{
    public static int Score(double x, double y)
    {
        double distance = Math.Sqrt(x * x + y * y);
        int score = distance switch
        {
            _ when distance <= 1    =>  10,
            _ when distance <= 5    =>  5,
            _ when distance <= 10   =>  1,
            _                       =>  0
        };
        return score;
    }
}
