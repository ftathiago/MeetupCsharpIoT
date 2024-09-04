#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
using Meetup.IoT;
using Iot.Device.CpuTemperature;

var cts = new CancellationTokenSource();

Console.WriteLine("Blinking LED. Press Ctrl+C to end.");

Console.CancelKeyPress += (sender, args) =>
{
    cts.Cancel();
};

var blink = new Blink();

blink.Execute(cts.Token);

var temperature = new CpuTemperature();

while (!cts.Token.IsCancellationRequested)
{
    if (temperature.IsAvailable)
    {
        Console.WriteLine(
            "CPU temperature: {0}",
            temperature.Temperature.DegreesCelsius);
    }

    Console.WriteLine("Type 'y' if you want to turn led on");
    var key = Console.ReadKey(true);

    blink.LedOn = key.Key.ToString().ToUpper().Equals("Y");
}

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
