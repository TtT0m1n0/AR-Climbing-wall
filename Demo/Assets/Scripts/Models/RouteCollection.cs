// všetky cesty jednej steny sú v jednom JSON súbore

using System;
using System.Collections.Generic;

[System.Serializable]
public class RouteData
{
    public string routeId;
    public string wallId;
    public string routeName;
    public string difficulty;
    public string color;
    public List<int> holds;
}

[System.Serializable]
public class RouteCollection
{
    public string wallId;
    public List<RouteData> routes;
}
