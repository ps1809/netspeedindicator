
using System;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using NetSpeedWidget.Helpers;

namespace NetSpeedWidget.Services;

public sealed class TrayService : IDisposable
{
    private readonly TaskbarIcon _trayIcon;
    private readonly Window _window;
    private readonly AppSettings _settings;

    public TrayService(Window window)
    {
        _window = window;
        _settings = AppSettings.Load();

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

        var fontSizeMenu = new System.Windows.Controls.MenuItem
        {
            Header = "Font Size"
        };

        var smallFontSize = new System.Windows.Controls.MenuItem
        {
            Header = "Small (10)",
            IsCheckable = true
        };
        smallFontSize.Click += (_, _) => UpdateFontSize(10);

        var mediumFontSize = new System.Windows.Controls.MenuItem
        {
            Header = "Medium (12)",
            IsCheckable = true
        };
        mediumFontSize.Click += (_, _) => UpdateFontSize(12);

        var largeFontSize = new System.Windows.Controls.MenuItem
        {
            Header = "Large (14)",
            IsCheckable = true
        };
        largeFontSize.Click += (_, _) => UpdateFontSize(14);

        var extraLargeFontSize = new System.Windows.Controls.MenuItem
        {
            Header = "Extra Large (16)",
            IsCheckable = true
        };
        extraLargeFontSize.Click += (_, _) => UpdateFontSize(16);

        var customFontSize = new System.Windows.Controls.MenuItem
        {
            Header = "Custom..."
        };
        customFontSize.Click += (_, _) => ShowCustomFontSizeDialog();

        fontSizeMenu.Items.Add(smallFontSize);
        fontSizeMenu.Items.Add(mediumFontSize);
        fontSizeMenu.Items.Add(largeFontSize);
        fontSizeMenu.Items.Add(extraLargeFontSize);
        fontSizeMenu.Items.Add(new System.Windows.Controls.Separator());
        fontSizeMenu.Items.Add(customFontSize);

        // Set the currently selected font size as checked
        UpdateFontSizeCheckedState();

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
        menu.Items.Add(fontSizeMenu);
        menu.Items.Add(new System.Windows.Controls.Separator());
        menu.Items.Add(exit);

        return menu;
    }

    private void UpdateFontSizeCheckedState()
    {
        // Check the appropriate menu item based on current font size setting
        var menu = _trayIcon.ContextMenu;
        if (menu != null)
        {
            foreach (var item in menu.Items)
            {
                if (item is System.Windows.Controls.MenuItem menuItem)
                {
                    if (menuItem.Header.ToString() == "Small (10)" && _settings.FontSize == 10)
                        menuItem.IsChecked = true;
                    else if (menuItem.Header.ToString() == "Medium (12)" && _settings.FontSize == 12)
                        menuItem.IsChecked = true;
                    else if (menuItem.Header.ToString() == "Large (14)" && _settings.FontSize == 14)
                        menuItem.IsChecked = true;
                    else if (menuItem.Header.ToString() == "Extra Large (16)" && _settings.FontSize == 16)
                        menuItem.IsChecked = true;
                }
            }
        }
    }

    private void UpdateFontSize(double fontSize)
    {
        // Update the settings and UI immediately
        _settings.FontSize = fontSize;
        _settings.Save();
        
        // Update the main window text sizes
        if (_window is MainWindow mainWindow)
        {
            mainWindow.ApplyFontSize(fontSize);
        }
    }

    private void ShowCustomFontSizeDialog()
    {
        var dialog = new CustomFontSizeDialog();
        if (dialog.ShowDialog() == true && !dialog.IsCancelled)
        {
            UpdateFontSize(dialog.SelectedFontSize);
        }
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
