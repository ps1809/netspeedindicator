using System;
using System.IO;
using System.Text.Json;

namespace NetSpeedWidget.Helpers;

public class AppSettings
{
    public string TextColor { get; set; } = "White";

    public double WindowLeft { get; set; } = -1;
    public double WindowTop { get; set; } = -1;

    // NEW
    public bool LockPosition { get; set; } = false;

    private static readonly string FilePath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NetSpeedWidget",
            "settings.json");

    public static AppSettings Load()
    {
        try
        {
            if (!File.Exists(FilePath))
                return new AppSettings();

            return JsonSerializer.Deserialize<AppSettings>(
                File.ReadAllText(FilePath))
                ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

        File.WriteAllText(
            FilePath,
            JsonSerializer.Serialize(
                this,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
    }
}