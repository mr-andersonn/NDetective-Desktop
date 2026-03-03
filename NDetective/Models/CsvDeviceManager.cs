using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NDetective.Models;

public class CsvDeviceManager
{

    private static readonly string DevicesPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Database", "devices.csv");
    
    public void SaveDevices(IEnumerable<Device> devices)
    {
        
        var sb = new StringBuilder();

        sb.AppendLine("IP,MAC");

        foreach (var d in devices)
        {
            sb.AppendLine(string.Join(",", d.Ip, d.Mac));
        }
        
        Directory.CreateDirectory(Path.GetDirectoryName(DevicesPath)!);
        
        File.WriteAllText(DevicesPath, sb.ToString(), Encoding.UTF8);
    }

    public IEnumerable<Device> LoadDevices()
    {
        
        var devices = new List<Device>();

        if(!File.Exists(DevicesPath)){return devices;}

        var lines = File.ReadAllLines(DevicesPath);

        if (lines.Length <= 1) return devices;

        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];

            if (string.IsNullOrWhiteSpace(line)) continue;
            
            var parts = line.Split(',');
            
            if (parts.Length < 2) continue;

            var ip = parts[0].Trim();
            var mac = parts[1].Trim();
            
            devices.Add(new Device(ip, mac));
        }
        
        return devices;
    }
}