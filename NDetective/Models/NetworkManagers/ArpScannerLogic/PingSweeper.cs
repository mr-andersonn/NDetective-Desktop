using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NDetective.Models.NetworkManagers;

public class PingSweeper
{
    public async Task PingAsync(IEnumerable<IPAddress> addresses, int timeout = 500)
    {
        var tasks = new List<Task>();

        foreach (var ip in addresses)
        {
            tasks.Add(SendPing(ip, timeout));
        }
        
        await Task.WhenAll(tasks);
    }
    
    private async Task SendPing(IPAddress ip, int timeout)
    {
        try
        {
            using var ping = new Ping();
            await ping.SendPingAsync(ip, timeout);
        }
        catch (PingException e)
        {
            // Do nothing
        }
    }

    public async Task<List<Device>> FilterAliveDevices(List<Device> devices, int timeout = 500)
    {
        var aliveDevices = new List<Device>();
        var tasks = devices.Select(async device =>
        {
            try
            {
                using var ping = new Ping();

                IPAddress ip = IPAddress.Parse(device.Ip);
                PingReply reply = await ping.SendPingAsync(ip, timeout);

                if (reply.Status == IPStatus.Success)
                {
                    lock (aliveDevices) aliveDevices.Add(device);
                }
            }
            catch
            {
                // Do nothing
            }
        });
        
        await Task.WhenAll(tasks);
        return aliveDevices;
    }
    
}