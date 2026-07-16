using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace NetSpeedWidget.Services;

public class NetworkMonitor
{
    private readonly NetworkInterface? _networkInterface;

    private long _lastReceived;
    private long _lastSent;
    private DateTime _lastCheck;

    public double DownloadSpeedBytes { get; private set; }
    public double UploadSpeedBytes { get; private set; }

    public NetworkMonitor()
    {
        _networkInterface = NetworkInterface
            .GetAllNetworkInterfaces()
            .FirstOrDefault(n =>
                n.OperationalStatus == OperationalStatus.Up &&
                n.NetworkInterfaceType != NetworkInterfaceType.Loopback);

        if (_networkInterface != null)
        {
            var stats = _networkInterface.GetIPv4Statistics();

            _lastReceived = stats.BytesReceived;
            _lastSent = stats.BytesSent;
        }

        _lastCheck = DateTime.Now;
    }

    public void Update()
    {
        if (_networkInterface == null)
            return;

        var now = DateTime.Now;
        var seconds = (now - _lastCheck).TotalSeconds;

        if (seconds <= 0)
            return;

        var stats = _networkInterface.GetIPv4Statistics();

        long received = stats.BytesReceived;
        long sent = stats.BytesSent;

        DownloadSpeedBytes = (received - _lastReceived) / seconds;
        UploadSpeedBytes = (sent - _lastSent) / seconds;

        _lastReceived = received;
        _lastSent = sent;
        _lastCheck = now;
    }

    public static string FormatSpeed(double bytesPerSecond)
    {
        string[] units = { "B/s", "KB/s", "MB/s", "GB/s" };

        int unit = 0;

        while (bytesPerSecond >= 1024 && unit < units.Length - 1)
        {
            bytesPerSecond /= 1024;
            unit++;
        }

        return $"{bytesPerSecond:0.##} {units[unit]}";
    }
}