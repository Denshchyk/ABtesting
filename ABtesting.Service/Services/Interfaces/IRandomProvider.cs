namespace ABtesting.Service;

public interface IRandomProvider
{
    /// <summary>
    /// This method gets random number from 0.0 to 1.0
    /// </summary>
    /// <returns></returns>
    double NextDouble();
}