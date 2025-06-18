# QACORDMS Client

This is a Windows Forms application that connects to the QACORDMS service.
It allows management of clients and projects and includes an automatic update feature.

## Prerequisites
- .NET 8 SDK
- Windows with support for WinForms

## Building and Running
```bash
# Restore packages and build
 dotnet build QACORDMS.Client/QACORDMS.Client.csproj

# Run the application
 dotnet run --project QACORDMS.Client/QACORDMS.Client.csproj
```

## Auto Update
On startup the application checks the API specified in `appsettings.json` for a newer version. If a higher version is available an update menu item is enabled. Selecting it downloads the installer and launches it silently as administrator. The installer is started with `/SILENT /NOCANCEL /NOCLOSEAPPLICATIONS /NORESTART` so open programs aren't closed and any required restart is suppressed.

## Repository Layout
- `QACORDMS.Client` – Windows Forms client source code.
- `Bakertilly` – Installer project files.

