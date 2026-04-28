class Lasagna
{
    
    public int ExpectedMinutesInOven() 
    {
        return 40;    
    }
    
    public int RemainingMinutesInOven(int elapsed_baking_minutes)
    {
        int total_baking_time = ExpectedMinutesInOven();
        int remaining_time = total_baking_time - elapsed_baking_minutes;
        return remaining_time;
    }
    
    public int PreparationTimeInMinutes(int number_of_layers)
    {
        int preparation_time  = number_of_layers * 2;
        return preparation_time;
    }
    
    public int ElapsedTimeInMinutes(int number_of_layers, int elapsed_baking_minutes)
    {
        int total_elapsed_time  = PreparationTimeInMinutes(number_of_layers) + elapsed_baking_minutes;

        return total_elapsed_time;
    }
    
}
