public static class PythagoreanTriplet
{
    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    {
        int max_a = sum / 3;
        for (int a = 1; a <= max_a; a++)
        {
            int a2 = a * a;

            int max_b = (sum - a) / 2;
            for (int b = a + 1; b <= max_b; b++)
            {
                int b2 = b * b;
                
                int c = sum - a - b;
                int c2 = c * c;
                if (a2 + b2 == c2)
                {
                    yield return (a, b, c);
                }
            }
        }
    }
}