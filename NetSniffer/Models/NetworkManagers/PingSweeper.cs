using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetSniffer.Models.NetworkManagers;

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
}