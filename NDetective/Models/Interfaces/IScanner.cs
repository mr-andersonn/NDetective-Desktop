using System.Threading.Tasks;

namespace NDetective.Models;

public interface IScanner
{
    public Task<ScanResult> RunScan();
}