#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
using Meetup.IoT;

namespace Meetup.Api;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Blink _blink;

    public Worker(ILogger<Worker> logger, Blink blink)
    {
        _logger = logger;
        _blink = blink;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _blink.Execute(stoppingToken);
        return Task.CompletedTask;
    }
}
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
