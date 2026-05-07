static class SavingsAccount
{
    public static float InterestRate(decimal balance)
    {
        float interest_rate = balance switch
        {
            <0M => 3.213F,
            <1000M => 0.5F,
            <5000M => 1.621F,
            >=5000M => 2.475F,
        };

        return interest_rate;
    }

    public static decimal Interest(decimal balance)
    {
        float interest_rate = InterestRate(balance);
        decimal interest = balance * (decimal)interest_rate / 100;
        return interest;
    }

    public static decimal AnnualBalanceUpdate(decimal balance)
    {
        decimal interest = Interest(balance);
        decimal annual_balance = balance + interest;
        return annual_balance;
    }

    public static int YearsBeforeDesiredBalance(decimal balance, decimal target_balance)
    {
        decimal current_balance = balance;
        int years = 0;

        while (current_balance < target_balance)
        {
            current_balance = AnnualBalanceUpdate(current_balance);
            years++;
        }

        return years;
    }
}
