using System.Diagnostics;

namespace NetSniffer.Models.NetworkManagers;

public class CommandRunner
{

    public static string Run(string command, string args)
    {
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        proc.Start();

        string output = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit();

        return output;
    }
    
}