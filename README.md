# ENGD — Modern Android Toolkit
GitHub release (latest by date)
Platform
Tech
License

ENGD is a powerful, modern, and safe tool for managing Android devices via ADB and Fastboot. Built with WinUI 3 and .NET 10, it features a beautiful Windows 11-style Fluent Design interface, real-time device monitoring, and a built-in firmware downloader for Xiaomi devices.

# ✨ Key Features
📱 Device Management (ADB)
Real-time Dashboard: Auto-detection of connected devices, models, and connection states.
Terminal: Built-in ADB shell console.
App Manager: Install (.apk) and Uninstall applications with one click.
File Transfer: Push and Pull files easily using a GUI.
Power Menu: Reboot to System, Recovery, Bootloader, or EDL.
🔧 Flashing & Fastboot
Smart Flashing: Flash specific partitions (boot, recovery, vbmeta, system, etc.) easily.
Safety First: Warnings and confirmations for dangerous operations (Erase Data, Flash Critical).
Slot Management: Full support for A/B partition devices.
Partition Manager: View partition table, slots, and sizes.
🌏 Xiaomi Firmware Downloader (New!) in v3.0
Search by Codename: Quickly find ROMs for your device (e.g., tucana, alioth).
Auto-Updates: Fetches the latest data from RSS feeds.
High-Speed Mirrors: Automatically generates download links for Aliyun (fastest), Bigota, and Hugeota.
Download Manager: Built-in downloader with Pause/Resume and Cancel support.
🎨 Modern UI & Core
WinUI 3: Uses the latest Windows App SDK for native Mica effects and Fluent Design.
Standalone: Comes with bundled Platform Tools (ADB/Fastboot) — no external installation required.
Localization: Fully translated into English and Russian.
📸 Screenshots
![Dashboard](sc1.png   sc2.png)

# Dashboard	Xiaomi Downloader
Dashboard Preview	Xiaomi Preview

# 📥 Download & Installation
Option 1: Installer (Recommended)
Download the .msi or .exe installer from the Releases Page.

# Run the installer.
The app will be installed to Program Files and added to your Start Menu.
Option 2: Portable
Download the .zip archive.

Extract the folder.
Run ENGD.WinUI.exe.
Requirements: Windows 10 (version 1809+) or Windows 11 (x64).

# 🚀 How to Use
Basic ADB Commands
Enable USB Debugging on your Android device.
Connect the device via USB.
Open the Dashboard tab to verify connection.
Use the ADB Manager tab to install apps or run shell commands.
Downloading Xiaomi Firmware
Go to the Xiaomi Firmware tab.
Enter your device codename (NOT the commercial name).
Example: Enter alioth for POCO F3, tucana for Mi Note 10.
Click Search.
Select the desired firmware and click Download.
Choose a mirror (Aliyun is recommended for speed).
⚠️ Disclaimer
PLEASE READ CAREFULLY:

The author (LIECO) is NOT responsible for any damage caused to your device.

Flashing partitions, unlocking bootloaders, and erasing data are advanced operations.
Always verify that you are flashing the correct file for your specific device model.
YOU perform all operations AT YOUR OWN RISK.
🛠 Tech Stack
Language: C#
Framework: .NET 10 (Preview/RC)
UI Framework: WinUI 3 (Windows App SDK 1.6)
Architecture: Clean Architecture (Core / Infrastructure / UI)
Logging: Serilog
🤝 Feedback & Bugs
If you encounter any issues or have suggestions:

Check the FAQ inside the app (About -> FAQ).
Open an Issue on GitHub.
Created by LIECO
