using System.Device.Gpio;
using System.Device.Spi;
using Iot.Device.Adc;

// Configurações do MCP3008
var gpioController = new GpioController();

// Configura o SPI
var spiConnectionSettings = new SpiConnectionSettings(0, 0) // busId, chipSelectLine
{
    Mode = SpiMode.Mode0, // Configura o modo SPI
    ClockFrequency = 1_000_000 // Frequência de clock (1 MHz)
};

using var spiDevice = SpiDevice.Create(spiConnectionSettings);
using var mcp3008 = new Mcp3008(spiDevice);

while (true)
{
    // Lê o valor do canal 0 onde o LDR está conectado
    int ldrValue = mcp3008.Read(0);

    // Exibe o valor lido
    Console.WriteLine($"Valor do LDR: {ldrValue}");

    // Atraso para a próxima leitura
    Thread.Sleep(1000);
}