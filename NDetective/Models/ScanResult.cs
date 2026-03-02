using System;
using System.Collections.Generic;
using System.Linq;

namespace NDetective.Models;

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
        if (other == null) return 1;
        if (this.Devices.Count != other.Devices.Count) return 1;

        var thisSorted = this.Devices.OrderBy(d => d.Mac).ToList();
        var otherSorted = other.Devices.OrderBy(d => d.Mac).ToList();

        for (int i = 0; i < thisSorted.Count; i++)
        {
            if (!thisSorted[i].Equals(otherSorted[i])) return 1;
        }

        return 0;

    }
    
}