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

    public static void SaveLevelMapToJSON(LevelMap levelMap)
    {
        //*****************************load all previous json as text
        //*****************************add new map at the end
        //*****************************save

        //JsonData jsonData = JsonMapper.ToJson(map);
        string finalJsonText;
        string jsonStartText = @"{  ""maps"":[ ";
        string jsonEndText = "]  }";

        finalJsonText = "" + jsonStartText;
        //extract information from map for json 
        finalJsonText += @"{  ""name"":"""+levelMap.name+@""",";
        finalJsonText += @"  ""tiles"":[ ";
        for (int i = 0; i < levelMap.mapObjects.Count; ++i) {
            GameObject currObj = levelMap.mapObjects[i];

            finalJsonText +=
                @"{""x"":"+ currObj.transform.position.x.ToString() + ","+
                @"""y"":" + currObj.transform.position.y.ToString() + "," +
                @"""spriteID"":" + ResourceReader.GetSpriteIDFromGameObject(currObj).ToString() + "}";

            if (i != levelMap.mapObjects.Count - 1) { 
                finalJsonText += ", ";
            }
            else {
                finalJsonText += " ]  }";
            }
        }
        finalJsonText += "]  }";
        
        File.WriteAllText(FilePaths.jsonMaps, finalJsonText);
    }





}







