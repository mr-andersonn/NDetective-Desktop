# NDetect

Have you ever wondered:

*"Is someone connected to my network and spying on me right now?"*

You open the terminal in panic, run `nmap`, and… nothing.

Crisis avoided. For now.

But what if you didn’t have to manually scan your network every time you got suspicious?

What if an app continuously monitored your LAN, recorded every device it sees, and alerted you when something unfamiliar appears?

**NDetect (Network Detective)** is a lightweight cross-platform network scanner built with .NET **Avalonia UI**.
It periodically scans your local network, tracks discovered devices, and notifies you about unknown or unauthorized connections.

When minimized, the app stays hidden in the **system tray**, quietly watching your network.

<br>

## 📸 Features

🌐 **ARP-based LAN scanning**
Continuously scans your local network using ARP to detect active devices. By default the scan runs every **10 seconds**.

<br>

<img width="793" height="723" alt="ndetectiveSearchPage" src="https://github.com/user-attachments/assets/3ecbb309-f36b-4ab0-95aa-9df1e7c268f7" />

<br>

💾 **Device history & management**
Detected devices can be saved to a local **SQLite database**.
In the **Devices** window you can add, edit, or remove known devices.

<img width="793" height="723" alt="NDetectiveDevicesPage" src="https://github.com/user-attachments/assets/01d63d59-2084-4e57-9d42-6be2e173167c" />

<br>
<br>

<img width="791" height="723" alt="NDetectiveEditDevicePage" src="https://github.com/user-attachments/assets/13b4c82b-6f95-4e5e-b3ab-4fd4af0e9f84" />

<br>
<br>

🔔 **Unauthorized device detection**
Devices that are not in your saved list can be flagged so you immediately know when something unexpected joins your network.

🖥 **Cross-platform**
Runs on **Windows**, **Linux**, and **macOS** thanks to Avalonia UI.

<br>

## 🛠 Requirements

* [.NET 7.0 or later](https://dotnet.microsoft.com/download)

Basic networking awareness is recommended. The scanner reports **IP** and **MAC addresses**, so you may need to manually identify which device corresponds to which hardware on your network (phones, laptops, smart TVs, etc.).

<br>

## 📦 Build & Run

Currently there is no prebuilt executable.

Clone the repository and run the project using **Rider** or **Visual Studio**.

To publish a single-file executable:

```
dotnet publish -c Release /p:PublishSingleFile=true
```

Without single-file publishing the output directory will contain many DLL files

