namespace ABtesting.Service;

public record ExperimentModel(Guid Id, string Key, string Value, int ChanceInPercents);