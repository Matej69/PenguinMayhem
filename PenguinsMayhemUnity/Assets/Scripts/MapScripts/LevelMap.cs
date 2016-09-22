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

    public static void RemoveCopiesWithSameName(ref LevelMap map, ref List<LevelMap> maps)
    {        
        for (int i = maps.Count - 1; i >= 0; --i) {
            if (maps[i].name == map.name)
                maps.Remove(maps[i]);
        }
    }
    public static void RemoveCopiesWithSameName(string mapName, ref List<LevelMap> maps)
    {
        for (int i = maps.Count - 1; i >= 0; --i)  {
            if (maps[i].name == mapName)
                maps.Remove(maps[i]);
        }
    }

    public static void ClearMapObjects(ref LevelMap _map) {        
        if (_map == null || _map.mapObjects.Count <= 0)
            return;           
        for(int i = _map.mapObjects.Count - 1; i >= 0; --i) {
            MonoBehaviour.Destroy(_map.mapObjects[i]);
            _map.mapObjects.RemoveAt(i);
        }
    }

    public static void ClearAllExcept(ref LevelMap _map, ref List<LevelMap> _allMaps)
    {
        for(int i = _allMaps.Count - 1; i >= 0; --i) {
            if(_map != _allMaps[i]) {
                for (int j = _allMaps[i].mapObjects.Count - 1; j >= 0; --j) {
                    MonoBehaviour.Destroy(_allMaps[i].mapObjects[j]);
                    _allMaps[i].mapObjects.RemoveAt(j);                   
                }
                _allMaps.RemoveAt(i);
            }
        }
    }


}
