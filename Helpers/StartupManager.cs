using Microsoft.Win32;
using System.Diagnostics;

namespace NetSpeedWidget.Helpers;

public static class StartupManager
{
    private const string AppName = "NetSpeedWidget";

    private const string RunKey =
        @"Software\Microsoft\Windows\CurrentVersion\Run";

    public static bool IsEnabled()
    {
        using RegistryKey? key =
            Registry.CurrentUser.OpenSubKey(RunKey);

        return key?.GetValue(AppName) != null;
    }

    public static void SetEnabled(bool enabled)
    {
        using RegistryKey? key =
            Registry.CurrentUser.OpenSubKey(RunKey, true);

        if (key == null)
            return;

        if (enabled)
        {
            string exePath =
                Process.GetCurrentProcess().MainModule!.FileName!;

            key.SetValue(AppName, $"\"{exePath}\"");
        }
        else
        {
            key.DeleteValue(AppName, false);
        }
    }
}
