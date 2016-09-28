using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using LitJson;
using System.Globalization;

public static class MapParser {   
        //fileJsonData = JsonMapper.ToJson(map);   
        //Debug.Log(fileJsonData["maps"][0]["tiles"].Count);

        //fileJsonData = JsonMapper.ToJson(map);
       // Debug.Log(fileJsonData);
       // File.WriteAllText(FilePaths.jsonMaps, fileJsonData.ToString());
        

    public static List<LevelMap> GetAllLevelMapsFromJSON()
    {
        List<LevelMap> levelMaps = new List<LevelMap>();
        string fileTextString = File.ReadAllText(FilePaths.jsonMaps);
        JsonData jsonData = JsonMapper.ToObject(fileTextString);

        List<string> mapNames = MapParser.GetMapNamesFromJSON();
        for(int n = 0; n < mapNames.Count; ++n) {
            levelMaps.Add(MapParser.GetLevelMapFromJSON(mapNames[n]));
        }
        return levelMaps;
    }
   
   public static LevelMap GetLevelMapFromJSON(string mapName)
    {
        LevelMap map = new LevelMap();
        string fileTextString = File.ReadAllText(FilePaths.jsonMaps);
        JsonData jsonData = JsonMapper.ToObject(fileTextString);

        //for every map
        for(int mapID = 0; mapID < jsonData["maps"].Count; ++mapID){
            //if map name was found
            if (jsonData["maps"][mapID]["name"].ToString() == mapName) {
                map.name = mapName;
                //for every tile in map
                for (int tileID = 0; tileID < jsonData["maps"][mapID]["tiles"].Count; ++tileID) {
                    //create that tile with info as gameobject
                    GameObject mapObj = MonoBehaviour.Instantiate(Resources.Load(FilePaths.platformTempObj)) as GameObject;
                    mapObj.transform.position = new Vector2(
                        float.Parse(jsonData["maps"][mapID]["tiles"][tileID]["x"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(jsonData["maps"][mapID]["tiles"][tileID]["y"].ToString(),CultureInfo.InvariantCulture.NumberFormat));
                    mapObj.GetComponent<SpriteRenderer>().sprite = ResourceReader.GetSpriteFromSpriteID(
                        Int32.Parse(jsonData["maps"][mapID]["tiles"][tileID]["spriteID"].ToString()));
                    //adding object to list
                    map.mapObjects.Add(mapObj);
                }
                return map;
            }
        }
        return null;
    }

    public static LevelMap GetRandomMap()
    {        
        List<string> names = MapParser.GetMapNamesFromJSON();
        int randNum = UnityEngine.Random.Range(0, names.Count);
        LevelMap map = MapParser.GetLevelMapFromJSON(names[randNum]);
        return map;
    }

    public static List<string> GetMapNamesFromJSON()
    {
        List<string> mapNames = new List<string>();

        string fileTextString = File.ReadAllText(FilePaths.jsonMaps);
        JsonData jsonData = JsonMapper.ToObject(fileTextString);
        for(int i = 0; i < jsonData["maps"].Count; ++i) {
            mapNames.Add(jsonData["maps"][i]["name"].ToString());
        }
        return mapNames;
    }
    
    public static void SaveLevelMapsToJSON(List<LevelMap> levelMap)
    {
        //JsonData jsonData = JsonMapper.ToJson(map);
        string finalJsonText = "";
        string jsonStartText = @"{  ""maps"":[ ";
        string jsonEndText = "]  }";

        //start tag*******        
        finalJsonText += jsonStartText;

        //info about all items from LevelMap list********
        //for every map in list
        for (int mapID = 0; mapID < levelMap.Count; ++mapID)
        {
            finalJsonText += Environment.NewLine;
            finalJsonText += @"{  ""name"":""" + levelMap[mapID].name + @""",";
            finalJsonText += @"  ""tiles"":[ ";
            //for every tile item in tiles
            for (int i = 0; i < levelMap[mapID].mapObjects.Count; ++i)
            {
                GameObject currObj = levelMap[mapID].mapObjects[i];

                finalJsonText +=
                    @"{""x"":" + currObj.transform.position.x.ToString() + "," +
                    @"""y"":" + currObj.transform.position.y.ToString() + "," +
                    @"""spriteID"":" + ResourceReader.GetSpriteIDFromGameObject(currObj).ToString() + "}";
                //special case for last tile item
                if (i != levelMap[mapID].mapObjects.Count - 1) {
                    finalJsonText += ", ";
                }else {
                    finalJsonText += " ]  }";
                }
            }
            //special case for last map item
            if (mapID != levelMap.Count - 1) {
                    finalJsonText += ", ";
            }else {
                finalJsonText += "";
            }
        }

        //end tag
        finalJsonText += "]  }";
        
        File.WriteAllText(FilePaths.jsonMaps, finalJsonText);
    }





}







