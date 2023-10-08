namespace ABtesting.Service;

public interface IRandomProvider
{
    /// <summary>
    /// This method gets random number from 0.0 to 1.0
    /// </summary>
    /// <returns></returns>
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
