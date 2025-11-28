// testovanie načítavnia JSONov

using UnityEngine;

public class TestLoaders : MonoBehaviour
{
    void Start()
    {
        Debug.Log("PERSISTENT PATH: " + Application.persistentDataPath);
        
        // 1. najdi všetky wall JSONy
        var walls = WallLoader.GetAllWallFiles();
        Debug.Log("Nájdené steny: " + walls.Count);

        foreach (var w in walls)
            Debug.Log("Wall file: " + w);

        // 2. načítaj jednu stenu
        if (walls.Count > 0)
        {
            var wall = WallLoader.LoadWall(walls[0]);
            Debug.Log("Načítaná stena: " + wall.wallId);
            Debug.Log("Počet chy­tov: " + wall.holds.Count);

            foreach (var h in wall.holds)
                Debug.Log($"Hold {h.id}: pos={h.position}, color={h.color}");

            // 3. načítaj routes
            var routes = RouteLoader.LoadRoutes(wall.wallId);

            if (routes != null)
            {
                Debug.Log("Routes loaded: " + routes.routes.Count);
                foreach (var r in routes.routes)
                    Debug.Log($"Route {r.routeId}: {string.Join(", ", r.holds)}");
            }
            else
            {
                Debug.Log("No route file found for: " + wall.wallId);
            }
        }
    }
}
