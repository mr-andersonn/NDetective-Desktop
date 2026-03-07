using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using NDetective.Models;

namespace NDetective.Data;

public static class DeviceRepository
{
    
    public static void Add(Device d)
    {
        using var connection = new SqliteConnection(Database.ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
                              INSERT INTO Devices(Ip, Mac, Description)
                              VALUES($ip, $mac, $description)
                              ON CONFLICT(Mac) DO NOTHING;
                              """;
        command.Parameters.AddWithValue($"ip", d.Ip);
        command.Parameters.AddWithValue($"mac", d.Mac);
        command.Parameters.AddWithValue($"description", d.Description ?? string.Empty);

        command.ExecuteNonQuery();
    }

    public static void AddAll(IEnumerable<Device> ds)
    {
        // TEMPORARY SOLUTION 

        foreach (var d in ds)
        {
            Add(d);
        }
    }

    public static Device? GetByMac(string mac)
    {
        using var connection = new SqliteConnection(Database.ConnectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = """
                              SELECT Ip, Mac, Description
                              FROM Devices
                              WHERE Mac = @mac;
                              """;
        
        command.Parameters.AddWithValue($"mac", mac);
        
        using var reader = command.ExecuteReader();
        
        if (!reader.Read()) return null;
        var foundIp = reader.GetString(0);
        var foundMac = reader.GetString(1);
        var foundDescription = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);

        var device = new Device(foundIp, foundMac)
        {
            Description = foundDescription
        };
        
        return device;

    }

    public static IEnumerable<Device> GetAll()
    {

        var devices = new List<Device>();
        
        using var connection = new SqliteConnection(Database.ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText = """
                              SELECT Ip,Mac,Description
                              FROM Devices;
                              """;
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var ip = reader.GetString(0);
            var mac = reader.GetString(1);
            var description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
            
            devices.Add(new Device(ip, mac));
        }
        
        return devices;
        
    }
    

    public static void Update(Device d)
    {
        using var connection = new SqliteConnection(Database.ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
                              UPDATE Devices
                              SET Ip = @ip, 
                                  Description = @description
                              WHERE Mac = @mac;
                              """;
        
        command.Parameters.AddWithValue($"ip", d.Ip);
        command.Parameters.AddWithValue($"mac", d.Mac);
        command.Parameters.AddWithValue($"description", d.Description ?? string.Empty);

        command.ExecuteNonQuery();
    }

    public static void Delete(Device d)
    {
        using var connection = new SqliteConnection(Database.ConnectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = """
                              DELETE FROM Devices
                              WHERE Mac = @mac;
                              """;
        
        command.Parameters.AddWithValue($"mac", d.Mac);
        
        command.ExecuteNonQuery();
        
    }

}