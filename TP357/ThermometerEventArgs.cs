namespace TP357;

public class ThermometerEventArgs : EventArgs
{
    public ThermometerData Data { get; }

    public ThermometerEventArgs(ThermometerData data)
    {
        Data = data;
    }
}