using System.Net;
using NetSniffer.Models.NetworkManagers;

namespace NeoSniffer.Tests.NetworkManagersTests;

public class PingSweeperTest
{
    [Fact]
    public async Task Ping_Does_Not_Throw_When_Pinging_Loopback()
    {
        var sweeper = new PingSweeper();
        var addresses = new List<IPAddress> { IPAddress.Loopback };

        var exception = await Record.ExceptionAsync(() => sweeper.PingAsync(addresses));

        Assert.Null(exception);
    }
    
    [Fact]
    public async Task PingAsync_Should_Handle_Unreachable_Ip_Gracefully()
    {
        var sweeper = new PingSweeper();
        var fakeIp = IPAddress.Parse("10.255.255.1"); // Reserved, unreachable
        var addresses = new List<IPAddress> { fakeIp };

        var exception = await Record.ExceptionAsync(() => sweeper.PingAsync(addresses));

        Assert.Null(exception); // It should swallow exceptions as designed
    }
}