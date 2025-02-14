using Banking.ConsumerService;
using Banking.ConsumerService.Consumers;
using BankingContract;
using MassTransit;

var host = Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<MoneyConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(hostContext.Configuration.GetValue<string>("RabbitMQSettings:RabbitMQUri"), configurator =>
            {
                configurator.Username(hostContext.Configuration.GetValue<string>("RabbitMQSettings:RabbitMQUsername"));
                configurator.Password(hostContext.Configuration.GetValue<string>("RabbitMQSettings:RabbitMQPassword"));
            });

            cfg.ReceiveEndpoint(RabbitMQSettingsConst.MoneyConsumerQueue, ep =>
                ep.ConfigureConsumer<MoneyConsumer>(context));
        });
    });
    services.AddHostedService<Worker>();
}).Build();


host.Run();