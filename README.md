# NetSpeedWidget

## Summary

NetSpeedWidget is a simple WPF application that displays network speed information in real-time. The application provides various features such as changing text color, locking position, and handling startup events. It runs in the system tray and displays download and upload speeds for your active network connection.

## Features

- **Real-time Network Monitoring**: Displays live download and upload speeds in bytes per second (B/s), kilobytes per second (KB/s), megabytes per second (MB/s), or gigabytes per second (GB/s)
- **Text Color Options**: Users can change the text color to White, Green, Yellow, Cyan, or Red.
- **Lock Position**: Users can lock the window's position on the screen to prevent accidental movement.
- **Startup Handling**: The application can be configured to start automatically when Windows starts.
- **System Tray Integration**: The application runs in the system tray with a context menu for showing/hiding and exiting.
- **Font Size Customization**: Users can adjust font size using context menu options (Small, Medium, Large, Extra Large) or a custom dialog. The custom dialog allows selecting any font size between 8 and 48 points. Font size changes apply to the main window display only.
- **Persistent Settings**: User preferences such as text color, window position, lock position, and font size are saved between application sessions.

## Directory Structure

### Main Components
- `MainWindow.xaml` - Main application window UI
- `MainWindow.xaml.cs` - Main window code-behind with event handling and UI logic
- `App.xaml` & `App.xaml.cs` - Application startup and configuration
- `NetSpeedWidget.csproj` - Project file with dependencies
- `CustomFontSizeDialog.xaml` & `CustomFontSizeDialog.xaml.cs` - Custom font size dialog for setting custom font sizes

### Helper Classes
- `Helpers/AppSettings.cs` - Manages application settings with JSON serialization
- `Helpers/StartupManager.cs` - Handles Windows startup registration
- `Helpers/WindowPosition.cs` - Manages window positioning logic

### Service Classes
- `Services/NetworkMonitor.cs` - Monitors network interface and calculates speed
- `Services/TrayService.cs` - Manages system tray icon and context menu

## Technical Details

### Network Monitoring
The application uses .NET's `System.Net.NetworkInformation` namespace to access network statistics. It:
- Identifies the active network interface (excluding loopback)
- Calculates download/upload speeds by measuring byte differences over time intervals
- Automatically formats speeds into appropriate units (B/s, KB/s, MB/s, GB/s)

### UI Components
The application features a modern WPF interface with:
- Real-time speed display for both download and upload
- Draggable window (unless position is locked)
- Context menu accessible via right-click or system tray icon
- Color-coded text options for better visibility

## Dependencies

This application uses the following NuGet packages:
- `CommunityToolkit.Mvvm` - MVVM pattern support
- `Hardcodet.NotifyIcon.Wpf` - System tray icon functionality
- `Microsoft.Windows.CsWin32` - Windows API interop

## System Requirements

### Runtime Dependencies
- .NET 8.0 Runtime (or higher)
- Windows 10 or higher operating system

### Resource Usage
- **CPU Usage**: Minimal, typically less than 1% when idle, up to 3% during active network monitoring
- **Memory Usage**: Approximately 25-35 MB RAM when running (varies based on system configuration)

## Installation and Usage


## Installation and Usage

1. Build the solution using Visual Studio or `dotnet build`
2. Run the executable to start the application
3. The application will appear in the system tray
4. Right-click the tray icon to show/hide the window or exit the application
5. Use the context menu in the main window or system tray to customize appearance and behavior, including font size adjustments

## Customization Options

- **Text Color**: Change the color of speed text using the context menu options
- **Window Position**: Drag the window to desired position, then lock it to prevent movement
- **Startup Behavior**: Enable or disable automatic startup with Windows
- **Font Size**: Adjust font size using the main window context menu (right-click on the widget) or system tray context menu (Small, Medium, Large, Extra Large) or through a custom dialog. The custom dialog allows selecting any font size between 6 and 24 points. Font size changes apply to the main window display.

## License

MIT License

Copyright (c) 2026 NetSpeedWidget Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

