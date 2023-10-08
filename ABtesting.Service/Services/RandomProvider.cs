namespace ABtesting.Service;

public class RandomProvider : IRandomProvider
{
    private readonly Random _random = new Random();
    
    /// <summary>
    /// Generates a random double value between 0.0 (inclusive) and 1.0 (exclusive).
    /// </summary>
    /// <returns>A random double value.</returns>
    public double NextDouble()
    {
        return _random.NextDouble();
    }
}
