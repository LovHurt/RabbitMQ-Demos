using BankingContract;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI;

public class BankingController : ControllerBase
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public BankingController(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Money([FromQuery] int money)
    {
        ISendEndpoint sendEndpoint;

        sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.MoneyConsumerQueue}"));

        await sendEndpoint.Send<IMoneyCommand>(new MoneyCommand() { Money = money, Date = DateTime.Now });
        return Ok("Created");
    }
}