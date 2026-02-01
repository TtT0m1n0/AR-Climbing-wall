using UnityEngine;
using System.IO;

public static class RouteSaver
{
    public static void SaveRoute(RouteData route)
    {
        // kam uložíme JSON
        string dir = Application.persistentDataPath;
        string filePath = Path.Combine(dir, $"routes_{route.wallId}.json");

        RouteCollection collection;

        // ak existuje, načítame
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            collection = JsonUtility.FromJson<RouteCollection>(json);

            if (collection.routes == null)
                collection.routes = new System.Collections.Generic.List<RouteData>();
        }
        else
        {
            // inak vytvoríme nový
            collection = new RouteCollection
            {
                wallId = route.wallId,
                routes = new System.Collections.Generic.List<RouteData>()
            };
        }

        // pridáme novú cestu
        collection.routes.Add(route);

        // uložíme ako JSON (pekné formátovanie = true)
        string output = JsonUtility.ToJson(collection, true);
        File.WriteAllText(filePath, output);

        Debug.Log($"Route saved to: {filePath}");
    }
}
