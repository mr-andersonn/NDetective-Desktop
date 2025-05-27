using NetSniffer.Models;

namespace NetSniffer.Tests;

public class ScanResultTests
{
    [Fact]
    public void AddDevice()
    {
        var scanResult = new ScanResult(1, new List<Device>());

        // Assert initial state
        Assert.Empty(scanResult.Devices);

        // Act
        scanResult.AddDevice(new Device("192.168.0.104", "94:bb:43:e9:c5:ca", "Device 1"));

        // Assert result
        Assert.Equal(1, scanResult.Devices.Count);

        // Act 2
        scanResult.AddDevice(new Device("192.168.0.104", "94:bb:43:e9:c5:ca", "Device 1"));
        scanResult.AddDevice(new Device("192.168.0.104", "94:bb:43:e9:c5:ca", "Device 2"));

        // Assert result
        Assert.Equal(3, scanResult.Devices.Count);
    }

    [Fact]
    public void CompareDevices()
    {
        // TODO: implement
    }

}