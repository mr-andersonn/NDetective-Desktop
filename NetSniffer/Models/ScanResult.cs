using System;
using System.Collections.Generic;

namespace NetSniffer.Models;

public class ScanResult
{
    public int Id { get; set; }
    public List<Device> Devices { get; set; }
    public DateTime Time { get; set; }

    public ScanResult(int id, List<Device> devices)
    {
        this.Id = id;
        this.Devices = devices;
        this.Time = DateTime.Now;
    }
    public void AddDevice(Device device)
    {
       Devices.Add(device);
    }
}