using MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("RabbitMQSettings:RabbitMQUri"), configurator =>
        {
            configurator.Username(builder.Configuration.GetValue<string>("RabbitMQSettings:RabbitMQUsername"));
            configurator.Password(builder.Configuration.GetValue<string>("RabbitMQSettings:RabbitMQPassword"));
        });
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();
