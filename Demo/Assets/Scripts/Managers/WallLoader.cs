using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class WallLoader
{
    // načítanie jedného JSON súboru so stenou
    public static WallData LoadWall(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("Wall JSON not found: " + path);
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<WallData>(json);
    }

    // nájde všetky steny v persistentDataPath
    public static List<string> GetAllWallFiles()
    {
        string dir = Application.persistentDataPath;

        // napr. wall_01.json, wall_02.json...
        string[] files = Directory.GetFiles(dir, "wall_*.json");

        return new List<string>(files);
    }
}
