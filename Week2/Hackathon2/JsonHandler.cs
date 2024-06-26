using System.Text.Json.Serialization;
using System.Text.Json;

namespace Hackathon2;

public static class JsonHandler
{
    private static string filePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

    private static string GetFilePath(string path)
    {
        string newFilePath = Directory.GetParent(filePath).FullName;
        newFilePath = Directory.GetParent(Directory.GetParent(newFilePath).FullName).FullName;
        newFilePath += path;
        return newFilePath;
    }

    public static List<DraftChoice> LoadDraftDataFromJson()
    {
        string filePath = GetFilePath(@"\draft.json");
        string? json = File.ReadAllText(filePath);
        List<DraftChoice> draftData = JsonSerializer.Deserialize<List<DraftChoice>>(json);
        return draftData;
    }
}