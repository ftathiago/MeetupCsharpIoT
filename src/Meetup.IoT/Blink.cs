using System.Device.Gpio;

namespace Meetup.IoT;

public class Blink
{
    private volatile bool _ledOn = true;

    public bool LedOn
    {
        get => _ledOn;
        set => _ledOn = value;
    }

    public async Task Execute(CancellationToken stoppingToken)
    {
        const int LedPin = 18;
        using var controller = new GpioController();

        controller.OpenPin(LedPin, PinMode.Output);

        while (!stoppingToken.IsCancellationRequested)
        {
            controller.Write(LedPin, LedOn ? PinValue.High : PinValue.Low);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
