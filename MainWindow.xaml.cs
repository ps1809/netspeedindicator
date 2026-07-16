
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using NetSpeedWidget.Helpers;
using NetSpeedWidget.Services;

namespace NetSpeedWidget;

public partial class MainWindow : Window
{
    private readonly NetworkMonitor _monitor = new();
    private readonly DispatcherTimer _timer = new();
    private readonly AppSettings _settings;

    public MainWindow()
    {
        InitializeComponent();

        _settings = AppSettings.Load();

        Loaded += MainWindow_Loaded;
        Closing += MainWindow_Closing;

        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        Dispatcher.BeginInvoke(new Action(() =>
        {
            if (_settings.WindowLeft >= 0 && _settings.WindowTop >= 0)
            {
                Left = _settings.WindowLeft;
                Top = _settings.WindowTop;
            }
            else
            {
                Left = SystemParameters.WorkArea.Right - ActualWidth - 10;
                Top = 10;
            }
        }));

        ApplyTextColor(_settings.TextColor);
        LockPositionMenu.IsChecked = _settings.LockPosition;
        StartupMenu.IsChecked = StartupManager.IsEnabled();
    }

    private void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
        _settings.WindowLeft = Left;
        _settings.WindowTop = Top;
        _settings.Save();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _monitor.Update();

        DownloadText.Text = $"↓ {NetworkMonitor.FormatSpeed(_monitor.DownloadSpeedBytes)}";
        UploadText.Text = $"↑ {NetworkMonitor.FormatSpeed(_monitor.UploadSpeedBytes)}";
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (_settings.LockPosition)
            return;

        DragMove();

        _settings.WindowLeft = Left;
        _settings.WindowTop = Top;
        _settings.Save();
    }

    private void Border_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (ContextMenu != null)
            ContextMenu.IsOpen = true;
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void LockPosition_Click(object sender, RoutedEventArgs e)
    {
        _settings.LockPosition = !_settings.LockPosition;
        LockPositionMenu.IsChecked = _settings.LockPosition;
        _settings.Save();
    }

    private void Startup_Click(object sender, RoutedEventArgs e)
    {
        bool enable = !StartupManager.IsEnabled();
        StartupManager.SetEnabled(enable);
        StartupMenu.IsChecked = StartupManager.IsEnabled();
    }

    private void ApplyTextColor(string colorName)
    {
        WhiteMenu.IsChecked = false;
        GreenMenu.IsChecked = false;
        YellowMenu.IsChecked = false;
        CyanMenu.IsChecked = false;
        RedMenu.IsChecked = false;

        Brush brush = Brushes.White;

        switch (colorName)
        {
            case "Green":
                brush = Brushes.Lime;
                GreenMenu.IsChecked = true;
                break;
            case "Yellow":
                brush = Brushes.Yellow;
                YellowMenu.IsChecked = true;
                break;
            case "Cyan":
                brush = Brushes.Cyan;
                CyanMenu.IsChecked = true;
                break;
            case "Red":
                brush = Brushes.Red;
                RedMenu.IsChecked = true;
                break;
            default:
                WhiteMenu.IsChecked = true;
                break;
        }

        DownloadText.Foreground = brush;
        UploadText.Foreground = brush;

        _settings.TextColor = colorName;
        _settings.Save();
    }

    private void White_Click(object sender, RoutedEventArgs e) => ApplyTextColor("White");
    private void Green_Click(object sender, RoutedEventArgs e) => ApplyTextColor("Green");
    private void Yellow_Click(object sender, RoutedEventArgs e) => ApplyTextColor("Yellow");
    private void Cyan_Click(object sender, RoutedEventArgs e) => ApplyTextColor("Cyan");
    private void Red_Click(object sender, RoutedEventArgs e) => ApplyTextColor("Red");
}
