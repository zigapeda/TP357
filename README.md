# TP357

This is a small hobby project to read Bluetooth LE advertisement messages of ThermoPro's TP357 thermometer.

```csharp
using TP357;

var watcher = new ThermometerWatcher();

watcher.ThermometerEvent += (s, e) =>
{
    Console.WriteLine($"{e.Data.Name}: Temp: {e.Data.Temperature}°C - Hum: {e.Data.Humidity}%");
};

watcher.Start();

Console.WriteLine("Press any key to exit");

Console.ReadKey();

watcher.Stop();
```
