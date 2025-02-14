namespace BankingContract;

public static class RabbitMQSettingsConst
{
    private const string BankingServicesPrefix = "Banking.";

    public static string MoneyConsumerQueue { get; } =
        BankingServicesPrefix + nameof(MoneyConsumerQueue);

}
