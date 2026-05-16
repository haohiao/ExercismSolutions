public static class SquareRoot
{
    public static int Root(int number)
    {
        Func<double, double> square = x => x * x - number;
        Func<double, double> differential_coefficient_square = x => 2 * x;
        int root = (int)Math.Round(NewTonMethod(
            function:       square, 
            derivative:     differential_coefficient_square, 
            initial_guess:  number / 2.0, 
            tolerance:      0.0001));

        return root;
    }

    public static double NewTonMethod(
        Func<double, double> function, 
        Func<double, double> derivative, 
        double initial_guess, 
        double tolerance)
    {
        double guess = initial_guess;
        while (Math.Abs(function(guess)) > tolerance)
        {
            guess = guess - function(guess) / derivative(guess);
        }
        return guess;
    }
}
