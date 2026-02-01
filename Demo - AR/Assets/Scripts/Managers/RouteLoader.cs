using System.IO;
using UnityEngine;

public static class RouteLoader
{
    public static RouteCollection LoadRoutes(string wallId)
    {
        string path = Path.Combine(
            Application.persistentDataPath,
            "routes_" + wallId + ".json"
        );

        if (!File.Exists(path))
        {
            Debug.LogWarning("Route file not found: " + path);
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<RouteCollection>(json);
    }
}
