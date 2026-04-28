static class AssemblyLine
{
    private readonly static int ItemsPerHour = 221;

    public static double SuccessRate(int speed)
    {
        double success_rate =
            speed switch
            {
                0 => 0.0,
                <= 4 => 1.0,
                <= 8 => 0.9,
                9 => 0.8,
                10 => 0.77,
            };
        return success_rate;
    }

    public static double ProductionRatePerHour(int speed)
    {
        double success_rate = SuccessRate(speed);
        double production_rate = success_rate * ItemsPerHour * speed;
        return production_rate;
    }

    public static int WorkingItemsPerMinute(int speed)
    {
        int working_items = (int)(ProductionRatePerHour(speed) / 60);
        return working_items;
    }
}
