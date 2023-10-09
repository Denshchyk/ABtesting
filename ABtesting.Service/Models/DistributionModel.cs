namespace ABtesting.Service;

public record DistributionModel(string ExperimentKey, string ExperimentValue, int NumberOfDevices);