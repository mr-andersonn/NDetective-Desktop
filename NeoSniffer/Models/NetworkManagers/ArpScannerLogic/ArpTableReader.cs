using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NetSniffer.Models.NetworkManagers;


public class ArpTableReader
{
    private enum HostOs
    {
        Linux,
        WinMac
    }

    private readonly HostOs _hostOs;

    public ArpTableReader()
    {
        _hostOs = DetectHostOs();
    }
    
    public List<Device> GetDevicesFromCache()
    {
        
        string command;
        string args;
        
        if (_hostOs == HostOs.Linux)
        {
            command = "/bin/bash";
            args = "-c \"ip neigh show\"";
        }
        else if (_hostOs == HostOs.WinMac)
        {
            command = "arp";
            args = "-a";
        }
        else
        {
            throw new PlatformNotSupportedException("Unsupported OS");
        }
        
        var output = CommandRunner.Run(command, args);
        return ParseOutput(output);
        
    }
    

    private List<Device> ParseOutput(string output)
    {
        var devices = new List<Device>();
        var regex = new Regex("");

        if (_hostOs == HostOs.Linux) regex = new Regex(@"(\d+\.\d+\.\d+\.\d+)\s+dev\s+\w+\s+lladdr\s+([a-fA-F0-9:]{17})");
        else if (_hostOs == HostOs.WinMac) regex = new Regex(@"(\d+\.\d+\.\d+\.\d+)\s+([a-fA-F0-9\-:]{17})", RegexOptions.IgnoreCase);
        
        foreach (Match match in regex.Matches(output))
        {
            devices.Add(new Device(match.Groups[1].Value, match.Groups[2].Value));
        }
        
        return devices;
    }

    private HostOs DetectHostOs()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return HostOs.Linux;
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                 || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return HostOs.WinMac;
        }
        
        throw new PlatformNotSupportedException("Unsupported OS");

    }
}