using System;

namespace NetSniffer.Models;

public class Device : IComparable
{
    public string Ip { get; private set;}
    public string Mac {get; private set;}
    public string Description {get; set;}

    public Device(string ip, string mac)
    {
        this.Ip = ip;
        this.Mac = mac;
        this.Description = $"IP: {ip}\nMAC: {mac}";
    }
    override public string ToString()
    {
        return $"if: {Ip}, mac: {Mac}, description: {Description}";
    }
    public int CompareTo(object? obj) 
    {
        throw new NotImplementedException(); 
        
        // TODO: implement
    }
}