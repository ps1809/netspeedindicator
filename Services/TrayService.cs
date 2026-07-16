
using System;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace NetSpeedWidget.Services;

public sealed class TrayService : IDisposable
{
    private readonly TaskbarIcon _trayIcon;
    private readonly Window _window;

    public TrayService(Window window)
    {
        _window = window;

        _trayIcon = (TaskbarIcon)Application.Current.Resources["TrayIcon"];

        _trayIcon.TrayLeftMouseDown += (_, _) => ToggleWindow();

        _trayIcon.ContextMenu = CreateContextMenu();
    }

    private System.Windows.Controls.ContextMenu CreateContextMenu()
    {
        var menu = new System.Windows.Controls.ContextMenu();

        var showHide = new System.Windows.Controls.MenuItem
        {
            Header = "Show / Hide"
        };
        showHide.Click += (_, _) => ToggleWindow();

        var exit = new System.Windows.Controls.MenuItem
        {
            Header = "Exit"
        };
        exit.Click += (_, _) =>
        {
            Dispose();
            Application.Current.Shutdown();
        };

        menu.Items.Add(showHide);
        menu.Items.Add(new System.Windows.Controls.Separator());
        menu.Items.Add(exit);

        return menu;
    }

    public void HideToTray()
    {
        _window.Hide();
    }

    public void ShowFromTray()
    {
        _window.Show();
        if (_window.WindowState == WindowState.Minimized)
            _window.WindowState = WindowState.Normal;

        _window.Activate();
    }

    public void ToggleWindow()
    {
        if (_window.IsVisible)
            HideToTray();
        else
            ShowFromTray();
    }

    public void Dispose()
    {
        _trayIcon.Dispose();
    }
}
