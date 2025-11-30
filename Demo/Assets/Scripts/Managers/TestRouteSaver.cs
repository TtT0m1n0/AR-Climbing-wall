using UnityEngine;
using System.Collections.Generic;

public class TestRouteSaver : MonoBehaviour
{
    void Start()
    {
        RouteData testRoute = new RouteData
        {
            routeId = "test_01",
            wallId = "wall_01",
            routeName = "Ukážková cesta",
            difficulty = "easy",
            color = "red",
            holds = new List<int> { 1, 5, 12 }   // testovacie chyty
        };

        RouteSaver.SaveRoute(testRoute);
    }
}
