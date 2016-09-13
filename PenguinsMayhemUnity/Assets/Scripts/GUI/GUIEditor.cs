using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIEditor : MonoBehaviour {

    private GameObject panel;
    private GridLayoutGroup grid;

    public GameObject gridElement;
    
    [HideInInspector]
    public static Sprite selectedSprite;

    private MapEditor mapEditor;
    
	// Use this for initialization
	void Start () {
        mapEditor = ((ScreenEditor)Screen.screen).mapEditor.GetComponent<MapEditor>();
        panel = transform.FindChild("Panel").gameObject;
        grid = panel.GetComponent<GridLayoutGroup>();
        
        grid.cellSize = new Vector2(mapEditor.cellSize, mapEditor.cellSize);
        InitPlatformToolSprites();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitPlatformToolSprites()
    {        
        //set texture
        foreach (KeyValuePair<int, Sprite> pair in ResourceReader.platformSpriteMap)
        {
            //create and set sprite for buttons that are part of toolbar
            GameObject element = Instantiate(gridElement) as GameObject;
            Image img = element.GetComponent<Image>();
            img.sprite = pair.Value;
            element.name = "GridElement";
            element.transform.SetParent(grid.transform);
            //set up listener for button
            Button elementButton = element.GetComponent<Button>();
            elementButton.onClick.AddListener(
                delegate
                {
                    GUIEditor.selectedSprite = elementButton.image.sprite;
                });
        }        
    }



}
