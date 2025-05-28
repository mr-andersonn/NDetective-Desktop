using System;
using System.Collections.Generic;

namespace NetSniffer.Models;

public class SavedScans
{

    private static int  _scanResultLastId = 0;
    public List<ScanResult> Scans { get; set; }

    public event EventHandler changeDetected;
    public SavedScans()
    {
        Scans = new List<ScanResult>();
    }

    public void AddScanResult(ScanResult scanResult)
    {
        scanResult.AssignId(++_scanResultLastId);
        Scans.Add(scanResult);

        if(_scanResultLastId > 0)
        CompareScanContents(Scans?.Find(s => s.Id == _scanResultLastId),
            Scans.Find(s => s.Id == _scanResultLastId - 1));
    }

    public void CompareScanContents(ScanResult f, ScanResult s)
    {
        if (f.CompareTo(s) != 0) changeDetected?.Invoke(this, EventArgs.Empty);
            
    }
    
}