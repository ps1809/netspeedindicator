using System.IO;
using System.Text.Json;

namespace NetSpeedWidget.Helpers;

public class WindowPosition
{
    public double Left { get; set; }
    public double Top { get; set; }

    private static readonly string FilePath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NetSpeedWidget",
            "position.json");

    public static WindowPosition Load()
    {
        try
        {
            if (!File.Exists(FilePath))
                return new WindowPosition();

            string json = File.ReadAllText(FilePath);

            return JsonSerializer.Deserialize<WindowPosition>(json)
                   ?? new WindowPosition();
        }
        catch
        {
            return new WindowPosition();
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

        File.WriteAllText(
            FilePath,
            JsonSerializer.Serialize(this));
    }
}