using System;
using System.Collections.Generic;

namespace NetSniffer.Models;

public class ScanResult : IComparable<ScanResult>
{
    public int Id { get; private set; }
    public List<Device> Devices { get; set; }
    public DateTime Time { get; set; }

    public ScanResult(List<Device> devices)
    {
        this.Devices = devices;
        this.Time = DateTime.Now;
    }

    internal void AssignId(int id) => this.Id = id;
    
    public int CompareTo(ScanResult? other)
    {
        throw new NotImplementedException();
    }
}