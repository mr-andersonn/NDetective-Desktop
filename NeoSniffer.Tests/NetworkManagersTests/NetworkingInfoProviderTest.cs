using System.Net;
using NetSniffer.Models.NetworkManagers;

namespace NeoSniffer.Tests.NetworkManagersTests;

public class NetworkingInfoProviderTest
{
    [Fact]
    public void Should_Initialize_With_Valid_Ip_And_Subnet()
    {
        var provider = new NetworkInfoProvider();

        Assert.NotNull(provider.Ip);
        Assert.NotNull(provider.SubnetMask);
        Assert.IsType<IPAddress>(provider.Ip);
        Assert.IsType<IPAddress>(provider.SubnetMask);

    }

    [Fact]
    public void Should_Return_Valid_Subnet_Range()
    {
        var provider = new NetworkInfoProvider();

        var range = provider.SubnetRange;
        Assert.NotNull(range);
        Assert.NotNull(range.Start);
        Assert.NotNull(range.End);
        Assert.True(range.Start.GetAddressBytes().Length == 4);
        Assert.True(range.End.GetAddressBytes().Length == 4);
        
    }
}