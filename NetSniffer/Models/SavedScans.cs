using System.Collections.Generic;

namespace NetSniffer.Models;

public class SavedScans
{
    public List<ScanResult> Scans { get; set; }

    public SavedScans()
    {
        Scans = new List<ScanResult>();
    }

    public void AddScanResult(ScanResult result)
    {
        // TODO: implement
    }

    public string CompareScanContents(ScanResult f, ScanResult s)
    {
        throw new System.NotImplementedException();
        
        // TODO: implement
    }

    public string Warning()
    {
        throw new System.NotImplementedException();
        
        // TODO: implement
    }
}