using System;
using System.Collections.Generic;

[Serializable]
public class RouteData
{
    public string routeId;        // jedinečný ID napr. "route_001"
    public string wallId;         // ID steny napr. "wall_01"
    public string routeName;      // názov cesty napr. "Tréningová A"
    public string difficulty;     // easy / medium / hard
    public string color;          // farba cesty napr. "red", "#FF0000"
    public List<int> holds;       // ID chytov (zatiaľ prázdny alebo test)
}
