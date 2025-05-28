# NeoSniffer

**NeoSniffer** is a cross-platform network scanner built with C# and [Avalonia UI](https://avaloniaui.net/). It scans your local network using ARP and Ping to detect active devices, and displays them in a desktop interface.

<br>

> 🚀 Built in C#
> 🎯 fast, extensible, and user-friendly

<br><br>

## 📸 Features

- 🌐 **ARP-based LAN scanning** (fast, accurate on local network)
- 📍 **Displays IP and MAC addresses** of all detected devices
- 🧠 **Filters stale ARP entries** (only shows devices currently online)
- 🔁 **One-click scanning** with a responsive Avalonia UI
- 💾 **Scan history tracking** with change detection
- ⚙️ Modular architecture (Scanner, SavedScans, ScanManager)
- ✅ Cross-platform: works on **Linux**, **Windows**, and **macOS**

<br><br>

## 🛠 Requirements

- [.NET 7.0 or later](https://dotnet.microsoft.com/download)
- Avalonia UI runtime (bundled with app)
- `nmap` (optional, for future advanced scanning)

<br><br>

## 🔧 How It Works

1. **PingSweeper** pings all IPs in your subnet to trigger ARP responses
2. **ArpTableReader** reads the ARP cache from the OS
3. **FilterAliveDevices** confirms each device is still reachable
4. **ScanManager** stores results and tracks changes over time

<br><br>

## 📦 Build & Run

```bash
git clone https://github.com/yourusername/NeoSniffer.git
cd NeoSniffer
dotnet restore
dotnet run


