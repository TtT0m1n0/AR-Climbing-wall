// JSON model steny

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HoldData
{
    public int id;
    public Vector3 position;
    public string color;
}

[Serializable]
public class WallData
{
    public string wallId;
    public List<HoldData> holds;
}
