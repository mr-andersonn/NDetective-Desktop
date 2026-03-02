using System.Threading.Tasks;

namespace NetSniffer.Models;

public interface IScanner
{
    public Task<ScanResult> RunScan();
}