using System;

namespace NetSniffer.Models;

public class Device : IComparable
{
    public string ip { get; private set;}
    public string mac {get; private set;}
    public string description {get; set;}

    public Device(string ip, string mac, string description)
    {
        this.ip = ip;
        this.mac = mac;
        this.description = description;
    }
    public string toString()
    {
        return $"if: {ip}, mac: {mac}, description: {description}";
    }
    public int CompareTo(object? obj) 
    {
        throw new NotImplementedException(); 
    }
}