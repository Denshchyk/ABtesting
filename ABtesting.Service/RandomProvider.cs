namespace ABtesting.Service;

public interface IRandomProvider
{
    double NextDouble();
}

public class RandomProvider : IRandomProvider
{
    private readonly Random _random = new Random();

    public double NextDouble()
    {
        return _random.NextDouble();
    }
}
