using System.Collections.Generic;
using System.Threading.Tasks;
using NDetective.Models.NetworkManagers;

namespace NDetective.Models;

public class ArpScanner : IScanner
{
    public async Task<ScanResult> RunScan()
    {
        //throw new System.NotImplementedException();
        
        // Phase 1: Get subnet range
        var networkProvider = new NetworkInfoProvider();
        var ips = networkProvider.GetAllHostsInRange();

        // Phase 2: Ping every ip in the subnet range
        var pingSweeper = new PingSweeper();
        await pingSweeper.PingAsync(ips);
        
        // Phase Intermediate: Wait for the OS to save all answers in cache
        await Task.Delay(1000);
        
        // Phase 3: Collect ips from cache
        var arpTableReader = new ArpTableReader();
        
        // Phase 4: Filter active devices 
        var devices = arpTableReader.GetDevicesFromCache();
        var activeDevices = await pingSweeper.FilterAliveDevices(devices);
        
        return new ScanResult(activeDevices);
    }
    
}