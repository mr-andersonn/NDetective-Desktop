using System;

namespace NetSniffer.Models;

public class Device : IEquatable<Device>
{
    public string Ip { get; private set;}
    public string Mac {get; private set;}
    public string Description {get; set;}

    public Device(string ip, string mac)
    {
        this.Ip = ip;
        this.Mac = mac;
        this.Description = $"IP: {ip}, MAC: {mac}";
    }
    
    public override string ToString()
    {
        return $"if: {Ip}, mac: {Mac}, description: {Description}";
    }
    public bool Equals(Device? other)
    {
        if (other == null) return false;
        return Ip == other.Ip && Mac == other.Mac;
    }
    
    public override bool Equals(object? obj) => Equals(obj as Device);
    public override int GetHashCode() => HashCode.Combine(Ip, Mac);
}