namespace CurrencyTracker;

public class CurrencyResponse
{
    public Rates Rates { get; set; }
}

public class Rates
{
    public double USD { get; set; }
    public double EUR { get; set; }
    public double GBP { get; set; }
    public double PLN { get; set; }
    public double UAH { get; set; }
}