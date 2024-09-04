using Meetup.IoT;
using Iot.Device.CpuTemperature;
using Meetup.Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Blink>();

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/led-controll", (Blink blink, [FromQuery] bool ledOn) =>
{
    blink.LedOn = ledOn;
    var temperature = new CpuTemperature();


    if (!temperature.IsAvailable)
    {
        return new Cpu();
    }

    return new Cpu { Temperature = temperature.Temperature.DegreesCelsius };
})
.WithName("LedControll")
.WithOpenApi();

app.Run();


record Cpu
{
    public double Temperature { get; set; } = -1;
}