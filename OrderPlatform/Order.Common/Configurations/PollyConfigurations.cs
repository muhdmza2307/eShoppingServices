namespace Order.Common.Configurations;

public static class PollyConfigurations
{
    public static TimeSpan[] ForOneSecondIncreasesUpToThree() =>
        new[]
        {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(3)
        };
}