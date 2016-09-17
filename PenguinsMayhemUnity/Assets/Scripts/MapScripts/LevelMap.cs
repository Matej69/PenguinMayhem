using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMap {
    [HideInInspector]
    public string name;
    [HideInInspector]
    public List<GameObject> mapObjects;

    public LevelMap()
    {
        name = "707_No Name Set Yet_707";
        mapObjects = new List<GameObject>();

    }
    
    
}
