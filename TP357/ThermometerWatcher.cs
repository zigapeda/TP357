using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace TP357;

public class ThermometerWatcher
{
    private readonly BluetoothLEAdvertisementWatcher bluetoothLEAdvertisementWatcher = new();

    public event EventHandler<ThermometerEventArgs>? ThermometerEvent;

    public ThermometerWatcher()
    {
        bluetoothLEAdvertisementWatcher.Received += OnAdvertisementReceived;
    }

    public void Start()
    {
        bluetoothLEAdvertisementWatcher.Start();
    }

    public void Start(CancellationToken stoppingToken)
    {
        stoppingToken.Register(bluetoothLEAdvertisementWatcher.Stop);
    }

    public void Stop()
    {
        bluetoothLEAdvertisementWatcher.Stop();
    }

    private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs e)
    {
        if (e.Advertisement.LocalName.StartsWith("TP357"))
        {
            var manufacturerData = e.Advertisement.DataSections.First(x => x.DataType == 255);
            using (var reader = DataReader.FromBuffer(manufacturerData.Data))
            {
                reader.ByteOrder = ByteOrder.LittleEndian;
                reader.ReadByte();
                var temp = reader.ReadUInt16() / 10.0;
                var hum = reader.ReadByte();
                var data = new ThermometerData(e.Advertisement.LocalName, temp, hum);
                ThermometerEvent?.Invoke(this, new ThermometerEventArgs(data));
            }
        }
    }
}
