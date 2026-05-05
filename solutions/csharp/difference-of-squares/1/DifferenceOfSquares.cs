public static class DifferenceOfSquares
{
    public static int CalculateSquareOfSum(int max)
    {
        int sum = max*(max+1)/2;
        int square_of_sum = sum*sum;
        return square_of_sum;
    }

    public static int CalculateSumOfSquares(int max)
    {
        int sum_of_squares = max*(max+1)*(2*max+1)/6;
        return sum_of_squares;
    }

    public static int CalculateDifferenceOfSquares(int max)
    {
        int square_of_sum = CalculateSquareOfSum(max);
        int sum_of_squares = CalculateSumOfSquares(max);
        int diff = square_of_sum - sum_of_squares;
        return diff;
    }
}