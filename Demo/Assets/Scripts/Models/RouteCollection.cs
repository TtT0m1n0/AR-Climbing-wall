// všetky cesty jednej steny sú v jednom JSON súbore

using System;
using System.Collections.Generic;

[Serializable]
public class RouteCollection
{
    public string wallId;                // ku ktorej stene patrí
    public List<RouteData> routes;       // zoznam všetkých ciest na tejto stene
}
