using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace NetSniffer.Models.NetworkManagers;

public record struct IpRange(IPAddress Start, IPAddress End);

/*
 * Responsibilities
 *
 * 1. Get Local IP
 * 2. Get Subnet mask
 * 3. Calculate subnet range
 * 
 */

public class NetworkInfoProvider
{
    public readonly IPAddress Ip;
    public readonly IPAddress SubnetMask;
    public readonly IpRange SubnetRange;
    
    private readonly UnicastIPAddressInformation _unicast;

    public NetworkInfoProvider()
    {
        _unicast = GetUnicast() ?? throw new Exception("No active IPv4 network interface found");
        
        Ip = GetLocalIp();
        SubnetMask = GetSubnetMask();
        
        SubnetRange = CalculateSubnetRange();
    }

    public IEnumerable<IPAddress> GetAllHostsInRange()
    {
        uint start = IpToUInt32(SubnetRange.Start);
        uint end = IpToUInt32(SubnetRange.End);

        for (uint i = start; i < end; i++)
            yield return UInt32ToIp(i);
    }

    private IPAddress GetLocalIp()
    {
        return _unicast.Address;
    }

    private IPAddress GetSubnetMask()
    {
        return _unicast.IPv4Mask;
    }

    private IpRange CalculateSubnetRange()
    {
        uint ip = IpToUInt32(Ip);
        uint mask = IpToUInt32(SubnetMask);
        
        uint network = ip & mask;
        uint broadcast = network | ~mask;

        IPAddress startIp = UInt32ToIp(network + 1);
        IPAddress endIp = UInt32ToIp(broadcast - 1);
        
        return new IpRange(startIp, endIp);
    }

    private uint IpToUInt32(IPAddress ip)
    {
        byte[] bytes = ip.GetAddressBytes();
        if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

    private IPAddress UInt32ToIp(uint ip)
    {
        byte[] bytes = BitConverter.GetBytes(ip);
        if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
        return new IPAddress(bytes);
    }
    
    private UnicastIPAddressInformation? GetUnicast()
    {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        NetworkInterface? active = interfaces.FirstOrDefault(n =>
            n.OperationalStatus == OperationalStatus.Up &&
            n.NetworkInterfaceType != NetworkInterfaceType.Loopback);

        if (active == null)
            return null;
        
        return active.GetIPProperties()
                .UnicastAddresses
                .FirstOrDefault(u => u.Address.AddressFamily == AddressFamily.InterNetwork);


    }
}
