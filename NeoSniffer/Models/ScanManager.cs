using System;
using System.Threading.Tasks;

namespace NetSniffer.Models;

public class ScanManager
{
    private readonly ArpScanner _arpScanner;
    private readonly SavedScans _savedScans;

    public event EventHandler? ScanChanged;

    public ScanManager()
    {
        _arpScanner = new ArpScanner();
        _savedScans = new SavedScans();
        
        _savedScans.changeDetected += (s, e) => ScanChanged?.Invoke(s, e);
    }
    
    public async Task RunArpScan()
    {
        // Run scanA
        ScanResult scanResult = await _arpScanner.RunScan();
        
        // Update SavedScans
        _savedScans.AddScanResult(scanResult);
        
    }
    
    public ScanResult? GetLastScan()
    {
        if (_savedScans.Scans.Count == 0) return null;
        return _savedScans.Scans[^1];
    }

    public SavedScans GetAllScans() => _savedScans;

}