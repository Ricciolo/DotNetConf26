namespace Orders.Domain;

public sealed class DomainRuleException(string message) : Exception(message);
