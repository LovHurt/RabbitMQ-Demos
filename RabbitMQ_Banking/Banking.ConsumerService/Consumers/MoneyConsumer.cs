using BankingContract;
using MassTransit;

namespace Banking.ConsumerService.Consumers;

public class MoneyConsumer : IConsumer<IMoneyCommand>
{
    public Task Consume(ConsumeContext<IMoneyCommand> context)
    {
        Console.WriteLine("message has been received");
        return Task.CompletedTask;    }
}